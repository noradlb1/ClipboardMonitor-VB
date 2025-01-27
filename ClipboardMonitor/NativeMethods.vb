Imports System
Imports System.Runtime.InteropServices

Namespace ClipboardMonitor
    Public Class NativeMethods
        ' استيراد الدوال من مكتبة user32.dll
        <DllImport("user32.dll", SetLastError:=True)>
        Public Shared Function AddClipboardFormatListener(ByVal hwnd As IntPtr) As Boolean
        End Function

        <DllImport("user32.dll", SetLastError:=True)>
        Public Shared Function RemoveClipboardFormatListener(ByVal hwnd As IntPtr) As Boolean
        End Function
    End Class
End Namespace
