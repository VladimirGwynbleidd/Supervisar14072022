Imports Entities

Public Class CatalogoServicios1
    Inherits System.Web.UI.Page

    Public Property Mensaje As String

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)

        For argument As Integer = 0 To gvConsulta.Rows.Count - 1
            ClientScript.RegisterForEventValidation(btnConsulta.UniqueID, argument)
        Next

        MyBase.Render(writer)

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargarImagenesEstatus()
            CargarFiltros()
            CargarCatalogo()
        End If
    End Sub

    Public Function ObtenerPaginacion() As Integer
        Return Conexion.SQLServer.Parametro.ObtenerValor("Paginación Servicios")
    End Function

#Region "Carga Datos"

    Private Sub CargarImagenesEstatus()

        imgOK.ImageUrl = ObtenerImagenEstatus(True)
        imgERROR.ImageUrl = ObtenerImagenEstatus(False)

    End Sub
    Private Sub CargarFiltros()

        ucFiltro1.resetSession()
        ucFiltro1.AddFilter("Vigencia", ucFiltro.AcceptedControls.DropDownList, Utilerias.Generales.VigenciaDataSourceBit, "Vigencia", "B_FLAG_VIG", ucFiltro.DataValueType.BoolType, False, True, False, True, True, -1)
        ucFiltro1.AddFilter("ID", ucFiltro.AcceptedControls.TextBox, Nothing, "", "N_ID_SERVICIO", ucFiltro.DataValueType.IntegerType, False, False, False, False, False, Nothing, 3)
        ucFiltro1.AddFilter("Servicio", ucFiltro.AcceptedControls.TextBox, Nothing, "", "T_DSC_SERVICIO", ucFiltro.DataValueType.StringType, False, True, False, False, False, Nothing, 100)
        ucFiltro1.LoadDDL("CatalogoServicios.aspx")

    End Sub

    Private Sub CargarCatalogo()

        Dim consulta As String = "1=1"
        For Each filtro In ucFiltro1.getFilterSelection
            consulta += " AND " + filtro
        Next

        Dim objServicio As New Servicio
        Dim dv As DataView = objServicio.ObtenerTodos()

        dv.RowFilter = consulta

        gvConsulta.DataSource = dv.ToTable()
        gvConsulta.DataBind()

        If gvConsulta.Rows.Count = 0 Then
            Noexisten.Visible = True
            gvConsulta.Visible = False
        Else
            Noexisten.Visible = False
            gvConsulta.Visible = True
        End If

    End Sub

#End Region

