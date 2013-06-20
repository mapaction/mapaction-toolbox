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

Imports ESRI.ArcGIS.Framework

Public Class frmMain_origional

    Private m_blnStandAlone As Boolean

    Private Sub readinessHasChanged(ByVal blnNewVal As Boolean) Handles dListSelectorPnl.ReadinessChanged

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
