
#1_create_difference_SB.py - creates a bat file and runs against osmosis to generate
#an OSC file (containing the changes only)


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
latestPBF = arcpy.GetParameterAsText(2)
latestPBFF = inWorkspace + "\\" + latestPBF
changeOSC =  arcpy.GetParameterAsText(3)
changeOSCC = inWorkspace + "\\" + changeOSC
#cumulPBF = arcpy.GetParameterAsText(4)
#cumulPBFF = inWorkspace + "\\" + cumulPBF

env.workspace = inWorkspace


#now run Osmosis hopefully!
#os.chdir(osmopt)
#FNULL = open(os.devnull, 'w')#use this if you want to suppress output to stdout from the subprocess
strtopass = osmopth + " --read-pbf file=" + '"' + masterPBFF + '"' + " --read-pbf file=" + '"' + latestPBFF + '"' + " --derive-change --write-xml-change file=" + '"' + changeOSCC + '"'
#print strtopass
#subprocess.Popen(strtopass) #, stdout=FNULL, stderr=FNULL, shell=True)
#os.system(strtopass)

try:
    bat_filename = r"c:\1_create_diff_SB.bat"
    #resorting to creating a bat file
    str = "creating " + bat_filename + " ..."
    print str
    arcpy.AddMessage(str)

    
    bat_file = open(bat_filename, "w")
    bat_file.write(strtopass)
    bat_file.close()
    #str = "...now run the file called " + bat_filename

    #subprocess.call([r + '"' + bat_filename + '"'])
    subprocess.call([r"c:\1_create_diff_SB.bat"])

    #now delete the bat file!
    os.remove(r"c:\1_create_diff_SB.bat")
    
    #print str
    #arcpy.AddMessage(str)
except:
    str = "there is a problem!"
    print str
    arcpy.AddMessage(str)



