import os
import sys
import re
import subprocess


def is64Windows():
    return 'PROGRAMFILES(X86)' in os.environ

def get_regaddinexe():
    sub_path = r'Common Files\ArcGIS\bin\ESRIRegAddIn.exe'
    pf = None

    if is64Windows():
        pf = os.environ['PROGRAMFILES(X86)']
    else:
        pf = os.environ['PROGRAMFILES']

    exe = os.path.join(pf,sub_path)
    if os.path.isfile(exe):
        return exe
    else:
        raise IOError(500, "The tool esriregaddin.exe was not found", exe)

def is_arc_licensed():
   
    try:
        import arcpy
    except RuntimeError:
        print "arc not licensed"
        return False
    else:
        print "arc is licensed for " + arcpy.ProductInfo()
        return not arcpy.ProductInfo() == "NotInitialized"

def test_start_menu_shortcut(path,name):
    return os.path.isfile(os.path.join(path,name+".lnk"))

def create_start_menu_shortcut(path,name):
    class CreateShortCutError(Exception):
        pass

    if not test_start_menu_shortcut(path,name):
        sub_path = ur'nircmd\nircmdc.exe'
        # pf = os.environ['PROGRAMFILES']
        pf = ur'C:\Program Files'
        cmd = [os.path.join(pf,sub_path), 
                ur'shortcut',
                ur'c:\py27arcgis101\ArcGIS10.1\python.exe',
                path,
                name,
                os.path.realpath(sys.argv[0])]
    
        # for e, v in os.environ:
            # print e, v
        # execmd del "~$folder.desktop$\calc.lnk" 
        print cmd
        
        if is64Windows():
            testvalue = 1073756732
        else:
            testvalue = 4206857
        
        try:
            if not testvalue == subprocess.call(cmd):
                raise CreateShortCutError("failed to create shortcur", str(cmd))
        except (subprocess.CalledProcessError, OSError):
            print "create shortcut failed;"
            print cmd
        else:
            print "command completed;"
            print cmd
   
def delete_start_menu_shortcut(path,name):
    if test_start_menu_shortcut(path,name):
        os.remove(os.path.join(path,name+".lnk"))
        
def run_reg_addin(esriregaddinexe, installdir):
    for root, dir, filenames in os.walk(installdir):
        for f in filenames:
            # print "all files = " + f
            if re.search(r"\.esriAddIn$", f) is not None:
                # print "just addins = " + f
                cmd = [esriregaddinexe, "/s", os.path.join(root, f)]
                # cmd_arg_str = os.path.join(root, f)
                print cmd
                try:
                    subprocess.check_call(cmd)
                except (subprocess.CalledProcessError, OSError):
                    print "command failed"
                else:
                    print "command completed"
            
            
if __name__ == '__main__':
    sub_path = ur'Microsoft\Windows\Start Menu\Programs\Startup'
    pf = os.environ['APPDATA']
    print "appdata = " + pf
    shortcut_path = os.path.join(pf,sub_path)
    print "shortcut_path = " + shortcut_path
    shortcut_name = ur'Register MA ESRI Addins'

    if is_arc_licensed():
        run_reg_addin(get_regaddinexe(), os.path.dirname(os.path.realpath(sys.argv[0])))
        delete_start_menu_shortcut(shortcut_path, shortcut_name)
    else:
        create_start_menu_shortcut(shortcut_path, shortcut_name)
