Public Class Drop
    Public Property X As Integer
    Public Property Y As Integer
    Public Property Width As Integer = 25
    Public Property Height As Integer = 25
    Public Property image As Image

    Private rec As Rectangle = New Rectangle(X, Y, Width, Height)
    Public Property dropRec As Rectangle
        Get
            Return rec
        End Get
        Set(value As Rectangle)
            rec = value
        End Set
    End Property

    Public Sub New(image As Image)
        Me.image = image
    End Sub

    Public Sub Draw(g As Graphics)
        g.DrawImage(image, X, Y, Width, Height)
    End Sub

End Class
