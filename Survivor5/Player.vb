Public Class Player
    Private moveSpeed As Integer = 5
    Private Width As Integer = 100
    Private Height As Integer = 100
    Private x As Integer = 250
    Private y As Integer = 250
    Private rec As Rectangle = New Rectangle(x + 25, y + 25, Width * 0.25, Height * 0.25)
    Public Property damage As Integer = 25
    Public Property shotSpeed As Integer = 5
    Public Property health As Integer = 100
    Public Property maxHealth As Integer = 100
    Public Property currenctyMultiplier As Integer = 1
    Public Property blueCurrency As Integer = 0
    Public Property greenCurrency As Integer = 0
    Public Property redCurrency As Integer = 0
    Public Property direction As Integer = 0
    Public Property secondShot As Boolean = False

    Private lastHitTime As Date = Date.MinValue

    Public Property playerRec As Rectangle
        Get
            Return rec
        End Get
        Set(value As Rectangle)
            rec = value
        End Set
    End Property
    Public Property playerMoveSpeed As Integer
        Get
            Return moveSpeed
        End Get
        Set(value As Integer)
            moveSpeed = value
        End Set
    End Property
    Public Property playerX As Integer
        Get
            Return x
        End Get
        Set(value As Integer)
            x = value
        End Set
    End Property
    Public Property playerY As Integer
        Get
            Return y
        End Get
        Set(value As Integer)
            y = value
        End Set
    End Property
    Public Property playerHeight As Integer
        Get
            Return Height
        End Get
        Set(value As Integer)
            Height = value
        End Set
    End Property
    Public Property playerWidth As Integer
        Get
            Return Width
        End Get
        Set(value As Integer)
            Width = value
        End Set
    End Property

    Public Sub moveRight()
        If x + Width < Game.Width Then
            x += moveSpeed
            rec = New Rectangle(x + 25, y + 25, Width * 0.25, Height * 0.25)
            direction = 1
        End If
    End Sub
    Public Sub moveLeft()
        If playerX > 0 Then
            x -= moveSpeed
            rec = New Rectangle(x + 25, y + 25, Width * 0.25, Height * 0.25)
        End If
    End Sub
    Public Sub moveUp()
        If y > 0 Then
            y -= moveSpeed
            rec = New Rectangle(x + 25, y + 25, Width * 0.25, Height * 0.25)
            direction = 0
        End If
    End Sub
    Public Sub moveDown()
        If y + Height < Game.Height Then
            y += moveSpeed
            rec = New Rectangle(x + 25, y + 25, Width * 0.25, Height * 0.25)
        End If
    End Sub

    Public Sub playerCurrencyCollisionCheck()

        For Each currency In Game.activeCurrencyItems.ToList()
            If playerRec.IntersectsWith(currency.currencyRec) Then
                If currency.gemValue = 1 Then
                    Game.player1.greenCurrency += currency.Value - 1
                ElseIf currency.gemValue = 2 Then
                    Game.player1.redCurrency += currency.Value - 2
                Else
                    Game.player1.blueCurrency += currency.Value
                End If

                Game.activeCurrencyItems.Remove(currency)
            End If
        Next
    End Sub

    Public Sub playerDropCollisionCheck()
        For Each item In Game.activeDropItems.ToList()
            If playerRec.IntersectsWith(item.dropRec) Then
                If Game.player1.health <= 80 Then
                    Game.player1.health += 20
                    Game.activeDropItems.Remove(item)
                End If
            End If
        Next
    End Sub

    Public Sub playerEnemyCollisionCheck()

        Dim timeSinceLastHit As TimeSpan = Date.Now - lastHitTime

        For Each enemy In Game.activeEnemies.Concat(Game.explodingEnemies)

            If enemy.enemyRec.IntersectsWith(Game.player1.rec) Then
                If timeSinceLastHit.TotalSeconds >= 2 Then
                    health -= enemy.damage
                    lastHitTime = Date.Now
                End If

            End If
        Next
    End Sub

    Public Sub FireWeapon(x As Integer, y As Integer)
        ' Calculate the velocity vector of the bullet
        Dim dx As Integer = x - (playerX + playerWidth / 2)
        Dim dy As Integer = y - (playerY + playerHeight / 2)
        Dim length As Double = Math.Sqrt(dx ^ 2 + dy ^ 2)
        Dim velocityX As Integer = dx * shotSpeed \ length
        Dim velocityY As Integer = dy * shotSpeed \ length

        'Create a New instance of the Bullet class with the calculated velocity vector
        Dim bullet As New Bullet With {
             .X = playerX + playerWidth / 2 - .bulletWidth / 2,
             .Y = playerY + playerHeight / 2 - .bulletHeight / 2,
             .VelocityX = velocityX,
             .VelocityY = velocityY}

        'Create second bullet if condition is met
        If secondShot = True Then
            Dim bullet2 As New Bullet With {
            .X = playerX + playerWidth / 2 - .bulletWidth / 2,
            .Y = playerY + playerHeight / 2 - .bulletHeight / 2,
            .VelocityX = -velocityX,
            .VelocityY = -velocityY}
            Game.activeBullets.Add(bullet2)
        End If

        'Add the bullet to a list of active bullets
        Game.activeBullets.Add(bullet)

    End Sub

    Public Sub lose()
        If Game.player1.health <= 0 Then
            'grab ending variables and load them into the gameover screen
            GameOver.waveCompletedLabel.Text = Game.currentStageIndex
            GameOver.Show()
            Game.Close()
        End If
    End Sub

End Class
