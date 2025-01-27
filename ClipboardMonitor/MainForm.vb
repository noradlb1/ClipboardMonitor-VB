Imports System
Imports System.Drawing
Imports System.Linq
Imports System.Reactive.Disposables
Imports System.Reactive.Linq
Imports System.Reactive.Subjects
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Namespace ClipboardMonitor
    Partial Public Class MainForm
        Inherits Form
        ''' <summary>
        '''  A description of the regular expression:
        '''
        '''  href="
        '''      href="
        '''  [1]: A numbered capture group. [.*?]
        '''      Any character, any number of repetitions, as few as possible
        '''  "
        ''' </summary>
        Private Shared ReadOnly Hrefs As Regex = New Regex("href=""(.*?)""", RegexOptions.CultureInvariant Or RegexOptions.Compiled)

        Private _lastText As String
        Private _timeouts As Subject(Of Integer)
        Private _defaultBackground As Color
        Private _disp As IDisposable

        Public Sub New()
            InitializeComponent()

            _timeouts = New Subject(Of Integer)()
        End Sub

        Protected Overrides Sub OnClosed(e As EventArgs)
            _disp.Dispose()
            MyBase.OnClosed(e)
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

            Select Case m.Msg
                Case WM_CLIPBOARDUPDATE
                    Dim thisText = GetLinksOrText()
                    If Not String.IsNullOrWhiteSpace(thisText) Then
                        If Not Equals(_lastText, thisText) Then
                            _timeouts.OnNext(42)
                            txtList.AppendText(thisText & Environment.NewLine)
                            notifyIcon.ShowBalloonTip(500, "Link received", thisText, ToolTipIcon.Info)
                        End If

                        _lastText = thisText
                    End If
                    m.Result = IntPtr.Zero
            End Select
        End Sub

        Private Shared Function GetLinksOrText() As String
            Try
                If Not Clipboard.ContainsText(TextDataFormat.Html) Then
                    Return Clipboard.GetText()
                End If

                Dim html = Clipboard.GetText(TextDataFormat.Html)
                Dim hrefs = MainForm.Hrefs.Matches(html).Cast(Of Match)().[Select](Function(x) x.Groups(1).Value)

                Return String.Join(Environment.NewLine, hrefs)
            Catch __unusedExternalException1__ As ExternalException
                Return Nothing
            End Try
        End Function

        Private Sub btnClear_Click(sender As Object, e As EventArgs)
            If MessageBox.Show("Do you really want to clear?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                txtList.Text = ""
            End If
        End Sub

        Private Sub btnCopy_Click(sender As Object, e As EventArgs)
            If Not String.IsNullOrEmpty(txtList.Text) Then
                Clipboard.SetText(txtList.Text)
            End If
        End Sub

        Private Sub MainForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles MyBase.FormClosed
            ' استدعاء RemoveClipboardFormatListener من NativeMethods
            ClipboardMonitor.NativeMethods.RemoveClipboardFormatListener(Handle)
        End Sub


        Private Sub MainForm_Shown(ByVal sender As Object, ByVal e As EventArgs)
            While Not NativeMethods.AddClipboardFormatListener(Handle)

                If MessageBox.Show("Could not set up clipboard monitor", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.[Error], MessageBoxDefaultButton.Button1) = DialogResult.Cancel Then
                    Return
                End If
            End While

            _defaultBackground = BackColor
            _disp = New CompositeDisposable(_timeouts.ObserveOn(Me).[Do](Function(__) CSharpImpl.__Assign(BackColor, Color.LightGreen)).Delay(TimeSpan.FromMilliseconds(500)).ObserveOn(Me).[Do](Function(__) CSharpImpl.__Assign(BackColor, _defaultBackground)).Subscribe())
        End Sub

        Private Class CSharpImpl
            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class
End Namespace
