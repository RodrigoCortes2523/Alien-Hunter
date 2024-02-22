Public Class Enemy
    Public Property X As Double
    Public Property Y As Double
    Public Property VelocityX As Double
    Public Property VelocityY As Double
    Public Property Width As Integer
    Public Property Height As Integer
    Public Property Health As Integer
    Public Property maxHealth As Integer
    Public Property moveSpeed As Integer
    Public Property image As Image
    Public Property damage As Integer
    Public Property gemValue As Integer

    Private rec As Rectangle = New Rectangle(X + 25, Y + 25, Width * 0.1, Height * 0.1)
    Public explostionImages() As Image = {My.Resources._0001, My.Resources._0001, My.Resources._0002, My.Resources._0003,
        My.Resources._0004, My.Resources._0005, My.Resources._0006, My.Resources._0007, My.Resources._0008, My.Resources._0009,
        My.Resources._0010, My.Resources._0011, My.Resources._0012, My.Resources._0013, My.Resources._0014, My.Resources._0015,
        My.Resources._0016, My.Resources._0017, My.Resources._0018, My.Resources._0019}
    Public Property enemyRec As Rectangle
        Get
            Return rec
        End Get
        Set(value As Rectangle)
            rec = value
        End Set
    End Property

    Public Sub New(image As Image, health As Integer, speed As Integer, damage As Integer, value As Integer)
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
        Me.maxHealth = health
        Me.moveSpeed = speed
        Me.damage = damage
        Me.image = image
        Me.gemValue = value
    End Sub

    Public Sub CheckCollisions(bullets As List(Of Bullet))
        Dim bounds As New Rectangle(CInt(X), CInt(Y), Width - 50, Height - 50) 'sets rectangle location of enemy

        For Each bullet In bullets
            If bounds.IntersectsWith(New Rectangle(CInt(bullet.X), CInt(bullet.Y), bullet.bulletWidth, bullet.bulletHeight)) Then
                ' Subtract health from the enemy if it is hit by a bullet
                Health -= Game.player1.damage

                ' Remove the bullet
                bullets.Remove(bullet)
                Exit For
            End If
        Next
    End Sub

    Public Overridable Sub Draw(g As Graphics)
        g.DrawImage(image, CInt(X), CInt(Y), Width, Height)
    End Sub

    Public Sub drawIndicator(g As Graphics)
        Dim brush As New SolidBrush(Color.FromArgb(128, Color.Red))
        Dim circleSize As Integer = 100
        g.FillEllipse(brush, CInt(X), CInt(Y), circleSize, circleSize)
    End Sub
    Public Sub DrawLifeBar(g As Graphics)
        ' Draw life bar
        Dim lifeBarWidth As Integer = 40
        Dim lifeBarHeight As Integer = 5
        Dim lifeBarX As Integer = X + 60
        Dim lifeBarY As Integer = Y + 25
        Dim lifeRemaining As Integer = CInt((Health / maxHealth) * lifeBarWidth)
        g.FillRectangle(Brushes.Green, lifeBarX, lifeBarY, lifeRemaining, lifeBarHeight)
        g.DrawRectangle(Pens.Black, lifeBarX, lifeBarY, lifeBarWidth, lifeBarHeight)
    End Sub
    ' Public Sub drawExpolsion(g As Graphics)
    ' Draw the explosion animation at the enemy's location
    ' Use a timer to loop through the explosion images
    '  Dim explosionTimer As New Timer With {.Interval = 25}
    '  Dim explosionIndex As Integer = 0
    '  AddHandler explosionTimer.Tick, Sub()
    ' Draw the next explosion image
    '  g.DrawImage(explostionImages(explosionIndex), CInt(X), CInt(Y), Width, Height)
    ' Increment the explosion index
    '  explosionIndex += 1
    ' If we've reached the end of the explosion images, stop the timer and dispose of it
    ' If explosionIndex >= explostionImages.Length Then
    '    explosionTimer.Stop()
    '    explosionTimer.Dispose()
    ' End If
    ' End Sub
    ' explosionTimer.Start()
    ' End Sub

    Public Sub moveTowardsPlayer(playerX As Integer, playerY As Integer)
        Dim angle As Double = Math.Atan2(playerY - Y, playerX - X)
        VelocityX = Math.Cos(angle) * moveSpeed
        VelocityY = Math.Sin(angle) * moveSpeed
        X += VelocityX
        Y += VelocityY
        rec = New Rectangle(X + 25, Y + 25, Width * 0.1, Height * 0.1)
    End Sub

    Public Sub moveTowardsPlayer2(playerX As Integer)
        X += VelocityX * moveSpeed
    End Sub
End Class
