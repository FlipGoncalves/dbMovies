Imports System.Data.SqlClient
Public Class Form3
    Dim CMD As SqlCommand
    Dim CN As SqlConnection
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim filmenome = Me.Tag.ToString.Split(",").ElementAt(0)
        Dim studionome = Me.Tag.ToString.Split(",").ElementAt(1)
        CN = New SqlConnection("Data Source = tcp:mednat.ieeta.pt\SQLSERVER,8101; Initial Catalog = p5g4; uid = p5g4; password = Pokemon123!")
        CMD = New SqlCommand
        CMD.Connection = CN
        CMD.CommandText = "filme_geral"
        CMD.CommandType = CommandType.StoredProcedure
        CMD.Parameters.Add("@nomefilme", SqlDbType.VarChar, 256).Value = filmenome
        CMD.Parameters.Add("@nomestudio", SqlDbType.VarChar, 256).Value = studionome

        CMD.Parameters.Add("@ano", SqlDbType.Date)
        CMD.Parameters("@ano").Direction = ParameterDirection.Output
        CMD.Parameters.Add("@dur", SqlDbType.Int)
        CMD.Parameters("@dur").Direction = ParameterDirection.Output
        CMD.Parameters.Add("@publico", SqlDbType.VarChar, 256)
        CMD.Parameters("@publico").Direction = ParameterDirection.Output
        CMD.Parameters.Add("@ganho", SqlDbType.Int)
        CMD.Parameters("@ganho").Direction = ParameterDirection.Output
        CMD.Parameters.Add("@rank", SqlDbType.Int)
        CMD.Parameters("@rank").Direction = ParameterDirection.Output
        CN.Open()
        CMD.ExecuteNonQuery()
        CN.Close()
        Dim Filme As New Movie
        Filme.studio = studionome
        Filme.filme = filmenome
        MsgBox(Convert.ToString(CMD.Parameters("@publico").Value))
        Filme.id_publico = Convert.ToString(CMD.Parameters("@publico").Value)
        Filme.dur = Convert.ToString(CMD.Parameters("@dur").Value)
        Filme.rank = Convert.ToString(CMD.Parameters("@rank").Value)
        Filme.data = Convert.ToString(CMD.Parameters("@ano").Value)
        Filme.ganh = Convert.ToString(CMD.Parameters("@ganho").Value)
        TextBox1.Text = filmenome + ", " + studionome
        TextBox2.Text = Filme.data
        TextBox3.Text = Filme.dur
        TextBox4.Text = Filme.id_publico
        TextBox5.Text = Filme.ganh
        TextBox7.Text = Filme.rank
    End Sub
End Class