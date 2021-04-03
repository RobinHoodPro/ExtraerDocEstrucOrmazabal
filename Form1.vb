Imports System.IO
Imports iText.Layout.Element
Imports Microsoft.Office.Interop

Public Class FrmExtDocEstOmzb
    Private fuentePequeña As Font = New Font("Verdana", 12, FontStyle.Bold)
    Private tiempo As Stopwatch
    Private archEstruct As String
    Private archModeling As String
    Private Delegate Sub delegadoLista()
    Private Delegate Sub delegadoCopiar(ByVal registro As Integer)
    Private listaArchEstruct As List(Of String)
    Private listaArchModeling As List(Of String)
    Private archConfig As New ClsArchConfigDocEstOmzb("DocEstOmzb.txt")
    Private articulo As String
    Private nombreArticulo As String
    Private ruta As String
    Private listaSubArticulosRevision As New List(Of String)
    Private listaSubArticulos As New List(Of String)
    Private listaArticulosModeling As New List(Of String)
    Private terminadoListas As Integer = 0
    Private terminadoGlobal As Integer = 0
    Private pdf As ClsTablaPdf
    Private listaBarrasProgreso As New List(Of barraProgreso)

    '
    'Centrar El Formulario Al Redimensionarse
    '
    Private Sub FrmListArch_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Centrar()
    End Sub

    '
    'Centrar El Formulario
    '
    Private Sub Centrar()
        Dim tamaño As Rectangle = My.Computer.Screen.Bounds
        Dim posicionX As Integer = (tamaño.Width - Me.Width) \ 2
        Dim posicionY As Integer = (tamaño.Height - Me.Height) \ 2
        Me.Location = New Point(posicionX, posicionY)
        Me.Refresh()
    End Sub

    '
    'Cerrar El Formulario Al Hacer Click En El contenedor Del Titulo O En El Titulo
    '
    Private Sub TlpMensaje_Click(sender As Object, e As EventArgs) Handles TlpTitulo.Click, LbTitulo.Click
        Me.Close()
    End Sub

    '
    'Mover El Formulario Al Mantener Pulsado Sobre El Titulo O Su Contenedor
    '
    Private Sub TlpMensaje_MouseMove(sender As Object, e As MouseEventArgs) Handles TlpTitulo.MouseMove, LbTitulo.MouseMove
        If e.Button = MouseButtons.Left Then moverForm(Me)
    End Sub





    Private Sub FrmExtDocEstOmzb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        iniciarBarrasProgreso()
    End Sub
    Private Sub iniciarBarrasProgreso()
        listaBarrasProgreso.Clear()
        For i = 0 To 8
            listaBarrasProgreso.Add(New barraProgreso(archConfig.getListaCopia(16)(i), i))
        Next
        updatebarra()
    End Sub

    '
    'Dibujar La ProgressBar
    '
    Private Sub PbBarra_Paint(sender As Object, e As PaintEventArgs) Handles PbBarra.Paint
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        For Each lbp In listaBarrasProgreso
            lbp.dibujar(e)
        Next
    End Sub











    '
    'Boton Para Seleccionar El Archivo De La Estructura De Ormazabal E Iniciar los Procesos Cuando Sean Validos Los Archivos Seleccionados De Las Estructuras
    '
    Private Sub BtArchEstruct_Click(sender As Object, e As EventArgs) Handles BtArchEstruct.Click
        gestionarBotones(archConfig.getValorConfig(7), archEstruct, listaArchEstruct, archConfig.getValorConfig(12), TbArchModeling, archConfig.getValorConfig(8), TbArchEstruct)
    End Sub

    '
    'Boton Para Seleccionar El Archivo De La Estructura De Modeling E Iniciar Los Procesos Cuando Sean Validos Los Archivos Seleccionados De Las Estructuras
    '
    Private Sub BtArchModeling_Click(sender As Object, e As EventArgs) Handles BtArchModeling.Click
        gestionarBotones(archConfig.getValorConfig(8), archModeling, listaArchModeling, archConfig.getValorConfig(9), TbArchEstruct, archConfig.getValorConfig(7), TbArchModeling)
    End Sub

    '
    'Gestionar La Validez Del Archivo De La Estructura De Ormazabal, De La Estructura De Modeling Y Iniciar Los Procesos
    '
    Private Sub gestionarBotones(ByVal textoArch As String, ByRef archEstructura As String, ByRef lista As List(Of String), ByVal textoClave As String, ByVal tbContrario As TextBox,
                                 ByVal textoArchContrario As String, ByVal tb As TextBox)
        archEstructura = selectArch(textoArch)
        If Not archEstructura.Contains(textoArch) Then
            tb.Text = archEstructura
            lista = File.ReadLines(archEstructura).ToList
            If lista.Count > 2 Then
                If lista(3).Contains(textoClave) Then
                    If Not tbContrario.Text.Contains(textoArchContrario) Then tb.Text = archEstructura : procesos()
                Else
                    tb.Text = textoArch & " Valido"
                End If
            Else
                tb.Text = textoArch & " Valido"
            End If
        Else
            tb.Text = archEstructura
        End If
    End Sub

    '
    'Obtener El Archivo De Estructura Seleccionado
    '
    Private Function selectArch(ByVal texto As String) As String
        Dim fileDialog As New OpenFileDialog
        fileDialog.Filter = "Documentos De Texto (*.txt)|*.txt"
        If fileDialog.ShowDialog = DialogResult.OK Then Return fileDialog.FileName
        Return texto
    End Function

    '
    'Autoajustar El Textbox A Su Contenido
    '
    Private Sub TbArchEstruct_TextChanged(sender As Object, e As EventArgs) Handles TbArchEstruct.TextChanged, TbArchModeling.TextChanged
        sender.Width = TextRenderer.MeasureText(sender.Text, sender.Font).Width
    End Sub

    '
    'Resetear Los Textbox De Las Estructuras, Que Se Ejecutara Al Finalizar Todos Los Procesos
    '
    Private Sub iniciarBotones()
        TbArchEstruct.Text = archConfig.getValorConfig(7)
        TbArchModeling.Text = archConfig.getValorConfig(8)
        Refresh()
    End Sub













    '
    'Obtener Listado Definitivo Del Archivo De Estructura De Ormazabal, Inicializar Barra De Progreso Y Limpiar Listas De Articulos
    '
    Private Sub procesos()
        iniciarBarrasProgreso()
        listaArchEstruct = finListaArchEstruct()
        listaSubArticulos.Clear()
        listaSubArticulosRevision.Clear()
        listaArticulosModeling.Clear()
        terminadoListas = 0
        terminadoGlobal = 0
        tiempo = Stopwatch.StartNew
        Dim myDelegadoEstructura As delegadoLista = AddressOf iniEstructuraCallBack
        myDelegadoEstructura.BeginInvoke(New AsyncCallback(AddressOf finIniEstructuraCallBack), Nothing)
        Dim myDelegadoModeling As delegadoLista = AddressOf iniModelingCallBack
        myDelegadoModeling.BeginInvoke(Nothing, Nothing)
        Dim myDelegadofusion As delegadoLista = AddressOf fusionEstructuraModelingCallBack
        myDelegadofusion.BeginInvoke(Nothing, Nothing)
    End Sub

    '
    'Obtener Listado Definitivo Del Archivo De Estructura De Ormazabal
    '
    Private Function finListaArchEstruct() As List(Of String)
        Dim line As String
        Dim delimitador() As String = {archConfig.getValorConfig(4)}
        Dim valores() As String
        Dim filaTitulo As Integer = getFilaTitulo()
        Dim lista As New List(Of String)
        Dim valoresInferiores() As String
        Dim titulo As String
        Dim campoSubdescripcion As Integer = archConfig.getValorConfig(21)
        For i = 0 To listaArchEstruct.Count - 1
            line = listaArchEstruct(i)
            valores = line.Split(delimitador, StringSplitOptions.RemoveEmptyEntries)
            If i < filaTitulo Then
                If valores.Count = 1 And line.Contains(":") Then lista.Add(Replace(listaArchEstruct(i), "|", ""))
                If valores.Count = 3 Then lista.Add(valores(1).Trim)
            ElseIf i = filaTitulo Then
                valoresInferiores = listaArchEstruct(i + 1).Split(delimitador, StringSplitOptions.RemoveEmptyEntries)
                lista.Add(getTitulo(valores, valoresInferiores) & "|")
                i += 1
            Else
                If line.Contains("| pz |") Then
                    titulo = "|"
                    For j = 0 To valores.Count - 1
                        If campoPermitido(j) Then titulo += valores(j).Trim & "|"
                    Next
                    lista.Add(titulo)
                ElseIf valores.Count = 7 Then
                    If valores(campoSubdescripcion).Trim.Length > 1 Then
                        valoresInferiores = line.Split(delimitador, StringSplitOptions.RemoveEmptyEntries)
                        titulo = ""
                        While valoresInferiores.Count = 7
                            If titulo.Length > 0 Then titulo += " " & valoresInferiores(campoSubdescripcion).Trim Else titulo = valoresInferiores(campoSubdescripcion).Trim
                            i += 1
                            valoresInferiores = listaArchEstruct(i).Split(delimitador, StringSplitOptions.RemoveEmptyEntries)
                        End While
                        lista.Add(titulo)
                    End If
                End If
            End If
        Next
        Return lista
    End Function

    '
    'Obtener Fila Del Titulo Del Archivo De Estructura De Ormazabal
    '
    Private Function getFilaTitulo() As Integer
        For i = 0 To listaArchEstruct.Count - 1
            If listaArchEstruct(i).Contains(archConfig.getValorConfig(22)) Then Return i
        Next
        Return 0
    End Function

    '
    'Obtener El Titulo Del Archivo De Estructura De Ormazabal
    '
    Private Function getTitulo(ByVal valores() As String, ByVal valoresInferiores() As String) As String
        Dim titulo As String = ""
        Dim subTitulo As String
        For i = 0 To valores.Count - 1
            If campoPermitido(i) Then
                If i = archConfig.getValorConfig(20) Then
                    subTitulo = valoresInferiores(i).Trim
                Else
                    If valoresInferiores(i).Trim.Length > 0 Then subTitulo = " " & valoresInferiores(i).Trim Else subTitulo = valoresInferiores(i).Trim
                End If
                titulo += "|" & Replace(valores(i).Trim, "-", "") & subTitulo
            End If
        Next
        Return titulo
    End Function

    '
    'Controlar Campo Permitido
    '
    Private Function campoPermitido(ByVal campo As String) As Boolean
        For Each field In archConfig.getListaCopia(19)
            If field = campo Then Return False
        Next
        Return True
    End Function











    '
    'Obtener Articulo, Revision Y Ruta
    '
    Private Sub iniEstructuraCallBack()
        articulo = valorArticuloRevision(linea(archConfig.getValorConfig(0)))
        Me.Invoke(New MethodInvoker(AddressOf updateTitulo))
        nombreArticulo = articulo & "-" & valorArticuloRevision(linea(archConfig.getValorConfig(2)))
        ruta = Mid(archEstruct, 1, InStrRev(archEstruct, "\")) & nombreArticulo
    End Sub

    '
    'Actualizar Titulo Del Articulo
    '
    Private Sub updateTitulo()
        LbTitleEstruct.Text = "Estructura: " & articulo
        Refresh()
    End Sub

    '
    'Obtener Linea Del Listado Del Archivo De Estructura De Ormazabal
    '
    Private Function linea(ByVal cadena As String) As String
        For Each line In listaArchEstruct
            If line.Contains(cadena) Then Return line
        Next
        Return Nothing
    End Function

    '
    'Obtener El Valor De La Linea Del Listado Del Archivo De Estructura De Ormazabal
    '
    Private Function valorArticuloRevision(ByVal linea As String) As String
        Return linea.Split(archConfig.getDelimitadores(1), StringSplitOptions.RemoveEmptyEntries)(1)
    End Function










    '
    'Iniciar Listado De Subarticulos Del Listado De La Estructura De Ormazabal Y Conversion De La Estructura De Ormazabal A 'PDF' 'XSLX'
    '
    Private Sub finIniEstructuraCallBack()
        Dim myDelegadoSubarticulos As delegadoLista = AddressOf subarticulos
        myDelegadoSubarticulos.BeginInvoke(Nothing, Nothing)
        Dim myDelegadoEstruc As delegadoLista = AddressOf estructuraCallBack
        myDelegadoEstruc.BeginInvoke(Nothing, Nothing)
    End Sub








    '
    'Obtener Listado De Planos Y Desarrollos
    '
    Private Sub subarticulos()
        Dim campos() As String
        Dim delimitador As String = archConfig.getValorConfig(4)
        Dim line As String
        Dim subRevision As String
        Dim subArticulo As String
        Dim subArticuloRevision As String
        Dim filaTitulo As Integer = getFilaTitulo()
        Dim contIni As Integer = 0
        Dim maxIni As Integer = listaArchEstruct.Count
        listaSubArticulos.Add(articulo)
        For i = 0 To listaArchEstruct.Count - 1
            line = listaArchEstruct(i)
            If i > filaTitulo And line.Contains(delimitador) Then
                campos = Split(line, delimitador)
                subRevision = campos(archConfig.getValorConfig(5))
                If subRevision.Length > 0 Then
                    subArticulo = campos(archConfig.getValorConfig(6)).Trim
                    subArticuloRevision = subArticulo & "-" & subRevision
                    If subArticulo.Length > 5 Then
                        If Not listaSubArticulos.Contains(subArticulo) Then
                            listaSubArticulosRevision.Add(subArticuloRevision)
                            listaSubArticulos.Add(subArticulo)
                        End If
                    End If
                End If
            End If
            contIni += 1
            listaBarrasProgreso(0).updateBarra(contIni, maxIni, tiempo)
            Me.Invoke(New delegadoLista(AddressOf updatebarra))
        Next
        listaSubArticulos.Sort()
        listaSubArticulosRevision.Sort()
        terminadoListas += 1
    End Sub

    '
    'Actualizar La Barra De Progreso
    '
    Private Sub updatebarra()
        PbBarra.Invalidate()
        PbBarra.Refresh()
    End Sub












    '
    'Convertir Estructura De Ormazabal A 'PDF' 'XSLX'
    '
    Private Sub estructuraCallBack()
        Dim subCarpeta As String = ruta & archConfig.getValorConfig(23)
        Dim archivo As String = subCarpeta & nombreArticulo
        crearCarpeta(subCarpeta)
        pdf = New ClsTablaPdf(archivo & ".pdf")
        Dim oxl As Excel.Application
        Dim owb As Excel.Workbook
        Dim osheet As Excel.Worksheet
        oxl = CreateObject("Excel.Application")
        owb = oxl.Workbooks.Add
        osheet = owb.ActiveSheet
        insertarDatosEstructura(osheet)
        oxl.WindowState = Excel.XlWindowState.xlMaximized
        oxl.DisplayAlerts = False
        owb.Close(True, archivo & ".xlsx")
        oxl.DisplayAlerts = True
        oxl.Quit()
        terminadoGlobal += 1
    End Sub

    '
    'Insertar Datos De La Estructura De Ormazabal En Archivo 'PDF' Y 'XLSX'
    '
    Private Sub insertarDatosEstructura(ByVal osheet As Excel.Worksheet)
        Dim line As String
        Dim filaTitulo As Integer = getFilaTitulo()
        Dim filaSubtitulo As Integer = 0
        Dim filaExcel As Integer
        Dim encabezado As Boolean
        Dim delimitador() As String = archConfig.getDelimitadores(4)
        Dim delimTitulo As String = archConfig.getValorConfig(4)
        Dim campos As Integer = getCamposEstructura(delimitador)
        Dim tabla = pdf.getTabla(1)
        Dim tablaInt = pdf.getTabla(campos)
        Dim subTitulo As String = ""
        Dim anchoSubtitulo As Integer = 0
        Dim subTituloMayor As String = ""
        Dim tablaTitulo = pdf.getTabla(1, 0).SetHorizontalAlignment(1)
        Dim valores() As String
        Dim columnSubtitulo As Integer = CInt(archConfig.getValorConfig(24))
        Dim contIni As Integer = 0
        Dim maxIni As Integer = listaArchEstruct.Count
        For i = 0 To listaArchEstruct.Count - 1
            line = listaArchEstruct(i)
            If i < filaTitulo Then
                filaExcel += 1
                If encabezado = True Then
                    If filaSubtitulo = 0 Then filaSubtitulo = filaExcel
                    getSubtituloMayor(line, anchoSubtitulo, subTituloMayor)
                    If subTitulo.Length > 0 Then subTitulo += vbCr & line Else subTitulo = line
                    osheet.Cells(filaExcel, columnSubtitulo + 1).value = line
                Else
                    tabla.AddCell(pdf.getMyCelda(line))
                    osheet.Cells(filaExcel, 1).value = line
                End If
                If line.Contains("Revisión") Then encabezado = True
            ElseIf i = filaTitulo Then
                tablaTitulo.AddCell(pdf.getMyCelda(subTitulo)).SetWidth(pdf.ancho(subTituloMayor))
                filaExcel += 1
                valores = line.Split(delimitador, StringSplitOptions.RemoveEmptyEntries)
                insertarTitulo(valores, tablaInt, osheet, filaExcel)
            Else
                valores = Split(line, delimTitulo)
                If valores.Count > 1 Then
                    filaExcel += 1
                    insertarRegistros(valores, osheet, filaExcel, i, delimTitulo, tablaInt)
                End If
            End If
            contIni += 1
            listaBarrasProgreso(8).updateBarra(contIni, maxIni, tiempo)
            Me.Invoke(New delegadoLista(AddressOf updatebarra))
        Next
        finalizarEstructura(osheet, filaSubtitulo, columnSubtitulo, filaTitulo, filaExcel, campos, tabla, tablaTitulo, tablaInt)
    End Sub

    '
    'Obtener El Numero De Campos De La Estructura De Ormazabal
    '
    Private Function getCamposEstructura(ByVal delim() As String) As Integer
        For Each line In listaArchEstruct
            If line.Contains(delim(0)) Then Return line.Split(delim, StringSplitOptions.RemoveEmptyEntries).Count
        Next
        Return 0
    End Function

    '
    'Obtener La Linea Mas Larga Del Subtitulo Y Su Longitud En Pixeles
    '
    Private Sub getSubtituloMayor(ByVal line As String, ByRef anchoSubtitulo As Integer, ByRef subTituloMayor As String)
        Dim anchoLinea As String = TextRenderer.MeasureText(line, fuentePequeña).Width
        If anchoSubtitulo < anchoLinea Then anchoSubtitulo = anchoLinea : subTituloMayor = line
    End Sub

    '
    'Insertar El Titulo De La Estructura En El Archivo 'PDF' Y 'XLSX'
    '
    Private Sub insertarTitulo(ByVal valores() As String, ByVal tablaint As Table, ByVal osheet As Excel.Worksheet, ByVal filaExcel As Integer)
        For j = 0 To valores.Count - 1
            tablaint.AddHeaderCell(pdf.getMyCeldaTitulo(StrConv(valores(j), VbStrConv.ProperCase)).SetVerticalAlignment(1))
            osheet.Cells(filaExcel, j + 1).value = StrConv(valores(j), VbStrConv.ProperCase)
            cargarIndices(filaExcel, j + 1, filaExcel, j + 1)
            tituloFuenteBorde(osheet)
        Next
    End Sub

    '
    'Insertar Los Registros De La Estructura De Ormazabal En El Archivo 'PDF' Y 'XLSX'
    '
    Private Sub insertarRegistros(ByVal valores() As String, ByVal osheet As Excel.Worksheet, ByVal filaExcel As Integer, ByVal i As Integer, ByVal delimTitulo As String, ByVal tablaint As Table)
        Dim valorCampo As String
        For j = 1 To valores.Count - 2
            osheet.Cells(filaExcel, j).numberformat = "@"
            valorCampo = valores(j)
            If i < listaArchEstruct.Count - 1 Then
                If j = archConfig.getValorConfig(21) + 1 And Not listaArchEstruct(i + 1).Contains(delimTitulo) Then valorCampo = listaArchEstruct(i + 1)
            End If
            osheet.Cells(filaExcel, j).value = valorCampo
            If controlarCamposDerecha(j) Then
                tablaint.AddCell(pdf.getMyCelda(valorCampo).SetTextAlignment(2).SetVerticalAlignment(1))
                osheet.Cells(filaExcel, j).value = valorCampo
                osheet.Cells(filaExcel, j).HorizontalAlignment = 4
            Else
                tablaint.AddCell(pdf.getMyCelda(valorCampo).SetVerticalAlignment(1))
            End If
        Next
    End Sub

    '
    'Controlar Si Nos Encontramos En Un Campo En El Que Es Necdesario Alinear Los Datos A La Derecha
    '
    Private Function controlarCamposDerecha(ByVal indice As Integer) As Boolean
        For Each cd In archConfig.getListaCopia(25)
            If cd = indice Then Return True
        Next
        Return False
    End Function

    '
    'Finalizar De Insertar Datos En El Archivo 'PDF', 'XLSX' Y Cerrar El Archivo 'PDF'
    '
    Private Sub finalizarEstructura(ByVal osheet As Excel.Worksheet, ByVal filaSubtitulo As Integer, ByVal columnSubtitulo As Integer, ByVal filaTitulo As Integer, ByVal filaExcel As Integer, ByVal campos As Integer, ByVal tabla As Table, ByVal tablaTitulo As Table, ByVal tablaInt As Table)
        osheet.Cells(1, 1).ColumnWidth = 6
        osheet.Range(osheet.Cells(1, 2), osheet.Cells(1, archConfig.getNumCampoPrograma)).EntireColumn.AutoFit()
        cargarIndices(filaSubtitulo, columnSubtitulo + 1, filaTitulo, columnSubtitulo + 1)
        bordeExterior(osheet)
        cargarIndices(filaTitulo + 2, 1, filaExcel, campos)
        borde(osheet)
        tabla.AddCell(pdf.getMyCelda(tablaTitulo))
        tabla.AddCell(tablaInt)
        pdf.getDoc().Add(tabla)
        pdf.getDoc().Close()
    End Sub









    '
    'Obtener Listado De Articulos De La Estructura Modeling
    '
    Private Sub iniModelingCallBack()
        Dim arch As String
        Dim contIni As Integer = 0
        Dim maxIni As Integer = listaArchModeling.Count
        For Each lt In listaArchModeling
            If lt.Contains(archConfig.getValorConfig(9)) Then
                arch = lt.Split(archConfig.getDelimitadores(10), StringSplitOptions.RemoveEmptyEntries)(1)
                If IsNumeric(Mid(arch, 1, 1)) Then
                    If Not listaArticulosModeling.Contains(arch) And arch.Length > 5 Then listaArticulosModeling.Add(arch)
                End If
            End If
            contIni += 1
            listaBarrasProgreso(1).updateBarra(contIni, maxIni, tiempo)
            Me.Invoke(New delegadoLista(AddressOf updatebarra))
        Next
        listaArticulosModeling.Sort()
        terminadoListas += 1
    End Sub








    '
    'Actualizar El Listado De Planos Y El Listado De Desarrollos
    '
    Private Sub fusionEstructuraModelingCallBack()
        While terminadoListas < 2
        End While
        Dim contIni As Integer = 0
        Dim maxIni As Integer = listaArticulosModeling.Count
        For Each am In listaArticulosModeling
            If Not listaSubArticulos.Contains(am) Then listaSubArticulos.Add(am) : listaSubArticulosRevision.Add(am)
            contIni += 1
            listaBarrasProgreso(2).updateBarra(contIni, maxIni, tiempo)
            Me.Invoke(New delegadoLista(AddressOf updatebarra))
        Next
        listaSubArticulos.Sort()
        listaSubArticulosRevision.Sort()
        Dim myDelegadoCopyPlanos As delegadoLista = AddressOf copiarPlanos
        myDelegadoCopyPlanos.BeginInvoke(Nothing, Nothing)
        updateRevisiones()
    End Sub














    '
    'Actualizacion Definitiva Del Listado De Desarrollos
    '
    Private Sub updateRevisiones()
        Dim rutaOrigen As String
        Dim listaArchivos As List(Of String)
        Dim nombreArticulo As String
        Dim contIni As Integer = 0
        Dim maxIni As Integer = listaSubArticulosRevision.Count
        For i = 0 To listaSubArticulosRevision.Count - 1
            If Not listaSubArticulosRevision(i).Contains("-") Then
                rutaOrigen = archConfig.getListaCopia(11)(0) & Mid(listaSubArticulosRevision(i), 1, 3)
                If Directory.Exists(rutaOrigen) Then
                    nombreArticulo = listaSubArticulosRevision(i)
                    If Not IsNumeric(listaSubArticulosRevision(i)) Then
                        nombreArticulo = ""
                        For Each caracter In listaSubArticulosRevision(i)
                            If IsNumeric(caracter) Then nombreArticulo += caracter Else Exit For
                        Next
                    End If
                    listaArchivos = Directory.GetFiles(rutaOrigen, nombreArticulo & "*-*.mi").ToList
                    listaArchivos = limpiarListaArchivos(listaArchivos)
                    If listaArchivos.Count > 0 Then listaSubArticulosRevision(i) = ultimaRevision(listaArchivos)
                End If
            End If
            contIni += 1
            listaBarrasProgreso(3).updateBarra(contIni, maxIni, tiempo)
            Me.Invoke(New delegadoLista(AddressOf updatebarra))
        Next
        File.Delete("listaDesarrollosFinal.txt")
        File.AppendAllLines("listaDesarrollosFinal.txt", listaSubArticulosRevision)
        Dim myDelegadoCopyDesarrollos As delegadoCopiar = AddressOf copiarDesarrollos
        myDelegadoCopyDesarrollos.BeginInvoke(11, Nothing, Nothing)
        Dim myDelegadoCopyGeo As delegadoCopiar = AddressOf copiarDesarrollos
        myDelegadoCopyGeo.BeginInvoke(13, Nothing, Nothing)
        Dim myDelegadoCopyGmt As delegadoCopiar = AddressOf copiarDesarrollos
        myDelegadoCopyGmt.BeginInvoke(14, Nothing, Nothing)
        Dim myDelegadoFinalizar As delegadoLista = AddressOf finalizar
        myDelegadoFinalizar.BeginInvoke(Nothing, Nothing)
    End Sub

    '
    'Obtener La Ultima Revision Del Articulo
    '
    Private Function ultimaRevision(ByVal lista As List(Of String)) As String
        Dim listaTemp As New List(Of String)
        Dim arch As String
        For i = 0 To lista.Count - 1
            arch = Mid(lista(i), InStrRev(lista(i), "\") + 1, InStrRev(lista(i), ".") - InStrRev(lista(i), "\") - 1)
            If arch.ToUpper.Contains("P") Then arch = Mid(arch, 1, InStr(arch, "-")) & "00" & Mid(arch, InStr(arch, "-") + 1)
            listaTemp.Add(arch)
        Next
        listaTemp.Sort()
        arch = listaTemp.Last.ToUpper
        If arch.Contains("P") Then arch = Mid(arch, 1, InStr(arch, "-")) & Mid(arch, InStr(arch, "P"))
        Return arch
    End Function

    '
    'Obtener Lista De Desarrollos Valida
    '
    Private Function limpiarListaArchivos(ByVal lista As List(Of String)) As List(Of String)
        Dim delimitadores() As String = {"-", "_"}
        Dim campos As Integer
        Dim listaTemp As New List(Of String)
        For Each arch In lista
            campos = arch.Split(delimitadores, StringSplitOptions.RemoveEmptyEntries).Count
            If campos = 2 Then
                listaTemp.Add(arch)
            End If
        Next
        Return listaTemp
    End Function




















    '
    'Copiar Desarrollos, Archivos 'GEO' Y 'GMT'
    '
    Private Sub copiarDesarrollos(ByVal registro As Integer)
        Dim lista As List(Of String) = archConfig.getListaCopia(registro)
        Dim append As Boolean
        Dim rutaSub As String = ruta & lista(1)
        Dim rutaOrigen As String
        Dim listaArchivos As List(Of String)
        Dim ultimoDesarrollo As String
        Dim contIni As Integer = 0
        Dim maxIni As Integer = listaSubArticulosRevision.Count
        crearCarpeta(rutaSub)
        For Each codigo In listaSubArticulosRevision
            If Not codigo.Contains("-") Then
                My.Computer.FileSystem.WriteAllText(rutaSub & "\NoEncontrado.txt", vbNewLine & codigo & "(No Encontrado)", append)
                append = True
            Else
                rutaOrigen = lista(0) & Mid(codigo, 1, 3)
                If Directory.Exists(rutaOrigen) Then
                    listaArchivos = Directory.GetFiles(rutaOrigen, Mid(codigo, 1, InStr(codigo, "-")) & "*" & lista(2)).ToList
                    listaArchivos = limpiarListaArchivos(listaArchivos)
                    If listaArchivos.Count = 0 Then
                        My.Computer.FileSystem.WriteAllText(rutaSub & "\NoEncontrado.txt", vbNewLine & codigo & "(No Encontrado)", append)
                        append = True
                    Else
                        ultimoDesarrollo = ultimaRevision(listaArchivos)
                        If codigo.ToUpper <> ultimoDesarrollo.ToUpper Then
                            My.Computer.FileSystem.WriteAllText(rutaSub & "\NoEncontrado.txt", vbNewLine & codigo & "(No Coincide Revision / " & ultimoDesarrollo & ")", append)
                            append = True
                        End If
                        copiar(rutaSub & "\" & codigo & lista(2), rutaOrigen & "\" & ultimoDesarrollo & lista(2))
                    End If
                End If
            End If
            contIni += 1
            gestionVariablesBarra(registro, contIni, maxIni)
            Me.Invoke(New delegadoLista(AddressOf updatebarra))
        Next
        terminadoGlobal += 1
    End Sub

    '
    'Crear Carpeta Si No Existe
    '
    Private Sub crearCarpeta(ByVal carpeta As String)
        If Not Directory.Exists(carpeta) Then MkDir(carpeta)
    End Sub

    '
    'Copiar Archivo En Caso De No Existir En El Destino Y Copiar El Archivo Mas Nuevo En Caso De Existir En El Destino
    '
    Private Sub copiar(ByVal archivoDestino As String, ByVal archivoOrigen As String)
        If File.Exists(archivoDestino) Then
            If File.GetLastWriteTime(archivoOrigen) > File.GetLastWriteTime(archivoDestino) Then
                FileCopy(archivoOrigen, archivoDestino)
            End If
        Else
            FileCopy(archivoOrigen, archivoDestino)
        End If
    End Sub

    '
    'Actualizar Barra De Progreso Dependiendo Del Listado Que Estemos Copiando
    '
    Private Sub gestionVariablesBarra(ByVal registro As Integer, ByVal contIni As Integer, ByVal maxIni As Integer)
        If registro = 11 Then
            listaBarrasProgreso(4).updateBarra(contIni, maxIni, tiempo)
        ElseIf registro = 13 Then
            listaBarrasProgreso(5).updateBarra(contIni, maxIni, tiempo)
        ElseIf registro = 14 Then
            listaBarrasProgreso(6).updateBarra(contIni, maxIni, tiempo)
        End If
    End Sub








    '
    'Copiar Planos
    '
    Private Sub copiarPlanos()
        Dim listaDirectorios As List(Of String) = archConfig.getListaCopia(15)
        Dim encontrado, append As Boolean
        Dim rutaSub As String = ruta & "\2D Planos"
        Dim contIni As Integer = 0
        Dim maxIni As Integer = listaSubArticulos.Count
        crearCarpeta(rutaSub)
        For Each codigo In listaSubArticulos
            encontrado = False
            For Each dire In listaDirectorios
                For Each arch In Directory.GetFiles(dire, codigo & ".mi")
                    copiar(rutaSub & "\" & codigo & ".mi", arch)
                    encontrado = True
                Next
            Next
            If Not encontrado Then
                My.Computer.FileSystem.WriteAllText(rutaSub & "\No Encontrado.txt", vbNewLine & codigo, append)
                append = True
            End If
            contIni += 1
            listaBarrasProgreso(7).updateBarra(contIni, maxIni, tiempo)
            Me.Invoke(New delegadoLista(AddressOf updatebarra))
        Next
        terminadoGlobal += 1
    End Sub








    '
    'Inicializar Los Textbox Al Finalizar Todos Los Procesos
    '
    Private Sub finalizar()
        While terminadoGlobal < 5

        End While
        Me.Invoke(New MethodInvoker(AddressOf iniciarBotones))
    End Sub











    '
    'Dibujar Borde Del Contenedor Del Titulo
    '
    Private Sub TlpTitulo_Paint(sender As Object, e As PaintEventArgs) Handles TlpTitulo.Paint
        Dim penIni As Pen = New Pen(Color.Crimson, 10)
        e.Graphics.DrawRectangle(penIni, New Rectangle(0, 0, TlpTitulo.Width, TlpTitulo.Height))
    End Sub

    '
    'Dibujar Borde Del Formulario
    '
    Private Sub FrmExtDocEstOmzb_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim penIni As Pen = New Pen(Color.Crimson, 10)
        e.Graphics.DrawRectangle(penIni, New Rectangle(0, 0, Width, Height))
    End Sub

    '
    'Dibujar Borde Del Contenedor Principal Y De Sus Filas
    '
    Private Sub TlpPrincipal_CellPaint(sender As Object, e As TableLayoutCellPaintEventArgs) Handles TlpPrincipal.CellPaint
        e.Graphics.DrawLine(New Pen(Color.Crimson, 5), e.CellBounds.Location, New Point(e.CellBounds.Right, e.CellBounds.Top))
        e.Graphics.DrawRectangle(New Pen(Color.Crimson, 10), New Rectangle(0, 0, TlpPrincipal.Width, TlpPrincipal.Height))
    End Sub

    '
    'Dibujar Bordes Del Contenedor 'Estructura'
    '
    Private Sub TlpEstructura_CellPaint(sender As Object, e As TableLayoutCellPaintEventArgs) Handles TlpEstructura.CellPaint
        e.Graphics.DrawLine(New Pen(Color.Crimson, 5), e.CellBounds.Location, New Point(e.CellBounds.Right, e.CellBounds.Top))
        e.Graphics.DrawRectangle(New Pen(Color.Crimson, 10), New Rectangle(0, 0, TlpEstructura.Width, TlpEstructura.Height))
    End Sub

End Class
