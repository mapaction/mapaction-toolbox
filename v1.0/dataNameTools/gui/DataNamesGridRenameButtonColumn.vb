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

Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles

Public Class DataNamesGridRenameButtonColumn
    Inherits DataGridViewButtonColumn

    Public Sub New()
        Me.CellTemplate = New DataNamesGridRenameButtonCell()
    End Sub

End Class

Public Class DataNamesGridRenameButtonCell
    Inherits DataGridViewButtonCell

    Private m_blnEnabled As Boolean

    Friend Property allowRename() As Boolean
        Get
            Return m_blnEnabled
        End Get
        Set(ByVal value As Boolean)
            m_blnEnabled = value
        End Set
    End Property

    Public Sub New()
        Me.allowRename = True
    End Sub

    Public Overrides Function Clone() As Object
        Dim cellResult As DataNamesGridRenameButtonCell

        cellResult = CType(MyBase.Clone(), DataNamesGridRenameButtonCell)
        cellResult.allowRename = m_blnEnabled
        Return cellResult
    End Function

    Protected Overrides Sub Paint(ByVal paintGraphics As Graphics, _
                                ByVal rectClipBounds As Rectangle, _
                                ByVal rectCellBounds As Rectangle, _
                                ByVal intRowIndex As Integer, _
                                ByVal dgvElementState As DataGridViewElementStates, _
                                ByVal objValue As Object, _
                                ByVal objFormattedValue As Object, _
                                ByVal strErrorText As String, _
                                ByVal dgvCellStyle As DataGridViewCellStyle, _
                                ByVal dgvAdvBorderStyle As DataGridViewAdvancedBorderStyle, _
                                ByVal dgvPaintParts As DataGridViewPaintParts)


        'Since the button is enabled then the normal base button paint method is useable
        If m_blnEnabled Then
            MyBase.Paint(paintGraphics, rectClipBounds, rectCellBounds, intRowIndex, _
                        dgvElementState, objValue, objFormattedValue, strErrorText, _
                        dgvCellStyle, dgvAdvBorderStyle, dgvPaintParts)
        Else
            'We have to draw the cell ourselves

            'Background
            If (dgvPaintParts And DataGridViewPaintParts.Background) = DataGridViewPaintParts.Background Then

                Dim brushCellBackground As New SolidBrush(dgvCellStyle.BackColor)
                paintGraphics.FillRectangle(brushCellBackground, rectCellBounds)
                brushCellBackground.Dispose()
            End If

            'Borders
            If (dgvPaintParts And DataGridViewPaintParts.Border) = DataGridViewPaintParts.Border Then
                PaintBorder(paintGraphics, rectClipBounds, rectCellBounds, dgvCellStyle, dgvAdvBorderStyle)
            End If

            'Calculate the size of the button not including the borders of teh cell
            Dim buttonArea As Rectangle = rectCellBounds
            Dim buttonAdjustment As Rectangle = Me.BorderWidths(dgvAdvBorderStyle)
            buttonArea.X += buttonAdjustment.X
            buttonArea.Y += buttonAdjustment.Y
            buttonArea.Height -= buttonAdjustment.Height
            buttonArea.Width -= buttonAdjustment.Width

            'Draw the disabled button.                
            ButtonRenderer.DrawButton(paintGraphics, buttonArea, PushButtonState.Disabled)

            'Text label
            If TypeOf Me.FormattedValue Is String Then
                TextRenderer.DrawText(paintGraphics, CStr(Me.FormattedValue), _
                                        Me.DataGridView.Font, buttonArea, SystemColors.GrayText)
            End If

        End If

    End Sub

End Class
