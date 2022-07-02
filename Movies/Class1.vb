<Serializable()> Public Class Person
    Private Identificador As String
    Private Nome As String

    Property id As String
        Get
            Return Identificador
        End Get
        Set(ByVal value As String)
            Identificador = value
        End Set
    End Property

    Property Nome_person As String
        Get
            Return Nome
        End Get
        Set(ByVal value As String)
            If value Is Nothing Or value = "" Then
                Throw New Exception("Precisa de um nome")
                Exit Property
            End If
            Nome = value
        End Set
    End Property

    Overrides Function ToString() As String
        Return Identificador & " -> " & Nome
    End Function

    Public Sub New(ByVal Identificador As String, ByVal Nome As String)
        MyBase.New()
        Me.Identificador = Identificador
        Me.Nome = Nome
    End Sub
    Public Sub New()
        MyBase.New()
    End Sub
End Class

