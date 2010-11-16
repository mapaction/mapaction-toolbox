Imports System.Windows.Forms
Imports mapaction.datanames.api

Public Class DataRenameDialog

    'Private m_lstStrExcluded As New List(Of String)
    Private m_Dataname As IDataName
    Private m_icoValid As Image = Drawing.Icon.FromHandle(My.Resources.icoOK.GetHicon).ToBitmap
    Private m_icoWarning As Image = Drawing.Icon.FromHandle(My.Resources.icoWarning.GetHicon).ToBitmap
    Private m_icoError As Image = Drawing.Icon.FromHandle(My.Resources.icoError.GetHicon).ToBitmap

    Public Property dataName() As IDataName
        Get
            Return m_Dataname
        End Get
        Set(ByVal dn_New As IDataName)
            If (dn_New Is Nothing) Then
                Throw New ArgumentException("cannot assign null value to DataRenameDialog.dataName")
            Else
                m_Dataname = dn_New
                m_lblOldName.Text = m_Dataname.getNameStr()
                m_freeTxtPnl.Text = m_Dataname.getNameStr()

                If m_Dataname.isRenameable() Then
                    OK_Button.Enabled = True
                    m_freeTxtPnl.Enabled = True
                Else
                    OK_Button.Enabled = False
                    m_freeTxtPnl.Enabled = False
                    m_lblProposedNameStatus.Text = m_Dataname.getNameStr() & " is read-only and cannot be renamed."
                End If
            End If
        End Set
    End Property

    Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub



    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            m_Dataname.rename(getPropossedName())

            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MsgBox("An error occured whilst renaming " & m_Dataname.getNameStr() & vbNewLine _
                   & ex.Message & vbNewLine _
                   & ex.ToString())
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub handleNameInputEvent() Handles m_freeTxtPnl.filteredTextChanged
        'When the dialog is changed to include lots of drop down menus then this part will need to be changed.
        updatePropossedNameStatusIndicator(getPropossedName())
    End Sub

    Private Function getPropossedName() As String
        'TODO: medium In due course using this function to concatenate all of the strings from drop down menus.
        Return m_freeTxtPnl.Text
    End Function

    Private Sub updatePropossedNameStatusIndicator(ByVal str_NewName As String)
        Dim lngPropNameStatus As Long
        Dim strStatusMsg As String

        'Get Name status
        lngPropNameStatus = m_Dataname.checkPropossedNameStatus(str_NewName)


        'Add Image
        If ((lngPropNameStatus And dnNameStatus.INVALID) = dnNameStatus.INVALID) Or _
             ((lngPropNameStatus And dnNameStatus.SYNTAX_ERROR) = dnNameStatus.SYNTAX_ERROR) Then
            '.Cells(3).Value = True  'Tick the error box
            m_picBxPropNameStatus.Image = m_icoError
        ElseIf (lngPropNameStatus And dnNameStatus.WARN) = dnNameStatus.WARN Then
            '.Cells(2).Value = True  'tick the Warning box
            m_picBxPropNameStatus.Image = m_icoWarning
        Else
            '.Cells(1).Value = True  'tick the Valid box
            m_picBxPropNameStatus.Image = m_icoValid
        End If

        'Add Comments label
        strStatusMsg = DataNameStringFormater.getDataNamingStatusMessage(lngPropNameStatus)
        'intLineCnt = 1 + strStatusMsg.Split(CChar(vbNewLine)).Length

        If strStatusMsg = String.Empty Then
            m_lblProposedNameStatus.Text = "OK"
        Else
            m_lblProposedNameStatus.Text = strStatusMsg
        End If

    End Sub

End Class
