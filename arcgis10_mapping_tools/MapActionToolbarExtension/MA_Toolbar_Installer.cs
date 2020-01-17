using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MapActionToolbarExtension
{
    [RunInstaller(true)]
    public partial class MA_Toolbar_Installer : System.Configuration.Install.Installer
    {
        public MA_Toolbar_Installer()
        {
            InitializeComponent();
        }
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);

            //Register the custom component.
            //-----------------------------
            //The default location of the ESRIRegAsm utility.
            //Note how the whole string is embedded in quotes because of the spaces in the path.
            string cmd1 = "\"" + Environment.GetFolderPath
                (Environment.SpecialFolder.CommonProgramFiles) +
                "\\ArcGIS\\bin\\ESRIRegAsm.exe" + "\"";
            //Obtain the input argument (via the CustomActionData Property) in the setup project.
            //An example CustomActionData property that is passed through might be something like:
            // /arg1="[ProgramFilesFolder]\[ProductName]\bin\ArcMapClassLibrary_Implements.dll",
            //which translates to the following on a default install:
            //C:\Program Files\MyGISApp\bin\ArcMapClassLibrary_Implements.dll.
            string part1 = this.Context.Parameters["arg1"];

            //Add the appropriate command line switches when invoking the ESRIRegAsm utility.
            //In this case: /p:Desktop = means the ArcGIS Desktop product, /s = means a silent install.
            string part2 = " /p:Desktop /s";

            //It is important to embed the part1 in quotes in case there are any spaces in the path.
            string cmd2 = "\"" + part1 + "\"" + part2;

            //Call the routing that will execute the ESRIRegAsm utility.
            int exitCode = ExecuteCommand(cmd1, cmd2, 30000);

            // pre-v10 method? esriRegAsm was introduced with v10
            //RegistrationServices regSrv = new RegistrationServices();
            //regSrv.RegisterAssembly(base.GetType().Assembly,
            //  AssemblyRegistrationFlags.SetCodeBase);
        }

        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            base.Uninstall(savedState);

            //Unregister the custom component.
            //-----------------------------
            //The default location of the ESRIRegAsm utility.
            //Note how the whole string is embedded in quotes because of the spaces in the path.
            string cmd1 = "\"" + Environment.GetFolderPath
                (Environment.SpecialFolder.CommonProgramFiles) +
                "\\ArcGIS\\bin\\ESRIRegAsm.exe" + "\"";
            //Obtain the input argument (via the CustomActionData Property) in the setup project.
            //An example CustomActionData property that is passed through might be something like:
            // /arg1="[ProgramFilesFolder]\[ProductName]\bin\ArcMapClassLibrary_Implements.dll",
            //which translate to the following on a default install:
            //C:\Program Files\MyGISApp\bin\ArcMapClassLibrary_Implements.dll.
            string part1 = this.Context.Parameters["arg1"];

            //Add the appropriate command line switches when invoking the ESRIRegAsm utility.
            //In this case: /p:Desktop = means the ArcGIS Desktop product, /u = means unregister the Custom Component, /s = means a silent install.
            string part2 = " /p:Desktop /u /s";

            //It is important to embed the part1 in quotes in case there are any spaces in the path.
            string cmd2 = "\"" + part1 + "\"" + part2;

            //Call the routing that will execute the ESRIRegAsm utility.
            int exitCode = ExecuteCommand(cmd1, cmd2, 30000);


            //RegistrationServices regSrv = new RegistrationServices();
            //regSrv.UnregisterAssembly(base.GetType().Assembly);
        }

        public static int ExecuteCommand(string Command1, string Command2, int
            Timeout)
        {
            //Set up a ProcessStartInfo using your path to the executable (Command1) and the command line arguments (Command2).
            ProcessStartInfo ProcessInfo = new ProcessStartInfo(Command1, Command2);
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = false;

            //Invoke the process.
            Process Process = Process.Start(ProcessInfo);
            Process.WaitForExit(Timeout);

            //Finish.
            int ExitCode = Process.ExitCode;
            Process.Close();
            return ExitCode;
        }
    }
}
