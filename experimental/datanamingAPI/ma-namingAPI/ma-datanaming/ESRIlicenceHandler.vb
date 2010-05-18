Option Strict On

Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.esriSystem.esriLicenseProductCode
Imports ESRI.ArcGIS.esriSystem.esriLicenseStatus
Imports ESRI.ArcGIS.esriSystem.esriLicenseExtensionCode


Public Class ESRIlicenceHandler

    Private m_pAoInitialize As IAoInitialize


    Public Sub getESRIlicence()
        'This sample is designed to perform license initialization on a system
        'that may have access to a floating license. It requires GDB editing
        'capability and A spatial Analyst extension

        'This sample will check the required licenses and keep them checked out
        Dim licenseStatus As esriLicenseStatus
        'First try copy protection EngineGeoDB
        licenseStatus = CheckOutLicenses(esriLicenseProductCodeEngineGeoDB)
        If (licenseStatus = esriLicenseNotLicensed) Then
            'Next try Desktop ArcEngine
            licenseStatus = CheckOutLicenses(esriLicenseProductCodeArcEditor)
            'For Desktop licenses we also need to consider them being unavailable
            If ((licenseStatus = esriLicenseNotLicensed) Or (licenseStatus = esriLicenseUnavailable)) Then
                'Last try Desktop ArcInfo
                licenseStatus = CheckOutLicenses(esriLicenseProductCodeArcInfo)
            End If
        End If

        'Take a look at the licenseStatus to see if it failed
        'Not licensed
        If (licenseStatus = esriLicenseNotLicensed) Then
            ' MsgBox("You are not licensed to run this product")
            dropESRILicence()
            'The licenses needed are currently in use
        ElseIf (licenseStatus = esriLicenseUnavailable) Then
            ' MsgBox("There are insufient licenses to run")
            dropESRILicence()
            'The licenses unexpected license failure
        ElseIf (licenseStatus = esriLicenseFailure) Then
            ' MsgBox("Unexpected license failure please contact you administrator'")
            dropESRILicence()
            'Already initialized (Initialization can only occur once)
        ElseIf (licenseStatus = esriLicenseAlreadyInitialized) Then
            'MsgBox("You license has already been initialized please check you implementation")
            dropESRILicence()
            'Everything was checkedout successfully
        ElseIf (licenseStatus = esriLicenseCheckedOut) Then
            'MsgBox("Licenses checked out successfully")
        End If

    End Sub

    Public Sub dropESRILicence()

        'Checkin the extension
        m_pAoInitialize.CheckInExtension(esriLicenseExtensionCodeSpatialAnalyst)
        'Shutdown
        m_pAoInitialize.Shutdown()

    End Sub

    Private Function CheckOutLicenses(ByVal productCode As esriLicenseProductCode) As esriLicenseStatus

        Dim licenseStatus As esriLicenseStatus
        m_pAoInitialize = New AoInitialize
        CheckOutLicenses = esriLicenseUnavailable

        'Check the productCode
        licenseStatus = m_pAoInitialize.IsProductCodeAvailable(productCode)
        If (licenseStatus = esriLicenseAvailable) Then
            'Check the extensionCode
            licenseStatus = m_pAoInitialize.IsExtensionCodeAvailable(productCode, esriLicenseExtensionCodeSpatialAnalyst)
            If (licenseStatus = esriLicenseAvailable) Then
                'Initialize the license
                licenseStatus = m_pAoInitialize.Initialize(productCode)
                If (licenseStatus = esriLicenseCheckedOut) Then
                    'Checkout the extension
                    licenseStatus = m_pAoInitialize.CheckOutExtension(esriLicenseExtensionCodeSpatialAnalyst)
                End If
            End If
        End If

        CheckOutLicenses = licenseStatus

    End Function


End Class
