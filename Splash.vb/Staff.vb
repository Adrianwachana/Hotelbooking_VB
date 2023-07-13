Imports System.Data.SqlClient
Public Class Staff
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\LogisticVbDb.mdf;Integrated Security=True;Connect Timeout=30")

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If StaffNameTb.Text = "" Or Password.Text = "" Or GenCb.SelectedIndex = -1 Or StaffPhoneTB.Text = "" Then
            MsgBox("Missing Information")
        Else
            Try
                Con.Open()
                Dim query As String = "INSERT INTO StaffTbl VALUES (@StaffName, @Gender, @Password, @StaffPhone)"
                Dim cmd As SqlCommand = New SqlCommand(query, Con)
                cmd.Parameters.AddWithValue("@StaffName", StaffNameTb.Text)
                cmd.Parameters.AddWithValue("@Gender", GenCb.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@Password", Password.Text)
                cmd.Parameters.AddWithValue("@StaffPhone", StaffPhoneTB.Text)
                cmd.ExecuteNonQuery()
                MsgBox("Staff Inserted Successfully")
                Con.Close()
                Populate()
                Clear()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    ' You need to define the Populate and Clear methods to complete the code
    Private Sub Populate()
        Try
            Con.Open()
            Dim sql As String = "SELECT * FROM StaffTbl"
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(sql, Con)
            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            AgentsDGV.DataSource = ds.Tables(0)
            Con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Dim key = 0
    Private Sub Clear()
        StaffNameTb.Clear()
        GenCb.SelectedIndex = -1
        Password.Clear()
        StaffPhoneTB.Clear()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Clear()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If key = 0 Then
            MsgBox("Missing Information")
        Else
            Try
                Con.Open()
                Dim query As String = "DELETE FROM StaffTbl WHERE StaffId = " & key & ""
                Dim cmd As SqlCommand = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Agent Deleted Successfully")
                Con.Close()
                Populate()
                Clear()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If key = 0 Then
            MsgBox("Missing Information")
        Else
            Try
                Con.Open()
                Dim query As String = "UPDATE StaffTbl SET StaffName = @StaffName, Gender = @Gender, Password = @Password, StaffPhone = @StaffPhone WHERE StaffId = @Key"
                Dim cmd As SqlCommand = New SqlCommand(query, Con)
                cmd.Parameters.AddWithValue("@StaffName", StaffNameTb.Text)
                cmd.Parameters.AddWithValue("@Gender", GenCb.SelectedItem.ToString())
                cmd.Parameters.AddWithValue("@Password", Password.Text)
                cmd.Parameters.AddWithValue("@StaffPhone", StaffPhoneTB.Text)
                cmd.Parameters.AddWithValue("@Key", key)
                cmd.ExecuteNonQuery()
                MsgBox("Staff Updated Successfully")
                Con.Close()
                Populate()
                Clear()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub AgentDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles AgentsDGV.CellMouseClick
        Dim row As DataGridViewRow = AgentsDGV.Rows(e.RowIndex)
        StaffNameTb.Text = row.Cells(1).Value.ToString()
        GenCb.SelectedItem = row.Cells(3).Value.ToString()
        Password.Text = row.Cells(3).Value.ToString()
        StaffPhoneTB.Text = row.Cells(2).Value.ToString()

        If StaffNameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(4).Value.ToString())
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim log As New Login
        log.Show()
        Me.Hide()
    End Sub
End Class

