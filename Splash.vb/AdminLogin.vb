Public Class AdminLogin
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim log As New Login
        log.Show()
        Me.Hide()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Application.Exit()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If PasswordTbl.Text = "Admin" Then
            Dim staff As New Staff
            staff.Show()
            Me.Hide()
        Else
            MsgBox("Wrong password.Contact your Admin")
        End If
    End Sub
End Class