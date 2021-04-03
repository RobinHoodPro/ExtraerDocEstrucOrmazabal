Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Module MdlTituloFuenteBorde
    Private listaIndices As New List(Of Integer) From {0, 0, 0, 0}
    'Carga En La Lista 'ListaIndices' Los Valores Pasados Como Parametros
    Sub cargarIndices(ByVal valor1 As Integer, ByVal valor2 As Integer, ByVal valor3 As Integer,
                              ByVal valor4 As Integer)
        listaIndices(0) = valor1 : listaIndices(1) = valor2 : listaIndices(2) = valor3 : listaIndices(3) = valor4
    End Sub
    'Devuelve Un Rango De Celdas De Excel
    Private Function rango(ByVal osheet As Worksheet) As Range
        Return osheet.Range(osheet.Cells(listaIndices(0), listaIndices(1)), osheet.Cells(listaIndices(2), listaIndices(3)))
    End Function
    'Formato De Titulo A Aplicar A Un Rango De Celdas De Excel
    Private Sub formatoTitulo(ByVal rango As Range)
        With rango
            .Merge() : .HorizontalAlignment = 3
        End With
    End Sub
    'Aplica Un Formato De Titulo A Un Rango De Celdas De Excel
    Private Sub titulo(ByVal osheet As Worksheet)
        formatoTitulo(rango(osheet))
    End Sub
    'Formato De Fuente A Aplicar A Un Rango De Celdas De Excel
    Private Sub formatoFuente(ByVal rango As Range)
        With rango.Font
            .Name = "Verdana" : .Bold = True : .Size = 10
        End With
    End Sub
    'Aplica Un Formato De Fuente A Un Rango De Celdas De Excel
    Private Sub fuente(ByVal osheet As Worksheet)
        formatoFuente(rango(osheet))
    End Sub
    'Formato De Borde A Aplicar A Un Rango De Celdas De Excel
    Private Sub formatoBorde(ByVal rango As Range)
        With rango
            .Borders.Weight = XlBorderWeight.xlThick
            .Borders(XlBordersIndex.xlInsideHorizontal).Weight = XlBorderWeight.xlMedium
            .Borders(XlBordersIndex.xlInsideVertical).Weight = XlBorderWeight.xlThin
        End With
    End Sub
    Private Sub formatoBordeExterior(ByVal rango As Range)
        rango.Borders.Weight = XlBorderWeight.xlThick
        rango.Borders(XlBordersIndex.xlInsideHorizontal).LineStyle = XlLineStyle.xlLineStyleNone
        rango.Borders(XlBordersIndex.xlInsideVertical).LineStyle = XlLineStyle.xlLineStyleNone
    End Sub
    'Aplica Un Formato De Borde A Un Rango De Celdas De Excel
    Sub borde(ByVal osheet As Worksheet)
        formatoBorde(rango(osheet))
    End Sub
    Sub bordeExterior(ByVal osheet As Worksheet)
        formatoBordeExterior(rango(osheet))
    End Sub
    'Aplica Formato De Titulo, Fuente Y Borde A Un Rango De Celdas De Excel
    Sub tituloFuenteBorde(ByVal osheet As Worksheet)
        titulo(osheet) : fuente(osheet) : borde(osheet)
    End Sub
End Module
