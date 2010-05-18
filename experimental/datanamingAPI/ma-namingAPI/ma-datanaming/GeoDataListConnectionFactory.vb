Imports System.IO
Imports ESRI.ArcGIS.ArcMapUI

Public Class GeoDataListConnectionFactory
    Shared myFactory As GeoDataListConnectionFactory

    Private Sub New()

    End Sub

    Public Shared Function getFactory() As GeoDataListConnectionFactory
        If myFactory Is Nothing Then
            myFactory = New GeoDataListConnectionFactory
        End If

        getFactory = myFactory
    End Function

    Public Function createGeoDataListConnection(ByVal listType As dnListType, ByRef args() As String) As IGeoDataListConnection
        Dim returnRef As IGeoDataListConnection

        Select Case listType
            Case dnListType.GDB
                returnRef = New DataListGeoDBConnection(args)
            Case dnListType.DIR
                returnRef = New DataListFileSystemDirectory(New DirectoryInfo(args(0)))
                'Case DATALIST_TYPE_MIXED_FILES
                'todo LOW: Add constructor call here, once it is writen
            Case dnListType.MXD
                returnRef = New DataListMXDConnection(args(0))
            Case Else
                Throw New ArgumentException("Unregconised GeoDataListConnection Type")
        End Select

        Return returnRef
    End Function

    Public Function createGeoDataListConnection(ByRef args() As String) As IGeoDataListConnection
        Dim returnRef As IGeoDataListConnection
        Dim argAsFileName As FileInfo

        If (args Is Nothing) OrElse args.Length < 1 Then
            Throw New ArgumentNullException()
        Else
            'See if the first argument is a valid file or directory
            argAsFileName = New FileInfo(args(0))

            If (argAsFileName.Attributes And FileAttributes.Directory) = FileAttributes.Directory Then
                'args(0) is a directory
                'therefore it is either a filebasedGDB or a normal directory
                If argAsFileName.FullName.EndsWith(".gdb") Then
                    returnRef = createGeoDataListConnection(dnListType.GDB, args)
                Else
                    returnRef = createGeoDataListConnection(dnListType.DIR, args)
                End If

            ElseIf argAsFileName.Exists() Then
                'args(0) is a file
                'therefore it is either a personal GDB, a GDB connection file or an MXD
                If argAsFileName.Extension = ".mxd" Then
                    returnRef = createGeoDataListConnection(dnListType.MXD, args)
                Else
                    returnRef = createGeoDataListConnection(dnListType.GDB, args)
                End If
            Else
                'args(0) is neither a file nor a directory, hence we will try it as a list of
                'connection proporties
                returnRef = createGeoDataListConnection(dnListType.GDB, args)
            End If

            End If

        Return returnRef
    End Function

    Public Function createGeoDataListConnection(ByRef args As String) As IGeoDataListConnection
        Dim newArgs() As String = {args}
        Return createGeoDataListConnection(newArgs)
    End Function

    Public Function createGeoDataListConnection(ByRef mxMap As IMxDocument) As IGeoDataListConnection
        Return New DataListMXDConnection(mxMap)
    End Function


End Class
