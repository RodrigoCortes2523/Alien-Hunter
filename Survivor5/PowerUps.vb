Public Class PowerUps
    Public Property X As Integer
    Public Property Y As Integer
    Public Property Width As Integer
    Public Property Height As Integer
    Public Property Health As Integer
    Public Property maxHealth As Integer
    Public Property image As Image
    Public Property rec As Rectangle

    Dim random As New Random

    Public Sub createShell(array As Array, image As Image, health As Integer)
        ' create health shell
        For i As Integer = 0 To array.Length - 1
            array(i) = New PowerUps()
            array(i).image = image
            array(i).health = health
            array(i).maxhealth = health
            array(i).width = 100
            array(i).height = 100
            array(i).X = random.Next(0, Game.Width - 200)
            array(i).Y = random.Next(0, Game.Height - 200)
            array(i).rec = New Rectangle(array(i).X, array(i).Y, Width, Height)
        Next
    End Sub

    Public Sub DrawLifeBar(g As Graphics)
        ' Draw life bar
        Dim lifeBarWidth As Integer = 80
        Dim lifeBarHeight As Integer = 5
        Dim lifeBarX As Integer = X + 10
        Dim lifeBarY As Integer = Y + 80
        Dim lifeRemaining As Integer = CInt((Health / maxHealth) * lifeBarWidth)
        g.FillRectangle(Brushes.Green, lifeBarX, lifeBarY, lifeRemaining, lifeBarHeight)
        g.DrawRectangle(Pens.Black, lifeBarX, lifeBarY, lifeBarWidth, lifeBarHeight)
    End Sub

    'check for when a bullet in the list intersectst an item in the array
    Public Sub shellBulletCollisionCheck(list As List(Of Bullet))

        Dim bounds As New Rectangle(X, Y, Width, Height) 'sets rectangle location of shell

        For Each bullet In list
            If bounds.IntersectsWith(New Rectangle(bullet.X, bullet.Y, bullet.bulletWidth, bullet.bulletHeight)) Then
                Health -= Game.player1.damage
                list.Remove(bullet)
                Exit For
            End If
        Next
    End Sub

End Class
