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

    'todo Look at where the getDataNamingStatusStrings code should be and if it need
    'wrapping up before tidying this up and adding to the summary. 
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="lngBitsum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function getDescriptionFromStatus(ByVal lngBitsum As Long) As String
        Dim strReturnVal As String = String.Empty

        'todo HIGH FIX ME
        'For Each strStatus In AbstractDataNameClauseLookup.getDataNamingStatusStrings(lngBitsum)
        '    strReturnVal = strReturnVal & strStatus & vbNewLine
        'Next
        'g_htbDNStatusStrMessages.Item(emuBitsum

        Return strReturnVal
    End Function

End Class
