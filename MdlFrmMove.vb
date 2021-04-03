Module MdlFrmMove
    Private Const WM_SYSCOMMAND As Integer = &H112&
    Private Const MOUSE_MOVE As Integer = &HF012&
    ' Declaraciones del API al estilo .NET
    <System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
    Private Sub ReleaseCapture()
    End Sub
    <System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint:="SendMessage")>
    Private Sub SendMessage(
        ByVal hWnd As System.IntPtr, ByVal wMsg As Integer,
        ByVal wParam As Integer, ByVal lParam As Integer
        )
    End Sub
    ' función privada usada para mover el formulario actual
    Public Sub moverForm(ByVal frm As Form)
        ReleaseCapture()
        SendMessage(frm.Handle, WM_SYSCOMMAND, MOUSE_MOVE, 0)
    End Sub
End Module
