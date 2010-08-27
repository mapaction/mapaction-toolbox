Imports mapaction.datanames.api
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles


Public Class DataNamesGridView

    Private m_icoValid As Icon = Drawing.Icon.FromHandle(My.Resources.icoOK.GetHicon)
    Private m_icoWarning As Icon = Drawing.Icon.FromHandle(My.Resources.icoWarning.GetHicon)
    Private m_icoError As Icon = Drawing.Icon.FromHandle(My.Resources.icoError.GetHicon)

    Private m_intIdxName As Integer = 0
    Private m_intIdxIco As Integer = 1
    Private m_intIdxButtn As Integer = 2
    Private m_intIdxComments As Integer = 3
    Private m_intIdxPath As Integer = 4

    Public Event addRowsProgress(ByVal processed As Integer, ByVal total As Integer)

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Dim column0 As New DataGridViewCheckBoxColumn()
        'column0.Name = "CheckBoxes"

        Dim column1 As New DataGridViewDisableButtonColumn()
        column1.Name = "Rename"

        datGV.Columns.Insert(m_intIdxButtn, column1)

        For i = 0 To (datGV.Columns.Count - 1)
            System.Console.WriteLine(datGV.Columns.Item(i).Name & " : " & datGV.Columns.Item(i).Index)
        Next

    End Sub

    Public Sub clearGrid()
        datGV.Rows.Clear()
    End Sub

    Public Sub addDataNames(ByRef dnList As IDataListConnection, ByRef dncl As IDataNameClauseLookup)
        Dim intTotal As Integer
        Dim intCnt As Integer = 1

        clearGrid()

        intTotal = dnList.getLayerDataNamesList(dncl).Count

        For Each dn In dnList.getLayerDataNamesList(dncl)
            addRow(dn)
            RaiseEvent addRowsProgress(intCnt, intTotal)
            intCnt = intCnt + 1
        Next

    End Sub

    Private Sub addRow(ByRef dName As IDataName)
        Dim lngNameStatus As Long
        Dim strStatusMsg As String
        Dim intLineCnt As Integer

        Dim rDGVRow As New DataGridViewRow  'Rows to be added to the datagrid view. I dont like Dim here - but it's the correct way
        rDGVRow.CreateCells(datGV)

        'Get Name status
        lngNameStatus = dName.checkNameStatus()

        'Start adding each column in

        'Add Name
        rDGVRow.Cells.Item(m_intIdxName).Value = dName.getNameStr()

        'Add Icon
        If ((lngNameStatus And dnNameStatus.INVALID) = dnNameStatus.INVALID) Or _
             ((lngNameStatus And dnNameStatus.SYNTAX_ERROR) = dnNameStatus.SYNTAX_ERROR) Then
            '.Cells(3).Value = True  'Tick the error box
            rDGVRow.Cells.Item(m_intIdxIco).Value = m_icoError
        ElseIf (lngNameStatus And dnNameStatus.WARN) = dnNameStatus.WARN Then
            '.Cells(2).Value = True  'tick the Warning box
            rDGVRow.Cells.Item(m_intIdxIco).Value = m_icoWarning
        Else
            '.Cells(1).Value = True  'tick the Valid box
            rDGVRow.Cells.Item(m_intIdxIco).Value = m_icoValid
        End If

        'Add Button column
        Dim buttonCell As New DataGridViewDisableButtonCell
        buttonCell.Enabled = dName.isRenameable()
        buttonCell.Value = "Rename..."

        rDGVRow.Cells.Item(m_intIdxButtn) = buttonCell

        'Add Comments column
        strStatusMsg = DataNameStringFormater.getDataNamingStatusMessage(lngNameStatus)
        intLineCnt = 1 + strStatusMsg.Split(CChar(vbNewLine)).Length

        rDGVRow.Cells.Item(m_intIdxComments).Value = strStatusMsg
        rDGVRow.Cells.Item(m_intIdxComments).Style.WrapMode = DataGridViewTriState.True
        'If intLineCnt > 1 Then
        '    rDGVRow.Height = CInt(datGV.RowTemplate.Height * intLineCnt * 0.9)
        'End If

        rDGVRow.Height = 5 + (10 * intLineCnt)

        'For Each statusStr In statusList
        '    If (Not statusStr Is Nothing) Or (Not statusStr = "") Then
        '        If tempCommentsStr = "" Then
        '            tempCommentsStr = statusStr
        '            lineCnt = 1
        '        Else
        '            tempCommentsStr = tempCommentsStr & vbNewLine & statusStr
        '            lineCnt = lineCnt + 1
        '        End If
        '    End If
        'Next


        'Add Path
        rDGVRow.Cells.Item(m_intIdxPath).Value = dName.getPathStr

        'Now add the completed row
        datGV.Rows.Add(rDGVRow)
    End Sub


    ' This event handler manually raises the CellValueChanged event
    ' by calling the CommitEdit method.
    Sub datGV_CurrentCellDirtyStateChanged( _
        ByVal sender As Object, ByVal e As EventArgs) _
        Handles datGV.CurrentCellDirtyStateChanged

        If datGV.IsCurrentCellDirty Then
            datGV.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub


    ' If the user clicks on an enabled button cell, this event handler  
    ' reports that the button is enabled.
    Sub datGV_CellClick(ByVal sender As Object, _
        ByVal e As DataGridViewCellEventArgs) _
        Handles datGV.CellClick

        If e.ColumnIndex = m_intIdxButtn Then

            Dim buttonCell As DataGridViewDisableButtonCell = _
                CType(datGV.Rows(e.RowIndex).Cells(m_intIdxButtn), DataGridViewDisableButtonCell)
            If buttonCell.Enabled Then
                'datGV.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString() & " is enabled: " & buttonCell.Enabled)
                MsgBox("Renaming not yet implenmented")
            End If
        End If
    End Sub


