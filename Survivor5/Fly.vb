Public Class Fly
    Inherits Enemy

    Private flyImageIndex As Integer = 0
    Private flyImageTimer As Integer = 0
    Private flyImages() As Image = {}

    Public Sub New(image As Image, health As Integer, speed As Integer, damage As Integer, gemValue As Integer)
        MyBase.New(image, health, speed, damage, gemValue)
        Dim random As New Random()
        Dim enemyWidth As Integer = 150
        Dim enemyHeight As Integer = 150
        Me.X = random.Next(0, Game.Width - enemyWidth)
        Me.Y = random.Next(0, Game.Height - enemyHeight)
        Me.VelocityX = random.NextDouble() * 2 - 1
        Me.VelocityY = random.NextDouble() * 2 - 1
        Me.Width = enemyWidth
        Me.Height = enemyHeight
        Me.Health = health
        Me.moveSpeed = speed
        Me.damage = damage
        Me.image = image
        Me.gemValue = gemValue
    End Sub

    Public Overrides Sub Draw(g As Graphics)
        ' Animate the enemy by changing its image every few frames
        flyImageTimer += 1
        If flyImageTimer >= 25 Then
            flyImageTimer = 0
            flyImageIndex = (flyImageIndex + 1) Mod flyImages.Length
        End If
        Dim flyImage As Image = flyImages(flyImageIndex)

        ' Draw the enemy using the animated fly image
        g.DrawImage(flyImage, CInt(X), CInt(Y), CInt(Width), CInt(Height))
        DrawLifeBar(g)
    End Sub
End Class