<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Documentos.aspx.cs" Inherits="DMC.DMC.Documentos.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        #GridViewServicios {
            max-width: 100%; /* Ajusta el ancho máximo del GridView */
            overflow-x: auto; /* Agrega una barra de desplazamiento horizontal si es necesario */
        }

        body, html {
            height: 100%;
            margin: 0;
            padding: 0;
        }

        body {
            font-family: 'Arial', sans-serif;
            background-color: #f8f8f8;
            color: #333;
            margin: 0;
            font-size:13px;
        }

        h1 {
            color: #6a1b9a;
            text-align: center;
            margin-top: 20px;
        }

        select {
            height: 50px;
            width: 100%; /* Ancho del 100% para dispositivos más pequeños */
            max-width: 200px; /* Máximo ancho para evitar que ocupe toda la pantalla en dispositivos grandes */
            background-color: #fff;
            color: #6a1b9a;
            border: 1px solid #6a1b9a;
            border-radius: 5px;
            margin-bottom: 10px;
        }

        table {
            width: 100%;
            margin-top: 20px;
            border-collapse: collapse;
            margin-bottom: 60px;
        }

            table th,
            table td {
                border: 1px solid #dddddd;
                text-align: left;
                padding: 8px;
            }

            table th {
                background-color: #6a1b9a;
                color: #fff;
            }

        @media screen and (max-width: 600px) {
            #GridViewServicios th, #GridViewServicios td {
                max-width: 200px;
                white-space: nowrap;
            }
        }

/* Estilo del panel modal */
.modal-panel {
    position: fixed;
    z-index: 1000;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    overflow: auto;
    background-color: rgba(0, 0, 0, 0.7);
    display: flex;
    align-items: center;
    justify-content: center;
    padding: 0;
}

/* Estilo del contenido del modal */
.modal-content {
    background-color: #fff;
    margin: auto;
    padding: 20px;
    border-radius: 8px;
    box-shadow: 0 5px 15px rgba(0, 0, 0, 0.5);
    width: 80%;
    max-width: 1000px;
    position: relative;
}

/* Estilo del botón de cierre */
.close {
    color: #333;
    float: right;
    font-size: 24px;
    font-weight: bold;
    cursor: pointer;
    max-width:20px;
}

/* Estilo del botón de cierre en hover y focus */
.close:hover,
.close:focus {
    color: #000;
    text-decoration: none;
}

/* Estilo para los encabezados */
h2 {
    font-family: 'Arial', sans-serif;
    color: #333;
    text-align: center;
    margin-bottom: 20px;
}

/* Estilo para las etiquetas strong */
strong {
    font-weight: bold;
    color: #555;
}

/* Estilo para los contenedores flex */
.flex-end {
    display: flex;
    justify-content: flex-end;
    align-items: center;
    margin-bottom: 10px;
}

/* Estilo para el área de observaciones */
textarea {
    width: 100%;
    height: 80px;
    padding: 10px;
    border: 1px solid #ccc;
    border-radius: 4px;
    resize: vertical;
}

/* Estilo para las etiquetas dentro del contenedor */
.modal-content div > strong {
    display: block;
    margin-bottom: 5px;
}

/* Estilo para las etiquetas de valor */
.modal-content div > asp\\:Label {
    margin-left: 10px;
}

/* Estilo del botón Buscar */
#BtnLogin {
    background-color: #6a1b9a;
    color: #fff;
    width: 100%;
    padding: 10px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
}

/* Estilo del cuadro de texto Filtrar */
#txtTextoBusqueda {
    margin-left: 950px;
    color: #6a1b9a;
    background-color: #fff;
    width: 250px;
    border: 1px solid #6a1b9a;
    padding: 5px;
    border-radius: 4px;
}

/* Estilo del contenedor flex-column */
.flex-column {
    display: flex;
    align-items: center; /* Alineación vertical centrada */
    margin-bottom: 10px; /* Espacio entre las filas */
}

/* Estilo para los divs internos para alinearlos horizontalmente */
.flex-column div {
    display: flex;
    align-items: center; /* Alineación vertical centrada */
    margin-bottom: 10px; /* Espacio entre las filas */
}

