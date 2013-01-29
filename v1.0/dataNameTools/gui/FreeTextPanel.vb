Imports System.Text

Friend Enum enmFreeTextIllegalChars As Integer
    'Constants relating to the status of an IDataListConnection object
    none = CInt(2 ^ 0)
    numberAtStart = CInt(2 ^ 1)
    illegalCharAtStart = CInt(2 ^ 2)
    illegalCharAnywhere = CInt(2 ^ 3)
    doubleUnderscore = CInt(2 ^ 4)
End Enum

Public Class FreeTextPanel

    Private m_lstStrIllegalChars As New List(Of String)
    Private m_blnAllowPrefixedNumbers As Boolean
    Private m_strPreviousAutoCorrectedText As String = Nothing
    Public Event filteredTextChanged()

    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_blnAllowPrefixedNumbers = False
        m_lstStrIllegalChars.AddRange(New String() {" ", "-", "!", """", "£", "$", "%", _
                "^", "&", "*", "(", ")", "=", "+", "[", "]", "{", "}", ";", "'", ".", _
                ":", "@", "~", ",", "/", "<", ">", "?", "\", "|", "`", "¬", "¦"})
    End Sub

    Sub New(ByVal blnAllowPrefixedNumbers As Boolean)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_blnAllowPrefixedNumbers = blnAllowPrefixedNumbers
        m_lstStrIllegalChars.AddRange(New String() {" ", "-", "!", """", "£", "$", "%", _
                "^", "&", "*", "(", ")", "=", "+", "[", "]", "{", "}", ";", "'", _
                ":", "@", "~", ",", "/", "<", ">", "?", "\", "|", "`", "¬", "¦"})
    End Sub

    Public Overrides Property Text() As String
        Get
            Return m_txtBx.Text
        End Get
        Set(ByVal value As String)
            m_txtBx.Text = value
        End Set
    End Property

    Property AllowPrefixedNumbers() As Boolean
        Get
            AllowPrefixedNumbers = m_blnAllowPrefixedNumbers
        End Get
        Set(ByVal blnAllowPrefixedNumbers As Boolean)
            m_blnAllowPrefixedNumbers = blnAllowPrefixedNumbers
        End Set
    End Property

    Private Sub m_txtBx_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_txtBx.TextChanged
        Dim intInitalCursorLoc As Integer
        Dim strInitalText As String
        Dim intIllegalCharsStatus As Integer
        Dim intNewCursorLoc As Integer
        Dim strNewText As String
        Dim stbMessage As StringBuilder
        'Dim strMessage As String = String.Empty


        'Record starting state
        intInitalCursorLoc = m_txtBx.SelectionStart
        strInitalText = m_txtBx.Text

        intIllegalCharsStatus = checkForIllegalChars(strInitalText)

        If (intIllegalCharsStatus = enmFreeTextIllegalChars.none) Then
            'decide when to reset label
            If strInitalText <> m_strPreviousAutoCorrectedText Then
                m_lblWarning.Text = String.Empty
            End If
        Else

            'Remove all illegal characters BEFORE the cursor location
            strNewText = removeIllegalCharsFromString(strInitalText, m_blnAllowPrefixedNumbers, True)
            intNewCursorLoc = intInitalCursorLoc - (strInitalText.Length - strNewText.Length)

            'Remove all illegal characters AFTER the cursor location
            'check that the cursor isn't now at the left end of the string
            If intNewCursorLoc > 0 Then
                strNewText = removeIllegalCharsFromString(strInitalText, True, False)
            Else
                strNewText = removeIllegalCharsFromString(strInitalText, m_blnAllowPrefixedNumbers, True)
            End If

            m_strPreviousAutoCorrectedText = strNewText

            m_txtBx.Text = strNewText
            m_txtBx.SelectionStart = intNewCursorLoc

            'now update label appropriately
            stbMessage = New StringBuilder()

            If ((Not m_blnAllowPrefixedNumbers) And (intIllegalCharsStatus And enmFreeTextIllegalChars.numberAtStart) = enmFreeTextIllegalChars.numberAtStart) Then
                stbMessage.AppendLine("Cannot start name with a numerical digit")
            ElseIf (intIllegalCharsStatus And enmFreeTextIllegalChars.illegalCharAtStart) = enmFreeTextIllegalChars.illegalCharAtStart Then
                stbMessage.AppendLine("Must start name with a letter")
            ElseIf (intIllegalCharsStatus And enmFreeTextIllegalChars.illegalCharAnywhere) = enmFreeTextIllegalChars.illegalCharAnywhere Then
                stbMessage.Append("Cannot include illegal characters """)

                'add illegal characters anywhere in the name
                For Each charTest In strInitalText.ToCharArray()
                    If m_lstStrIllegalChars.Contains(charTest.ToString()) Then
                        stbMessage.Append(charTest)
                    End If
                Next
                stbMessage.Append("""")
                stbMessage.Append(Environment.NewLine)
            ElseIf (intIllegalCharsStatus And enmFreeTextIllegalChars.doubleUnderscore) = enmFreeTextIllegalChars.doubleUnderscore Then
                stbMessage.AppendLine("Cannot include two consecutive underscores")
            End If

            System.Console.WriteLine(stbMessage.ToString)
            m_lblWarning.Text = stbMessage.ToString
        End If

        'Now check whether anything acctually changed. If not than assume that
        'all the required illegal char stripping function have succeded (or wheren't
        'required at all), in which case raise an event.
        If ((intInitalCursorLoc = m_txtBx.SelectionStart) And _
            (strInitalText = m_txtBx.Text)) Then
            'nothing changed
            RaiseEvent filteredTextChanged()
        End If


    End Sub

    Private Function checkForIllegalChars(ByRef strTestName As String) As Integer
        Dim intResult As Integer
        Dim strFirstChar As String

        intResult = 0

        If strTestName <> String.Empty Then

            strFirstChar = strTestName.Substring(0, 1)

            'numbers at the begining of name
            If IsNumeric(strFirstChar) Then
                intResult = intResult Or enmFreeTextIllegalChars.numberAtStart
            End If

            'illegal characters at the begining of name
            If m_lstStrIllegalChars.Contains(strFirstChar) Then
                intResult = intResult Or enmFreeTextIllegalChars.illegalCharAtStart
            End If

            'illegal characters anywhere in the name
            For Each charTest In strTestName.ToCharArray()
                If m_lstStrIllegalChars.Contains(charTest.ToString) Then
                    intResult = intResult Or enmFreeTextIllegalChars.illegalCharAnywhere
                End If
            Next

            'double underscores
            If strTestName.Contains("__") Then
                intResult = intResult Or enmFreeTextIllegalChars.doubleUnderscore
            End If

        End If

        'if still nothing then assume that there are no illegal characters
        If intResult = 0 Then
            intResult = enmFreeTextIllegalChars.none
        End If

        Return intResult
    End Function


    Private Function removeIllegalCharsFromString(ByRef strEnteredText As String, ByVal blnAllowNumberPrefix As Boolean, ByVal blnBeginingOfName As Boolean) As String
        Dim strResult As String
        Dim strFirstChar As String

        strResult = strEnteredText

        If strEnteredText <> String.Empty Then
            'numbers at the begining of name
            'illegal characters at the begining of name
            strFirstChar = strResult.Substring(0, 1)

            Do While ((Not blnAllowNumberPrefix) And IsNumeric(strFirstChar)) Or _
                      (blnBeginingOfName And m_lstStrIllegalChars.Contains(strFirstChar))

                strResult = strResult.Substring(1, (strResult.Length - 1))
                If strResult = String.Empty Then
                    strFirstChar = strResult
                Else
                    strFirstChar = strResult.Substring(0, 1)
                End If

            Loop

            'illegal characters anywhere in the name
            'and double underscores
            For Each strChr In m_lstStrIllegalChars
                strResult = strResult.Replace(strChr, "_")
                strResult = strResult.Replace("__", "_")
            Next
        End If

        Return strResult
    End Function


End Class
