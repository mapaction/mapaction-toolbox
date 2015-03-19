import os
import sys
import re
import subprocess


old_addin_uids = {r'{56a42ca7-d9c5-4c88-b57d-e04e9c8f9fb5}',
                  r'{1472afad-112a-4f7e-9d6e-5b2083d133bf}',
                  r'{8ed7a3cf-3f55-4460-b5f7-333ce742b3f5}',
                  r'{d206869a-5273-4370-a062-c3ecad281c58}'}

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

def shellquote(s):
    return '"' + s + '"'

def test_start_menu_shortcut(path,name):
    return os.path.isfile(os.path.join(path,name))

def create_start_menu_shortcut(path,name):
    if not test_start_menu_shortcut(path,name):

        cmdlst = [r'cmd',
                r'/c',
                r'echo', 
                shellquote(r'c:\py27arcgis102\ArcGIS10.2\python.exe'),
                shellquote(os.path.realpath(sys.argv[0])),
                r'>',
                shellquote(os.path.join(path, name))]

        cmd = " ".join(cmdlst)
        
        try:
            subprocess.check_call(cmd)
        except (subprocess.CalledProcessError, OSError):
            print "create shortcut failed;"
            print cmd
        else:
            print "command completed;"
            print cmd
   
def delete_start_menu_shortcut(path,name):
    if test_start_menu_shortcut(path,name):
        os.remove(os.path.join(path,name))


def remove_old_addin(esriregaddinexe, uids):
    for uid in uids:
        # print "all files = " + f
        if re.search(r"{(\w|\d){8}(-(\w|\d){4}){3}-(\w|\d){12}}", uid) is not None:
            # print "just addins = " + f
            cmd = [esriregaddinexe, uid, "/u", "/s"]
            # cmd_arg_str = os.path.join(root, f)
            print cmd
            try:
                subprocess.check_call(cmd)
            except (subprocess.CalledProcessError, OSError):
                print "command failed"
            else:
                print "command completed"
            
        
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
    sub_path = r'Microsoft\Windows\Start Menu\Programs\Startup'
    pf = os.environ['APPDATA']
    print "appdata = " + pf
    shortcut_path = os.path.join(pf,sub_path)
    print "shortcut_path = " + shortcut_path
    shortcut_name = r'Register_MA_ESRI_Addins.bat'

    if is_arc_licensed():
        regaddinexe = get_regaddinexe()
        remove_old_addin(regaddinexe, old_addin_uids)
        run_reg_addin(regaddinexe, os.path.dirname(os.path.realpath(sys.argv[0])))
        delete_start_menu_shortcut(shortcut_path, shortcut_name)
    else:
        create_start_menu_shortcut(shortcut_path, shortcut_name)
