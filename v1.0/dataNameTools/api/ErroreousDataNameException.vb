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
''' An exception which is thrown when certain operations are performed on IDataName
''' which requires the IDataName to be either at a minimum, parsable or even valid.
''' </summary>
''' <remarks>
''' An exception which is thrown when certain operations are performed on IDataName
''' which requires the IDataName to be either at a minimum, parsable or even valid.
''' 
''' It should be noted that IDataName.checkNameStatus() should never throw an
''' ErroreousDataNameException.
''' </remarks>
Public Class ErroreousDataNameException
    Inherits Exception

    Private m_lngNameStatus As Long

    Protected Friend Sub New(ByVal lngBitsum As Long)
        MyBase.New(getDescriptionFromStatus(lngBitsum))
        m_lngNameStatus = lngBitsum
    End Sub

    Protected Friend Sub New(ByVal description As String, ByVal lngBitsum As Long, ByRef innerEx As Exception)
        MyBase.New(description, innerEx)
        m_lngNameStatus = lngBitsum
    End Sub

    ''' <summary>
    ''' Returns the status of the IDataName which caused the problem.
    ''' </summary>
    ''' <returns>Long. The status of the IDataName which caused the problem.</returns>
    ''' <remarks>Returns the status of the IDataName which caused the problem.</remarks>
    Public Function getNameStatus() As Long
        Return m_lngNameStatus
    End Function

    'todo HIGH: Look at where the getDataNamingStatusStrings code should be and if it need
    'wrapping up before tidying this up and adding to the summary. 
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="lngBitsum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function getDescriptionFromStatus(ByVal lngBitsum As Long) As String
        Dim strReturnVal As String = String.Empty

        'todo HIGH: FIX ME
        'For Each strStatus In AbstractDataNameClauseLookup.getDataNamingStatusStrings(lngBitsum)
        '    strReturnVal = strReturnVal & strStatus & vbNewLine
        'Next
        'g_htbDNStatusStrMessages.Item(emuBitsum

        Return strReturnVal
    End Function

End Class
