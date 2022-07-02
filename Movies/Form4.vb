Imports System.Data.SqlClient
Public Class Form4
    Dim CMD As SqlCommand
    Dim CN As SqlConnection = New SqlConnection("Data Source = tcp:mednat.ieeta.pt\SQLSERVER,8101; Initial Catalog = p5g4; uid = p5g4; password = Pokemon123!")
    Dim filmes As List(Of Movie) = New List(Of Movie)
    Dim current_movie As Integer
    Dim adding As Boolean

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button5.Visible = False
        Button6.Visible = False
        Button7.Visible = False
        Button8.Visible = False
        Button9.Visible = False
        Button10.Visible = False
        Button11.Visible = False
        TextBox9.Visible = False
        TextBox10.Visible = False
        TextBox11.Visible = False
        TextBox12.Visible = False
        TextBox9.ReadOnly = False
        TextBox10.ReadOnly = False
        TextBox11.ReadOnly = False
        TextBox12.ReadOnly = False
        If Me.Tag = "view" Then
            Button3.Visible = False
            Button4.Visible = False
        Else
            Button3.Visible = True
            Button4.Visible = True
        End If

        CMD = New SqlCommand
        CMD.CommandType = CommandType.Text
        CMD.Connection = CN
        CMD.CommandText = "SELECT * FROM all_movies"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        ListBox1.Items.Clear()
        While RDR.Read
            Dim Filme As New Movie
            Filme.studio = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("StudioNome")), "", RDR.Item("StudioNome")))
            Filme.filme = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("FilmeNome")), "", RDR.Item("FilmeNome")))
            Filme.id_publico = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Publico Alvo")), "", RDR.Item("Publico Alvo")))
            Filme.dur = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Duracao")), "", RDR.Item("Duracao")))
            Filme.rank = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Ranking")), "", RDR.Item("Ranking")))
            Filme.ganh = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Ganho")), "", RDR.Item("Ganho")))
            Filme.data = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Ano")), "", RDR.Item("Ano")))
            ListBox1.Items.Add(Filme)
            filmes.Add(Filme)
        End While
        CN.Close()
        loadgenres()
        loadactors()
        loadlocations()
        loadaderecos()
        ComboBox1.SelectedIndex = 0
    End Sub
    Sub loadgenres()
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
    Sub loadaderecos()
        CMD = New SqlCommand
        CMD.Connection = CN
        For i As Integer = 0 To filmes.Count - 1
            Dim Filme As Movie = filmes.ElementAt(i)
            CMD.CommandText = "SELECT * FROM movie_aderecos ('" & Filme.filme & "', '" & Filme.studio & "')"
            CN.Open()
            Dim RDR As SqlDataReader
            RDR = CMD.ExecuteReader
            While RDR.Read
                Filme.addAdereco(Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Nome")), "", RDR.Item("Nome"))))
            End While
            CN.Close()
        Next
    End Sub
    Sub loadlocations()
        CMD = New SqlCommand
        CMD.Connection = CN
        For i As Integer = 0 To filmes.Count - 1
            Dim Filme As Movie = filmes.ElementAt(i)
            CMD.CommandText = "SELECT * FROM movie_locations ('" & Filme.filme & "', '" & Filme.studio & "')"
            CN.Open()
            Dim RDR As SqlDataReader
            RDR = CMD.ExecuteReader
            While RDR.Read
                Filme.addLocation(Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Endereco")), "", RDR.Item("Endereco"))))
            End While
            CN.Close()
        Next
    End Sub
    Sub loadactors()
        CMD = New SqlCommand
        CMD.Connection = CN
        For i As Integer = 0 To filmes.Count - 1
            Dim Filme As Movie = filmes.ElementAt(i)
            CMD.CommandText = "SELECT * FROM filme_ator ('" & Filme.filme & "')"
            CN.Open()
            Dim RDR As SqlDataReader
            RDR = CMD.ExecuteReader
            While RDR.Read
                Dim Pessoa As New Person
                Pessoa.id = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Identificador")), "", RDR.Item("Identificador")))
                Pessoa.Nome_person = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Nome")), "", RDR.Item("Nome")))
                Filme.addAtor(Pessoa)
            End While
            CN.Close()
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
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
        loadactors()
        loadlocations()
        loadaderecos()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        Form2.Show()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex > -1 Then
            current_movie = ListBox1.SelectedIndex
            ShowMovie()
            ShowStudioAvgCount()
        End If
    End Sub

    Sub ShowStudioAvgCount()
        If ListBox6.SelectedIndex < 0 Then
            ListBox6.SelectedIndex = 0
        End If

        CMD = New SqlCommand
        CMD.CommandType = CommandType.Text
        CMD.Connection = CN
        CMD.CommandText = "SELECT * FROM avgStudio('" & ListBox6.SelectedItem & "')"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        While RDR.Read
            TextBox13.Text = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("AVG Quantia")), "", RDR.Item("AVG Quantia")))
            TextBox7.Text = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Numero Pagamentos")), "", RDR.Item("Numero Pagamentos")))
        End While
        CN.Close()
    End Sub

    Sub ShowMovie()
        If ListBox1.Items.Count = 0 Or current_movie < 0 Then Exit Sub
        Dim Filme As New Movie
        Filme = CType(ListBox1.Items.Item(current_movie), Movie)
        TextBox1.Text = Filme.filme
        TextBox2.Text = Filme.data
        TextBox6.Text = Filme.dur
        TextBox5.Text = Filme.rank
        TextBox3.Text = Filme.ganh
        TextBox4.Text = Filme.id_publico
        filmegasto(Filme)
        ListBox6.Items.Clear()
        ListBox6.Items.Add(Filme.studio)
        ListBox4.Items.Clear()
        For i As Integer = 0 To Filme.Genero.Count - 1
            ListBox4.Items.Add(Filme.Genero.ElementAt(i))
        Next
        ListBox2.Items.Clear()
        For i As Integer = 0 To Filme.Ator.Count - 1
            ListBox2.Items.Add(Filme.Ator.ElementAt(i))
        Next
        ListBox3.Items.Clear()
        For i As Integer = 0 To Filme.Location.Count - 1
            ListBox3.Items.Add(Filme.Location.ElementAt(i))
        Next
        ListBox5.Items.Clear()
        For i As Integer = 0 To Filme.Aderec.Count - 1
            ListBox5.Items.Add(Filme.Aderec.ElementAt(i))
        Next
    End Sub
    Sub filmegasto(ByVal c As Movie)
        CMD.CommandType = CommandType.Text
        CMD.CommandText = "SELECT Gasto, Diferenca FROM money_movie ('" & c.filme & "', '" & c.studio & "')"
        CMD.Parameters.Clear()
        CN.Open()
        Dim Nome As String
        Dim igual As Boolean = False
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        While RDR.Read
            c.gast = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Gasto")), "", RDR.Item("Gasto")))
            c.diferenca = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Diferenca")), "", RDR.Item("Diferenca")))
        End While
        CN.Close()
        TextBox8.Text = c.gast
    End Sub
    Sub UnlockControls()
        TextBox2.ReadOnly = False
        TextBox6.ReadOnly = False
        TextBox5.ReadOnly = False
        TextBox3.ReadOnly = False
        TextBox4.ReadOnly = False
        TextBox8.ReadOnly = False
    End Sub
    Sub LockControls()
        TextBox2.ReadOnly = True
        TextBox6.ReadOnly = True
        TextBox5.ReadOnly = True
        TextBox3.ReadOnly = True
        TextBox4.ReadOnly = True
        TextBox8.ReadOnly = True
    End Sub
    Sub ClearFields()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox6.Text = ""
        TextBox5.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox8.Text = ""
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click 'edit
        UnlockControls()
        adding = False
        ListBox1.Enabled = False
        Button3.Visible = False
        Button4.Visible = False
        Button5.Visible = True
        Button6.Visible = True
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click 'add
        adding = True
        UnlockControls()
        TextBox1.ReadOnly = False
        ClearFields()
        ListBox1.Enabled = False
        Button3.Visible = False
        Button4.Visible = False
        Button5.Visible = True
        Button6.Visible = True
        Button7.Visible = True
        Button8.Visible = True
        Button9.Visible = True
        Button10.Visible = True
        Button11.Visible = True
        TextBox9.Visible = True
        TextBox10.Visible = True
        TextBox11.Visible = True
        TextBox12.Visible = True
        allStuff()
    End Sub

    Sub allStuff()
        CMD.Connection = CN
        CMD.CommandType = CommandType.Text
        CMD.CommandText = "SELECT DISTINCT Pessoa.Identificador, Nome, Role FROM Projeto.Pessoa JOIN Projeto.WorksOn ON Pessoa.Identificador = WorksOn.Identificador"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        ListBox2.Items.Clear()
        While RDR.Read
            Dim Pessoa As New Person
            Pessoa.id = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Identificador")), "", RDR.Item("Identificador")))
            Pessoa.Nome_person = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Nome")), "", RDR.Item("Nome")))
            Dim Role As String = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Role")), "", RDR.Item("Role")))
            ListBox2.Items.Add(Pessoa.ToString + " + " + Role)
        End While
        CN.Close()

        CMD.Connection = CN
        CMD.CommandType = CommandType.Text
        CMD.CommandText = "SELECT DISTINCT Localizacao.Endereco, Data FROM Projeto.Localizacao JOIN Projeto.FilmaEm ON Localizacao.Endereco = FilmaEm.Endereco"
        CN.Open()
        RDR = CMD.ExecuteReader
        ListBox3.Items.Clear()
        While RDR.Read
            Dim loc = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Endereco")), "", RDR.Item("Endereco")))
            Dim Data = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Data")), "", RDR.Item("Data")))
            ListBox3.Items.Add(loc + " + " + Data)
        End While
        CN.Close()

        CMD.Connection = CN
        CMD.CommandType = CommandType.Text
        CMD.CommandText = "SELECT DISTINCT Nome, Price, Quantidade FROM Projeto.Adereco JOIN Projeto.Vende ON Codigo = Adereco"
        CN.Open()
        RDR = CMD.ExecuteReader
        ListBox5.Items.Clear()
        While RDR.Read
            Dim Nome = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Nome")), "", RDR.Item("Nome")))
            Dim Preco = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Price")), "", RDR.Item("Price")))
            Dim Qnty = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Quantidade")), "", RDR.Item("Quantidade")))
            ListBox5.Items.Add(Nome + " + " + Preco + " + " + Qnty)
        End While
        CN.Close()

        CMD.Connection = CN
        CMD.CommandType = CommandType.Text
        CMD.CommandText = "SELECT DISTINCT Genero FROM Projeto.Genero"
        CN.Open()
        RDR = CMD.ExecuteReader
        ListBox4.Items.Clear()
        While RDR.Read
            Dim Genero = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Genero")), "", RDR.Item("Genero")))
            ListBox4.Items.Add(Genero)
        End While
        CN.Close()

        CMD.Connection = CN
        CMD.CommandType = CommandType.Text
        CMD.CommandText = "SELECT DISTINCT NOME FROM Projeto.FilmeStudio"
        CN.Open()
        RDR = CMD.ExecuteReader
        ListBox6.Items.Clear()
        While RDR.Read
            Dim Nome = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("NOME")), "", RDR.Item("NOME")))
            ListBox6.Items.Add(Nome)
        End While
        CN.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click 'ok
        Dim Filme As New Movie
        Try
            Filme = CType(ListBox1.Items.Item(current_movie), Movie)
            Filme.filme = TextBox1.Text
            Filme.studio = ListBox6.SelectedItem
            Filme.data = TextBox2.Text
            Filme.dur = TextBox6.Text
            Filme.rank = TextBox5.Text
            Filme.ganh = TextBox3.Text
            Filme.id_publico = TextBox4.Text
            Filme.ganh = TextBox8.Text
            If adding Then
                SubmitMovie(Filme)
                addActorDB(Filme, act)
                addGenreDB(Filme, gen)
                addLocationDB(Filme, loc)
                addPropDB(Filme, ader)
                ListBox1.Items.Add(Filme)
            Else
                UpdateMovie(Filme)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Exit Sub
        End Try
        Button3.Visible = True
        Button4.Visible = True
        Button5.Visible = False
        Button6.Visible = False
        Button7.Visible = False
        Button8.Visible = False
        Button9.Visible = False
        Button10.Visible = False
        Button11.Visible = False
        TextBox9.Visible = False
        TextBox10.Visible = False
        TextBox11.Visible = False
        TextBox12.Visible = False
        LockControls()
        TextBox1.ReadOnly = True
        ListBox1.Enabled = True
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click 'close
        Button3.Visible = True
        Button4.Visible = True
        Button5.Visible = False
        Button6.Visible = False
        Button7.Visible = False
        Button8.Visible = False
        Button9.Visible = False
        Button10.Visible = False
        Button11.Visible = False
        TextBox9.Visible = False
        TextBox10.Visible = False
        TextBox11.Visible = False
        TextBox12.Visible = False
        LockControls()
        TextBox1.ReadOnly = True
        ListBox1.Enabled = True
    End Sub
    Private Sub SubmitMovie(ByVal C As Movie)
        CMD.CommandType = CommandType.Text
        CMD.CommandText = "INSERT Projeto.Filme (StudioNome, FilmeNome, Duracao, ID_Publico_Alvo, Ranking, Ganho, Ano) " &
                          "VALUES (@StudioNome, @FilmeNome, @Duracao, @Id_Publico_Alvo, @Ranking, @Ganho, @Ano)"
        CMD.Parameters.Clear()
        MsgBox(C.studio)
        CMD.Parameters.AddWithValue("@StudioNome", C.studio)
        CMD.Parameters.AddWithValue("@FilmeNome", C.filme)
        CMD.Parameters.AddWithValue("@Id_Publico_Alvo", Convert.ToInt32(C.id_publico))
        CMD.Parameters.AddWithValue("@Duracao", Convert.ToInt32(C.dur))
        CMD.Parameters.AddWithValue("@Ranking", Convert.ToDecimal(C.rank))
        CMD.Parameters.AddWithValue("@Ganho", Convert.ToInt32(C.ganh))
        CMD.Parameters.AddWithValue("@Ano", Convert.ToDateTime(C.data))
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update movie in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
        CN.Close()
    End Sub
    Private Sub UpdateMovie(ByVal C As Movie)
        CMD.CommandType = CommandType.Text
        CMD.CommandText = "UPDATE Projeto.Filme " &
            "SET Duracao = @Duracao, " &
            "    Ranking = @Ranking, " &
            "    Ganho = @Ganho, " &
            "    ID_Publico_Alvo = @Id_Publico_Alvo, " &
            "    Ano = @Ano " &
            "WHERE StudioNome = @studio AND FilmeNome = @filme"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@studio", C.studio)
        CMD.Parameters.AddWithValue("@filme", C.filme)
        CMD.Parameters.AddWithValue("@Id_Publico_Alvo", C.id_publico)
        CMD.Parameters.AddWithValue("@Duracao", Convert.ToInt32(C.dur))
        CMD.Parameters.AddWithValue("@Ranking", Convert.ToDecimal(C.rank))
        CMD.Parameters.AddWithValue("@Ganho", Convert.ToInt32(C.ganh))
        CMD.Parameters.AddWithValue("@Ano", Convert.ToDateTime(C.data))
        CN.Open()
        Try
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Failed to update movie in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
        Finally
            CN.Close()
        End Try
    End Sub
    Sub addActorDB(ByVal C As Movie, ByVal act As List(Of String))
        For i As Integer = 0 To act.Count - 1
            Dim nome As String = act.ElementAt(i).Split("+").ElementAt(0)
            Dim role As String = act.ElementAt(i).Split("+").ElementAt(1)
            CMD.Connection = CN
            CMD.CommandText = "addActor"
            CMD.Parameters.Clear()
            CMD.Parameters.AddWithValue("@nome", nome)
            CMD.Parameters.AddWithValue("@role", role)
            CMD.Parameters.AddWithValue("@filme", C.filme)
            CMD.Parameters.AddWithValue("@studio", C.studio)
            CMD.CommandType = CommandType.StoredProcedure
            Try
                CN.Open()
                CMD.ExecuteNonQuery()
            Catch e As Exception
                MsgBox(e.ToString)
            End Try
            CN.Close()
        Next
    End Sub
    Sub addGenreDB(ByVal C As Movie, ByVal gen As List(Of String))
        For i As Integer = 0 To gen.Count - 1
            Dim genre As String = gen.ElementAt(i)
            CMD.CommandType = CommandType.StoredProcedure
            CMD.CommandText = "addgenre"
            CMD.Parameters.Clear()
            CMD.Parameters.AddWithValue("@Genero", genre)
            CMD.Parameters.AddWithValue("@filme", C.filme)
            CMD.Parameters.AddWithValue("@studio", C.studio)
            CN.Open()
            Try
                CMD.ExecuteNonQuery()
            Catch ex As Exception
                Throw New Exception("Failed to update pessoa in database. " & vbCrLf & "ERROR MESSAGE: " & vbCrLf & ex.Message)
            Finally
                CN.Close()
            End Try
            CN.Close()
            CMD.CommandType = CommandType.Text
        Next
    End Sub
    Sub addLocationDB(ByVal C As Movie, ByVal loc As List(Of String))
        For i As Integer = 0 To loc.Count - 1
            Dim ender As String = loc.ElementAt(i).Split("+").ElementAt(0)
            Dim data As String = loc.ElementAt(i).Split("+").ElementAt(1)
            CMD.Connection = CN
            CMD.CommandText = "addLocation"
            CMD.Parameters.Clear()
            CMD.Parameters.AddWithValue("@endereco", ender)
            CMD.Parameters.AddWithValue("@data", data)
            CMD.Parameters.AddWithValue("@filme", C.filme)
            CMD.Parameters.AddWithValue("@studio", C.studio)
            CMD.CommandType = CommandType.StoredProcedure
            Try
                CN.Open()
                CMD.ExecuteNonQuery()
            Catch e As Exception
                MsgBox(e.ToString)
            End Try
            CN.Close()
        Next
    End Sub
    Sub addPropDB(ByVal C As Movie, ByVal pro As List(Of String))
        For i As Integer = 0 To pro.Count - 1
            Dim name As String = pro.ElementAt(i).Split("+").ElementAt(0)
            Dim price As String = pro.ElementAt(i).Split("+").ElementAt(1)
            Dim qnty As String = pro.ElementAt(i).Split("+").ElementAt(2)
            CMD.Connection = CN
            CMD.CommandText = "addProp"
            CMD.Parameters.Clear()
            CMD.Parameters.AddWithValue("@name", name)
            CMD.Parameters.AddWithValue("@price", Convert.ToInt32(price))
            CMD.Parameters.AddWithValue("@qnty", Convert.ToInt32(qnty))
            CMD.Parameters.AddWithValue("@filme", C.filme)
            CMD.Parameters.AddWithValue("@studio", C.studio)
            CMD.CommandType = CommandType.StoredProcedure
            Try
                CN.Open()
                CMD.ExecuteNonQuery()
            Catch e As Exception
                MsgBox(e.ToString)
            End Try
            CN.Close()
        Next
    End Sub

    Dim act As List(Of String) = New List(Of String)
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click 'add actor
        If Not TextBox9.Text = "" Then
            act.Add(TextBox9.Text)
            TextBox9.Text = ""
        Else
            act.Add(ListBox2.SelectedItem.ToString.Split("->").ElementAt(1))
        End If
    End Sub

    Dim gen As List(Of String) = New List(Of String)
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click 'add genre
        If Not TextBox10.Text = "" Then
            gen.Add(TextBox10.Text)
            TextBox10.Text = ""
        Else
            gen.Add(ListBox4.SelectedItem)
        End If
    End Sub

    Dim ader As List(Of String) = New List(Of String)
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click 'add adereco
        If Not TextBox11.Text = "" Then
            ader.Add(TextBox11.Text)
            TextBox11.Text = ""
        Else
            ader.Add(ListBox5.SelectedItem.ToString)
        End If
    End Sub

    Dim loc As List(Of String) = New List(Of String)
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click 'add location
        If Not TextBox12.Text = "" Then
            loc.Add(TextBox12.Text)
            TextBox12.Text = ""
        Else
            loc.Add(ListBox3.SelectedItem.ToString)
        End If
    End Sub

    Dim studio As String
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click 'add Studio
        If ListBox6.SelectedIndex < 0 Then
            ListBox6.SelectedIndex = 0
            studio = ListBox6.SelectedItem
        Else
            studio = ListBox6.SelectedItem
        End If
    End Sub

    Function getStudio()
        Return studio
    End Function
End Class