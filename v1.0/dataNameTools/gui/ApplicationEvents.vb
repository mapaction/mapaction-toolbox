Imports ESRI.ArcGIS.esriSystem

Namespace My

    Partial Friend Class MyApplication

        Private m_AOLicenseInitializer As LicenseInitializer = New gui.LicenseInitializer()
        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            'ESRI License Initializer generated code.
            m_AOLicenseInitializer.InitializeApplication(New esriLicenseProductCode() {esriLicenseProductCode.esriLicenseProductCodeArcEditor, esriLicenseProductCode.esriLicenseProductCodeArcInfo}, _
            New esriLicenseExtensionCode() {})
        End Sub

        Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown
            'ESRI License Initializer generated code.
            'Do not make any call to ArcObjects after ShutDownApplication()
            m_AOLicenseInitializer.ShutdownApplication()
        End Sub
    End Class

End Namespace

