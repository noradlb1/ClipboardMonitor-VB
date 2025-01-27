Imports System

Namespace ClipboardMonitor
    Partial Class MainForm
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <paramname="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            components = New ComponentModel.Container()
            Dim btnCopy As Windows.Forms.Button
            Dim btnClear As Windows.Forms.Button
            Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(MainForm))
            txtList = New Windows.Forms.TextBox()
            notifyIcon = New Windows.Forms.NotifyIcon(components)
            btnCopy = New Windows.Forms.Button()
            btnClear = New Windows.Forms.Button()
            SuspendLayout()
            '
            ' btnCopy
            '
            btnCopy.Anchor = Windows.Forms.AnchorStyles.Bottom Or Windows.Forms.AnchorStyles.Right
            btnCopy.Location = New Drawing.Point(564, 377)
            btnCopy.Name = "btnCopy"
            btnCopy.Size = New Drawing.Size(75, 23)
            btnCopy.TabIndex = 1
            btnCopy.Text = "Copy"
            btnCopy.UseVisualStyleBackColor = True
            AddHandler btnCopy.Click, New EventHandler(AddressOf btnCopy_Click)
            '
            ' btnClear
            '
            btnClear.Anchor = Windows.Forms.AnchorStyles.Bottom Or Windows.Forms.AnchorStyles.Right
            btnClear.Location = New Drawing.Point(483, 377)
            btnClear.Name = "btnClear"
            btnClear.Size = New Drawing.Size(75, 23)
            btnClear.TabIndex = 2
            btnClear.Text = "Clear"
            btnClear.UseVisualStyleBackColor = True
            AddHandler btnClear.Click, New EventHandler(AddressOf btnClear_Click)
            '
            ' txtList
            '
            txtList.Anchor = Windows.Forms.AnchorStyles.Top Or Windows.Forms.AnchorStyles.Bottom Or Windows.Forms.AnchorStyles.Left Or Windows.Forms.AnchorStyles.Right
            txtList.Location = New Drawing.Point(12, 12)
            txtList.Multiline = True
            txtList.Name = "txtList"
            txtList.ScrollBars = Windows.Forms.ScrollBars.Both
            txtList.Size = New Drawing.Size(627, 359)
            txtList.TabIndex = 0
            '
            ' notifyIcon
            '
            notifyIcon.Icon = CType(resources.GetObject("notifyIcon.Icon"), Drawing.Icon)
            notifyIcon.Visible = True
            '
            ' MainForm
            '
            AutoScaleDimensions = New Drawing.SizeF(6.0F, 13.0F)
            AutoScaleMode = Windows.Forms.AutoScaleMode.Font
            ClientSize = New Drawing.Size(651, 409)
            Controls.Add(btnClear)
            Controls.Add(btnCopy)
            Controls.Add(txtList)
            Icon = CType(resources.GetObject("$this.Icon"), Drawing.Icon)
            Name = "MainForm"
            Text = "Clipboard Monitor"
            AddHandler FormClosed, New Windows.Forms.FormClosedEventHandler(AddressOf MainForm_FormClosed)
            AddHandler Shown, New EventHandler(AddressOf MainForm_Shown)
            ResumeLayout(False)
            PerformLayout()

        End Sub

#End Region

        Private txtList As Windows.Forms.TextBox
        Private notifyIcon As Windows.Forms.NotifyIcon
    End Class
End Namespace
