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

    'Private m_lstDataNames As List(Of IDataName)
    Private m_dicDataNames As Dictionary(Of DataGridViewRow, IDataName)

    Private m_dnRenameDialog As DataRenameDialog = New DataRenameDialog

    Public Event addRowsProgress(ByVal processed As Integer, ByVal total As Integer)

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Dim column0 As New DataGridViewCheckBoxColumn()
        'column0.Name = "CheckBoxes"

        Dim colRenameBtns As New DataNamesGridRenameButtonColumn()
        colRenameBtns.Name = "Rename"
        colRenameBtns.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        colRenameBtns.DefaultCellStyle.BackColor = SystemColors.ButtonFace

        datGV.Columns.Insert(m_intIdxButtn, colRenameBtns)

        For i = 0 To (datGV.Columns.Count - 1)
            System.Console.WriteLine(datGV.Columns.Item(i).Name & " : " & datGV.Columns.Item(i).Index)
        Next
    End Sub

    Public Sub clearGrid()
        datGV.Rows.Clear()
        If m_dicDataNames IsNot Nothing Then
            For Each dName In m_dicDataNames.Values
                RemoveHandler dName.NameChanged, AddressOf dataNameChangeHandler
            Next
        End If

        'm_lstDataNames = New List(Of IDataName)
        m_dicDataNames = New Dictionary(Of DataGridViewRow, IDataName)()
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
        Dim rDGVRow As New DataGridViewRow  'Rows to be added to the datagrid view. I dont like Dim here - but it's the correct way
        rDGVRow.CreateCells(datGV)

        'Add the dataName to our list
        'm_lstDataNames.Add(dName)
        m_dicDataNames.Add(rDGVRow, dName)

        'add as rename listener
        AddHandler dName.NameChanged, AddressOf dataNameChangeHandler

        'update the contents of the row
        updateRow(rDGVRow, dName)

        'Now add the completed row
        datGV.Rows.Add(rDGVRow)
    End Sub

    Private Sub updateRow(ByRef rDGVRow As DataGridViewRow, ByRef dName As IDataName)
        Dim lngNameStatus As Long
        Dim strStatusMsg As String
        Dim intLineCnt As Integer

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
        Dim buttonCell As New DataNamesGridRenameButtonCell
        buttonCell.allowRename = dName.isRenameable()
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

        'rDGVRow.Height = 5 + (10 * intLineCnt)

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
    End Sub

    Private Sub dataNameChangeHandler(ByVal strOldName As String, ByRef dnRenamed As IDataName)
        'updateRow(datGV.Rows(m_lstDataNames.IndexOf(dnRenamed)), dnRenamed)
        'updateRow(datGV.Rows(m_dicDataNames.Keys.
        'updateRow(m_dicDataNames.GetO

        For Each pair In m_dicDataNames
            'KeyValuePair(Of DataGridViewRow, IDataName)
            If dnRenamed.Equals(pair.Value) Then
                updateRow(pair.Key, dnRenamed)
            End If
        Next
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

        If ((e.ColumnIndex = m_intIdxButtn) And (e.RowIndex >= 0)) Then

            Dim buttonCell As DataNamesGridRenameButtonCell = _
                CType(datGV.Rows(e.RowIndex).Cells(m_intIdxButtn), DataNamesGridRenameButtonCell)
            If buttonCell.allowRename Then
                'datGV.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString() & " is enabled: " & buttonCell.Enabled)
                'm_dnRenameDialog.dataName = m_lstDataNames.Item(e.RowIndex)
                m_dnRenameDialog.dataName = m_dicDataNames.Item(datGV.Rows.Item(e.RowIndex))
                m_dnRenameDialog.ShowDialog()
                'MsgBox("Renaming not yet implenmented")
            End If
        End If
    End Sub


End Class
