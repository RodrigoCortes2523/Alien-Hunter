Imports System.Data.OleDb
Public Class Shop
    Dim SQLStatement As String = ""
    Dim connectionString As String = "Provider = Microsoft.ACE.OLEDB.12.0;" & "Data Source =C:\Users\Rod\Desktop\gameDB1.accdb"
    Dim dbConnection As New OleDbConnection(connectionString)

    Private Sub Shop_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        healthLabel.Text = Game.player1.health
        mHealthLabel.Text = Game.player1.maxHealth
        damageLabel.Text = Game.player1.damage
        speedLabel.Text = Game.player1.playerMoveSpeed
        bSpeedLabel.Text = Game.player1.shotSpeed
        cMultiLabel.Text = Game.player1.currenctyMultiplier
        blueGemLabel.Text = Game.player1.blueCurrency
        greenGemLabel.Text = Game.player1.greenCurrency
        redGemLabel.Text = Game.player1.redCurrency

        If Game.player1.health <= 30 Then
            healthLabel.ForeColor = Color.Red
        End If

        'load items into listbox
        SQLStatement = "SELECT shopItems.name, shopItems.Price " &
                       "FROM shopItems "

        Dim dtProperties As New DataTable
        Dim dbDataAdapter As OleDbDataAdapter = New OleDbDataAdapter(SQLStatement, connectionString)
        dbDataAdapter.Fill(dtProperties)

        For Each row As DataRow In dtProperties.Rows
            ListBox1.Items.Add(row("name") & " - " & row("price"))
        Next

        ListBox1.SelectedIndex = 0
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        SQLStatement = "SELECT shopItems.Description, shopItems.ImagePath, shopItems.Quantity " &
                 "FROM shopItems " &
                 "WHERE shopItems.Name = @itemName"

        Dim dtProperties As New DataTable
        Dim dbDataAdapter As OleDbDataAdapter = New OleDbDataAdapter(SQLStatement, connectionString)
        dbDataAdapter.SelectCommand.Parameters.AddWithValue("@itemName", ListBox1.SelectedItem.ToString().Split("-")(0).Trim())
        dbDataAdapter.Fill(dtProperties)

        If dtProperties.Rows.Count > 0 Then
            Dim selectedDescription As String = dtProperties.Rows(0)("Description").ToString()
            Dim imageName As String = dtProperties.Rows(0)("ImagePath").ToString
            Dim itemQuantity As String = dtProperties.Rows(0)("Quantity").ToString
            descriptionLabel.Text = selectedDescription
            PictureBox1.Image = My.Resources.ResourceManager.GetObject(imageName)
            quantityLabel.Text = "Quantity Remaining: " & itemQuantity
        Else
            descriptionLabel.Text = ""
        End If
    End Sub

    Private Sub buyButton_Click(sender As Object, e As EventArgs) Handles buyButton.Click

        Dim selectedItemName As String = ListBox1.SelectedItem.ToString().Split("-")(0).Trim()

        SQLStatement = "SELECT shopItems.Value, shopItems.Quantity " &
                   "FROM shopItems " &
                   "WHERE shopItems.Name = @itemName"

        Dim dtProperties As New DataTable
        Dim dbDataAdapter As OleDbDataAdapter = New OleDbDataAdapter(SQLStatement, connectionString)
        dbDataAdapter.SelectCommand.Parameters.AddWithValue("@itemName", selectedItemName)
        dbDataAdapter.Fill(dtProperties)

        If dtProperties.Rows.Count > 0 Then
            Dim itemValue As Integer = dtProperties.Rows(0)("Value")
            Dim itemQuantity As Integer = dtProperties.Rows(0)("Quantity")

            If selectedItemName = "Health Potion" Then
                If Game.player1.blueCurrency >= 5 Then
                    Game.player1.health += itemValue
                    Game.player1.blueCurrency -= 5

                    openConnection(itemQuantity, selectedItemName)

                    healthLabel.Text = Game.player1.health.ToString()
                    healthLabel.ForeColor = Color.ForestGreen
                Else
                    descriptionLabel.Text = "Not enoudgh gems"
                End If
            End If

            If selectedItemName = "Sword" Then
                If Game.player1.blueCurrency >= 5 Then
                    Game.player1.damage += itemValue
                    Game.player1.blueCurrency -= 5

                    openConnection(itemQuantity, selectedItemName)

                    damageLabel.Text = Game.player1.damage.ToString()
                    damageLabel.ForeColor = Color.ForestGreen
                Else
                    descriptionLabel.Text = "Not enough gems"
                End If
            End If

            If selectedItemName = "Bullet Speed" Then
                If Game.player1.blueCurrency >= 3 Then
                    Game.player1.shotSpeed += itemValue
                    Game.player1.blueCurrency -= 3

                    openConnection(itemQuantity, selectedItemName)

                    bSpeedLabel.Text = Game.player1.shotSpeed.ToString()
                    bSpeedLabel.ForeColor = Color.ForestGreen
                Else
                    descriptionLabel.Text = "Not enough gems"
                End If
            End If

            If selectedItemName = "Speed Boots" Then
                If Game.player1.blueCurrency >= 2 Then
                    Game.player1.playerMoveSpeed += itemValue
                    Game.player1.blueCurrency -= 2

                    openConnection(itemQuantity, selectedItemName)

                    speedLabel.Text = Game.player1.playerMoveSpeed.ToString()
                    speedLabel.ForeColor = Color.ForestGreen
                Else
                    descriptionLabel.Text = "Not enough gems"
                End If
            End If

            If selectedItemName = "Max Health" Then
                If Game.player1.blueCurrency >= 10 Then
                    Game.player1.maxHealth += itemValue
                    Game.player1.blueCurrency -= 10

                    openConnection(itemQuantity, selectedItemName)

                    mHealthLabel.Text = Game.player1.maxHealth.ToString()
                    mHealthLabel.ForeColor = Color.ForestGreen
                Else
                    descriptionLabel.Text = "Not enough gems"
                End If
            End If
        End If
    End Sub

    Private Sub nextStageButton_Click(sender As Object, e As EventArgs) Handles nextButton.Click
        Game.currentStage = Game.stages(Game.currentStageIndex)
        Game.gameLoopTimer.Start()
        Game.enemySpawner.Start()
        Game.activeEnemies.Clear()
        Game.activeBullets.Clear()
        Game.timeTimer.Start()
        Game.stageLabel.Text = Game.currentStageIndex + 1
        Me.Close()
        Game.Show()
    End Sub

    Private Sub openConnection(q As Integer, name As String)
        q -= 1
        SQLStatement = "UPDATE shopItems SET Quantity=@newQuantity WHERE Name=@itemName"
        Dim dbCommand As New OleDbCommand(SQLStatement, dbConnection)
        dbCommand.Parameters.AddWithValue("@newQuantity", q)
        dbCommand.Parameters.AddWithValue("@itemName", name)
        dbConnection.Open()
        dbCommand.ExecuteNonQuery()
        dbConnection.Close()

        quantityLabel.Text = "Quantity Remaining: " & q
        blueGemLabel.Text = Game.player1.blueCurrency
    End Sub
End Class