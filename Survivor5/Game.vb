Public Class Game
    Public player1 As New Player
    Private painter As New Painter()
    Public activeBullets As New List(Of Bullet)
    Public activeEnemies As New List(Of Enemy)
    Public explodingEnemies As New List(Of Enemy)

    Public activeCurrencyItems As New List(Of Currency)
    Public activeDropItems As New List(Of Drop)
    Public activeIndicators As New List(Of Indicator)
    Public activeExplosions As New List(Of Explosion)
    Public activeExplosionsG As New List(Of Explosion)

    'temporary lists
    Public newEnemies As New List(Of Enemy)
    Public newEnemies2 As New List(Of Enemy)

    'array example
    Public shell(1) As PowerUps

    Dim powerup As New PowerUps()
    Public random As New Random

    Public stages As New List(Of Stage) From {
                New Stage(20), ' Stage 1 lasts 20 seconds
                New Stage(30), ' Stage 2 lasts 30 seconds
                New Stage(30), ' Stage 3 lasts 45 seconds
                New Stage(30)  'Final Stage
}
    Public currentStageIndex As Integer = 0
    Public currentStage As Stage = stages(currentStageIndex)
    Public Shared enemySpawnTimer As Integer = 0

    'images
    Public enemyImages() As Image = {My.Resources.enemy4, My.Resources.enemy2, My.Resources.enemy3, My.Resources.circle}
    Private backgroundImages() As Image = {My.Resources.purpleNebula, My.Resources.blueNebula, My.Resources.blueNebula2, My.Resources.blueNebula3}
    Public dropImages() As Image = {My.Resources.heart1}
    Public shellImage As Image = My.Resources.shell
    Private explosionImages() As Image = {My.Resources._0001, My.Resources._0002, My.Resources._0003,
        My.Resources._0004, My.Resources._0005, My.Resources._0006, My.Resources._0007, My.Resources._0008, My.Resources._0009,
        My.Resources._0010, My.Resources._0011, My.Resources._0012, My.Resources._0013, My.Resources._0014, My.Resources._0015,
        My.Resources._0016, My.Resources._0017, My.Resources._0018, My.Resources._0019}
    Private explosionImagesG() As Image = {My.Resources._1, My.Resources._2, My.Resources._3, My.Resources._4, My.Resources._5,
        My.Resources._6, My.Resources._7, My.Resources._8, My.Resources._9, My.Resources._10, My.Resources._11, My.Resources._1, My.Resources._12,
        My.Resources._13, My.Resources._14, My.Resources._15, My.Resources._16, My.Resources._17, My.Resources._18, My.Resources._19, My.Resources._20,
        My.Resources._21, My.Resources._22, My.Resources._23, My.Resources._24, My.Resources._25, My.Resources._26, My.Resources._0027, My.Resources._0028,
        My.Resources._0029, My.Resources._0030, My.Resources._0031}

    Private Sub Game_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler Me.Paint, AddressOf painter.Paint
        gameLoopTimer.Start()
        enemySpawner.Start()
        powerup.createShell(shell, shellImage, 500)
    End Sub

    Private Sub gameLoopTimer_Tick(sender As Object, e As EventArgs) Handles gameLoopTimer.Tick
        'constantly refreshes
        Refresh()
        'constantly checks for collision events
        bulletCollisionCheck()
        player1.playerCurrencyCollisionCheck()
        player1.playerEnemyCollisionCheck()
        player1.playerDropCollisionCheck()
        player1.lose()
        BackgroundImage = backgroundImages(currentStageIndex) 'move to a different place?
    End Sub
    Private Sub Form1_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        player1.FireWeapon(e.X, e.Y)
    End Sub

    Private Sub Game_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.D
                moveRightTimer.Start()
            Case Keys.A
                moveLeftTimer.Start()
            Case Keys.W
                moveUpTimer.Start()
            Case Keys.S
                moveDownTimer.Start()
        End Select
    End Sub
    Private Sub Game_KeyUP(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        Select Case e.KeyCode
            Case Keys.D
                moveRightTimer.Stop()
            Case Keys.A
                moveLeftTimer.Stop()
            Case Keys.W
                moveUpTimer.Stop()
            Case Keys.S
                moveDownTimer.Stop()
        End Select
    End Sub

    Private Sub moveRightTimer_Tick(sender As Object, e As EventArgs) Handles moveRightTimer.Tick
        player1.moveRight()
    End Sub
    Private Sub moveLeftTimer_Tick(sender As Object, e As EventArgs) Handles moveLeftTimer.Tick
        player1.moveLeft()
    End Sub
    Private Sub moveDownTimer_Tick(sender As Object, e As EventArgs) Handles moveDownTimer.Tick
        player1.moveDown()
    End Sub
    Private Sub moveUpTimer_Tick(sender As Object, e As EventArgs) Handles moveUpTimer.Tick
        player1.moveUp()
    End Sub

    Private Sub enemySpawner_Tick(sender As Object, e As EventArgs) Handles enemySpawner.Tick
        Dim random As New Random
        Dim randomNumber As Integer = random.Next(0, 3)

        For i As Integer = 0 To randomNumber
            Dim newEnemy As New Enemy(enemyImages(1), 100, 1, 10, 0)

            ' Generate random position for the enemy
            Dim x As Integer = random.Next(0, Me.Width - newEnemy.Width)
            Dim y As Integer = random.Next(0, Me.Height - newEnemy.Height)
            newEnemy.X = x
            newEnemy.Y = y

            newEnemy.enemyRec = New Rectangle(newEnemy.X, newEnemy.Y, newEnemy.Height, newEnemy.Width)

            'draw indicator
            Dim indicator As New Indicator(newEnemy.X + 50, newEnemy.Y + 50, 25, Color.Red, 2)
            activeIndicators.Add(indicator)

            'wait 2 seconds

            'add enemy to temporary list
            newEnemies.Add(newEnemy)
            'activeEnemies.Add(newEnemy)
        Next

        Timer1.Start()

        If currentStageIndex = 1 Then
            flySpawner.Start()
        End If
        If currentStageIndex = 2 Then
            enemy3Spawner.Start()
        End If
        If currentStageIndex = 3 Then
            enemy4Spawner.Start()
        End If

        If enemySpawner.Interval > 200 Then
            enemySpawner.Interval -= 100
        Else

        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick ' 2 second delay timer

        For Each newEnemy In newEnemies
            activeEnemies.Add(newEnemy)
        Next
        For Each newEnemy2 In newEnemies2
            explodingEnemies.Add(newEnemy2)
        Next

        activeIndicators.Clear()

        newEnemies.Clear()
        newEnemies2.Clear()
        Timer1.Stop()
    End Sub

    'collision between bullets and enemies, should be moved to bullet class?
    Public Sub bulletCollisionCheck()
        ' Check for collisions with enemies
        For Each enemy In activeEnemies.ToList()
            enemy.CheckCollisions(activeBullets)

            ' Remove the enemy if its health reaches 0
            If enemy.Health <= 0 Then
                activeEnemies.Remove(enemy)

                'draw explosion here
                Dim explosion As New Explosion(enemy.X + 25, enemy.Y + 25, explosionImages)
                activeExplosions.Add(explosion)

                Dim Gem As New Currency
                Gem.X = enemy.X + 50
                Gem.Y = enemy.Y + 50
                Gem.Value = enemy.gemValue + 1
                Gem.gemValue = enemy.gemValue
                Gem.currencyRec = New Rectangle(Gem.X - 50, Gem.Y - 50, Gem.height + 100, Gem.width + 100)
                activeCurrencyItems.Add(Gem)
            End If
        Next

        For Each enemy4 In explodingEnemies.ToList
            enemy4.CheckCollisions(activeBullets)
            If enemy4.Health <= 0 Then

                explodingEnemies.Remove(enemy4)

                'draw new explosion
                Dim explosionG As New Explosion(enemy4.X + 25, enemy4.Y + 25, explosionImagesG)
                activeExplosionsG.Add(explosionG)
            End If
        Next

        For Each item In shell
            item.shellBulletCollisionCheck(activeBullets)
            If item.Health <= 0 Then

                'create drop
                Dim drop As New Drop(dropImages(0))
                drop.X = item.X + 25
                drop.Y = item.Y + 25
                drop.dropRec = New Rectangle(drop.X, drop.Y, drop.Height, drop.Width)
                activeDropItems.Add(drop)
                item.X = -200
                item.Health = 500
            End If
        Next
    End Sub

    Private Sub timeTimer_Tick(sender As Object, e As EventArgs) Handles timeTimer.Tick
        currentStage.Update(Me)
        timeLabel.Text = currentStage.Timer
    End Sub


    Private Sub flySpawner_Tick(sender As Object, e As EventArgs) Handles flySpawner.Tick
        Dim random As New Random
        Dim enemy2 As New Enemy(enemyImages(0), 200, 1, 10, 1)
        Dim x As Integer = random.Next(0, Me.Width - enemy2.Width)
        Dim y As Integer = random.Next(0, Me.Height - enemy2.Height)
        enemy2.X = x
        enemy2.Y = y

        enemy2.enemyRec = New Rectangle(enemy2.X, enemy2.Y, enemy2.Height, enemy2.Width)

        'activeEnemies.Add(enemy2)
        Dim indicator As New Indicator(enemy2.X + 50, enemy2.Y + 50, 45, Color.DeepPink, 2)
        activeIndicators.Add(indicator)
        newEnemies.Add(enemy2)
    End Sub

    Private Sub enemy3Spawner_Tick(sender As Object, e As EventArgs) Handles enemy3Spawner.Tick
        Dim random As New Random
        Dim enemy3 As New Enemy(enemyImages(2), 500, 1, 20, 2)
        Dim x As Integer = random.Next(0, Me.Width - enemy3.Width)
        Dim y As Integer = random.Next(0, Me.Height - enemy3.Height)
        enemy3.X = x
        enemy3.Y = y

        enemy3.enemyRec = New Rectangle(enemy3.X, enemy3.Y, enemy3.Height, enemy3.Width)

        'activeEnemies.Add(enemy3)
        Dim indicator As New Indicator(enemy3.X + 50, enemy3.Y + 50, 75, Color.DarkOrange, 2)
        activeIndicators.Add(indicator)
        newEnemies.Add(enemy3)
    End Sub
    Private Sub enemy4Spawner_Tick(sender As Object, e As EventArgs) Handles enemy4Spawner.Tick
        Dim random As New Random
        Dim enemy4 As New Enemy(enemyImages(3), 800, 2, 50, 2)
        Dim x As Integer = random.Next(0, Me.Width - enemy4.Width)
        Dim y As Integer = random.Next(0, Me.Height - enemy4.Height)
        enemy4.X = x
        enemy4.Y = y

        enemy4.enemyRec = New Rectangle(enemy4.X, enemy4.Y, enemy4.Height, enemy4.Width)

        Dim indicator As New Indicator(enemy4.X + 50, enemy4.Y + 50, 25, Color.Wheat, 2)
        activeIndicators.Add(indicator)
        newEnemies2.Add(enemy4)

    End Sub

    Public Sub showShopScreen()

        If currentStageIndex = 3 Then
            'add win screen
            Victory.Show()

            Me.Close()
        Else
            moveRightTimer.Stop()
            moveLeftTimer.Stop()
            moveUpTimer.Stop()
            moveDownTimer.Stop()
            currentStageIndex += 1
            gameLoopTimer.Stop()
            enemySpawner.Stop()
            timeTimer.Stop()
            activeEnemies.Clear()
            activeBullets.Clear()
            activeIndicators.Clear()
            enemySpawner.Interval = 3000
            Shop.Show()
        End If
    End Sub


End Class