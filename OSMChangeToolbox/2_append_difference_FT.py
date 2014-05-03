
#2_append_difference_FT.py - creates a bat file and runs Osmosis to append the
#OSC file to the master OSM.PBF file

import arcpy
from arcpy import env

import subprocess, os

import urllib2, re, os

#set the paths for the software
gnupth = r"D:\GnuWin32\bin\wget.exe"
osmopt = r"c:\osmosis\bin"
osmopth = r"c:\osmosis\bin\osmosis"
svnzpth = r"C:\program files\7-zip\7z.exe"
javapth = r"C:\Windows\System32\java.exe"

##osmURL = r"http://labs.geofabrik.de/haiyan/" #arcpy.GetParameterAsText(0)
##inWorkspace   = r"d:\Tools\toolbox\labs.geofabrik.de\haiyan"  #arcpy.GetParameterAsText(1)
##masterPBF = "2014-03-26-20-17.osm.pbf" #arcpy.GetParameterAsText(2)
##masterPBFF = inWorkspace + "\\" + masterPBF
##latestPBF = "latest.osm.pbf"
##latestPBFF = inWorkspace + "\\" + latestPBF
##changeOSC = "planetdiff-latest.osc"
##changeOSCC = inWorkspace + "\\" + changeOSC


inWorkspace   = arcpy.GetParameterAsText(0)
masterPBF = arcpy.GetParameterAsText(1)
masterPBFF = inWorkspace + "\\" + masterPBF
#latestPBF = arcpy.GetParameterAsText(2)
#latestPBFF = inWorkspace + "\\" + latestPBF
changeOSC =  arcpy.GetParameterAsText(2)
changeOSCC = inWorkspace + "\\" + changeOSC
cumulPBF = arcpy.GetParameterAsText(3)
cumulPBFF = inWorkspace + "\\" + cumulPBF

env.workspace = inWorkspace






#now create FT .bat file

#D:\osmosis\bin\osmosis --read-xml-change file="planetdiff-latest.osc" --read-pbf "2014-03-26-19-17.osm.pbf" --apply-change --write-pbf file="cumulative2.osm.pbf"   
strtopass2 = osmopth + " --read-xml-change file=" + '"' + changeOSCC + '"' + " --read-pbf " + '"' + masterPBFF + '"' + " --apply-change --write-pbf file=" + '"' + cumulPBFF + '"'

try:
    bat_filename2 = r"c:\4_apply_diff_FT.bat"
    #resorting to creating a bat file
    str = "creating " + bat_filename2 + " ..."
    print str
    arcpy.AddMessage(str)

    
    bat_file2 = open(bat_filename2, "w")
    bat_file2.write(strtopass2)
    bat_file2.close()
    #str = "...now run the file called " + bat_filename2

    subprocess.call([r"c:\4_apply_diff_FT.bat"])
    #now delete the bat file!
    os.remove(r"c:\4_apply_diff_FT.bat")

    
except:
    str = "there is a problem!"
    print str
    arcpy.AddMessage(str)
    

