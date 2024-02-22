Public Class Currency
    Public Property X As Integer
    Public Property Y As Integer
    Public Property Size As Integer
    Public Property width As Integer = 25
    Public Property height As Integer = 25
    Public Property Value As Integer
    Public Property Image As Image
    Public Property gemValue As Integer

    Private rec As Rectangle = New Rectangle(X, Y, width, height)
    Private images() As Image = {My.Resources.gem, My.Resources.gemGreen, My.Resources.gemRed}

    Public Property currencyRec As Rectangle
        Get
            Return rec
        End Get
        Set(value As Rectangle)
            rec = value
        End Set
    End Property

    Public Sub DrawCurrency(g As Graphics, currency As Currency)

        g.DrawImage(images(gemValue), currency.X, currency.Y, currency.width, currency.height)

    End Sub

End Class