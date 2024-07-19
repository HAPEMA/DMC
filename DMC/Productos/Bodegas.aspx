<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bodegas.aspx.cs" Inherits="DMC.DMC.Productos.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<style>
    #GridViewBodega {
        max-width: 100%; /* Ajusta el ancho máximo del GridView */
        overflow-x: auto; /* Agrega una barra de desplazamiento horizontal si es necesario */
        table-layout: fixed; /* Controla mejor el ancho de las celdas */
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
        font-size: 13px;
    }

    h1 {
        color: #6a1b9a;
        text-align: center;
        margin-top: 20px;
    }

    input, select {
        background-color: #6a1b9a;
        color: #fff;
        width: 100%; /* Ancho del 100% para dispositivos más pequeños */
        border: none;
        border-radius: 5px;
        cursor: pointer;
    }

    table {
        margin-left:20%;
        width: 800px;
        margin-top: 20px;
        border-collapse: collapse;
        margin-bottom: 60px;
        /*table-layout: fixed;*/ /* Controla mejor el ancho de las celdas */
    }

    .small-width {
        width: auto;
        padding: 5px 10px;
        white-space: nowrap;
    }

    .fixed-width {
        width: 100px; /* Ajusta este valor según tus necesidades */
    }

    table th,
    table td {
        border: 1px solid #dddddd;
        text-align: left;
        padding: 4px;
    }

    table th {
        background-color: #6a1b9a;
        color: #fff;
    }

    @media screen and (max-width: 600px) {
        #GridViewBodega th, #GridViewBodega td {
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
    max-width: 500px;
    position: relative;
}

/* Estilo del botón de cierre */
.close {
    color: #333;
    float: right;
    font-size: 24px;
    font-weight: bold;
    cursor: pointer;
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



</style>





    <asp:Panel ID="ModalPanel" runat="server" CssClass="modal-panel" Visible="False">
        <div class="modal-content">
            <span class="close" onclick="document.getElementById('<%= ModalPanel.ClientID %>').style.display='none'">&times;</span>

            <h1 style="font-family: Arial; font-size: 25px;">Actualizar Bodega  </h1>

            <div class="mb-3">
                <asp:Label ID="LBlID" runat="server" Text="ID:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="TextID" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>

            <div class="mb-3">
                <asp:Label ID="LblCodigobo" runat="server" Text="Codigo Bodega:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="TextBodega" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <asp:Label ID="LblCodig" runat="server" Text="Nombre Bodega:"></asp:Label>
                <asp:TextBox ID="TxtNombreBo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <hr />

            <div class="mb-3">
                <asp:Button ID="actualizar" runat="server" Text="Actualizar" OnClick="btnEnviar_Click" Visible="false" />
            </div>

            <div>
                <asp:Button ID="volvercasa" runat="server" Text="Cerrar" OnClick="OculatarActualizar"></asp:Button>
            </div>

        </div>
    </asp:Panel>







  <asp:GridView ID="GridViewBodega" runat="server" AutoGenerateColumns="false" OnRowCommand="GridViewServicios_RowCommand" OnRowEditing="GridViewBodega_RowEditing" DataKeyNames="id_bodega">
    <Columns>
        <asp:BoundField DataField="id_bodega" HeaderText="#" ItemStyle-CssClass="small-width" HeaderStyle-Width="50px" />
        <asp:BoundField DataField="codigo_bodega" HeaderText="C.Bodega" ItemStyle-CssClass="small-width" HeaderStyle-Width="50px" /> 
        <asp:BoundField DataField="Nombre_Bodega" HeaderText="N.Bodega" ItemStyle-CssClass="small-width" HeaderStyle-Width="100px" />
        <asp:BoundField DataField="activo" HeaderText="Estado" ItemStyle-CssClass="small-width" HeaderStyle-Width="50px" />

        <asp:TemplateField HeaderText="Editar" ItemStyle-CssClass="small-width" HeaderStyle-Width="30px">
            <ItemTemplate>
                <asp:Button ID="lnkEditar" runat="server" Text="Editar" CommandName="Edit"  OnClick="MostrarModal" CommandArgument='<%# Eval("id_bodega") %>' CssClass="small-width"/>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="A/D" ItemStyle-CssClass="small-width" HeaderStyle-Width="30px">
            <ItemTemplate>
                <asp:Button ID="btUnaUOtra" runat="server" Text="A/D" OnClientClick="return confirm('¿Estás seguro de ejecutar esta acción?');" CommandName="Activar" CommandArgument='<%# Container.DataItemIndex %>' CssClass="small-width"/>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>


</asp:Content>