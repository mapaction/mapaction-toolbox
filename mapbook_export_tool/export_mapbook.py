#-------------------------------------------------------------------------------
# Name:        export_dpp_and_metadata
# Purpose:      Export Data Driven Pages with Metadata
#
# Author:      Andrew K, Mapaction
#
# Created:     16/06/2016
#-------------------------------------------------------------------------------
import arcpy
import os

# Parameters:
#  0. Map Document
#  1. Export Path
#  2. Export Mode (e.g. individual PDF's etc..)
#  3. Return - page count
#  4. Return - output file size for metadata

class Export_mapbook(object):
    def __init__(self):
        # Setup parameters from ArcPy.
        self.initialise_params()

    def initialise_params(self):
        # Setup properties.

        self.map_doc = self._get_mxd(arcpy.GetParameterAsText(0))
        self.export_path = arcpy.GetParameterAsText(1) # Includes file name but not extension.
        self.export_mode = arcpy.GetParameterAsText(2)
    def _get_mxd(self, mxd_file):
        if mxd_file is None:
            return arcpy.mapping.MapDocument("current")
            # TODO - Handle no MXD
        else:
            return arcpy.mapping.MapDocument(mxd_file)
    def export_dpps(self):

        self.page_count = self.map_doc.dataDrivenPages.pageCount
        # Export Data Driven Pages to specified directory
        file_name = self.export_path + ".pdf"
        self.map_doc.dataDrivenPages.exportToPDF(self.export_path + ".pdf")
        self.file_size = os.path.getsize(file_name)


    def export(self):
        # Main method.
        if self.map_doc.isDDPEnabled == False:
            arcpy.AddError("Data Driven Pages is not enabled in MXD:\n" + self.map_doc.filePath)
            arcpy.SetParameter(3,self.page_count)
            arcpy.SetParameter(4,self.file_size)
            return  # TODO - return page count, output PDF file size.

        self.export_dpps()




if __name__ == '__main__':
    exp_tool = Export_mapbook()
    exp_tool.export()