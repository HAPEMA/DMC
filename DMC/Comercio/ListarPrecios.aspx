<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListarPrecios.aspx.cs" Inherits="DMC.DMC.Comercio.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <style>
       #GridViewClientes {
        max-width: 100%; 
        overflow-x: auto;
    }

    body, html {
        height: 100%;
        margin: 0;
        padding: 0;
    }

    body {
        font-family: 'Arial', sans-serif;
        font-size:14px;
        background-color: #f8f8f8;
        color: #333;
        margin: 0;
    }

    h1 {
        color: #6a1b9a;
        text-align: center;
        margin-top: 15px;
        font-size: 30px;
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    }

.form-row {
    display: flex;
    align-items: center;
    margin-bottom: 15px;
}

.form-label {
    flex: 0 0 150px;
    margin-right: 10px;
    font-size: 15px;
}

.form-control {
    flex: 1;
    padding: 2px 2px;
    border: 1px solid #ddd;
    border-radius: 5px;
    height: auto;
 
}

.btn {
    padding: 10px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    margin-top: 5px;
    width: 100%;
}

.btn-primary {
    background-color: #6a1b9a;
    color: #fff;
}

.btn-secondary {
    background-color: #ddd;
    color: #333;
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
        width: 750px;
        margin-top: 20px;
        border-collapse: collapse;
        margin-bottom: 60px;
        table-layout: fixed; /* Controla mejor el ancho de las celdas */
    }

    table th,
    table td {
        border: 1px solid #dddddd;
        text-align: left;
        padding: 4px;
        overflow: hidden; /* Asegura que el contenido se mantenga dentro de la celda */
        text-overflow: ellipsis; /* Añade puntos suspensivos si el contenido es demasiado largo */
        white-space: nowrap; /* Evita el salto de línea */
    }

    table th {
        background-color: #6a1b9a;
        color: #fff;
    }

    /* Estilos específicos para GridViewInventario */
    #GridViewClientes {
        max-width: 100%; /* Ajusta el ancho máximo del GridView */
        overflow-x: auto; /* Agrega una barra de desplazamiento horizontal si es necesario */
    }

    /* Estilos para los filtros */
    #filters {
        display: flex;
        justify-content: flex-end; /* Alinea los filtros a la derecha */
        margin-bottom: 20px;
    }

    #filters input,
    #filters button {
        margin-left: 10px;
        padding: 10px;
        border: 1px solid #6a1b9a;
        border-radius: 5px;
        background-color: #fff;
        color: #6a1b9a;
    }

    #filters button {
        background-color: #6a1b9a;
        color: #fff;
        cursor: pointer;
    }

    /* Reglas específicas para dispositivos móviles */
    @media screen and (max-width: 600px) {
        table th,
        table td {
            max-width: 300px;
        }

        #filters {
            flex-direction: column;
            align-items: flex-start;
        }

        #filters input,
        #filters button {
            width: 100%;
            margin-left: 0;
            margin-top: 10px;
        }
    }
    #GridViewClientes{
        display:flex;
        justify-content:center;
        align-content:center;
    }
    #GridViewClientesCSS{
        display:flex;
        justify-content:center;
        align-content:center;
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

<h1>Maestro Lista de Precios</h1>

                <div class="form-row">
                    <asp:Button ID="AgregarPrecio" runat="server" Text="+ AGREGAR" CssClass="btn btn-primary" OnClick="AgregarListarPrecio"/>
                </div>

    <div id="GridViewClientesCSS">
    <asp:GridView ID="GridViewPrecio" runat="server" AutoGenerateColumns="false" DataKeyNames="ID_LISTAPRECIO" OnRowEditing="GridView_RowEditing">
        <Columns>
            <asp:BoundField DataField="ID_LISTAPRECIO" HeaderText="N° LISTA" HeaderStyle-Width="50px"/>
            <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION" HeaderStyle-Width="100px" />
            <asp:BoundField DataField="ACTIVO" HeaderText="ACTIVO" HeaderStyle-Width="50px"/>

            <asp:TemplateField HeaderText="EDITAR LISTA PRECIO" HeaderStyle-Width="80px">
                <ItemTemplate>
                    <asp:Button ID="lnkEditar" runat="server" Text="Editar" CommandName="Edit" CommandArgument='<%# Eval("ID_LISTAPRECIO") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Detalle Productos" HeaderStyle-Width="80px">
                <ItemTemplate>
                    <asp:Button ID="Detalle" runat="server" Text="Detalle" CommandName="Detalle" CommandArgument='<%# Container.DataItemIndex %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
        </div>
    <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje" />

        <asp:Panel ID="PanelActualizarPrecio" runat="server" CssClass="modal-panel" Visible="False">
        <div class="modal-content">
            <span class="close" onclick="document.getElementById('<%= PanelActualizarPrecio.ClientID %>').style.display='none'">&times;</span>

            <h1 style="font-family: Arial; font-size: 25px;">Editar Lista de Precio Cliente</h1>

            <asp:Panel runat="server" Visible="false">
                <div class="mb-3">
                    <asp:Label ID="LBlID" runat="server" Text="ID Lista:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="TextID" runat="server" CssClass="form-control" ReadOnly="false"></asp:TextBox>
                </div>
            </asp:Panel>

            <div class="mb-3">
                <asp:Label ID="LblCodigoFa" runat="server" Text="Descripción:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtDescrip" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <asp:Label ID="estado" runat="server" Text="Activo:" CssClass="form-label"></asp:Label>
                    <asp:DropDownList ID="ddlestado" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Activo" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Desactivado" Value="0"></asp:ListItem>
                    </asp:DropDownList>
            </div>

            <div class="mb-3">
                <asp:Button ID="actualizar" runat="server" Text="Actualizar" OnClick="btnEnviar_Click" Visible="false" />
            </div>

        </div>
    </asp:Panel>


    <asp:Panel ID="PanelAgrearPrecio" runat="server" CssClass="modal-panel" Visible="False">
        <div class="modal-content">
            <span class="close" onclick="document.getElementById('<%= PanelAgrearPrecio.ClientID %>').style.display='none'">&times;</span>

            <h1 style="font-family: Arial; font-size: 25px;">Agregar Precio</h1>

            <div class="mb-3">
                <asp:Label ID="Label2" runat="server" Text="Descripción:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="DescipPrecio" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <asp:Button ID="Guardar" runat="server" Text="Guardar" OnClick="Guardar_Click" Visible="true" />
            </div>
        </div>
    </asp:Panel>


</asp:Content>