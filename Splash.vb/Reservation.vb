﻿Imports System.Data.SqlClient

Public Class Reservation
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\LogisticVbDb.mdf;Integrated Security=True;Connect Timeout=30")

    Private Sub FillClient()
        Try
            Con.Open()
            Dim cmd As New SqlCommand("SELECT * FROM ClientTbl", Con)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim Tbl As New DataTable()
            adapter.Fill(Tbl)
            ClientId.DataSource = Tbl
            ClientId.DisplayMember = "ClId" ' Corrected property name
            ClientId.ValueMember = "ClId"
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            Con.Close()
        End Try
    End Sub
    Private Sub FillRoom()
        Try
            Dim Status = "Free"
            Con.Open()
            Dim cmd As New SqlCommand("SELECT * FROM RoomTbl WHERE RStatus = @Status", Con)
            cmd.Parameters.AddWithValue("@Status", Status)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim Tbl As New DataTable()
            adapter.Fill(Tbl)
            RoomNumberCb.DataSource = Tbl
            RoomNumberCb.DisplayMember = "RId" ' Corrected property name
            RoomNumberCb.ValueMember = "RId"
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            Con.Close()
        End Try
    End Sub
    Private Sub populate()
        Try
            Con.Open()
            Dim sql As String = "SELECT * FROM ReservationTbl"
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(sql, Con)
            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            ReservationDGV.DataSource = ds.Tables(0)
            Con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Dim key = 0

    Private Sub Reservation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FillClient()
        FillRoom()
        populate()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim Cl As New Client
        Cl.Show()
        Me.Hide()
    End Sub
    Private Sub GetName()
        Con.Open()
        Con.Close()
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim room As New Room
        room.Show()
        Dim query = "SELECT * FROM ClientTbl WHERE ClId=" & ClientId.SelectedValue.ToString()
        Dim cmd As New SqlCommand(query, Con)
        Dim dt As New DataTable()
        Dim reader As SqlDataReader = Nothing

        Try
            Con.Open()
            reader = cmd.ExecuteReader()

            If reader.Read() Then
                ClientNameTb.Text = reader(1).ToString()
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If reader IsNot Nothing Then
                reader.Close()
            End If

            Con.Close()
        End Try

        Me.Hide()
    End Sub

    Private Sub ClientId_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ClientId.SelectionChangeCommitted
        GetName()
    End Sub
    Private Sub UpdateRoom()
        Try
            Dim Status As String = "Booked"
            Con.Open()
            Dim query As String = "UPDATE RoomTbl SET RStatus = @Status WHERE RId = @RId"
            Dim cmd As SqlCommand = New SqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@Status", Status)
            cmd.Parameters.AddWithValue("@RId", RoomNumberCb.SelectedValue.ToString())
            cmd.ExecuteNonQuery()
            MsgBox("Room Updated Successfully")
            Con.Close()
            'populate()
            'Clear()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ClientId.SelectedIndex = -1 Or RoomNumberCb.SelectedIndex = -1 Then
            MsgBox("Missing Information")
        Else
            Try
                Con.Open()
                Dim query As String
                query = "INSERT INTO ReservationTbl (ClId, ClientName, DateIn, DateOut) VALUES (@ClId, @ClientName, @DateIn, @DateOut)"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.Parameters.AddWithValue("@ClId", ClientId.SelectedValue.ToString())
                cmd.Parameters.AddWithValue("@ClientName", ClientNameTb.Text)
                cmd.Parameters.AddWithValue("@DateIn", DateIn.Value)
                cmd.Parameters.AddWithValue("@DateOut", DateOut.Value)
                cmd.ExecuteNonQuery()
                MsgBox("Room booked successfully")
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            Finally
                Con.Close()
                populate()
                UpdateRoom()
            End Try
        End If
    End Sub
End Class
