<Serializable()> Public Class Movie
    Private StudioNome As String
    Private FilmeNome As String
    Private ID_Publico_Alvo As String
    Private Duracao As String
    Private Ranking As String
    Private Ganho As String
    Private Gasto As String
    Private dif As String
    Private Ano As String
    Private genre As List(Of String) = New List(Of String)
    Private locations As List(Of String) = New List(Of String)
    Private aderecos As List(Of String) = New List(Of String)
    Private actors As List(Of Person) = New List(Of Person)

    Property diferenca As String
        Get
            Return dif
        End Get
        Set(ByVal value As String)
            dif = value
        End Set
    End Property

    Property gast As String
        Get
            Return Gasto
        End Get
        Set(ByVal value As String)
            Gasto = value
        End Set
    End Property

    Sub addAdereco(ByVal value As String)
        aderecos.Add(value)
    End Sub
    Property Aderec As List(Of String)
        Get
            Return aderecos
        End Get
        Set(ByVal value As List(Of String))
            aderecos = value
        End Set
    End Property
    Sub addAtor(ByVal value As Person)
        actors.Add(value)
    End Sub
    Property Ator As List(Of Person)
        Get
            Return actors
        End Get
        Set(ByVal value As List(Of Person))
            actors = value
        End Set
    End Property
    Sub addLocation(ByVal value As String)
        locations.Add(value)
    End Sub
    Property Location As List(Of String)
        Get
            Return locations
        End Get
        Set(ByVal value As List(Of String))
            locations = value
        End Set
    End Property
    Sub addGenero(ByVal value As String)
        genre.Add(value)
    End Sub
    Property Genero As List(Of String)
        Get
            Return genre
        End Get
        Set(ByVal value As List(Of String))
            genre = value
        End Set
    End Property
    Property studio As String
        Get
            Return StudioNome
        End Get
        Set(ByVal value As String)
            StudioNome = value
        End Set
    End Property

    Property filme As String
        Get
            Return FilmeNome
        End Get
        Set(ByVal value As String)
            FilmeNome = value
        End Set
    End Property

    Property id_publico As String
        Get
            Return ID_Publico_Alvo
        End Get
        Set(ByVal value As String)
            ID_Publico_Alvo = value
        End Set
    End Property

    Property dur As String
        Get
            Return Duracao
        End Get
        Set(ByVal value As String)
            Duracao = value
        End Set
    End Property

    Property rank As String
        Get
            Return Ranking
        End Get
        Set(ByVal value As String)
            Ranking = value
        End Set
    End Property

    Property ganh As String
        Get
            Return Ganho
        End Get
        Set(ByVal value As String)
            Ganho = value
        End Set
    End Property

    Property data As String
        Get
            Return Ano
        End Get
        Set(ByVal value As String)
            Ano = value
        End Set
    End Property

    Overrides Function ToString() As String
        Return FilmeNome & ", " & StudioNome
    End Function
    Public Sub New()
        MyBase.New()
    End Sub
End Class
