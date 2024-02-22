Public Class Painter
    Dim playerImage As Image = My.Resources.player1
    Dim gemImages() As Image = {My.Resources.gem, My.Resources.gemGreen, My.Resources.gemRed}

    Dim font As New Font("Asus Rog", 16)
    Dim healthColor As Brush = Brushes.DarkGreen
    Dim healthBackColor As Brush = Brushes.DarkGray

    Public Sub Paint(sender As Object, e As PaintEventArgs)
        ' Draw the health shells
        For i As Integer = 0 To Game.shell.Length - 1
            e.Graphics.DrawImage(Game.shell(i).image, Game.shell(i).X, Game.shell(i).Y, 100, 100)
            Game.shell(i).DrawLifeBar(e.Graphics)
        Next

        'Draw item drops
        For Each drop In Game.activeDropItems
            drop.Draw(e.Graphics)
        Next

        'Draw indicators
        For Each indicator In Game.activeIndicators
            indicator.Draw(e.Graphics)
        Next

        'draw currency items
        For Each currency In Game.activeCurrencyItems
            currency.DrawCurrency(e.Graphics, currency)
        Next

        ' Update the position of each active bullet and draw it on the screen
        For Each bullet In Game.activeBullets
            bullet.Move()
            bullet.Draw(e.Graphics)
        Next

        ' Update the position of each active enemy and draw it on the screen
        For Each enemy In Game.activeEnemies
            enemy.Draw(e.Graphics)
            enemy.DrawLifeBar(e.Graphics)
            enemy.moveTowardsPlayer(Game.player1.playerX, Game.player1.playerY)
        Next

        For Each enemy In Game.explodingEnemies
            enemy.Draw(e.Graphics)
            enemy.DrawLifeBar(e.Graphics)
            enemy.moveTowardsPlayer(Game.player1.playerX, Game.player1.playerY)
        Next

        'Draw explosions
        For Each expl In Game.activeExplosions
            expl.Draw(e.Graphics)
        Next
        For Each explG In Game.activeExplosionsG
            explG.Draw(e.Graphics)
        Next

        ' Remove any bullets that are offscreen, move to other class
        Game.activeBullets.RemoveAll(Function(bullet) bullet.X < 0 OrElse bullet.X > Game.ClientSize.Width OrElse bullet.Y < 0 OrElse bullet.Y > Game.ClientSize.Height)

        'draw player
        e.Graphics.DrawImage(playerImage, Game.player1.playerX, Game.player1.playerY, Game.player1.playerHeight, Game.player1.playerWidth)

        'Draw the inventory icons and amounts
        Dim gemImageWidth As Integer = 50
        Dim gemImageHeight As Integer = 50
        Dim gemImageX As Integer = 5
        Dim gemImageY As Integer = 60
        e.Graphics.DrawImage(gemImages(0), gemImageX, gemImageY, gemImageWidth, gemImageHeight)
        e.Graphics.DrawImage(gemImages(1), gemImageX, gemImageY + 50, gemImageWidth, gemImageHeight)
        e.Graphics.DrawImage(gemImages(2), gemImageX, gemImageY + 100, gemImageWidth, gemImageHeight)
        e.Graphics.DrawString("=  " & Game.player1.blueCurrency, font, Brushes.White, gemImageX + 50, gemImageY + 15)
        e.Graphics.DrawString("=  " & Game.player1.greenCurrency, font, Brushes.White, gemImageX + 50, gemImageY + 65)
        e.Graphics.DrawString("=  " & Game.player1.redCurrency, font, Brushes.White, gemImageX + 50, gemImageY + 115)

        ' Draw the health bar in the upper left corner of the screen
        Dim healthBarWidth As Integer = 200
        Dim healthBarHeight As Integer = 20
        Dim healthBarX As Integer = 10
        Dim healthBarY As Integer = 10

        ' Calculate the percentage of health remaining for the player
        Dim healthPercent As Double = Game.player1.health / Game.player1.maxHealth

        ' Draw the background of the health bar
        e.Graphics.FillRectangle(healthBackColor, healthBarX, healthBarY, healthBarWidth, healthBarHeight)

        ' Draw the foreground of the health bar based on the player's current health level
        Dim healthBarForegroundWidth As Integer = CInt(healthPercent * healthBarWidth)
        Dim healthBarForegroundHeight As Integer = healthBarHeight

        e.Graphics.FillRectangle(healthColor, healthBarX, healthBarY, healthBarForegroundWidth, healthBarForegroundHeight)

        ' Draw the current health level and max health level of the player
        e.Graphics.DrawString("Health: " & Game.player1.health & "/" & Game.player1.maxHealth, font, Brushes.White, healthBarX, healthBarY + healthBarHeight + 5)

    End Sub
End Class
