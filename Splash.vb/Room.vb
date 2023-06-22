Imports System.Data.SqlClient
Public Class Room
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\LogisticVbDb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If RoomPhone.Text = "" Or RoomStatus.SelectedIndex = -1 Then
            MsgBox("Missing Information")
        Else
            Try
                Con.Open()
                Dim query As String = "INSERT INTO RoomTbl VALUES ('" & RoomPhone.Text & "', '" & RoomStatus.SelectedItem.ToString() & "')"
                Dim cmd As SqlCommand = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Client Inserted Successfully")
                Con.Close()
                populate()
                Clear()
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                Con.Close()
            End Try
        End If
    End Sub
    Private Sub populate()
        Try
            Con.Open()
            Dim sql As String = "SELECT * FROM RoomTbl"
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(sql, Con)
            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            RoomDGV.DataSource = ds.Tables(0)
            Con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Dim key = 0
    Private Sub Clear()
        RoomPhone.Clear()
        RoomStatus.SelectedIndex = -1
    End Sub

    Private Sub Room_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        populate()
    End Sub
    ''Dim key = 0
    Private Sub RoomDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles RoomDGV.CellMouseClick
        Dim row As DataGridViewRow = RoomDGV.Rows(e.RowIndex)
        RoomPhone.Text = row.Cells(1).Value.ToString()
        RoomStatus.SelectedItem = row.Cells(2).Value.ToString()

        If RoomPhone.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString())
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        clear()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If key = 0 Then
            MsgBox("Missing Information")
        Else
            Try
                Con.Open()
                Dim query As String
                query = "DELETE FROM RoomTbl WHERE RId = @Key"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.Parameters.AddWithValue("@Key", key)
                cmd.ExecuteNonQuery()
                MsgBox("Room Deleted Successfully")
                Con.Close()
                populate()
                Clear()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If RoomPhone.Text = "" Or RoomStatus.Text = "" Then
            MsgBox("Missing Information")
        Else
            Try
                Con.Open()
                Dim query As String = "UPDATE RoomTbl SET RPhone = '" & RoomPhone.Text & "', RStatus = '" & RoomStatus.SelectedItem.ToString() & "' WHERE RId = " & key & ""
                Dim cmd As SqlCommand = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Room Updated Successfully")
                Con.Close()
                populate()
                Clear()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim res As New Reservation
        res.Show()
        Me.Hide()
    End Sub
End Class


