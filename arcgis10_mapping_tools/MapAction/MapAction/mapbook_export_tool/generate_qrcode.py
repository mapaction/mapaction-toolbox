#-------------------------------------------------------------------------------
# Name:        generate_qrcode
# Purpose:     Generate QR Code
#
# Author:      Steve Hurst, Mapaction
#
# Created:     17/03/2019
#-------------------------------------------------------------------------------
import arcpy
import os, glob
import qrcode
from PIL import Image
import uuid

# Parameters:
#  0. URL
#  1. Path
#  2. Return - True

class Generate_QRCode(object):
    def __init__(self):
        # Setup parameters from ArcPy.
        self.initialise_params()

    def initialise_params(self): 
        self.url = arcpy.GetParameterAsText(0) 
        self.imgPath = arcpy.GetParameterAsText(1) 

    def generateQRCode(self):
        qr = qrcode.QRCode(version=1,
                           error_correction=qrcode.constants.ERROR_CORRECT_L,
                           box_size=10,
                           border=4,
                               )
        qr.add_data(self.url)
        qr.make(fit=True)
        img = qr.make_image(fill_color="black", back_color="white")
        img.save(self.imgPath)

    def generate(self):
        self.generateQRCode()
        arcpy.SetParameter(2,True)

if __name__ == '__main__':
    exp_tool = Generate_QRCode()
    exp_tool.generate()