#-------------------------------------------------------------------------------
# Name:        generate_product
# Purpose:     Generate Product
#
# Author:      Steve Hurst, Mapaction
#
# Created:     25/07/2019
#-------------------------------------------------------------------------------
import arcpy
import os, glob
import uuid
import json 
import os
from os import listdir
from os.path import isfile, join
from arcpy import env
import re

class LayerProperty:
    def __init__(self, row):
       self.mapFrame = row["MapFrame"]
       self.layerGroup = row["LayerGroup"]
       self.layerName = row["LayerName"]
       self.sourceFolder = row["SourceFolder"]
       self.regExp = row["RegExp"]
       self.definitionQuery = row["DefinitionQuery"]
       self.display = row["Display"]

class LayerProperties:
    def __init__(self, layerPropertiesJsonFile):
        self.layerPropertiesJsonFile=layerPropertiesJsonFile
        self.properties = set();

    def parse(self):
        with open(self.layerPropertiesJsonFile) as json_file:  
            jsonContents = json.load(json_file)
            for layer in jsonContents['layerProperties']:
                property = LayerProperty(layer)
                #print (property.layerName)
                self.properties.add(property)

    def get(self, layerName):
        for property in self.properties:
            if (property.layerName == layerName):
                # Find shape file if it exists
                return property
        return None

class MapRecipe:
    def __init__(self, product, layers):
        self.product = product
        self.layers = layers;

class MapCookbook:
    def __init__(self, cookbookJsonFile):
        self.cookbookJsonFile=cookbookJsonFile
        self.products = list()

    def parse(self):
        with open(self.cookbookJsonFile) as json_file:  
            jsonContents = json.load(json_file)
            for recipe in jsonContents['recipes']:
                print (recipe['product'])
                rec = MapRecipe(recipe['product'], recipe['layers'])
                self.products.append(rec)

    def layers(self, productName):
        result = list()
        for product in self.products:
            if (product.product == productName):
                result = product.layers
                break
        return result

