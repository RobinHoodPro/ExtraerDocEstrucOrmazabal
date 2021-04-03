Imports System.IO
Imports iText.IO.Font
Imports iText.Kernel.Colors
Imports iText.Kernel.Font
Imports iText.Kernel.Pdf
Imports iText.Layout
Imports iText.Layout.Borders
Imports iText.Layout.Element
Imports iText.Layout.Properties.UnitValue

Public Class ClsTablaPdf
    Private listaTextoTch As List(Of String)
    Private archConfig As List(Of String) = File.ReadLines("Pdf.txt").ToList
    Private writer As PdfWriter
    Private pdf As PdfDocument
    Private documento As Document
    Private fuente As PdfFont
    Private fuenteBold As PdfFont
    Private tamañoFuente As String
    Private colorFuente As Color
    Private colorFuenteTitulo As Color
    Private estilo As Style
    Private estiloTitulo As Style
    Private colorBordeTabla As Color
    Private colorBordeCelda As Color
    Private docMarginTop As Integer
    Private docMarginRight As Integer
    Private docMarginBottom As Integer
    Private docMarginLeft As Integer
    Sub New(ByVal archivo As String, ByVal listaTextoTch As List(Of String))
        Me.listaTextoTch = listaTextoTch
        writer = New PdfWriter(archivo)
        pdf = New PdfDocument(writer)
        documento = New Document(pdf)
        fuente = getFuente(getValorArchivo(archConfig(0)))
        fuenteBold = getFuente(getValorArchivo(archConfig(1)))
        tamañoFuente = getValorArchivo(archConfig(2))
        colorFuente = stringColor(getValorArchivo(archConfig(3)))
        colorFuenteTitulo = stringColor(getValorArchivo(archConfig(4)))
        estilo = getEstilo(fuente, tamañoFuente, colorFuente, 0)
        estiloTitulo = getEstilo(fuenteBold, tamañoFuente, colorFuenteTitulo, 1)
        colorBordeTabla = stringColor(getValorArchivo(archConfig(5)))
        colorBordeCelda = stringColor(getValorArchivo(archConfig(10)))
        docMarginTop = getValorArchivo(archConfig(6))
        docMarginRight = getValorArchivo(archConfig(7))
        docMarginBottom = getValorArchivo(archConfig(8))
        docMarginLeft = getValorArchivo(archConfig(9))
        documento.SetMargins(docMarginTop, docMarginRight, docMarginBottom, docMarginLeft)
    End Sub
    Sub New(ByVal archivo As String)
        writer = New PdfWriter(archivo)
        pdf = New PdfDocument(writer)
        documento = New Document(pdf)
        fuente = getFuente(getValorArchivo(archConfig(0)))
        fuenteBold = getFuente(getValorArchivo(archConfig(1)))
        tamañoFuente = getValorArchivo(archConfig(2))
        colorFuente = stringColor(getValorArchivo(archConfig(3)))
        colorFuenteTitulo = stringColor(getValorArchivo(archConfig(4)))
        estilo = getEstilo(fuente, tamañoFuente, colorFuente, 0)
        estiloTitulo = getEstilo(fuenteBold, tamañoFuente, colorFuenteTitulo, 1)
        colorBordeTabla = stringColor(getValorArchivo(archConfig(5)))
        colorBordeCelda = stringColor(getValorArchivo(archConfig(10)))
        docMarginTop = getValorArchivo(archConfig(6))
        docMarginRight = getValorArchivo(archConfig(7))
        docMarginBottom = getValorArchivo(archConfig(8))
        docMarginLeft = getValorArchivo(archConfig(9))
        documento.SetMargins(docMarginTop, docMarginRight, docMarginBottom, docMarginLeft)
    End Sub
    Private Function getValorArchivo(ByVal palabra As String) As String
        Return Right(palabra, palabra.Length - InStrRev(palabra, ":"))
    End Function
    Private Function getFuente(ByVal rutaFuente As String) As PdfFont
        Dim fuenteExterna As FontProgram = FontProgramFactory.CreateFont(rutaFuente)
        Return PdfFontFactory.CreateFont(fuenteExterna, PdfEncodings.WINANSI, True)
    End Function
    Private Function stringColor(ByVal mycolor As String) As Color
        Dim valores() As String = Split(mycolor, ",")
        Return New DeviceRgb(CInt(valores(0)), CInt(valores(1)), CInt(valores(2)))
    End Function
    Private Function getEstilo(ByVal fuente As PdfFont, ByVal tamaño As String, ByVal colorFuente As Color,
                               Optional ByVal alineacion As Integer = 0) As Style
        Dim myEstilo As New Style
        With myEstilo
            .SetFont(fuente) : .SetFontSize(tamaño) : .SetFontColor(colorFuente) : .SetTextAlignment(alineacion)
        End With
        Return myEstilo
    End Function
    Function getTabla(Optional ByVal numcol As Integer = 1,
                              Optional ByVal grosor As Integer = 2, Optional ByVal anchoTabla As Integer = 100) As Table
        Dim mytabla As New Table(numcol)
        mytabla.SetWidth(CreatePercentValue(anchoTabla))
        mytabla.SetBorder(New SolidBorder(colorBordeTabla, grosor))
        Return mytabla
    End Function
    Function getMyCelda(ByVal texto As String, Optional ByVal grosor As Integer = 1) As Cell
        Dim celda As New Cell
        celda.SetBorder(New SolidBorder(colorBordeCelda, grosor))
        celda.AddStyle(estilo)
        celda.Add(New Paragraph(texto))
        Return celda
    End Function
    Function getMyCelda(ByVal tabla As Table) As Cell
        Dim celda As New Cell
        celda.SetBorder(New SolidBorder(colorBordeCelda, 1))
        celda.AddStyle(estilo)
        celda.Add(tabla)
        Return celda
    End Function
    Function getMyCeldaTitulo(ByVal texto As String) As Cell
        Dim celda As New Cell
        celda.SetBorder(New SolidBorder(colorBordeTabla, 2))
        celda.AddStyle(estiloTitulo)
        celda.Add(New Paragraph(texto))
        Return celda
    End Function
    Function getDoc() As Document
        Return documento
    End Function
    Function getBordeCelda() As Border
        Return New SolidBorder(colorBordeCelda, 1)
    End Function

    Function ancho(ByVal cadena As String) As Properties.UnitValue
        Dim texto As Paragraph = New Paragraph(cadena)
        texto.AddStyle(estilo)
        Return texto.GetWidth()
    End Function
End Class
