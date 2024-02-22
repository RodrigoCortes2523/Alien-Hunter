Public Class Explosion
    Private images() As Image
    Private currentIndex As Integer
    Private x, y As Integer
    Private timer As Integer

    Public Sub New(x As Integer, y As Integer, images() As Image)
        Me.x = x
        Me.y = y
        Me.images = images
        currentIndex = 0
        timer = 0
    End Sub

    Public Sub Draw(g As Graphics)
        If currentIndex >= images.Length Then
            Return
        End If

        g.DrawImage(images(currentIndex), x, y)

        timer += 1

        If timer Mod 6 = 0 Then
            currentIndex += 1
        End If
    End Sub
End Class
