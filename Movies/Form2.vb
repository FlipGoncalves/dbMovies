Imports System.Data.SqlClient
Public Class Form2
    Dim CMD As SqlCommand
    Dim CN As SqlConnection = New SqlConnection("Data Source = tcp:mednat.ieeta.pt\SQLSERVER,8101; Initial Catalog = p5g4; uid = p5g4; password = Pokemon123!")

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim user As String = TextBox1.Text
        Dim pass As String = TextBox2.Text
        Dim bool As Integer
        CMD = New SqlCommand
        CMD.Connection = CN
        CMD.CommandText = "ValidationUser"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@user", user)
        CMD.Parameters.AddWithValue("@pass", pass)
        Dim PAR As SqlParameter = New SqlParameter("@RETURN_VALUE", SqlDbType.Int)
        CMD.Parameters.Add(PAR).Direction = ParameterDirection.ReturnValue
        CMD.CommandType = CommandType.StoredProcedure
        Try
            CN.Open()
            CMD.ExecuteNonQuery()
            bool = Convert.ToInt32(PAR.Value)
        Catch EXC As Exception
            MsgBox(EXC.ToString)
        End Try
        CN.Close()
        If bool = 1 Then
            If ComboBox1.SelectedIndex = 1 Then
                Me.Hide()
                Form4.Tag = "edit"
                Form4.Show()
            Else
                Me.Hide()
                ActorView.Tag = "edit"
                ActorView.Show()
            End If
        Else
            MsgBox("no entry allowed")
        End If

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.SelectedIndex = 1 Then
            Me.Hide()
            Form4.Tag = "view"
            Form4.Show()
        Else
            Me.Hide()
            ActorView.Tag = "view"
            ActorView.Show()
        End If
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0
    End Sub
End Class