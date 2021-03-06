<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Motivo.ascx.vb" Inherits="SEPRIS.motivo" %>
<asp:UpdatePanel ID="upnlConsulta" runat="server">
      <ContentTemplate>

        <script language="javascript" type="text/javascript">

            function validaLimiteLongitudLocal(obj, maxchar) {
                if (this.id) obj = this; 
                var remaningChar = maxchar - obj.value.length; 
                //document.getElementById('lblComentariosCaracteresMot').innerHTML = remaningChar;
                $("#<%=lblComentariosCaracteresMot.ClientID%>").text("" + remaningChar);

                    if (remaningChar <= 0) {
                        //document.getElementById('lblComentariosCaracteresMot').innerHTML = 0;
                        $("#<%=lblComentariosCaracteresMot.ClientID%>").text("" + 0);
                    obj.value = obj.value.substring(maxchar, 0);
                    return false;
                }
                else { return true; }
            }


            function ValidateText(obj) {
                if (obj.value.length > 0) {
                    obj.value = obj.value.replace(/[^\d]+/g, '');
                }
            }
        </script>

        <div id="divComentarios">
            <table style="width: 95%">
                <tr runat="server" clientidmode="Static">
                    <td style="text-align: left; width: 15%" class="auto-style1">
                        <asp:Label ID="LblComentarios" runat="server" Text="Motivo" CssClass="txt_gral"></asp:Label>
                    </td>
                    <td style="text-align: left;" class="auto-style2" rowspan="2">
                        <asp:TextBox ID="txtMotivo" runat="server" onkeypress="ReemplazaCEspeciales(this.id)"
                            onblur="ReemplazaCEspeciales(this.id)" onfocus="ReemplazaCEspeciales(this.id)"
                            onkeyup="validaLimiteLongitudLocal(this,500)"
                            TextMode="MultiLine" Width="100%" MaxLength="500" Height="70px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4" colspan="2"></td>
                </tr>
                <tr id="ObjCaracteres" runat="server" clientidmode="Static">
                    <td style="text-align: right;" colspan="2">
                        <div id="divConttxtObjetoOPI" runat="server" style="width: 100%; text-align: right; float: left;">
                            <asp:Label runat="server" ID="lblAsterico" CssClass="AsteriscoHide" Text="*"><samp style="color: red; font-size: 1.3em"><b>&nbsp;*</b></samp></asp:Label>
                            <asp:Label runat="server" ID="lblDescripcionContadorMot" Style="color: red; font-size: 9pt" Text="Caracteres restantes: "></asp:Label>
                            <asp:Label runat="server" ID="lblComentariosCaracteresMot" Style="color: red; font-size: 9pt" Text="500" ></asp:Label>
                        </div>
                        <div id="div1" runat="server" style="width: 100%; text-align:right; float: left;">
                            <asp:Label runat="server" ID="Label1" CssClass="AsteriscoHide" Text="*"><samp style="color: red ; font-size: 1.3em"><b>&nbsp;*</b></samp></asp:Label>
                            <asp:Label runat="server" ID="Label2" Style="color: red; font-size: 9pt" Text="Campo Obligatorio"></asp:Label>
                             </div> 
                    </td>
                   
                    
              

                </tr>
            </table>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
