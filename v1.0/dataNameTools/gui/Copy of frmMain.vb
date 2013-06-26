Imports ESRI.ArcGIS.Framework

Public Class frmMain

    Private m_blnStandAlone As Boolean

    Private Sub readinessHasChanged(ByVal blnNewVal As Boolean)

        If blnNewVal Then
            'Label1.Text = "READY!!!"
            picBxReadiness.Image = My.Resources.icoTrafficLightGreen
            btnStartNameChk.Enabled = True
        Else
            'Label1.Text = "NOT READY!!!"
            picBxReadiness.Image = My.Resources.icoTrafficLightRed
            btnStartNameChk.Enabled = False
        End If

    End Sub

    Private Sub btnStartNameChk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartNameChk.Click
        If dListSelectorPnl.isReady Then
            dNamesGridView.addDataNames(dListSelectorPnl.DataListConnection, _
                                            dListSelectorPnl.DataNameClauseLookup)
        End If

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



End Class
