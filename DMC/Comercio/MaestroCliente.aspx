<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaestroCliente.aspx.cs" Inherits="DMC.DMC.Comercio.WebForm1" %>
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
        font-size:12px;
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
        width: 1000px;
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
            max-width: 200px;
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


<h1>Clientes DMC</h1>

<div id="filters">
        <asp:TextBox ID="txtTextoBusqueda" runat="server" placeholder="Filtrar"></asp:TextBox>
        <asp:Button ID="BtnLogin" runat="server" Text="Buscar" OnClick="ButtonMostrar_Click" />
</div>
    <!-- Botón "Agregar Cliente" -->
                <div class="form-row">
                    <asp:Button ID="AgregarCliente" runat="server" Text="+ AGREGAR" CssClass="btn btn-primary" OnClick="btnAgregarcliente" />
                </div>

    <div id="GridViewClientesCSS">
    <asp:GridView ID="GridViewClientes" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridViewClientes_PageIndexChanging" OnRowCommand="GridViewServicios_RowCommand" OnRowEditing="GridViewClientes_RowEditing" DataKeyNames="ID_CLIENTE">
        <Columns>
            <asp:BoundField DataField="ID_CLIENTE" HeaderText="#" HeaderStyle-Width="50px"/>
            <asp:BoundField DataField="RUT_CLIENTE" HeaderText="RUT" HeaderStyle-Width="100px" />
            <asp:BoundField DataField="RZN_SOCIAL" HeaderText="RAZON SOCIAL" HeaderStyle-Width="350px"/>
            <asp:BoundField DataField="DIRECCION" HeaderText="DIRECCION" HeaderStyle-Width="350px"/>

            <asp:TemplateField HeaderText="Acciones" HeaderStyle-Width="90px">
                <ItemTemplate>
                    <asp:Button ID="lnkEditar" runat="server" Text="Editar" CommandName="Edit" CommandArgument='<%# Eval("ID_CLIENTE") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="ADD" HeaderStyle-Width="90px">
                <ItemTemplate>
                    <asp:Button ID="verrr" runat="server" Text="VER" CommandName="sucursal" CommandArgument='<%# Container.DataItemIndex %>' />
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
        </div>
    <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje" />


    <!-- Modal para agregar nuevo cliente -->

    <asp:Panel ID="PanelCliente" runat="server" CssClass="modal-panel" Visible="false">
        <div class="modal-content">
            <span class="close" onclick="document.getElementById('<%= PanelCliente.ClientID %>').style.display='none'">&times;</span>

            <asp:Panel runat="server" ID="PanelTituloNormal" Visible="true">
                <h1 style="font-family: Arial; font-size: 25px;">Agregar Cliente</h1>
            </asp:Panel>
            
            <asp:Panel runat="server" ID="PanelTituloEditar" Visible="false">
                <h1 style="font-family: Arial; font-size: 25px;">Editar Cliente</h1>
            </asp:Panel>

                        <asp:Panel runat="server" Visible="false">
                <div class="mb-3">
                    <asp:Label ID="LBlID" runat="server" Text="ID:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="IDCliente" runat="server" CssClass="form-control" Visible="true"></asp:TextBox>
                </div>
            </asp:Panel>
                        <div class="form-row align-items-center">
                            <asp:Label ID="rutLabel" runat="server" Text="RUT:" CssClass="form-label mr-2"></asp:Label>
                            <asp:TextBox ID="txtrut1" runat="server" CssClass="form-control mr-2" MaxLength="9" placeholder="1.123.456-7"></asp:TextBox>
                        </div>
                        <div class="form-row">
                            <asp:Label ID="razonsocialLabel" runat="server" Text="RAZÓN SOCIAL:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtrazonsocial" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-row">
                            <asp:Label ID="contactoclienteLabel" runat="server" Text="CONTACTO CLIENTE:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtcontactocliente" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-row">
                            <asp:Label ID="telefonosLabel" runat="server" Text="TELEFONOS:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txttelefonos" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-row">
                            <asp:Label ID="correoLabel" runat="server" Text="EMAIL:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtcorreo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-row">
                            <asp:Label ID="direccionLabel" runat="server" Text="DIRECCION:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtdireccion" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-row">
                            <asp:Label ID="comunaLabel" runat="server" Text="COMUNA:" CssClass="form-label"></asp:Label>
                            <asp:TextBox ID="txtcomuna" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-row">
                            <asp:Label ID="regionLabel" runat="server" Text="REGION:" CssClass="form-label"></asp:Label>
                            <asp:DropDownList ID="ddlregiones" runat="server" CssClass="form-control" AutoPostBack="false">
                                <asp:ListItem Text="Seleccione Región" Value=""></asp:ListItem>
                                <asp:ListItem Text="XV Región de Arica y Parinacota" Value="Arica y Parinacota"></asp:ListItem>
                                <asp:ListItem Text="I Región de Tarapacá" Value="Tarapaca"></asp:ListItem>
                                <asp:ListItem Text="II Región de Antofagasta" Value="Antofagasta"></asp:ListItem>
                                <asp:ListItem Text="III Región de Atacama" Value="Atacama"></asp:ListItem>
                                <asp:ListItem Text="IV Región de Coquimbo" Value="Coquimbo"></asp:ListItem>
                                <asp:ListItem Text="V Región de Valparaíso" Value="Valparaiso"></asp:ListItem>
                                <asp:ListItem Text="RM Región Metropolitana" Value="R Metropolitana"></asp:ListItem>
                                <asp:ListItem Text="VI Región del Libertador General Bernardo O'Hiiggins" Value="Libertador General Bernardo O'Hiiggins"></asp:ListItem>
                                <asp:ListItem Text="VII Región del Maule" Value="Maule"></asp:ListItem>
                                <asp:ListItem Text="VIII Región del BíoBío" Value="BioBio"></asp:ListItem>
                                <asp:ListItem Text="IX Región de La Araucanía" Value="Araucania"></asp:ListItem>
                                <asp:ListItem Text="X Región de Los Lagos" Value="Los Lagos"></asp:ListItem>
                                <asp:ListItem Text="XI Región de Aysén del General Carlos Ibáñez del Campo" Value="Aysen"></asp:ListItem>
                                <asp:ListItem Text="XII Región de Magallanes y de la Antártica Chilena" Value="Magallanes"></asp:ListItem>
                                <asp:ListItem Text="XIV Región de Los Ríos" Value="Los Rios"></asp:ListItem>
                                <asp:ListItem Text="XVI Región de Ñuble" Value="Ñuble"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-row">
                            <asp:Label ID="Label9" runat="server" Text="EJECUTIVO DMC" CssClass="form-label"></asp:Label>
                            <asp:DropDownList ID="ddlejecutivodmc" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>


                        <div class="form-row">
                            <asp:Label ID="Label1" runat="server" Text="LISTA DE PRECIOS:" CssClass="form-label"></asp:Label>
                            <asp:DropDownList ID="ddlListarPrecio" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>

            <asp:Panel runat="server" Visible="false" ID="Panelbottoagregar">
                <div class="form-row">
                    <asp:Button ID="btnagregar" runat="server" Text="+ AGREGAR" CssClass="btn btn-primary" OnClick="btnagregar_Click" />
                </div>
            </asp:Panel>
            <asp:Panel runat="server" Visible="false" ID="PanelbottoActualizar">
                <div class="form-row">
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-primary" OnClick="btnEnviar_Click" />
                </div>
            </asp:Panel>

                    </div>
 </asp:Panel>
</asp:Content>