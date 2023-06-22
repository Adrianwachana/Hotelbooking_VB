Imports System.Data.SqlClient
Public Class Client
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\LogisticVbDb.mdf;Integrated Security=True;Connect Timeout=30
")
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles ClAge.TextChanged

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ClNameTb.Text = "" Or ClAge.Text = "" Or ClPhoneTb.Text = "" Or GenCB.SelectedIndex = -1 Or CountryCb.SelectedIndex = -1 Then
            MsgBox("Missing Information")
        Else
            Try
                Con.Open()
                Dim query As String
                query = "INSERT INTO ClientTbl VALUES ('" & ClNameTb.Text & "', '" & GenCB.SelectedItem.ToString() & "', '" & ClAge.Text & "', '" & ClPhoneTb.Text & "', '" & CountryCb.SelectedItem.ToString() & "')"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Client Inserted Successfully")
                Con.Close()
                Populate()
                Clear()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Private Sub populate()
        Try
            Con.Open()
            Dim sql As String = "SELECT * FROM ClientTbl"
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(sql, Con)
            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            ClientDGV.DataSource = ds.Tables(0)
            Con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Dim key = 0

    Private Sub Clear()
        ClNameTb.Clear()
        GenCB.SelectedIndex = -1
        ClAge.Clear()
        ClPhoneTb.Clear()
        CountryCb.SelectedIndex = -1
    End Sub
    Private Sub Client_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        populate()
    End Sub
    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Clear()
    End Sub
    Private Sub ClientDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles ClientDGV.CellMouseClick
        Dim row As DataGridViewRow = ClientDGV.Rows(e.RowIndex)
        ClNameTb.Text = row.Cells(1).Value.ToString()
        GenCB.SelectedItem = row.Cells(2).Value.ToString()
        ClAge.Text = row.Cells(3).Value.ToString()
        CountryCb.SelectedItem = row.Cells(5).Value.ToString()
        If ClNameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString())
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If key = 0 Then
            MsgBox("Missing Information")
        Else
            Try
                Con.Open()
                Dim query As String
                query = "DELETE FROM ClientTbl WHERE ClId = @Key"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.Parameters.AddWithValue("@Key", key)
                cmd.ExecuteNonQuery()
                MsgBox("Client Deleted Successfully")
                Con.Close()
                populate()
                Clear()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ClNameTb.Text = "" Or ClAge.Text = "" Or ClPhoneTb.Text = "" Or GenCB.SelectedIndex = -1 Or CountryCb.SelectedIndex = -1 Or key = 0 Then
            MsgBox("Missing Information")
        Else
            Try
                Con.Open()
                Dim query As String
                query = "UPDATE ClientTbl SET ClName = '" & ClNameTb.Text & "', ClGender = '" & GenCB.SelectedItem.ToString() & "', ClAge = " & ClAge.Text & ", ClPhone = '" & ClPhoneTb.Text & "', ClCountry = '" & CountryCb.SelectedItem.ToString() & "' WHERE ClId = " & key & ""
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Client Updated Successfully")
                Con.Close()
                populate()
                Clear()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub ClNameTb_TextChanged(sender As Object, e As EventArgs) Handles ClNameTb.TextChanged

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim Res As New Reservation
        Res.Show()
        Me.Hide()
    End Sub
End Class