/* Estilo para el strong y el Label para mantener el espacio entre ellos */
.flex-column strong {
    margin-right: 10px; /* Espacio entre el texto fuerte y la etiqueta */
}

/* Estilo adicional para los contenedores flex específicos */
.flex-space-between {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 10px;
}

.flex-column-items {
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    align-items: flex-start;
}

/*  .container {
      width: 100%;
      max-width: 100px;
      margin: 0 auto;
      padding: 20px;
       border-radius: 5px;
      box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  }*/


    .filter-container {
        display: flex;
        justify-content: center;
        align-items: center;
        margin-bottom: 20px;
    }

    .filter-container div, .filter-container input, .filter-container select, .filter-container button {
        margin: 0 10px;
    }

    .filter-container button {
        background-color: #6a1b9a;
        color: #fff;
        width: 100px;
        padding: 10px;
        border: none;
        border-radius: 5px;
        cursor: pointer;
    }

    .filter-container input {
        color: #6a1b9a;
        background-color: #fff;
        width: 250px;
        border: 1px solid #6a1b9a;
        padding: 10px;
        border-radius: 5px;
    }

    .filter-container select {
        color: #6a1b9a;
        background-color: #fff;
        border: 1px solid #6a1b9a;
        padding: 10px;
        border-radius: 5px;
    }

</style>

<div class="filter-container">
    <div>
        <asp:DropDownList ID="DropAño" runat="server"> </asp:DropDownList>

        <asp:DropDownList ID="DropMes" runat="server"> </asp:DropDownList>
    </div>
        <asp:TextBox ID="txtTextoBusqueda" runat="server" placeholder="Filtrar"></asp:TextBox>
        <asp:Button ID="BtnLogin" runat="server" Text="Buscar" OnClick="ButtonMostrar_Click" />
</div>



