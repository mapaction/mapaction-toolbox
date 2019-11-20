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
import pycountry

from mapactionpy_controller.crash_move_folder import CrashMoveFolder
from mapactionpy_controller.event import Event
from mapactionpy_arcmap import MapChef

# Parameters:
#  0. Product name
#  1. Crash Move Folder
#  2. Return - String result

if __name__ == '__main__':
    map_doc = arcpy.mapping.MapDocument("current")
    productName = arcpy.GetParameterAsText(0)
    crashMoveFolder = arcpy.GetParameterAsText(1)
	
    eventFilePath = os.path.join(crashMoveFolder, "event_description.json")
    event = Event(eventFilePath)	
    countryName = None
    cmf = None

    if event is not None:
		cmf = CrashMoveFolder(os.path.join(event.cmf_descriptor_path, "cmf_description.json"))  
		country = pycountry.countries.get(alpha_3=event.affected_country_iso3.upper())
		countryName = country.name
    else:
        raise Exception("Error: Could not derive country from " + eventFilePath)

    cookbookJsonFile = cmf.map_definitions		
    layerPropertiesJsonFile = cmf.layer_properties	
    layerDirectory = cmf.layer_rendering
			
    chef = MapChef(map_doc, cookbookJsonFile, layerPropertiesJsonFile, crashMoveFolder, layerDirectory)
    chef.cook(productName, countryName)
    arcpy.SetParameter(2, chef.report())