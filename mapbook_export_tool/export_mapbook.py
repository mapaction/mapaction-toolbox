#-------------------------------------------------------------------------------
# Name:        export_dpp_and_metadata
# Purpose:      Export Data Driven Pages with Metadata
#
# Author:      Andrew K, Mapaction
#
# Created:     16/06/2016
#-------------------------------------------------------------------------------
import arcpy
import os, glob

# Parameters:
#  0. Map Document
#  1. Export Path / Folder
#  2. Export file name (MapDocument from export tool dialogue)
#  3. Export Mode (e.g. individual PDF's etc..)
#  4. Return - page count
#  5. Return - output file size for metadata

class Export_mapbook(object):
    def __init__(self):
        # Setup parameters from ArcPy.
        self.initialise_params()

    def _set_export_mode(self, sp_export_mode):
        # Get export mode from parameter. Defaults to single file if mode not supported.
        _requested_mode = sp_export_mode.upper()
        if _requested_mode == 'PDF_MULTIPLE_FILES_PAGE_NAME' or _requested_mode == 'PDF_MULTIPLE_FILES_PAGE_INDEX' or _requested_mode == 'PDF_SINGLE_FILE':
            self.multiple_files = sp_export_mode.upper()
        else:
            self.multiple_files = 'PDF_SINGLE_FILE'
            arcpy.AddWarning("Requested mode {0} not valid. set to single PDF file",sp_export_mode)

    def initialise_params(self):
        # Setup properties.

        self.map_doc = self._get_mxd(arcpy.GetParameterAsText(0))
        self.export_path = arcpy.GetParameterAsText(1)
        self.file_name = arcpy.GetParameterAsText(2)  # + ".pdf"
        self._set_export_mode(arcpy.GetParameterAsText(3))  # Sets self.multiple files.
        # TODO: Validate export path exists (potentially also that have write access?)

    def _get_mxd(self, mxd_file):
        if mxd_file is None:
            return arcpy.mapping.MapDocument("current")
            # TODO - Handle no MXD
        else:
            return arcpy.mapping.MapDocument(mxd_file)
    def export_dpps(self):
        self.file_size = 0
        self.page_count = self.map_doc.dataDrivenPages.pageCount
        # Export Data Driven Pages to specified directory
        file_name = os.path.join(self.export_path, self.file_name)
        self.map_doc.dataDrivenPages.exportToPDF(file_name, page_range_type='ALL',multiple_files=self.multiple_files)
        if self.multiple_files == 'PDF_SINGLE_FILE':
            self.file_size = os.path.getsize(file_name + ".pdf")
        else:
            # This is fudge - just lists files based on path + file name and a wildcard to get total size.
            files = glob.glob(file_name + "*")
            for file in files:
                self.file_size += os.path.getsize(file)

        arcpy.AddMessage("Exported {0},{1} Bytes".format(file_name, self.file_size ))


    def export(self):
        # Main method.
        if self.map_doc.isDDPEnabled == False:
            arcpy.AddError("Data Driven Pages is not enabled in MXD:\n" + self.map_doc.filePath)

            return

        self.export_dpps()
        arcpy.SetParameter(4,self.page_count)
        arcpy.SetParameter(5,self.file_size)




if __name__ == '__main__':
    exp_tool = Export_mapbook()
    exp_tool.export()