<!-- Modal Panel -->
    <asp:Panel ID="ModalPanel" runat="server" CssClass="modal-panel" Visible="False">
    <div class="modal-content">
        <span class="close" onclick="document.getElementById('<%= ModalPanel.ClientID %>').style.display='none'">&times;</span>

        <div id="Hearder" style="border: red solid; padding: 10px; display:flex; ">



            <div id="Img-header" style="border: green solid; margin: 10px; max-width: 100px; max-height: 100px;">
                <asp:Image ID="IMGdmc" runat="server" ImageUrl="~/IMG/dmc.jpg" Style="width: 100px; height: 100px; object-fit: cover;" />
            </div>


            <div id="Titulo-Header" style="border: green solid; margin: 10px 150px 10px 90px; max-width:600px">
                <h2 style="font-family: Arial; font-size: 15px;">COMERCIALIZADORA DMC S.P.A<br />
                    RUT: 76269438-7 / Dirección: Madrid 1269, Santiago / Teléfono +562 2 5555739
                </h2>
            </div>

            <div id="Folio-header"  style=" border: green solid; margin:10px;">
                <div class="flex-space-between">
                    <div class="flex-column">
                        <strong>Folio:</strong>
                        <asp:Label ID="lblFolio" runat="server"></asp:Label>
                    </div>
 
                </div>
            </div>
        </div>

        <div id="Posgridview"  style="border: blue solid;  padding:10px; display:flex;" >
            <div id="Clientes-Posgridview"  style=" border: green solid; margin:10px; max-width:750px;">
                <div style="margin-bottom: 20px;">
                    <strong>Cliente:</strong><br />
                    <asp:Label ID="RazonSocialCliente" runat="server"></asp:Label><br />
                    <asp:Label ID="RutCliente" runat="server"></asp:Label><br />
                    <asp:Label ID="DireccionCliente" runat="server"></asp:Label>
                </div>
            </div>
            <div id="Ejecutivo-posgridview"  style=" border: green solid; margin:10px;">
                <div class="flex-column">
                    <strong>Ejecutivo:</strong>
                    <asp:Label ID="lblEjecutivo" runat="server"></asp:Label>
                </div>
            </div>
        </div>

        <div id="Gridview"  style=" border: red solid; padding:10px;">
            <asp:GridView ID="GridViewModal" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="LINEA" HeaderText="LINEA" />
                    <asp:BoundField DataField="CODIGO_MAESTRO" HeaderText="COD. ARTÍCULO" />
                    <asp:BoundField DataField="DESCRIPCION_PRODUCTO" HeaderText="DESCRIPCIÓN" />
                    <asp:BoundField DataField="CANTIDAD" HeaderText="CANT" />
                    <asp:BoundField DataField="VALOR_UNITARIO" HeaderText="VALOR UNITARIO" />
                    <asp:BoundField DataField="DESCUENTOS" HeaderText="DESCUENTOS (%)" />
                    <asp:BoundField DataField="TOTAL_LINEA" HeaderText="TOTAL" />
                </Columns>
            </asp:GridView>
        </div>

        <div id="ValoresFinal"  style=" border: red solid;  padding:10px; display:flex;">
            <div id="Descricion-final"  style=" border: green solid; margin:10px; max-width:430px;">
                <div style="margin-bottom: 20px;">
                    <strong>Observaciones:</strong><br />
                    <asp:Label ID="ObserCliente" runat="server" TextMode="MultiLine" Rows="3"></asp:Label>
                </div>
            </div>
            <div id="ValorpagoFinal"  style=" border: green solid; margin:10px; max-width:200px;">
                <div class="flex-column-items">
                    <div class="flex-end">
                        <strong>Sub Total:</strong>
                        <asp:Label ID="ValorSubTotal" runat="server"></asp:Label>
                    </div>
                    <div class="flex-end" >
                        <strong>IVA:</strong>
                        <asp:Label ID="ValorIVA" runat="server"></asp:Label>
                    </div>
                    <div class="flex-end">
                        <strong>Total:</strong>
                        <asp:Label ID="ValorTotal" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Panel>


    <asp:GridView ID="GridViewCotizacion" runat="server" AutoGenerateColumns="false" AllowPaging="True" PageSize="15" OnPageIndexChanging="GridViewServicios_PageIndexChanging" OnRowCommand="GridViewCotizacion_RowCommand" DataKeyNames="ID_DOCADMINISTRATIVO">
        <Columns>
            <asp:BoundField DataField="ID_DOCADMINISTRATIVO" HeaderText="#" Visible="true" />
            <asp:BoundField DataField="FOLIO_DOC" HeaderText="Folio" Visible="true" />
            <asp:BoundField DataField="FECHA_DOCUMENTO" HeaderText="Fecha" />
            <asp:BoundField DataField="RZN_SOCIAL" HeaderText="Razon social" />
            <asp:BoundField DataField="PROYECTO" HeaderText="Proyecto" />
            <asp:BoundField DataField="TOTAL_DOCUMENTO" HeaderText="Total" />
            <asp:BoundField DataField="ID_ESTADOCUMENTO" HeaderText="Estado" />
            <asp:BoundField DataField="NOMBRE" HeaderText="Ejecutivo" />

            <asp:TemplateField HeaderText="ACTUALIZAR">
                <ItemTemplate>
                    <asp:Button ID="DescargarPDF" runat="server" Text="PDF" CommandName="PDFRow" CommandArgument='<%# Container.DataItemIndex %>' />

                    <asp:Button ID="VerCotizacion" runat="server" OnClick="MostrarModal" Text="Ver" CommandName="VerRow" CommandArgument='<%# Container.DataItemIndex %>' />
                    
                    <asp:Button ID="EditarCotizacion" runat="server" Text="Editar" CommandName="EditRow" CommandArgument='<%# Container.DataItemIndex %>' />
                    <asp:Button ID="ClonarCotizacion" runat="server" Text="Clonar" CommandName="ClonarRow" CommandArgument='<%# Container.DataItemIndex %>' />
                    <asp:Button ID="btdesactivar" runat="server" Text="ELIMINAR" OnClientClick="return confirm('¿Estás seguro que deseas ELIMINAR?');" CommandName="Desactivar" CommandArgument='<%# Container.DataItemIndex %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
