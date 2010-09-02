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

Imports System.IO

''' <summary>
''' Provides a specfic implenmentation of the IDataName, based on files which
''' are "normal" operating system files and are not a component of a logical
''' GIS file (eg shapefiles)
''' </summary>
''' <remarks>
''' Provides a specfic implenmentation of the IDataName, based on files which
''' are "normal" operating system files and are not a component of a logical
''' GIS file (eg shapefiles)
'''
''' There is no public constructor for this class. New instances should
''' be generated using the getLayerDataNamesList() method of a relevant
''' IDataListConnection object.
'''  </remarks>
Public Class DataNameNormalFile
    Inherits AbstractDataName

    Private m_fInfo As FileInfo

    ''' <summary>
    ''' Create a new IDataName for the specified file.
    ''' </summary>
    ''' <param name="fInfo">A FileInfo object</param>
    ''' <param name="dncl">The DataNameLookupTable against which the validity
    ''' of the name will be checked.</param>
    ''' <param name="blnAllowRenames">Allows the caller to create the object in readonly 
    ''' mode. Useful if the IDataListConnection would need to be modified to accomidate
    ''' renamed IDataName objects</param>
    ''' <remarks>
    ''' Create a new IDataName for the specified file.
    ''' </remarks>
    Friend Sub New(ByVal fInfo As FileInfo, ByRef dncl As IDataNameClauseLookup, ByVal blnAllowReNames As Boolean)
        MyBase.new(fInfo.Name.Remove(fInfo.Name.LastIndexOf(fInfo.Extension)), dncl, blnAllowReNames)
        m_fInfo = fInfo
    End Sub


    ''' <summary>
    ''' Returns the path of the directory containing the current file as a String.
    ''' Should not include trailing slash or backslash.
    ''' </summary>
    ''' <returns>
    ''' A string of the path of the directory containing the current file. 
    ''' </returns>
    ''' <remarks>
    ''' Returns the path of the directory containing the current file as a String.
    ''' Should not include trailing slash or backslash.
    ''' </remarks>
    Public Overrides Function getPathStr() As String
        Return m_fInfo.DirectoryName
    End Function

    ''' <summary>
    ''' Returns the underlying FileInfo Object.
    ''' </summary>
    ''' <returns>A object appropriate for the particular implenmentation</returns>
    ''' <remarks>
    ''' Returns the underlying FileInfo Object.
    ''' </remarks>
    Public Overrides Function getObject() As Object
        Return m_fInfo
    End Function


    ''' <summary>
    ''' Returns the fully qualified path and name of the current file as a String.
    ''' Should not include trailing slash or backslash.
    ''' </summary>
    ''' <returns>
    ''' A string of the fully qualified path and name of the current file. 
    ''' </returns>
    ''' <remarks>
    ''' Returns the fully qualified path and name of the current file as a String.
    ''' Should not include trailing slash or backslash.
    ''' </remarks>
    Public Overrides Function getNameAndFullPathStr() As String
        Return m_fInfo.FullName
    End Function

    ''' <summary>
    ''' Generally assumed to be a table for the non-GIS files.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Friend Overrides Function getUnderlyingDataType() As String
        Return DATATYPE_CLAUSE_TABLE
    End Function


    ''' <summary>
    ''' Renames the underlying file with the new name. The new name does not
    ''' have to be valid or even parsable as an IDataName, but must be acceptable
    ''' for the underlying filesystems.
    ''' </summary>
    ''' <param name="strNewName">The new Name for the file.</param>
    ''' <remarks>
    ''' Renames the underlying file with the new name. The new name does not
    ''' have to be valid or even parsable as an IDataName, but must be acceptable
    ''' for the underlying filesystems.
    ''' 
    ''' If the underlying storage is readonly then the implenmenting method
    ''' will throw a RenamingDataException exception.
    ''' 
    ''' End users should not call this method, but use the rename() method 
    ''' instead.
    ''' </remarks>
    Public Overrides Sub performRename(ByVal strNewName As String)
        If Not isRenameable() Then
            'todo MEDIUM: move string into constants file
            Throw New RenamingDataException("Unable to rename File: " & m_strName, Me)
        Else
            If m_fInfo.DirectoryName.EndsWith(Path.DirectorySeparatorChar) Then
                m_fInfo.MoveTo(m_fInfo.DirectoryName & strNewName)
            Else
                m_fInfo.MoveTo(m_fInfo.DirectoryName & Path.DirectorySeparatorChar & strNewName)
            End If
            m_strName = m_fInfo.Name.Remove(m_fInfo.Name.LastIndexOf(m_fInfo.Extension))
        End If
    End Sub


    ''' <summary>
    ''' Returns a runtime value indicating whether or not the underlying file can be
    ''' renamed or not. This is dependant on whether or not the file is readonly or
    ''' not.
    ''' </summary>
    ''' <returns>
    ''' TRUE = The file can be renamed.
    ''' FALSE = Attempting to rename the file will cause an exception to be raised.
    ''' </returns>
    ''' <remarks>
    ''' Returns a runtime value indicating whether or not the underlying file can be
    ''' renamed or not. This is dependant on whether or not the file is readonly or
    ''' not.
    ''' 
    ''' The runtime conbination of being both *allowed* and *possible* are combined by 
    ''' the isRenameable() method. End users should call this method.
    ''' </remarks>
    Protected Friend Overrides Function isRenamePossible() As Boolean
        Return Not m_fInfo.IsReadOnly
    End Function
End Class
