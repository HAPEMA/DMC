<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MantencionUsuario.aspx.cs" Inherits="DMC.DMC.Administracion.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        /* Estilos generales */
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
            width: 700px;
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
        #GridViewSubFamilia {
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
                    width: 80%;
                    margin-left: 0;
                    margin-top: 10px;
                }
        }

        #GridViewSubFamiliaCss {
            display: flex;
            justify-content: center;
            align-content: center;
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

        #GridViewCuentasCss {
            display: flex;
            justify-content: center;
            align-content: center;
        }
    </style>


    <asp:Panel ID="PanelAgrearFa" runat="server" CssClass="modal-panel" Visible="False">
        <div class="modal-content">
            <span class="close" onclick="document.getElementById('<%= PanelAgrearFa.ClientID %>').style.display='none'">&times;</span>

            <h1 style="font-family: Arial; font-size: 25px;">Agregar Usuario</h1>

            <div class="form-group">
                <asp:Label ID="lbtNombre" runat="server" Text="Nombre:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Colocar Nombre"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="A.Paterno:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="TextPaterno" runat="server" CssClass="form-control" placeholder="Colocar apellido Paterno"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="A.Materno:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="TextMaterno" runat="server" CssClass="form-control" placeholder="Colocar apellido Materno"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="Label3" runat="server" Text="Clave:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="TextClave" runat="server" CssClass="form-control" placeholder="Colocar clave"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="Label4" runat="server" Text="Correo:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="TextCorreo" runat="server" CssClass="form-control" placeholder="Correo@grupodmc.cl"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Button ID="btnagregar" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="Guardar_Click" />
            </div>
        </div>
    </asp:Panel>



    <div id="filters">
        <asp:TextBox ID="txtTextoBusqueda" runat="server" placeholder="Filtrar"></asp:TextBox>
        <asp:Button ID="BtnLogin" runat="server" Text="Buscar" OnClick="ButtonMostrar_Click" />
    </div>


    <div id="GridViewCuentasCss">
        <asp:GridView ID="GridViewUsuario" runat="server" AutoGenerateColumns="false" OnRowCommand="GridViewServicios_RowCommand" AllowPaging="True" PageSize="20" OnPageIndexChanging="GridViewServicios_PageIndexChanging" DataKeyNames="ID_USUARIO">
            <Columns>
                <asp:BoundField DataField="ID_USUARIO" HeaderText="#" HeaderStyle-Width="40px" />
                <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" HeaderStyle-Width="80px" />
                <asp:BoundField DataField="APPAT" HeaderText="Apellido" HeaderStyle-Width="90px" />
                <asp:BoundField DataField="CLAVE" HeaderText="Clave" HeaderStyle-Width="110px" />
                <asp:BoundField DataField="CORREO" HeaderText="Correo" HeaderStyle-Width="200px" />
                <asp:BoundField DataField="Activo" HeaderText="Estado" HeaderStyle-Width="70px" />

                <asp:TemplateField HeaderText="A/D" HeaderStyle-Width="50px">
                    <ItemTemplate>
                        <asp:Button ID="btUnaUOtra" runat="server" Text="A/D" OnClientClick="return confirm('¿Estás seguro de ejecutar esta acción?');" CommandName="Activar_Desactivar" CommandArgument='<%# Container.DataItemIndex %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Agregar" HeaderStyle-Width="80px">
                    <ItemTemplate>
                        <asp:Button ID="MostrarFormulario" runat="server" Text="Agregar" CommandName="Agregar" CommandArgument='<%# Container.DataItemIndex %>' OnClick="MostrarAgregar" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
