'This modules gets the data from the selected MXD and puts the files into the DataGridView

Module ModGetMXD
    Public Sub GetMXDFiles()
        Dim strMXDFilePath As String

        If frmMain.txtWorkingDirectory.Text = "" Then  'it shouldnt be possible to be empty - but just in case
            MsgBox("Please select an MXD file first")
            Exit Sub
        Else
            strMXDFilePath = frmMain.txtWorkingDirectory.Text  'read the MXD file path and file name
        End If

        ''###############################
        'Dim pMapDocument As IMapDocument
        'Dim pMap As IMap
        'Dim pEnumLayer As IEnumLayer
        'Dim pFeatureLayer As IFeatureLayer
        'Dim pDataLayer As IDataLayer2
        'Dim pDatasetName As IDatasetName
        'Dim pWorkspaceName As IWorkspaceName
        'Dim pUID As New UID
        'Dim i As Integer

        'Try
        '' Open the map document.
        'pMapDocument = New MapDocument
        'pMapDocument.Open(sPath)

        '' Process each map in the document.
        'For i = 0 To pMapDocument.MapCount - 1
        'pMap = pMapDocument.Map(i)

        '' Get the feature layers in the map.
        'pUID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"
        'pEnumLayer = pMap.Layers(pUID)

        '' Update only the SDE feature classes.
        'pEnumLayer.Reset()
        'pFeatureLayer = CType(pEnumLayer.Next, IFeatureLayer)
        'Do While Not pFeatureLayer Is Nothing
        'If pFeatureLayer.DataSourceType = "SDE Feature Class" Then
        'pDataLayer = CType(pFeatureLayer, IDataLayer2)
        'If TypeOf pDataLayer.DataSourceName Is IDatasetName Then
        'pDatasetName = CType(pDataLayer.DataSourceName, IDatasetName)
        'pWorkspaceName = pDatasetName.WorkspaceName

        'If chkSaveUserPass.CheckState = CheckState.Unchecked Then
        'pConnectionProperties.RemoveProperty("USER")
        'pConnectionProperties.RemoveProperty("PASSWORD")
        'End If

        'pWorkspaceName.ConnectionProperties = pConnectionProperties
        'End If
        'End If

        'pFeatureLayer = CType(pEnumLayer.Next, IFeatureLayer)
        'Loop

        'pMapDocument.ReplaceContents(CType(pMap, IMxdContents))
        'pMapDocument.Save()
        'pMapDocument.Close()
        'Next
        'Catch ex As Exception
        'MessageBox.Show(ex.Message, "frmChangeSDE:UpdateMapDocument()", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try




    End Sub
End Module
