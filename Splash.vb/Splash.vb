﻿Public Class Splash
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Percentage.Click

    End Sub

    Private Sub Splash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Myprogress.Increment(1)
        Percentage.Text = Convert.ToString(Myprogress.Value) + "%"
        If Myprogress.Value = 100 Then
            Me.Hide()
            Dim log = New Login
            log.Show()
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub Myprogress_Click(sender As Object, e As EventArgs) Handles Myprogress.Click

    End Sub
End Class
