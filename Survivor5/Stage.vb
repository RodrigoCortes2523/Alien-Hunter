Public Class Stage
    Public Property Duration As Integer
    Public Property Timer As Integer

    Public Sub New(duration As Integer)
        Me.Duration = duration
        Me.Timer = duration
    End Sub

    Public Sub Update(game As Game)
        Timer -= 1
        If Timer <= 0 Then
            ' Stage is complete, show shop screen

            game.showShopScreen()
            game.Hide()
            ' Reset the timer for the next stage
            'Timer = Duration
        End If
    End Sub
End Class
