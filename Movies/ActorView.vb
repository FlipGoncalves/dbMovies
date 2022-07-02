Imports System.Data.SqlClient

Public Class ActorView
    Dim CMD As SqlCommand
    Dim CN As SqlConnection
    Dim current_actor As Integer
    Private Sub ActorView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button3.Visible = False
        Button6.Visible = False
        Button7.Visible = False
        If Me.Tag = "view" Then
            Button4.Visible = False
            Button5.Visible = False
        Else
            Button4.Visible = True
            Button5.Visible = True
        End If
        CN = New SqlConnection("Data Source = tcp:mednat.ieeta.pt\SQLSERVER,8101; Initial Catalog = p5g4; uid = p5g4; password = Pokemon123!")
        CMD = New SqlCommand
        CMD.Connection = CN
        CMD.CommandText = "SELECT * FROM Projeto.Pessoa"
        CMD.CommandType = CommandType.Text
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        ListBox1.Items.Clear()
        While RDR.Read
            Dim Pessoa As New Person
            Pessoa.id = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Identificador")), "", RDR.Item("Identificador")))
            Pessoa.Nome_person = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Nome")), "", RDR.Item("Nome")))
            ListBox1.Items.Add(Pessoa)
        End While
        CN.Close()
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex > -1 Then
            current_actor = ListBox1.SelectedIndex
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox5.Text = ""
            ShowActor()
        End If
    End Sub

    Sub ShowActor()
        If ListBox1.Items.Count = 0 Or current_actor < 0 Then Exit Sub
        Dim Pessoa = CType(ListBox1.Items.Item(current_actor), Person)
        CN = New SqlConnection("Data Source = tcp:mednat.ieeta.pt\SQLSERVER,8101; Initial Catalog = p5g4; uid = p5g4; password = Pokemon123!")
        CMD = New SqlCommand
        CMD.Connection = CN
        CMD.CommandText = "SELECT * FROM ator_filtro ('" & Pessoa.Nome_person & "')"
        TextBox1.Text = Pessoa.Nome_person
        TextBox4.Text = Pessoa.id
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        ListBox2.Items.Clear()
        While RDR.Read
            Dim Film As New Movie
            Film.studio = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("StudioNome")), "", RDR.Item("StudioNome")))
            Film.filme = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("FilmeNome")), "", RDR.Item("FilmeNome")))
            Film.id_publico = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Publico Alvo")), "", RDR.Item("Publico Alvo")))
            Film.dur = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Duracao")), "", RDR.Item("Duracao")))
            Film.rank = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Ranking")), "", RDR.Item("Ranking")))
            Film.ganh = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Ganho")), "", RDR.Item("Ganho")))
            Film.data = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Ano")), "", RDR.Item("Ano")))
            ListBox2.Items.Add(Film)
        End While
        CN.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CN = New SqlConnection("Data Source = tcp:mednat.ieeta.pt\SQLSERVER,8101; Initial Catalog = p5g4; uid = p5g4; password = Pokemon123!")
        CMD = New SqlCommand
        Select Case ComboBox1.SelectedIndex
            Case 0
                CMD.CommandText = "SELECT * FROM nome_ator ('" & SearchBarBox.Text & "')"
            Case 1
                CMD.CommandText = "SELECT * FROM filme_ator ('" & SearchBarBox.Text & "')"
            Case 2
                CMD.CommandText = "SELECT * FROM role_ator ('" & SearchBarBox.Text & "')"
            Case 3
                CMD.CommandText = "SELECT * FROM prizes_ator('" & SearchBarBox.Text & "')"
            Case Else
                CMD.CommandText = "SELECT * FROM Projeto.Pessoa"
        End Select
        CMD.Connection = CN
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        ListBox1.Items.Clear()
        While RDR.Read
            Dim Pessoa As New Person
            Pessoa.id = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Identificador")), "", RDR.Item("Identificador")))
            Pessoa.Nome_person = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Nome")), "", RDR.Item("Nome")))
            ListBox1.Items.Add(Pessoa)
        End While
        CN.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        Form2.Show()
    End Sub

    Private Sub ListBox2_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        If ListBox2.SelectedIndex > -1 And add = False Then
            ShowRolePayment(ListBox2.SelectedItem)
        End If
    End Sub

    Sub ShowRolePayment(ByVal movie As Movie)
        Dim Pessoa = CType(ListBox1.Items.Item(current_actor), Person)
        Dim filme As String = movie.filme
        Dim studio As String = movie.studio
        Dim ID As Integer = Pessoa.id
        CMD = New SqlCommand
        CMD.Connection = CN
        CMD.CommandText = "WhichRole"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@filme", filme)
        CMD.Parameters.AddWithValue("@studio", studio)
        CMD.Parameters.AddWithValue("@ID", ID)
        Dim PAR As SqlParameter = New SqlParameter("@Role", SqlDbType.VarChar, 256)
        CMD.Parameters.Add(PAR).Direction = ParameterDirection.Output
        Dim PAR2 As SqlParameter = New SqlParameter("@Pay", SqlDbType.Int)
        CMD.Parameters.Add(PAR2).Direction = ParameterDirection.Output
        Dim PAR3 As SqlParameter = New SqlParameter("@Prize", SqlDbType.VarChar, 256)
        CMD.Parameters.Add(PAR3).Direction = ParameterDirection.Output
        CMD.CommandType = CommandType.StoredProcedure
        Try
            CN.Open()
            CMD.ExecuteNonQuery()
        Catch EXC As Exception
            MsgBox(EXC.ToString)
        End Try
        CN.Close()
        TextBox2.Text = Convert.ToString(PAR.Value)
        TextBox3.Text = Convert.ToString(PAR2.Value)
        TextBox5.Text = Convert.ToString(PAR3.Value)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        add = False
        TextBox1.ReadOnly = False
        TextBox2.ReadOnly = False
        TextBox3.ReadOnly = False
        TextBox5.ReadOnly = False
        Button4.Visible = False
        Button5.Visible = False
        Button3.Visible = True
        Button6.Visible = True
        ListBox1.Enabled = False
        ListBox2.Enabled = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click 'ok
        If add Then
            AddActor(movies, roles, pay, prize)
        Else
            EditActor(ListBox2.SelectedItem)
        End If
        ListBox1.Enabled = True
        ListBox2.Enabled = True
        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = True
        TextBox3.ReadOnly = True
        TextBox5.ReadOnly = True
        Button4.Visible = True
        Button5.Visible = True
        Button3.Visible = False
        Button6.Visible = False
        Button7.Visible = False
        add = False
    End Sub

    Sub AddActor(ByVal mov As List(Of Movie), ByVal role As List(Of String), ByVal py As List(Of String), ByVal priz As List(Of String))
        For i As Integer = 0 To mov.Count - 1
            Dim filme As String = mov.ElementAt(i).filme
            Dim studio As String = mov.ElementAt(i).studio
            Dim Pessoa As Person = New Person
            Pessoa.Nome_person = TextBox1.Text
            CMD = New SqlCommand
            CMD.Connection = CN
            CMD.CommandText = "addActor"
            CMD.Parameters.Clear()
            CMD.Parameters.AddWithValue("@filme", filme)
            CMD.Parameters.AddWithValue("@studio", studio)
            CMD.Parameters.AddWithValue("@role", role.ElementAt(i))
            CMD.Parameters.AddWithValue("@nome", Pessoa.Nome_person)
            CMD.CommandType = CommandType.StoredProcedure
            Try
                CN.Open()
                CMD.ExecuteNonQuery()
                CN.Close()

                CMD.CommandText = "addprizepay"
                CMD.Parameters.Clear()
                CMD.Parameters.AddWithValue("@filme", filme)
                CMD.Parameters.AddWithValue("@studio", studio)
                CMD.Parameters.AddWithValue("@prize", priz.ElementAt(i))
                CMD.Parameters.AddWithValue("@pay", py.ElementAt(i))
                CMD.Parameters.AddWithValue("@nome", Pessoa.Nome_person)
                CMD.CommandType = CommandType.StoredProcedure
                Try
                    CN.Open()
                    CMD.ExecuteNonQuery()
                Catch EXC As Exception
                    MsgBox(EXC.ToString)
                End Try
            Catch EXC As Exception
                MsgBox(EXC.ToString)
            End Try
            CN.Close()
        Next
        movies.Clear()
        roles.Clear()
        pay.Clear()
        prize.Clear()
    End Sub

    Sub EditActor(ByVal movie As Movie)
        Dim Pessoa = CType(ListBox1.Items.Item(current_actor), Person)
        Dim filme As String = movie.filme
        Dim studio As String = movie.studio
        Dim ID As Integer = Pessoa.id
        CMD = New SqlCommand
        CMD.Connection = CN
        CMD.CommandText = "EditActor"
        CMD.Parameters.Clear()
        CMD.Parameters.AddWithValue("@filme", filme)
        CMD.Parameters.AddWithValue("@studio", studio)
        CMD.Parameters.AddWithValue("@id", ID)
        CMD.Parameters.AddWithValue("@role", TextBox2.Text)
        CMD.Parameters.AddWithValue("@pay", Convert.ToInt64(TextBox3.Text))
        CMD.Parameters.AddWithValue("@nome", TextBox1.Text)
        CMD.Parameters.AddWithValue("@prize", TextBox5.Text)
        CMD.CommandType = CommandType.StoredProcedure
        Try
            CN.Open()
            CMD.ExecuteNonQuery()
        Catch EXC As Exception
            MsgBox(EXC.ToString)
        End Try
        CN.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click 'cancel
        ListBox1.Enabled = True
        ListBox2.Enabled = True
        ListBox2.Items.Clear()
        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = True
        TextBox3.ReadOnly = True
        TextBox5.ReadOnly = True
        Button4.Visible = True
        Button5.Visible = True
        Button3.Visible = False
        Button6.Visible = False
        Button7.Visible = False
        add = False
    End Sub

    Dim add As Boolean = False
    Private Sub Button5_Click(sender As Object, e As EventArgs)
        ListBox1.Enabled = False
        ListBox2.Enabled = False
        TextBox1.ReadOnly = False
        TextBox2.ReadOnly = False
        TextBox3.ReadOnly = False
        TextBox5.ReadOnly = False
        Button4.Visible = False
        Button5.Visible = False
        Button3.Visible = True
        Button6.Visible = True
        Button7.Visible = True
        add = True
    End Sub

    Dim movies As List(Of Movie) = New List(Of Movie)
    Dim roles As List(Of String) = New List(Of String)
    Dim pay As List(Of String) = New List(Of String)
    Dim prize As List(Of String) = New List(Of String)
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        movies.Add(ListBox2.SelectedItem)
        roles.Add(TextBox2.Text)
        pay.Add(TextBox3.Text)
        prize.Add(TextBox5.Text)
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
        ListBox1.Enabled = False
        ListBox2.Enabled = True
        TextBox1.ReadOnly = False
        TextBox2.ReadOnly = False
        TextBox3.ReadOnly = False
        TextBox5.ReadOnly = False
        Button4.Visible = False
        Button5.Visible = False
        Button3.Visible = True
        Button6.Visible = True
        Button7.Visible = True
        add = True
        CMD = New SqlCommand
        CMD.Connection = CN
        CMD.CommandText = "SELECT * FROM all_movies"
        CN.Open()
        Dim RDR As SqlDataReader
        RDR = CMD.ExecuteReader
        ListBox2.Items.Clear()
        While RDR.Read
            Dim Filme As New Movie
            Filme.studio = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("StudioNome")), "", RDR.Item("StudioNome")))
            Filme.filme = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("FilmeNome")), "", RDR.Item("FilmeNome")))
            Filme.id_publico = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Publico Alvo")), "", RDR.Item("Publico Alvo")))
            Filme.dur = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Duracao")), "", RDR.Item("Duracao")))
            Filme.rank = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Ranking")), "", RDR.Item("Ranking")))
            Filme.ganh = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Ganho")), "", RDR.Item("Ganho")))
            Filme.data = Convert.ToString(IIf(RDR.IsDBNull(RDR.GetOrdinal("Ano")), "", RDR.Item("Ano")))
            ListBox2.Items.Add(Filme)
        End While
        CN.Close()
    End Sub
End Class