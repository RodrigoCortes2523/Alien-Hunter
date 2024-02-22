Public Class Bullet
    Private width As Integer = 10
    Private height As Integer = 10
    Private image As Image = My.Resources.bullet1
    Public Property X As Integer
    Public Property Y As Integer
    Public Property VelocityX As Integer
    Public Property VelocityY As Integer
    Public ReadOnly Property rec As Rectangle
        Get
            Return New Rectangle(X, Y, width, height)
        End Get
    End Property
    Public Property bulletWidth As Integer
        Get
            Return width
        End Get
        Set(value As Integer)
            width = value
        End Set
    End Property
    Public Property bulletHeight As Integer
        Get
            Return height
        End Get
        Set(value As Integer)
            height = value
        End Set
    End Property
    Public Property bulletImage As Image
        Get
            Return image
        End Get
        Set(value As Image)
            image = value
        End Set
    End Property
    Public Sub Move()
        X += VelocityX
        Y += VelocityY
    End Sub
    Public Sub Draw(g As Graphics)
        g.DrawImage(bulletImage, X, Y, bulletWidth, bulletHeight)
    End Sub
End Class
