''' <summary>
''' For want of anywhere better this module is for functions which
''' manipulate strings to make them suitable for display to users.
''' </summary>
''' <remarks>
''' For want of anywhere better this module is for functions which
''' manipulate strings to make them suitable for display to users.
''' </remarks>
Public Class DataNameStringFormater

    Private Sub New()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="lngStatus"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getDataNamingStatusMessage(ByVal lngStatus As Long) As String
        Dim stb As New System.Text.StringBuilder

        For Each enuStaCode In g_lstDNNameStatusValues
            If ((lngStatus And enuStaCode) = enuStaCode) Then
                stb.AppendLine(g_htbDNStatusStrMessages.Item(enuStaCode))
            End If
        Next

        Return stb.ToString()
    End Function

End Class
