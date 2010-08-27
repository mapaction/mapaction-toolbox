Imports ESRI.ArcGIS.Framework

Public Class frmMain

    Private m_blnStandAlone As Boolean

    Private Sub datalistRevertFromErrorMsg(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_tmrErrorDisplay.Tick
        m_tmrErrorDisplay.Enabled = False
        datalistStatusChanged(dListSelectorPnl.getStatus)
    End Sub

    Private Sub datalistStatusChanged(ByVal srtNewStatus As DataListSelectorPanelStatus) Handles dListSelectorPnl.statusChanged

        If srtNewStatus = DataListSelectorPanelStatus.READY Then
            'Label1.Text = "READY!!!"
            'picBxReadiness.Image = My.Resources.icoTrafficLightGreen
            btnStartNameChk.Enabled = True
        Else
            'Label1.Text = "NOT READY!!!"
            'picBxReadiness.Image = My.Resources.icoTrafficLightRed
            btnStartNameChk.Enabled = False
        End If

        m_tssLabel.Text = dListSelectorPnl.getStatusString(srtNewStatus)

    End Sub

    Private Sub datalistInputError(ByVal strErrorMsg As String) Handles dListSelectorPnl.inputError
        m_tssLabel.Text = strErrorMsg

        m_tmrErrorDisplay.Enabled = True
    End Sub

    Private Sub btnStartNameChk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartNameChk.Click
        If dListSelectorPnl.getStatus = DataListSelectorPanelStatus.READY Then
            m_tssLabel.Text = "Checking names..."
            m_tsProgBar.Visible = True
            m_tsProgBar.Minimum = 0
            m_tssProgLbl.Visible = True

            dNamesGridView.addDataNames(dListSelectorPnl.DataListConnection, _
                                            dListSelectorPnl.DataNameClauseLookup)

            m_tssLabel.Text = "Complete"
            'm_tsProgBar.Visible = False
            'm_tssProgLbl.Visible = False
            m_tmrErrorDisplay.Enabled = True
        End If

    End Sub

    Private Sub updateProgDisplay(ByVal processed As Integer, ByVal total As Integer) Handles dNamesGridView.addRowsProgress
        m_tsProgBar.Maximum = total + 3
        m_tsProgBar.Value = processed

        m_tssProgLbl.Text = String.Format("{0} out of {1} completed", processed, total)

    End Sub

    Public Sub updateArcGISRef(ByRef app As IApplication)
        dListSelectorPnl.setArcGISRef(app)
        m_blnStandAlone = False
    End Sub

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_blnStandAlone = True
    End Sub

    Public Sub New(ByRef app As IApplication)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Me.dListSelectorPnl.setArcGISRef(app)
        m_blnStandAlone = False
    End Sub

    Protected Overrides Sub OnFormClosing(ByVal e As System.Windows.Forms.FormClosingEventArgs)
        MyBase.OnFormClosing(e)

        If Not m_blnStandAlone Then
            e.Cancel = True
            Me.Hide()
        End If

    End Sub

    Protected Overrides Sub OnShown(ByVal e As System.EventArgs)
        MyBase.OnShown(e)

        m_tssLabel.Text = dListSelectorPnl.getStatusString(dListSelectorPnl.getStatus)
    End Sub

End Class
