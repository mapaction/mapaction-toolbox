import argparse
import arcpy
import os, glob
import uuid
import json 
import os
from os import listdir
from os.path import isfile, join
from arcpy import env
import re

from mapactionpy_arcmap import MapChef

# Parameters:
#  1. Product name
#  2. Country name
#  3. Cookboook Json File
#  4. Layer Properties Json File
#  5. Crash Move Folder
#  6. Layer directory
#  7. Return - String result

if __name__ == '__main__':
    map_doc = arcpy.mapping.MapDocument("current")
    productName = arcpy.GetParameterAsText(0)
    countryName = arcpy.GetParameterAsText(1)
    cookbookJsonFile = arcpy.GetParameterAsText(2)
    layerPropertiesJsonFile = arcpy.GetParameterAsText(3)
    crashMoveFolder = arcpy.GetParameterAsText(4)
    layerDirectory = arcpy.GetParameterAsText(5)
    chef = MapChef(map_doc, cookbookJsonFile, layerPropertiesJsonFile, crashMoveFolder, layerDirectory)
    chef.cook(productName, countryName)

