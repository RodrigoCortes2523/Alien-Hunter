Imports System.Data.OleDb

Public Class Victory
    Dim SQLStatement As String = ""
    Dim connectionString As String = "Provider = Microsoft.ACE.OLEDB.12.0;" & "Data Source =C:\Users\Rod\Desktop\gameDB1.accdb"
    Dim dbConnection As New OleDbConnection(connectionString)
    Dim score As Integer

    Private Sub Victory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load ending game variables
        score = Game.currentStageIndex + 1
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DataGridView1.Visible = True

        SQLStatement = "SELECT Scores.name, Scores.Score " &
                      "FROM Scores "

        Dim dtProperties As New DataTable
        Dim dbDataAdapter As OleDbDataAdapter = New OleDbDataAdapter(SQLStatement, connectionString)
        dbDataAdapter.Fill(dtProperties)
        DataGridView1.DataSource = dtProperties
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim SQLInsert As String = "INSERT INTO Scores (name, score) VALUES (?, ?)"
        Dim dbCommand As New OleDbCommand(SQLInsert, dbConnection)

        Dim name As String = TextBox1.Text

        dbCommand.Parameters.AddWithValue("name", name)
        dbCommand.Parameters.AddWithValue("score", score)

        dbConnection.Open()
        dbCommand.ExecuteNonQuery()
        dbConnection.Close()

        TextBox1.Enabled = False
        Button1.Enabled = False
    End Sub
End Class