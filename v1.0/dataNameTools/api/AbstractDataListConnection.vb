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
''' Provides a framework for the implenmentation of the IDataListConnection interface.
''' </summary>
''' <remarks>
''' Provides a framework for the implenmentation of the IDataListConnection interface.
''' </remarks>
Public MustInherit Class AbstractDataListConnection
    Implements IDataListConnection

    Private m_blnRecuse As Boolean

    ''' <summary>
    ''' The Constructor.
    ''' </summary>
    ''' <param name="blnRecuse">The default Recusive behaviour, if there is a heirachircal 
    ''' structure to the underlying DataList. TRUE = the heirachy should be recused.
    ''' FALSE = the heirachy should not be recused.
    ''' </param>
    ''' <remarks>
    ''' The Constructor. 
    ''' 
    ''' The constructor is declared Protected to discourage implenmenters from
    ''' making their constructors public. Use the related Factory class instead.
    ''' </remarks>
    Protected Sub New(ByVal blnRecuse As Boolean)
        m_blnRecuse = blnRecuse
    End Sub

    ''' <summary>
    ''' The Constructor. Sets default Recusive behaviour to TRUE.
    ''' </summary>
    ''' <remarks>
    ''' The Constructor. Sets default Recusive behaviour to TRUE.
    ''' 
    ''' The constructor is declared Protected to discourage implenmenters from
    ''' making their constructors public. Use the related Factory class instead.
    ''' </remarks>
    Protected Sub New()
        m_blnRecuse = True
    End Sub

    Public MustOverride Function doesLayerExist(ByVal layerName As String) As Boolean Implements IDataListConnection.doesLayerExist

    Public MustOverride Function getDefaultDataNameClauseLookup() As IDataNameClauseLookup Implements IDataListConnection.getDefaultDataNameClauseLookup

    Public MustOverride Function getDetails() As String Implements IDataListConnection.getDetails

    Public MustOverride Function getpath() As FileInfo Implements IDataListConnection.getPath

    Public MustOverride Function isheirachical() As Boolean Implements IDataListConnection.isHeirachical

    Public MustOverride Function getDataListConnectionType() As dnListType Implements IDataListConnection.getDataListConnectionType

    Public MustOverride Function getDataListConnectionTypeDesc() As String Implements IDataListConnection.getDataListConnectionTypeDesc

    Public MustOverride Function getLayerNamesStrings() As List(Of String)
    'Public MustOverride Function getLayerNamesStrings() As List(Of String) Implements IDataListConnection.getLayerNamesStrings

    Public MustOverride Function getLayerDataNamesList(ByRef myDNCL As IDataNameClauseLookup) As List(Of IDataName) Implements IDataListConnection.getLayerDataNamesList

    Public Function getLayerDataNamesList() As List(Of IDataName) Implements IDataListConnection.getLayerDataNamesList
        Return getLayerDataNamesList(getDefaultDataNameClauseLookup())
    End Function

    Public Sub setRecuse(ByVal blnRecuse As Boolean) Implements IDataListConnection.setRecuse
        m_blnRecuse = blnRecuse
    End Sub

    Public Function getRecuse() As Boolean Implements IDataListConnection.getRecuse
        Return m_blnRecuse
    End Function

End Class
