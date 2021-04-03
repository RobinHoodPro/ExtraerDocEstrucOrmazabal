Imports System.IO

Public Class ClsArchConfigDocEstOmzb
    Private archConfig As List(Of String)
    Sub New(ByVal archivo As String)
        archConfig = File.ReadLines(archivo).ToList
    End Sub
    Function getValorConfig(ByVal registro As Integer) As String
        Return Split(archConfig(registro), "=")(1)
    End Function
    Function getDelimitadores(ByVal registro As Integer) As String()
        Return Split(getValorConfig(registro), ",")
    End Function
    Function getListaCopia(ByVal indice As Integer) As List(Of String)
        Return Split(getValorConfig(indice), ",").ToList
    End Function
    Function getNumFilaTitulo() As Integer
        Return getValorConfig(16)
    End Function
    Function getNumCampoPrograma() As Integer
        Return getValorConfig(18)
    End Function
End Class
Public Class barraProgreso
    Private fuentePequeña As Font = New Font("Verdana", 12, FontStyle.Bold)
    Private tituloBarra As String
    Private tiempoBarra As String
    Private pctBarra As String
    Private ptXtitulo As Integer
    Private ptYtituloTiempo As Integer
    Private ptXtiempo As Integer
    Private ptXpct As Integer
    Private ptYpct As Integer
    Private ptXinicioBarra As Integer
    Private ptXfinalBarra As Integer
    Private ptYbarra As Integer
    Private lapizInicial As Pen
    Private lapizFinal As Pen
    Private actualBarra As Integer
    Sub New(ByVal titulo As String, ByVal indice As Integer)
        lapizInicial = New Pen(Color.DarkGray, 15)
        lapizFinal = New Pen(Color.DarkBlue, 15)
        ptXfinalBarra = 525
        ptXinicioBarra = 60
        Me.tituloBarra = titulo
        ptXtitulo = 0
        If indice = 0 Then ptYtituloTiempo = indice Else ptYtituloTiempo = indice * 45
        tiempoBarra = "0''"
        ptXtiempo = ptXfinalBarra - TextRenderer.MeasureText(tiempoBarra, fuentePequeña).Width
        pctBarra = "0%"
        If indice = 0 Then ptYpct = 20 Else ptYpct = (indice * 45) + 20
        ptXpct = (ptXinicioBarra - TextRenderer.MeasureText(pctBarra, fuentePequeña).Width) / 2
        If indice = 0 Then ptYbarra = 30 Else ptYbarra = (indice * 45) + 30
        actualBarra = ptXinicioBarra
    End Sub
    Sub dibujar(ByVal e As PaintEventArgs)
        e.Graphics.DrawString(tituloBarra, fuentePequeña, Brushes.White, ptXtitulo, ptYtituloTiempo)
        e.Graphics.DrawString(tiempoBarra, fuentePequeña, Brushes.White, ptXtiempo, ptYtituloTiempo)
        e.Graphics.DrawString(pctBarra, fuentePequeña, Brushes.White, ptXpct, ptYpct)
        e.Graphics.DrawLine(lapizInicial, ptXinicioBarra, ptYbarra, ptXfinalBarra, ptYbarra)
        e.Graphics.DrawLine(lapizFinal, ptXinicioBarra, ptYbarra, actualBarra, ptYbarra)
    End Sub
    Sub updateBarra(ByVal contIni As Integer, ByVal maxIni As Integer, ByVal tiempo As Stopwatch)
        actualBarra = (((ptXfinalBarra - ptXinicioBarra) / maxIni) * contIni) + ptXinicioBarra
        pctBarra = CInt(contIni / maxIni * 100) & "%"
        ptXpct = (ptXinicioBarra - TextRenderer.MeasureText(pctBarra, fuentePequeña).Width) / 2
        tiempoBarra = CInt(tiempo.Elapsed.TotalSeconds) & "''"
    End Sub
End Class