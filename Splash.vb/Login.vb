Imports System.Data.SqlClient
Public Class Login
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\LogisticVbDb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Dim Adm As New AdminLogin
        Adm.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If UidTb.Text = "" Or PasswordTb.Text = "" Then
            MsgBox("Enter the staff Name and Password")
        Else
            Try
                Con.Open()
                Dim query As String = "SELECT * FROM staffTbl WHERE StName = @StName"
                Dim cmd As SqlCommand = New SqlCommand(query, Con)
                cmd.Parameters.AddWithValue("@StName", UidTb.Text)

                Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
                Dim ds As DataSet = New DataSet()
                da.Fill(ds)

                Dim rowCount As Integer = ds.Tables(0).Rows.Count

                If rowCount = 0 Then
                    MsgBox("Wrong username or password")
                Else
                    Dim res As Reservation = New Reservation()
                    res.Show()
                    Me.Hide()
                End If
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            Finally
                Con.Close()
            End Try
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            PasswordTb.PasswordChar = ""
        Else
            PasswordTb.PasswordChar = "*"
        End If
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Application.Exit()
    End Sub
End Class