class MapChef:
    def __init__(self, mxd, cookbookJsonFile, layerPropertiesJsonFile, crashMoveFolder, layerDirectory):
        self.mxd = mxd
        self.cookbookJsonFile = cookbookJsonFile
        self.layerPropertiesJsonFile = layerPropertiesJsonFile
        self.crashMoveFolder = crashMoveFolder
        self.layerDirectory = layerDirectory
        self.cookbook = self.readCookbook()
        self.root = crashMoveFolder
        self.layerProperties = self.readLayerPropertiesFile()

    def readCookbook(self):
        cookbook = MapCookbook(self.cookbookJsonFile)
        cookbook.parse()
        return cookbook

    def readLayerPropertiesFile(self):
        layerProperties = LayerProperties(self.layerPropertiesJsonFile) 
        layerProperties.parse()
        return layerProperties

    def removeLayers(self):
        for df in arcpy.mapping.ListDataFrames(self.mxd):
            for lyr in arcpy.mapping.ListLayers(self.mxd, "", df):
                arcpy.mapping.RemoveLayer(df, lyr)
        self.mxd.save()

    def cook(self, productName, countryName):
        arcpy.env.addOutputsToMap = False
        self.removeLayers()
        for layer in self.cookbook.layers(productName):
            properties = self.layerProperties.get(layer)
            if (properties is not None):             
                layerFilePath = self.layerDirectory + os.path.sep + properties.layerName + ".lyr"
                if (os.path.exists(layerFilePath)):
                    self.dataFrame = arcpy.mapping.ListDataFrames(self.mxd, properties.mapFrame)[0]
                    layerToAdd = arcpy.mapping.Layer(layerFilePath)
                    #if (layerToAdd.isFeatureLayer == True):
                    #    if (len(layerToAdd.definitionQuery) > 0):
                    #        print (layerToAdd.definitionQuery)                        
                    dataFilePath= self.root + "/GIS/2_Active_Data/" +  properties.sourceFolder
                    if (os.path.isdir(dataFilePath)):
                        if ("/" not in properties.regExp):
                            onlyfiles = [f for f in listdir(dataFilePath) if isfile(join(dataFilePath, f))]
                            for fileName in onlyfiles:
                                if re.match(properties.regExp, fileName):
                                    self.dataFrame = arcpy.mapping.ListDataFrames(self.mxd, properties.mapFrame)[0]
                                    dataFile = dataFilePath + "/" + fileName
                                    self.addToLayer(self.dataFrame, dataFile, layerToAdd, properties.definitionQuery, properties.display, countryName)
                        else:
                            parts = properties.regExp.split("/")
                            for root, dirs, files in os.walk(dataFilePath):
                                for gdb in dirs:
                                    if re.match(parts[0], gdb):
                                        rasterFile = (dataFilePath + "/" + gdb).replace("/", os.sep)
                                        arcpy.env.workspace = rasterFile
                                        rasters = arcpy.ListRasters("*")
                                        for raster in rasters:
                                           if re.match(parts[1], raster):
                                               rasterLayer = (rasterFile + "\\" + raster)
                                               self.dataFrame = arcpy.mapping.ListDataFrames(self.mxd, properties.mapFrame)[0]
                                               self.addRasterToLayer(self.dataFrame, rasterFile, layerToAdd, raster, properties.display)    
        arcpy.RefreshTOC()
        arcpy.RefreshActiveView()
        arcpy.env.addOutputsToMap = True
        self.mxd.save()

    def addRasterToLayer(self, dataFrame, rasterFile, layer, raster, display):
        #print ("Adding \'" + rasterFile + os.sep + raster + "\' to layer \'" + layer + "\'")
        for lyr in arcpy.mapping.ListLayers(layer): 
            lyr.replaceDataSource(rasterFile, "FILEGDB_WORKSPACE", raster)
            if (display.upper() == "YES"):
                lyr.visible = True
            else:
                lyr.visible = False 
            arcpy.mapping.AddLayer(dataFrame, lyr, "BOTTOM")            

    def addToLayer(self, dataFrame, dataFile, layer, definitionQuery, display, countryName):
        dataDirectory = os.path.dirname(os.path.realpath(dataFile))
        #print ("Layer[" + layer.name + "] - Adding \"" + dataFile + "\"")
        for lyr in arcpy.mapping.ListLayers(layer):
            # https://community.esri.com/thread/60097
            base=os.path.basename(dataFile)
            extension = os.path.splitext(base)[1]
            if (extension.upper() == ".SHP"):
                lyr.replaceDataSource(dataDirectory, "SHAPEFILE_WORKSPACE", os.path.splitext(base)[0])  
            if (extension.upper() == ".TIF"):
                lyr.replaceDataSource(dataDirectory, "RASTER_WORKSPACE", os.path.splitext(base)[0])  
            if (definitionQuery):
                definitionQuery = definitionQuery.replace('{COUNTRY_NAME}', countryName)
               # https://gis.stackexchange.com/questions/90736/setting-definition-query-on-arcpy-layer-from-shapefile
                lyr.definitionQuery = definitionQuery
                arcpy.SelectLayerByAttribute_management(lyr, "SUBSET_SELECTION", definitionQuery)
            if (display.upper() == "YES"):
                lyr.visible = True
            else:
                lyr.visible = False
            arcpy.mapping.AddLayer(dataFrame, lyr, "BOTTOM")

# Parameters:
#  0. product name
#  1. Return - String result

class ProductGenerator(object):
    def __init__(self):
        # Setup parameters from ArcPy.
        self.initialise_params()

    def initialise_params(self): 
        self.map_doc = arcpy.mapping.MapDocument("current")
        self.productName = arcpy.GetParameterAsText(0)
        self.countryName = arcpy.GetParameterAsText(1)
        self.cookbookJsonFile = arcpy.GetParameterAsText(2)
        self.layerPropertiesJsonFile = arcpy.GetParameterAsText(3)
        self.crashMoveFolder = arcpy.GetParameterAsText(4)
        self.layerDirectory = arcpy.GetParameterAsText(5)
        self.chef = MapChef(self.map_doc, self.cookbookJsonFile, self.layerPropertiesJsonFile, self.crashMoveFolder, self.layerDirectory)

    def generate(self):
        self.chef.cook(self.productName, self.countryName)

if __name__ == '__main__':
    gen = ProductGenerator()
    gen.generate()