End Class

Public Class DataGridViewDisableButtonColumn
    Inherits DataGridViewButtonColumn

    Public Sub New()
        Me.CellTemplate = New DataGridViewDisableButtonCell()
    End Sub
End Class

Public Class DataGridViewDisableButtonCell
    Inherits DataGridViewButtonCell

    Private enabledValue As Boolean
    Public Property Enabled() As Boolean
        Get
            Return enabledValue
        End Get
        Set(ByVal value As Boolean)
            enabledValue = value
        End Set
    End Property

    ' Override the Clone method so that the Enabled property is copied.
    Public Overrides Function Clone() As Object
        Dim Cell As DataGridViewDisableButtonCell = _
            CType(MyBase.Clone(), DataGridViewDisableButtonCell)
        Cell.Enabled = Me.Enabled
        Return Cell
    End Function

    ' By default, enable the button cell.
    Public Sub New()
        Me.enabledValue = True
    End Sub

    Protected Overrides Sub Paint(ByVal graphics As Graphics, _
        ByVal clipBounds As Rectangle, ByVal cellBounds As Rectangle, _
        ByVal rowIndex As Integer, _
        ByVal elementState As DataGridViewElementStates, _
        ByVal value As Object, ByVal formattedValue As Object, _
        ByVal errorText As String, _
        ByVal cellStyle As DataGridViewCellStyle, _
        ByVal advancedBorderStyle As DataGridViewAdvancedBorderStyle, _
        ByVal paintParts As DataGridViewPaintParts)

        ' The button cell is disabled, so paint the border,  
        ' background, and disabled button for the cell.
        If Not Me.enabledValue Then

            ' Draw the background of the cell, if specified.
            If (paintParts And DataGridViewPaintParts.Background) = _
                DataGridViewPaintParts.Background Then

                Dim cellBackground As New SolidBrush(cellStyle.BackColor)
                graphics.FillRectangle(cellBackground, cellBounds)
                cellBackground.Dispose()
            End If

            ' Draw the cell borders, if specified.
            If (paintParts And DataGridViewPaintParts.Border) = _
                DataGridViewPaintParts.Border Then

                PaintBorder(graphics, clipBounds, cellBounds, cellStyle, _
                    advancedBorderStyle)
            End If

            ' Calculate the area in which to draw the button.
            Dim buttonArea As Rectangle = cellBounds
            Dim buttonAdjustment As Rectangle = _
                Me.BorderWidths(advancedBorderStyle)
            buttonArea.X += buttonAdjustment.X
            buttonArea.Y += buttonAdjustment.Y
            buttonArea.Height -= buttonAdjustment.Height
            buttonArea.Width -= buttonAdjustment.Width

            ' Draw the disabled button.                
            ButtonRenderer.DrawButton(graphics, buttonArea, _
                PushButtonState.Disabled)

            ' Draw the disabled button text. 
            If TypeOf Me.FormattedValue Is String Then
                TextRenderer.DrawText(graphics, CStr(Me.FormattedValue), _
                    Me.DataGridView.Font, buttonArea, SystemColors.GrayText)
            End If

        Else
            ' The button cell is enabled, so let the base class 
            ' handle the painting.
            MyBase.Paint(graphics, clipBounds, cellBounds, rowIndex, _
                elementState, value, formattedValue, errorText, _
                cellStyle, advancedBorderStyle, paintParts)
        End If
    End Sub

End Class

