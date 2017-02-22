#-------------------------------------------------------------------------------
# Name:        check_dpp_mxd
# Purpose:     Checks whether Data Driven Pages is enabled on an MXD. Requires ArcGIS Installation for ArcPy.
#
# Author:      Andrew K, Mapaction
#
# Created:     16/06/2016
#-------------------------------------------------------------------------------

import arcpy

def get_mxd(mxdfile):
    if mxdfile is None:
        return arcpy.mapping.MapDocument("current")
    else:
        return arcpy.mapping.MapDocument(mxdfile)

def main():
    mxd = arcpy.GetParameterAsText(0)
    mapDoc = get_mxd(mxd)
    b_dpp_status = mapDoc.isDDPEnabled
    arcpy.SetParameter(1,b_dpp_status)
    if b_dpp_status:
        arcpy.AddMessage("Data Driven Pages is enabled")
    else:
        arcpy.AddWarning("Data Driven Pages is not enabled")
    return

if __name__ == '__main__':

    main()
