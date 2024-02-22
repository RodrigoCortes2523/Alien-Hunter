<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Game
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.stageLabel = New System.Windows.Forms.Label()
        Me.timeLabel = New System.Windows.Forms.Label()
        Me.moveRightTimer = New System.Windows.Forms.Timer(Me.components)
        Me.moveLeftTimer = New System.Windows.Forms.Timer(Me.components)
        Me.moveUpTimer = New System.Windows.Forms.Timer(Me.components)
        Me.moveDownTimer = New System.Windows.Forms.Timer(Me.components)
        Me.gameLoopTimer = New System.Windows.Forms.Timer(Me.components)
        Me.enemySpawner = New System.Windows.Forms.Timer(Me.components)
        Me.timeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.flySpawner = New System.Windows.Forms.Timer(Me.components)
        Me.enemy3Spawner = New System.Windows.Forms.Timer(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.enemy4Spawner = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Asus Rog", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(426, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(170, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Wave:"
        '
        'stageLabel
        '
        Me.stageLabel.AutoSize = True
        Me.stageLabel.BackColor = System.Drawing.Color.Transparent
        Me.stageLabel.Font = New System.Drawing.Font("Cooper Black", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.stageLabel.ForeColor = System.Drawing.Color.White
        Me.stageLabel.Location = New System.Drawing.Point(614, -3)
        Me.stageLabel.Name = "stageLabel"
        Me.stageLabel.Size = New System.Drawing.Size(53, 55)
        Me.stageLabel.TabIndex = 0
        Me.stageLabel.Text = "1"
        '
        'timeLabel
        '
        Me.timeLabel.AutoSize = True
        Me.timeLabel.BackColor = System.Drawing.Color.Transparent
        Me.timeLabel.Font = New System.Drawing.Font("Cooper Black", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.timeLabel.ForeColor = System.Drawing.Color.White
        Me.timeLabel.Location = New System.Drawing.Point(516, 48)
        Me.timeLabel.Name = "timeLabel"
        Me.timeLabel.Size = New System.Drawing.Size(53, 55)
        Me.timeLabel.TabIndex = 0
        Me.timeLabel.Text = "0"
        '
        'moveRightTimer
        '
        Me.moveRightTimer.Interval = 5
        '
        'moveLeftTimer
        '
        Me.moveLeftTimer.Interval = 5
        '
        'moveUpTimer
        '
        Me.moveUpTimer.Interval = 5
        '
        'moveDownTimer
        '
        Me.moveDownTimer.Interval = 5
        '
        'gameLoopTimer
        '
        Me.gameLoopTimer.Interval = 15
        '
        'enemySpawner
        '
        Me.enemySpawner.Interval = 4000
        '
        'timeTimer
        '
        Me.timeTimer.Enabled = True
        Me.timeTimer.Interval = 1000
        '
        'flySpawner
        '
        Me.flySpawner.Interval = 5000
        '
        'enemy3Spawner
        '
        Me.enemy3Spawner.Interval = 6000
        '
        'Timer1
        '
        Me.Timer1.Interval = 2000
        '
        'enemy4Spawner
        '
        Me.enemy4Spawner.Interval = 4000
        '
        'Game
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Gray
        Me.BackgroundImage = Global.Survivor5.My.Resources.Resources.purpleNebula
        Me.ClientSize = New System.Drawing.Size(1106, 823)
        Me.Controls.Add(Me.timeLabel)
        Me.Controls.Add(Me.stageLabel)
        Me.Controls.Add(Me.Label1)
        Me.DoubleBuffered = True
        Me.Name = "Game"
        Me.Text = "Game"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents stageLabel As Label
    Friend WithEvents timeLabel As Label
    Friend WithEvents moveRightTimer As Timer
    Friend WithEvents moveLeftTimer As Timer
    Friend WithEvents moveUpTimer As Timer
    Friend WithEvents moveDownTimer As Timer
    Friend WithEvents gameLoopTimer As Timer
    Friend WithEvents enemySpawner As Timer
    Friend WithEvents timeTimer As Timer
    Friend WithEvents flySpawner As Timer
    Friend WithEvents enemy3Spawner As Timer
    Friend WithEvents Timer1 As Timer
    Friend WithEvents enemy4Spawner As Timer
End Class
