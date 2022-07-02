Imports System.Data.SqlClient
Public Class Form1
    Dim CMD As SqlCommand
    Dim CN As SqlConnection
    Dim filmes As List(Of Movie) = New List(Of Movie)
    Dim current_movie As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CN = New SqlConnection("Data Source = tcp:mednat.ieeta.pt\SQLSERVER,8101; Initial Catalog = p5g4; uid = p5g4; password = Pokemon123!")
        CMD = New SqlCommand
        CMD.Connection = CN
        CMD.CommandText = "SELECT * FROM Projeto.Filme"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        ListBox1.Items.Clear()
        While RDR.Read
            Dim Filme As New Movie
            Filme.studio = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("StudioNome")), "", RDR.Item("StudioNome")))
            Filme.filme = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("FilmeNome")), "", RDR.Item("FilmeNome")))
            Filme.id_publico = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("ID_Publico_Alvo")), "", RDR.Item("ID_Publico_Alvo")))
            Filme.dur = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Duracao")), "", RDR.Item("Duracao")))
            Filme.rank = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Ranking")), "", RDR.Item("Ranking")))
            Filme.ganh = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Ganho")), "", RDR.Item("Ganho")))
            Filme.data = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Ano")), "", RDR.Item("Ano")))
            ListBox1.Items.Add(Filme)
            filmes.Add(Filme)
        End While
        CN.Close()
        loadgenres()
        ComboBox1.SelectedIndex = 0
    End Sub
    Sub loadgenres()
        CN = New SqlConnection("Data Source = tcp:mednat.ieeta.pt\SQLSERVER,8101; Initial Catalog = p5g4; uid = p5g4; password = Pokemon123!")
        CMD = New SqlCommand
        CMD.Connection = CN
        For i As Integer = 0 To filmes.Count - 1
            Dim Filme As Movie = filmes.ElementAt(i)
            CMD.CommandText = "SELECT * FROM movie_genres ('" & Filme.filme & "')"
            CN.Open()
            Dim RDR As SqlDataReader
            RDR = CMD.ExecuteReader
            While RDR.Read
                Filme.addGenero(Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Genero")), "", RDR.Item("Genero"))))
            End While
            CN.Close()
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CN = New SqlConnection("Data Source = tcp:mednat.ieeta.pt\SQLSERVER,8101; Initial Catalog = p5g4; uid = p5g4; password = Pokemon123!")
        CMD = New SqlCommand
        Select Case ComboBox1.SelectedIndex
            Case 0
                CMD.CommandText = "SELECT * FROM filme_filtro ('" & SearchBarBox.Text & "')"
            Case 1
                CMD.CommandText = "SELECT * FROM filmestudio_filtro ('" & SearchBarBox.Text & "')"
            Case 2 'year
                CMD.CommandText = "SELECT * FROM year_movies ('" & SearchBarBox.Text & "')"
            Case 3
                CMD.CommandText = "SELECT * FROM ator_filtro('" & SearchBarBox.Text & "')"
            Case 4
                CMD.CommandText = "SELECT * FROM genre_movies ('" & SearchBarBox.Text & "')"
            Case Else
                CMD.CommandText = "SELECT * FROM Projeto.Filme"
        End Select
        CMD.Connection = CN
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        ListBox1.Items.Clear()
        filmes.Clear()
        While RDR.Read
            Dim Film As New Movie
            Film.studio = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("StudioNome")), "", RDR.Item("StudioNome")))
            Film.filme = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("FilmeNome")), "", RDR.Item("FilmeNome")))
            Film.id_publico = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Publico Alvo")), "", RDR.Item("Publico Alvo")))
            Film.dur = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Duracao")), "", RDR.Item("Duracao")))
            Film.rank = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Ranking")), "", RDR.Item("Ranking")))
            Film.ganh = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Ganho")), "", RDR.Item("Ganho")))
            Film.data = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Ano")), "", RDR.Item("Ano")))
            ListBox1.Items.Add(Film)
            filmes.Add(Film)
        End While
        CN.Close()
        loadgenres()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Form2.Show()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ListBox1.SelectedIndex > -1 Then
            current_movie = ListBox1.SelectedIndex
            ShowMovie()
        End If
    End Sub

    Sub ShowMovie()
        If ListBox1.Items.Count = 0 Or current_movie < 0 Then Exit Sub
        Dim Filme As New Movie
        Filme = CType(ListBox1.Items.Item(current_movie), Movie)
        TextBox1.Text = Filme.filme
        TextBox7.Text = Filme.studio
        TextBox2.Text = Filme.data
        TextBox6.Text = Filme.dur
        TextBox5.Text = Filme.rank

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        Form3.Tag = TextBox1.Text + ", " + TextBox7.Text
        Form3.Show()
    End Sub
End Class
