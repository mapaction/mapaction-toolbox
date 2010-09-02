'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''Copyright (C) 2010 MapAction UK Charity No. 1075977
''
''www.mapaction.org
''
''This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 3 of the License, or (at your option) any later version.
''
''This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
''
''You should have received a copy of the GNU General Public License along with this program; if not, see <http://www.gnu.org/licenses>.
''
''Additional permission under GNU GPL version 3 section 7
''
''If you modify this Program, or any covered work, by linking or combining it with 
''ESRI ArcGIS Desktop Products (ArcView, ArcEditor, ArcInfo, ArcEngine Runtime and ArcEngine Developer Kit) (or a modified version of that library), containing parts covered by the terms of ESRI's single user or concurrent use license, the licensors of this Program grant you additional permission to convey the resulting work.
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports mapaction.datanames.api


Module maDataNameChecker

    Private m_strDataListPath As String = Nothing
    Private m_strLookupTablesPath As String = Nothing
    Private m_blnRecuse As Boolean = True

    Sub Main(ByVal CmdArgs() As String)
        Dim blnNextArgIsDataList As Boolean = False
        Dim blnNextAgrIsLookupTable As Boolean = False
        Dim blnPrintVersion As Boolean = False
        Dim blnPrintHelp As Boolean = False
        Dim blnUnregconisedArg As Boolean = False
        Dim strMDBpath(1) As String
        Dim licHandler As New ESRIlicenceHandler

        licHandler.getESRIlicence()

        ''
        ' Read the arguments
        If CmdArgs Is Nothing OrElse CmdArgs.Length < 1 Then
            blnPrintHelp = True
        Else
            For Each strArg As String In CmdArgs
                'System.Console.WriteLine(arg)
                Select Case strArg
                    Case "-l", "/l"
                        blnNextArgIsDataList = True
                    Case "-t", "/t"
                        blnNextAgrIsLookupTable = True
                    Case "-v", "/v"
                        blnPrintVersion = True
                    Case "-h", "-?", "/h", "/?"
                        blnPrintHelp = True
                    Case "-r"
                        m_blnRecuse = True
                    Case "-n"
                        m_blnRecuse = False
                    Case Else
                        If blnNextArgIsDataList Then
                            m_strDataListPath = strArg
                            blnNextArgIsDataList = False
                        ElseIf blnNextAgrIsLookupTable Then
                            m_strLookupTablesPath = strArg
                            blnNextAgrIsLookupTable = False
                        Else
                            blnUnregconisedArg = True
                        End If
                End Select
            Next
        End If

        ''
        ' Now decide want to do
        If blnNextAgrIsLookupTable Or blnNextArgIsDataList Or blnUnregconisedArg Then
            printUnregconisedArgs()
        ElseIf blnPrintHelp Then
            printUsageMessage()
        ElseIf blnPrintVersion Then
            printNameAndVersion()
        Else
            testDataNames()
        End If

        licHandler.dropESRILicence()
        'MsgBox("done")
    End Sub

    Private Sub testDataNames()
        Dim dlc As IDataListConnection = Nothing
        Dim dnclFactory As DataNameClauseLookupFactory
        Dim dncl As IDataNameClauseLookup
        Dim lstDNtoTest As New List(Of IDataName)
        Dim lngStatus As Long

        Try

            '''''''''''''''''''
            '' First look for an IGeoDataListConnection argument
            '''''''''''''''''''
            If m_strDataListPath Is Nothing Then
                printUnregconisedArgs()
            Else
                'We are using names read from a directory or GDB
                dlc = DataListConnectionFactory.getFactory().createDataListConnection(m_strDataListPath)

                '''''''''''''''''''
                '' Secound look for an IDataNameClauseLookup argument
                '''''''''''''''''''
                If m_strLookupTablesPath Is Nothing Then
                    dncl = dlc.getDefaultDataNameClauseLookup()

                    System.Console.WriteLine()
                    System.Console.WriteLine("Using Data Name Clause Lookup Tables at:")
                    System.Console.WriteLine(dncl.getDetails())
                Else
                    dnclFactory = DataNameClauseLookupFactory.getFactory()
                    dncl = dnclFactory.createDataNameClauseLookup(m_strLookupTablesPath)
                End If

                dlc.setRecuse(m_blnRecuse)
                lstDNtoTest.AddRange(dlc.getLayerDataNamesList(dncl))

                '''''''''''''''''''
                '' Third loop through IDataName details
                '''''''''''''''''''
                For Each dnCurrent In lstDNtoTest
                    'todo MEDIUM: Rewrite the way that the names are tested in the testingCommandline
                    lngStatus = dnCurrent.checkNameStatus()

                    System.Console.WriteLine()
                    System.Console.WriteLine("********************************************************")
                    System.Console.WriteLine("DATA NAME:  " & dnCurrent.getNameStr())
                    System.Console.WriteLine("********************************************************")
                    System.Console.Write(DataNameStringFormater.getDataNamingStatusMessage(lngStatus))
                Next

            End If

        Catch ex As Exception
            System.Console.WriteLine(ex.ToString())
            System.Console.WriteLine("Test commandline ended with error")
        End Try

    End Sub


    Private Sub printUnregconisedArgs()
        Dim stb As New System.Text.StringBuilder

        stb.AppendLine()
        stb.AppendLine("Unregconised Arguments:")
        stb.AppendFormat("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}", My.Application.CommandLineArgs)

        System.Console.Write(stb.ToString())
        printUsageMessage()
    End Sub

    Private Sub printUsageMessage()
        Dim stb As New System.Text.StringBuilder

        stb.AppendLine()
        stb.AppendFormat("Usage: {0}", My.Application.Info.AssemblyName)
        stb.AppendLine()
        stb.AppendLine()
        stb.AppendFormat("{0} {{-l <path to data list> [-r|-n] [-t <path to non-default clause lookup tables>] | -v | -h | -? }}", My.Application.Info.AssemblyName)
        stb.AppendLine()
        stb.AppendLine()
        stb.AppendLine("Options:")
        stb.AppendLine(" -v       Print the name and version number and quit")
        stb.AppendLine(" -h or -? Print this usage message and quit")
        stb.AppendLine(" -l <path to data list> Specify the path of the list of datanames to be checked. This may be an MXD file (*.mxd), " & _
                       "a personal GDB (*.mdb), a filebased GDB (*.gdb), a directory of shapefiles (<dir>), or an SDE connection file (*.sde)")
        stb.AppendLine(" -r       (default) Recuse the list if appropriate (eg for a directory)")
        stb.AppendLine(" -n       Do not recuse the list if appropriate (eg for a directory)")
        stb.AppendLine(" -t       Optional: Override the default clause table locations by speficing a location of an MDB or GDB containing the " & _
                       "clause tables. If this option is not specified then the program will attempt to automatically detrive their location. " & _
                       "If they cannot be automatically located then the program will quit with an error.")

        System.Console.Write(stb.ToString())
    End Sub

    Private Sub printNameAndVersion()
        Dim stb As New System.Text.StringBuilder

        stb.AppendLine()
        stb.AppendFormat("{0} version {1}", My.Application.Info.AssemblyName, My.Application.Info.Version)
        stb.AppendLine()

        System.Console.Write(stb.ToString())
    End Sub

End Module
