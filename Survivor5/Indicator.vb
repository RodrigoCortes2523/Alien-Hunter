Public Class Indicator
    Private x As Integer
    Private y As Integer
    Private radius As Integer
    Private color As Color
    Private timer As Integer

    Public Sub New(x As Integer, y As Integer, radius As Integer, color As Color, durationSeconds As Integer)
        Me.x = x
        Me.y = y
        Me.radius = radius
        Me.color = color
        Me.timer = durationSeconds * 1000 ' Convert seconds to milliseconds
    End Sub

    Public Sub Draw(g As Graphics)
        Dim brush As New SolidBrush(Color.FromArgb(128, color))
        g.FillEllipse(brush, x - radius, y - radius, radius * 2, radius * 2)
    End Sub

    Public Sub Update(gameTime As Integer)
        timer -= gameTime
    End Sub

    Public Function IsExpired() As Boolean
        Return timer <= 0
    End Function
End Class
