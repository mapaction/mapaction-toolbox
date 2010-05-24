Imports ESRI.ArcGIS.Geodatabase
Imports System.IO

Public Class DataNameNormalFile
    Inherits AbstractDataName

    Private myFileInfo As FileInfo

    Friend Sub New(ByVal fi As FileInfo, ByRef theDNCL As IDataNameClauseLookup, ByVal allowReNames As Boolean)
        MyBase.new(fi.Name.Remove(fi.Name.LastIndexOf(fi.Extension)), theDNCL, allowReNames)
        myFileInfo = fi
    End Sub

    Public Overrides Function getPathStr() As String
        Return myFileInfo.DirectoryName
    End Function

    'todo Implenment this!
    ''' <summary>
    ''' Returns the fully qualified IDataName as a String if possible.
    ''' </summary>
    ''' <returns>
    ''' a string of the current IDataName's the fully qualified name. Or is a path is not
    ''' available then just the fully qualified name is returned (ie including the filename
    ''' extension [eg ".shp"] the RDBMS database or RDBMS user name prefixes
    ''' [eg "mapaction.sde."]
    ''' </returns>
    ''' <remarks>
    ''' Returns getPathStr() + "\" + getNameStr()
    ''' Returns the fully qualified IDataName as a String, if a suitable meaning of
    ''' path is applicable. If there is no easy or meaningful sense of a path (eg for a 
    ''' RDBMS) then the fully qualified name is returned (ie including the filename
    ''' extension [eg ".shp"] the RDBMS database or RDBMS user name prefixes
    ''' [eg "mapaction.sde."].
    ''' </remarks>
    Public Overrides Function getNameAndFullPathStr() As String

    End Function

    ''' <summary>
    ''' Generally assumed to be a table for the non-GIS files
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Friend Overrides Function getUnderlyingDataType() As String
        Return DATATYPE_CLAUSE_TABLE
    End Function

    Public Overrides Sub performRename(ByVal newNameStr As String)
        If Not isRenameable() Then
            'todo move string into constants file
            Throw New RenamingDataException("Unable to rename File: " & m_strName)
        Else
            If myFileInfo.DirectoryName.EndsWith(Path.DirectorySeparatorChar) Then
                myFileInfo.MoveTo(myFileInfo.DirectoryName & newNameStr)
            Else
                myFileInfo.MoveTo(myFileInfo.DirectoryName & Path.DirectorySeparatorChar & newNameStr)
            End If
        End If
    End Sub

    Protected Friend Overrides Function renamePossible() As Boolean
        Return Not myFileInfo.IsReadOnly
    End Function
End Class
