Option Explicit On


Module testingCommandline

    Sub Main(ByVal CmdArgs() As String)
        Dim dataListPath As String = Nothing
        Dim lookupTablesPath As String = Nothing
        Dim namesToTest As New List(Of IDataName)
        Dim nextArgIsDataList As Boolean = False
        Dim nextAgrIsLookupTable As Boolean = False
        Dim mdbPathStr(1) As String
        Dim dataListObj As IGeoDataListConnection = Nothing
        Dim licHandler As New ESRIlicenceHandler

        licHandler.getESRIlicence()

        Try

            For Each arg As String In CmdArgs
                'System.Console.WriteLine(arg)
                Select Case arg
                    Case "-l"
                        nextArgIsDataList = True
                    Case "-t"
                        nextAgrIsLookupTable = True
                    Case Else
                        If nextArgIsDataList Then
                            dataListPath = arg
                            nextArgIsDataList = False
                        ElseIf nextAgrIsLookupTable Then
                            lookupTablesPath = arg
                            nextAgrIsLookupTable = False
                        Else
                            'namesToTest.Add(arg)
                        End If
                End Select

            Next

            System.Console.WriteLine()


            '''''''''''''''''''
            '' First look for an IGeoDataListConnection argument
            '''''''''''''''''''

            If Not dataListPath Is Nothing Then
                'We are using names read from a directory or GDB
                dataListObj = GeoDataListConnectionFactory.getFactory().createGeoDataListConnection(dataListPath)

                namesToTest.AddRange(dataListObj.getLayerDataNamesList())
            Else
                'We are using standalone names from the commandline


                'If namesToTest.Count = 0 Then

                '    'geoextent_datacategory_theme_datatype[_scale]_source[_permission][_FreeText]
                '    Dim testOptionalClauses() As String = { _
                '        "alb-popu-cas-py-s3_osm_hp", _
                '        "bgd_popu_cas_py_s3_osm_hp", _
                '        "bgd_popu_cas_py_osm_hp", _
                '        "bgd_popu_cas_py_osm", _
                '        "bgd_popu_cas_py_s2_osm", _
                '        "bgd_popu_cas__py_s2_osm", _
                '        "bgd_popu_cas_py_s3_osm_hp_Fe_txt", _
                '        "bgd_popu_cas_py_osm_hp_Fe_txt", _
                '        "bgd_popu_cas_py_osm_Fe_txt", _
                '        "bgd_popu_cas_py_s2_osm_Fe_txt", _
                '        "bgd_popu_cas_py_s3_osm_hp_Free_txt", _
                '        "bgd_popu_cas_py_osm_hp_Free_txt", _
                '        "bgd_popu_cas_py_osm_Free_txt", _
                '        "bgd_popu_cas_py_s2_osm_Free_txt"}

                '    namesToTest.AddRange(testOptionalClauses)
                'End If
            End If

            '''''''''''''''''''
            '' Secound look for an IDataNameClauseLookup argument
            '''''''''''''''''''
            Dim dncl As IDataNameClauseLookup
            Dim dnclFactory As DataNameClauseLookupFactory = DataNameClauseLookupFactory.getFactory()

            If lookupTablesPath Is Nothing Then
                'mdbPathStr(0) = My.Application.Info.DirectoryPath & "\data-naming-conventions-beta_v0.8.mdb"
                If Not dataListObj Is Nothing Then
                    dncl = dataListObj.getDefaultDataNameClauseLookup()
                End If
            Else
                mdbPathStr(0) = lookupTablesPath
                dncl = dnclFactory.createDataNameClauseLookup(dnClauseLookupType.MDB, mdbPathStr)
            End If

            System.Console.WriteLine("Using data naming tables at = " & dncl.getDetails())



            '''''''''''''''''''
            '' Thrid loop through IDataName details
            '''''''''''''''''''
            Dim status As Long

            'System.Console.WriteLine("allDataNameStrMessages.Count = ", allDataNameStrMessages.Count)

            For Each testDName In namesToTest
                'todo Rewrite the way that the names are tested in the testingCommandline
                status = testDName.checkNameStatus()
                System.Console.WriteLine()
                System.Console.WriteLine("********************************************************")
                System.Console.WriteLine("DATA NAME:  " & testDName.getNameStr())
                System.Console.WriteLine("********************************************************")
                'System.Console.WriteLine("Data name status: " & status)
                For Each statusStr In AbstractDataNameClauseLookup.getDataNamingStatusStrings(status)
                    System.Console.WriteLine(statusStr)
                Next
            Next



        Catch ex As Exception
            System.Console.WriteLine(ex.ToString())
            System.Console.WriteLine("Test commandline ended with error")
        End Try

        licHandler.dropESRILicence()
        'MsgBox("done")
    End Sub

End Module
