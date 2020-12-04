Imports System.Data.SqlClient

Public Class Form1

    Dim connection As New SqlConnection(My.Settings.connectiontoSQL)
    Dim id As String = ""
    Dim column As String = ""
    Dim querySQL As String = ""
    Dim cmd As New SqlCommand()
    Dim dAdapter As New SqlDataAdapter()
    Dim dSet As New DataSet()
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.querySQL = "select * from gestores_bd"
        Me.cmd = New SqlCommand(querySQL, connection)
        Try
            Me.dAdapter = New SqlDataAdapter(cmd)
            Me.dSet = New DataSet
            dAdapter.Fill(dSet, "gestores_bd")
            Me.dgv_prueba.DataSource = dSet.Tables("gestores_bd")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dgv_prueba_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_prueba.CellEndEdit
        If (Me.dgv_prueba.CurrentRow.IsNewRow = False) Then
            Me.connection.Open()
            Me.querySQL = "UPDATE gestores_bd SET " + Me.column + "=" & "'" + Me.dgv_prueba(e.ColumnIndex, e.RowIndex).Value.ToString() & "'" + " WHERE id=" + Me.id
            MessageBox.Show(Me.querySQL)
            Me.cmd = New SqlCommand(querySQL, Me.connection)
            Try
                Dim cant As Integer = cmd.ExecuteNonQuery()
                If cant > 0 Then
                    MessageBox.Show("Se han actualizado el registro")
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Me.connection.Close()
        End If
    End Sub

    Private Sub dgv_prueba_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_prueba.CellClick
        Me.column = Me.dgv_prueba.Columns(Me.dgv_prueba.CurrentCell.ColumnIndex).Name.ToString()
    End Sub

    Private Sub dgv_prueba_UserDeletedRow(sender As Object, e As DataGridViewRowEventArgs) Handles dgv_prueba.UserDeletedRow
        Me.connection.Open()
        Me.querySQL = "DELETE FROM gestores_bd WHERE id =" + Me.id
        MessageBox.Show(Me.querySQL)
        Me.cmd = New SqlCommand(querySQL, Me.connection)
        Try
            Dim cant As Integer = cmd.ExecuteNonQuery()
            If cant > 0 Then
                MessageBox.Show("Se ha eliminado el registro")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Me.connection.Close()
    End Sub


    Private Sub dgv_prueba_SelectionChanged(sender As Object, e As EventArgs) Handles dgv_prueba.SelectionChanged
        If (Me.dgv_prueba.CurrentRow.IsNewRow() = False) Then
            Me.id = Me.dgv_prueba.CurrentRow.DataBoundItem("id")
        End If
    End Sub
End Class
