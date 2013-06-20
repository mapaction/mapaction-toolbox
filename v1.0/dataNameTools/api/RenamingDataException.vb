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

''' <summary>
''' An Exception indicating at an error occured whilst attempting to rename
''' the underlying data store.
''' </summary>
''' <remarks>
''' An Exception indicating at an error occured whilst attempting to rename
''' the underlying data store.
''' 
''' Provides a convenence function to get hold of a reference to the offending 
''' IDataname object.
''' </remarks>
Public Class RenamingDataException
    Inherits Exception

    Private m_dnOffendingName As IDataName

    Protected Friend Sub New(ByVal strMessage As String, ByRef dnOffendingName As IDataName)
        MyBase.New(strMessage)
        m_dnOffendingName = dnOffendingName
    End Sub

    Protected Friend Sub New(ByRef dnOffendingName As IDataName)
        'todo LOW: add a constant with a meaningful default string here
        'eg. "Unable to rename Feature Class: " & myNameStr
        'MyBase.New(strMessage)
        m_dnOffendingName = dnOffendingName
    End Sub

    ''' <summary>
    ''' Returns the IDataName which could not be renamed.
    ''' </summary>
    ''' <returns>The IDataName which could not be renamed.</returns>
    ''' <remarks>
    ''' Returns the IDataName which could not be renamed
    ''' </remarks>
    Public Function getDataName() As IDataName
        Return m_dnOffendingName
    End Function

End Class
