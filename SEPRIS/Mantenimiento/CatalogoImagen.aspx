<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/SiteInterno.Master"
    CodeBehind="CatalogoImagen.aspx.vb" Inherits="SEPRIS.CatalogoImagen" %>

<%@ Register Src="../Controles/ucFiltro.ascx" TagName="ucFiltro" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upnlConsulta" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlConsulta" runat="server" Visible="true">
                <div align="center" style="padding: 20px 20px 15px 20px">
                    <label class="TitulosWebProyectos">
                        Catálogo de Imágenes</label>
                </div>
                <div style="text-align: left; width: 100%; padding-bottom: 5px;">
                    <asp:Button ID="btnExportaExcel" runat="server" Text="Exportar a Excel" />
                </div>
                <uc1:ucFiltro ID="ucFiltro1" runat="server" Width="100%" />
                <br />
                <cc1:CustomGridView ID="gvConsulta" runat="server" DataKeyNames="N_ID_IMAGEN" Width="100%"
                    AllowSorting="true">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkElemento" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="15px" />
                            <HeaderStyle Width="15px" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Clave" DataField="N_ID_IMAGEN" SortExpression="N_ID_IMAGEN" >
                            <ItemStyle Width="50px" />
                            <HeaderStyle Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Descripción" DataField="T_DSC_IMAGEN" SortExpression="T_DSC_IMAGEN" />
                        <asp:BoundField HeaderText="Tipo de Imagen" DataField="T_DSC_TIPO_IMAGEN" SortExpression="T_DSC_TIPO_IMAGEN" />
                        <asp:TemplateField HeaderText="Imagen" SortExpression="T_DSC_RUTA_IMAGEN">
                            <ItemTemplate>
                                <asp:Image ID="imagen" runat="server" Width="22px" Height="22px" ImageUrl='<%# ObtenerImagen(DataBinder.Eval(Container.DataItem, "T_DSC_RUTA_IMAGEN"))  %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estatus" SortExpression="N_FLAG_VIG">
                            <ItemTemplate>
                                <asp:Image ID="imagenEstatus" runat="server" ImageUrl='<%# ObtenerImagenEstatus(DataBinder.Eval(Container.DataItem, "N_FLAG_VIG"))  %>' />
                            </ItemTemplate>
                            <ItemStyle Width="50px" />
                            <HeaderStyle Width="50px" />
                        </asp:TemplateField>
                    </Columns>
                </cc1:CustomGridView>
                <div id="pnlNoExiste" runat="server" align="center" style="padding: 20px 20px 15px 20px">
                    <asp:Image ID="Image1" runat="server" AlternateText="No existen registros para la consulta"
                        ImageAlign="Middle" ImageUrl="../Imagenes/no EXISTEN.gif" />
                </div>
                <asp:HiddenField ID="hfGridView1SV" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfGridView1SH" runat="server" ClientIDMode="Static" />
                <br />
                <br />
                <asp:Image ID="imgOK" runat="server" />
                <label class="txt_gral">
                    Vigente</label>
                <asp:Image ID="imgERROR" runat="server" />
                <label class="txt_gral">
                    No vigente</label>
                <br />
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" />
            </asp:Panel>
            <asp:Panel ID="pnlRegistro" runat="server" Visible="false">
                <div align="center" style="padding: 20px 20px 15px 20px">
                    <asp:Label ID="lblTituloRegistro" runat="server" CssClass="TitulosWebProyectos" Text="Alta de Imagen" EnableTheming="false"></asp:Label>
                </div>
                <asp:Panel ID="pnlControles" runat="server">
                    <table>
                        <tr>
                            <td align="right">
                                <label class="txt_gral">
                                    Clave:</label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtClave" runat="server" CssClass="txt_solo_lectura" Enabled="false"
                                    Width="100px"></asp:TextBox>
                            </td>
                            <td align="left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label class="txt_gral">
                                    Descripción*:</label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="txt_gral" Width="300px"
                                    ValidationGroup="Forma"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:CustomValidator ID="cvDescripcion" runat="server" 
                                    ControlToValidate="txtDescripcion" Display="Dynamic" EnableClientScript="false" 
                                    ForeColor="Red" ValidateEmptyText="true" ValidationGroup="Forma">*</asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <label class="txt_gral">
                                    Tipo de Imagen*:</label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlTipoImagen" runat="server" Width="100%" CssClass="txt_gral"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:CustomValidator ID="cvTipoImagen" runat="server" 
                                    ControlToValidate="ddlTipoImagen" Display="Dynamic" EnableClientScript="false" 
                                    ForeColor="Red" ValidateEmptyText="true" ValidationGroup="Forma">*</asp:CustomValidator>
                            </td>
                        </tr>
                        <tr id="trImagenActual" runat="server" visible="false">
                            <td align="right">
                                <label class="txt_gral">
                                    Imagen Actual:</label>
                            </td>
                            <td align="left">
                                <asp:Image ID="imgActual" runat="server" Width="22px" Height="22px" />
                            </td>
                            <td align="left">
                                &nbsp;</td>
                        </tr>
                        <tr id="trImagen" runat="server">
                            <td align="right">
                                <label class="txt_gral">
                                    Imagen*:</label>
                            </td>
                            <td align="left">
                                <asp:FileUpload ID="fuImagen" runat="server" Width="100%" AutoPostBack="true" CssClass="txt_gral" />
                            </td>
                            <td align="left">
                                <asp:CustomValidator ID="cvFileUpload" runat="server" 
                                    ControlToValidate="fuImagen" Display="Dynamic" EnableClientScript="false" 
                                    ForeColor="Red" ValidateEmptyText="true" ValidationGroup="Forma">*</asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlBotones" runat="server">
                    <table>
                        <tr>
                            <td colspan="2">
                                <label class="txt_gral">
                                    *Datos Obligatorios</label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClientClick="Deshabilita(this);" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlRegresar" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:Panel>


            <div id="divMensajeUnBotonNoAccion" style="display: none">
                <table width="100%">
                    <tr>
                        <td style="width: 50px; text-align: center; vertical-align:top">
                            <asp:Image ID="imgUnBotonNoAccion" runat="server" Width="32px" Height="32px" ImageUrl="~/Imagenes/Errores/Error1.png" />
                        </td>
                        <td style="text-align: left">
                            <div class="MensajeModal-UI">
                            <%= Mensaje%>
                            <asp:ValidationSummary ID="vsErrores" runat="server" ValidationGroup="Forma" CssClass="MensajeModal-UI" />
                           </div>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="divConfirmacionM2B2A" style="display: none">
                <table width="100%">
                    <tr>
                        <td style="width: 50px; text-align: center; vertical-align:top">
                            <asp:Image ID="imgM2B2A" runat="server" Width="32px" Height="32px" ImageUrl="~/Imagenes/Errores/Error1.png" />
                        </td>
                        <td style="text-align: left">
                            <div class="MensajeModal-UI">
                                <%= Mensaje%>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divMensajeUnBotonUnaAccion" style="display: none">
                <table width="100%">
                    <tr>
                        <td style="width: 50px; text-align: center; vertical-align:top">
                            <asp:Image ID="imgUnBotonUnaAccion" runat="server" Width="32px" Height="32px" ImageUrl="~/Imagenes/Errores/Error1.png" />
                        </td>
                        <td style="text-align: left">
                            <div class="MensajeModal-UI">
                                <%= Mensaje%>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divMensajeDosBotonesUnaAccion" style="display: none">
                <table width="100%">
                    <tr>
                        <td style="width: 50px; text-align: center; vertical-align:top">
                            <asp:Image ID="imgDosBotonesUnaAccion" runat="server" Width="32px" Height="32px" ImageUrl="~/Imagenes/Errores/Error1.png" />
                        </td>
                        <td style="text-align: left">
                            <div class="MensajeModal-UI">
                                <%= Mensaje%>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Button runat="server" ID="btnSalirM2B2A" Style="display: none" ClientIDMode="Static" />
            <asp:Button runat="server" ID="btnContinuarM2B2A" Style="display: none" ClientIDMode="Static" />
            <asp:Button runat="server" ID="btnAceptarM1B1A" Style="display: none" ClientIDMode="Static" />
            <asp:Button runat="server" ID="btnAceptarM2B1A" Style="display: none" ClientIDMode="Static" />
            <asp:Button runat="server" ID="btnConsulta" Style="display: none" ClientIDMode="Static" />
            <script type="text/javascript">
                $(function () {

                    MensajeUnBotonNoAccionLoad();
                    MensajeDosBotonesDosAccionesLoad();
                    MensajeUnBotonUnaAccionLoad();
                    MensajeDosBotonesUnaAccionLoad();
                });


                function AquiMuestroMensaje() {

                    MensajeUnBotonNoAccion();

                };


                function ConfirmacionEliminar() {

                    MensajeDosBotonesUnaAccion();

                };


                function MensajeFinalizar() {
                    MensajeUnBotonUnaAccion();
                }

                function MensajeConfirmacion() {
                    MensajeDosBotonesUnaAccion();
                }

            </script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSalirM2B2A" />
            <asp:PostBackTrigger ControlID="btnContinuarM2B2A" />
            <asp:PostBackTrigger ControlID="btnAceptarM1B1A" />
            <asp:PostBackTrigger ControlID="btnAceptarM2B1A" />
            <asp:PostBackTrigger ControlID="btnConsulta" />
            <asp:PostBackTrigger ControlID="btnAceptar" />
            <asp:PostBackTrigger ControlID="btnRegresar" />
            <asp:PostBackTrigger ControlID="btnExportaExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