#Region "Eventos Controles"


    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click

        lblTituloRegistro.Text = "Alta de Servicio"

        Dim objServicio As New Servicio()

        txtId.Text = objServicio.ObtenerSiguienteIdentificador.ToString
        txtServicio.Text = String.Empty

        pnlRegistro.Visible = True
        pnlConsulta.Visible = False


    End Sub
    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click

        Page.Validate("Forma")

        If Not Page.IsValid Then
            Mensaje = String.Empty
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Confirmacion", "AquiMuestroMensaje();", True)
            Exit Sub
        End If

        btnAceptarM2B1A.CommandArgument = "btnAceptar"
        Dim objServicio As New Servicio(Convert.ToInt32(txtId.Text))

        If Not objServicio.Existe Then
            Dim errores As New Entities.EtiquetaError(1080)
            Mensaje = errores.Descripcion
            imgDosBotonesUnaAccion.ImageUrl = Entities.Imagen.RutaCarpeta & errores.Imagen.Ruta
        Else
            Dim errores As New Entities.EtiquetaError(1081)
            Mensaje = errores.Descripcion
            imgDosBotonesUnaAccion.ImageUrl = Entities.Imagen.RutaCarpeta & errores.Imagen.Ruta
        End If

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Confirmacion", "MensajeConfirmacion();", True)

    End Sub
    Protected Sub btnModificar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnModificar.Click

        lblTituloRegistro.Text = "Modificación de Servicio"

        If gvConsulta.SelectedIndex = -1 Then
            Dim errores As New Entities.EtiquetaError(1074)
            Mensaje = errores.Descripcion
            imgUnBotonNoAccion.ImageUrl = Entities.Imagen.RutaCarpeta & errores.Imagen.Ruta
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Mostraremos", "AquiMuestroMensaje();", True)
            Exit Sub
        End If

        Dim objServicio As New Servicio(CInt(gvConsulta.DataKeys(gvConsulta.SelectedIndex)("N_ID_SERVICIO").ToString()))

        If Not objServicio.Vigente Then
            Dim errores As New Entities.EtiquetaError(1075)
            Mensaje = errores.Descripcion
            imgUnBotonNoAccion.ImageUrl = Entities.Imagen.RutaCarpeta & errores.Imagen.Ruta
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Mostraremos", "AquiMuestroMensaje();", True)
            Exit Sub
        Else
            txtId.Text = objServicio.Identificador
            txtServicio.Text = objServicio.Descripcion

            pnlRegistro.Visible = True
            pnlConsulta.Visible = False

        End If

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click

        btnAceptarM2B1A.CommandArgument = "btnCancelar"
        Dim errores As New Entities.EtiquetaError(1082)
        Mensaje = errores.Descripcion
        imgDosBotonesUnaAccion.ImageUrl = Entities.Imagen.RutaCarpeta & errores.Imagen.Ruta
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Confirmacion", "MensajeConfirmacion();", True)

    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRegresar.Click

        btnAceptarM2B1A.CommandArgument = "btnCancelar"
        btnAceptarM2B1A_Click(sender, e)

    End Sub

    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminar.Click

        btnAceptarM2B1A.CommandArgument = "btnEliminar"
        Dim errores
        If gvConsulta.SelectedIndex = -1 Then
            errores = New Entities.EtiquetaError(1074)
            Mensaje = errores.Descripcion
            imgUnBotonNoAccion.ImageUrl = Entities.Imagen.RutaCarpeta & errores.Imagen.Ruta
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Mostraremos", "AquiMuestroMensaje();", True)
            Exit Sub
        End If

        Dim objServicio As New Servicio(CInt(gvConsulta.DataKeys(gvConsulta.SelectedIndex)("N_ID_SERVICIO").ToString()))

        If Not objServicio.Vigente Then
            errores = New Entities.EtiquetaError(1075)
            Mensaje = errores.Descripcion
            imgUnBotonNoAccion.ImageUrl = Entities.Imagen.RutaCarpeta & errores.Imagen.Ruta
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Mostraremos", "AquiMuestroMensaje();", True)
            Exit Sub
        End If

        errores = New Entities.EtiquetaError(1083)
        Mensaje = errores.Descripcion
        imgDosBotonesUnaAccion.ImageUrl = Entities.Imagen.RutaCarpeta & errores.Imagen.Ruta
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Eliminar", "ConfirmacionEliminar();", True)

    End Sub

    Protected Sub btnAceptarM2B1A_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptarM2B1A.Click

        Select Case btnAceptarM2B1A.CommandArgument

            Case "btnCancelar"

                pnlControles.Enabled = True
                pnlBotones.Visible = True
                pnlRegresar.Visible = False

                pnlRegistro.Visible = False

                pnlConsulta.Visible = True

            Case "btnAceptar"

                Dim objServicio As New Servicio(Convert.ToInt32(txtId.Text))
                objServicio.Descripcion = txtServicio.Text
                
                If Not objServicio.Existe Then
                    objServicio.Identificador = objServicio.ObtenerSiguienteIdentificador()
                    objServicio.Agregar()
                Else
                    objServicio.Actualizar()
                End If

                CargarCatalogo()

                btnAceptarM2B1A.CommandArgument = "btnCancelar"
                btnAceptarM2B1A_Click(sender, e)

            Case "btnEliminar"

                Dim objServicio As New Servicio(CInt(gvConsulta.DataKeys(gvConsulta.SelectedIndex)("N_ID_SERVICIO").ToString()))
                objServicio.Baja()

                CargarCatalogo()

        End Select

    End Sub

    Protected Sub gvConsulta_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvConsulta.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("ondblclick", ClientScript.GetPostBackEventReference(btnConsulta, e.Row.RowIndex.ToString(), False))
        End If
    End Sub

    Protected Sub btnConsulta_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConsulta.Click

        lblTituloRegistro.Text = "Consulta de Servicio"

        Dim index As Integer = Convert.ToInt32(Request("__EVENTARGUMENT"))

        Dim objServicio As New Servicio(CInt(gvConsulta.DataKeys(index)("N_ID_SERVICIO").ToString()))

        txtId.Text = objServicio.Identificador.ToString
        txtServicio.Text = objServicio.Descripcion
       
        pnlRegistro.Visible = True
        pnlControles.Enabled = False
        pnlBotones.Visible = False
        pnlRegresar.Visible = True

        pnlConsulta.Visible = False

    End Sub

    Protected Sub btnFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ucFiltro1.Filtrar
        CargarCatalogo()
    End Sub

    Protected Sub btnExportaExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportaExcel.Click
        Dim utl As New Utilerias.ExportarExcel
        Dim referencias As New List(Of String)
        referencias.Add(CType(Session(Entities.Usuario.SessionID), Entities.Usuario).IdentificadorUsuario.ToString)
        referencias.Add(Now.ToString)

        Dim dt As DataTable = TryCast(gvConsulta.DataSourceSession, DataTable)
        dt.Columns("B_FLAG_VIG").ColumnName = "Estatus"

        utl.ExportaGrid(dt, gvConsulta, "Catálogo de Servicios", referencias)
    End Sub

    Private Sub gvConsulta_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvConsulta.Sorting
        gvConsulta.Ordenar(e)
    End Sub

#End Region

#Region "Metodos"
    Public Function ObtenerImagen(ByVal imagen As String) As String

        Return "~/Imagenes/Errores/" + imagen

    End Function


    Public Function ObtenerImagenEstatus(ByVal estatus As Boolean) As String

        If estatus Then
            Return "~/Imagenes/vigente.gif"
        Else
            Return "~/Imagenes/no_vigente.gif"
        End If

    End Function
#End Region


#Region "Validaciones"

    Private Sub cvServicio_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvServicio.ServerValidate
        If txtServicio.Text.Trim() = String.Empty Then
            Dim errores As New Entities.EtiquetaError(1076)
            source.ErrorMessage = errores.Descripcion
            imgUnBotonNoAccion.ImageUrl = Entities.Imagen.RutaCarpeta & errores.Imagen.Ruta
            args.IsValid = False
        End If
    End Sub

#End Region


    Protected Sub gvConsulta_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gvConsulta.PageIndexChanging
        gvConsulta.PageIndex = e.NewPageIndex
        gvConsulta.DataBind()
    End Sub

End Class