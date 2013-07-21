#SplitLayerByAttributes.py
#
#Author
#  Dan Patterson
#  Dept of Geography and Environmental Studies
#  Carleton University, Ottawa, Canada
#  Dan_Patterson@carleton.ca
#
# Date  June 23 2005
# Modified June 13 2007
#
#Purpose
#  Converts each shape in a feature class to a separate shapefile
#
#Properties (right-click on the tool and specify the following)
#General
#  Name   SplitLayerByAttributes
#  Label  Split Layer By Attributes
#  Desc   Splits a layer according to attributes within the selected field producing
#         a separate shapefile for common attributes.
#
#Source script SplitLayerByAttributes.py
#
#Parameter list
#                                Parameter Properties
#           Display Name         Data type        Type      Direction  MultiValue
#  argv[1]  Input feature class  Feature Layer    Required  Input      No
#  argv[2]  Field to query       Field            Required  Input      No
#  argv[3]  File basename        String           Optional  Input      No
#  argv[4]  Output folder        Folder           Required  Input      No
#--------------------------------------------------------------------
#Import the standard modules and the geoprocessor
#
import os, sys, string#common examples
#for 9.2
try:
  import arcgisscripting
  gp = arcgisscripting.create()
  gp.AddMessage("\n" + "Using ArcMap 9.2 or above with arcgisscripting..." + "\n")
except:
  import win32com.client
  gp = win32com.client.Dispatch("esriGeoprocessing.GpDispatch.1")
  gp.AddMessage("\n" + "Using ArcMap 9.0/9.1 or above with win32com.client.Dispatch..." + "\n")
#
#
try:
  gp.AddToolbox("C:/Program Files/ArcGIS/ArcToolbox/Toolboxes/Data Management Tools.tbx")
except:
  gp.AddMessage("Cannot find toolbox " + "\n" + "C:/Program Files/ArcGIS/ArcToolbox/Toolboxes/Data Management Tools.tbx")
  sys.exit()
gp.OverWriteOutput = 1
#
#Get the input feature class, optional fields and the output filename
inFC = sys.argv[1]
inField = sys.argv[2]
theFName = sys.argv[3]
outFolder = sys.argv[4]
desc=gp.Describe
theType = desc(inFC).ShapeType
FullName = desc(inFC).CatalogPath
thePath = (os.path.split(FullName)[0]).replace("\\","/")
if theFName != "#":
  theFName = theFName.replace(" ","_")
else:
  theFName = (os.path.split(FullName)[1]).replace(".shp","")

outFolder = outFolder.replace("\\","/")

#Determine if the field is integer, decimal (0 scale) or string field
gp.AddMessage("\n" + "Checking for appropriate field type" \
              + "(  string, decimal (0 scale) or integer)")
theFields = gp.ListFields(inFC)
inType = ""
OIDField = desc(inFC).OIDFieldName
OKFields = [OIDField]
aField = theFields.next()
gp.AddMessage("%-10s %-10s %-6s %-6s " % ("Field","Type","Scale","Useable"))
while aField:
  fType = aField.Type
  fScale = aField.Scale
  fName = aField.Name
  if fName == inField:
    inType = fType   #used to determine field type later on
    inScale = fScale
    inName = fName
  isOK = "Y"
  if (fType == "String"):
    OKFields.append(fName)
  elif ((fScale == 0) and (fType !="Geometry")):
    OKFields.append(fName)
  else:
    isOK = "N"
    
  gp.AddMessage("%-10s %-10s %-6s %-6s " % (fName, fType, fScale,isOK))
  aField = theFields.next()
#
if inField not in OKFields:
  gp.AddMessage("The field " + inField + " is not an appropriate" + \
                " field type.  Terminating operation." + "\n")  
  del gp
  sys.exit()
#  
#Determine unique values in the selected field
gp.AddMessage(inField + " is being queried for unique values.")
valueList = []
rows = gp.SearchCursor(inFC)
row = rows.next()
aString = ""
aLen = 0; aFac = 1

while row:
  aVal = row.GetValue(inField)
  if aVal not in valueList:
    valueList.append(aVal)
    aLen = len(aString)
    if aLen > 50 * aFac:
      aString = aString + "\n"
      aFac = aFac + 1
    aString = aString + " " + str(aVal)
  row = rows.next()
gp.AddMessage("Unique values: " + "\n" + aString)
#
gp.AddMessage("\n" + "Processing: " + FullName )
#
#Do the actual work of producing the unique shapefiles
aMax = 1
for aVal in valueList:
  aMax = max(aMax,len(str(aVal)))
for aVal in valueList:
  if (str(aVal).isdigit()) and (not inType == "String"):
    fs = '"'+"%"+str(aMax)+"."+str(aMax)+'i"'
    aSuffix = fs % aVal
    aVal = str(aVal)
  elif inType == "Double" and inScale == 0:
    aSuffix = str(aVal).replace(".0","")  ###### 
    aVal = str(aVal).replace(".0","")
  else:
    aSuffix = str(aVal) 
    aVal = str(aVal)
  try:
    aSuffix = aSuffix.replace(" ","_")
    aSuffix = aSuffix.replace('"',"")
    outName = theFName + aSuffix+ ".shp"
    outShapeFile = outFolder + "/" + outName
    outShapeFile = outShapeFile.replace("\\","/")
    # 
    #Create a query and produce the file
    if (not aVal.isdigit()) or (inType == "String"):
      aVal = "'"+aVal+"'"
    whereClause = "%s = %s" % (inField, aVal)
    gp.AddMessage("Output and query: " + outShapeFile + "  " + whereClause)
    gp.makeFeatureLayer(inFC, "TempLayer", whereClause)
    gp.CopyFeatures("TempLayer",outShapeFile)
  except:
    gp.AddMessage("did not work")
    whereClause = "%s = %s" % (inField, aVal)
    gp.AddMessage("Output and query: " + outShapeFile + "  " + whereClause)
#
gp.Addmessage("\n" + "Processing complete" + "\n")
del gp
