<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaestroProductos.aspx.cs" Inherits="DMC.DMC.Productos.WebForm4" %>

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


    <div style="display: flex; flex-wrap: nowrap; flex-direction: row-reverse;">
        <div>
            <asp:Button ID="BtnLogin" runat="server" Text="Buscar" Style="background-color: #6a1b9a; margin-right: 200px; color: #fff; width: 100%; padding: 10px; border: none; border-radius: 5px; cursor: pointer;" OnClick="ButtonMostrar_Click" />
        </div>
        <asp:TextBox ID="txtTextoBusqueda" runat="server" Style="color: #6a1b9a; background-color: #fff; width: 275px; border: 1px solid #6a1b9a; margin-left: 600px;" placeholder="Filtrar"></asp:TextBox>
    </div>

    <asp:Panel ID="ModalPanelBodega" runat="server" CssClass="modal-panel" Visible="False">
        <div class="modal-content">
            <span class="close" onclick="document.getElementById('<%= ModalPanelBodega.ClientID %>').style.display='none'">&times;</span>
            <div class="modal-body">
                <h1 style="font-family: Arial;">Saldos Por Bodega</h1>
                <asp:GridView ID="GridViewSaldos" runat="server" AutoGenerateColumns="False" CssClass="table">
                    <Columns>
                        <asp:BoundField DataField="Nombre_Bodega" HeaderText="BODEGA" />
                        <asp:BoundField DataField="Saldo" HeaderText="SALDO" />
                    </Columns>
                    <HeaderStyle BackColor="#e1bee7" />
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>


    <asp:Panel ID="ModalPanel" runat="server" CssClass="modal-panel" Visible="False">
        <div class="modal-content">
            <span class="close" onclick="document.getElementById('<%= ModalPanel.ClientID %>').style.display='none'">&times;</span>

            <h1 style="font-family: Arial; font-size: 50px;">Actualizar Bodega  </h1>

            <asp:Panel runat="server" Visible="True">
                <div class="mb-3 row align-items-center">
                    <div class="col-auto" style="padding-right:95px">
                        <asp:Label ID="LBlID" runat="server" Text="ID:" CssClass="form-label mb-0"></asp:Label>
                    </div>
                    <div class="col">
                        <asp:TextBox ID="TextID" runat="server" CssClass="form-control" ReadOnly="false"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>

            <div class="mb-3 row align-items-center">
                <div class="col-auto" style="padding-right:68px">
                    <asp:Label ID="LblCodigoPro" runat="server" Text="Codigo:" CssClass="form-label mb-0"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="TextProducto" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="mb-3 row align-items-center">
                <div class="col-auto" style="padding-right:42px">
                    <asp:Label ID="LblDescrip" runat="server" Text="Descripción:" CssClass="form-label mb-0"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="TxtDescrip" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="mb-3 row align-items-center">
                <div class="col-auto" style="padding-right:60px">
                    <asp:Label ID="Label1" runat="server" Text="Cantidad:" CssClass="form-label mb-0"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="TextCantidadPro" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="mb-3 row align-items-center">
                <div class="col-auto" style="padding-right:65px">
                    <asp:Label ID="Label2" runat="server" Text="Formato:" CssClass="form-label mb-0"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="TextFormatoMaestro" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="mb-3 row align-items-center">
                <div class="col-auto" style="padding-right:65px">
                    <asp:Label ID="familia" runat="server" Text="FAMILIA:" CssClass="form-label mb-0"></asp:Label>
                </div>
                <div class="col">
                    <asp:DropDownList ID="ddlfamilia" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                </div>
            </div>

            <div class="mb-3 row align-items-center">
                <div class="col-auto" style="padding-right:20px">
                    <asp:Label ID="subfamilia" runat="server" Text="ID SUBFAMILIA:" CssClass="form-label mb-0"></asp:Label>
                </div>
                <div class="col">
                    <asp:DropDownList ID="ddlsubfamilia" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                </div>
            </div>

            <div class="mb-3 row align-items-center">
                <div class="col-auto" style="padding-right:73px">
                    <asp:Label ID="Label3" runat="server" Text="Estado:" CssClass="form-label mb-0"></asp:Label>
                </div>
                <div class="col">
                    <asp:DropDownList ID="DropEstados" runat="server" CssClass="form-control" AutoPostBack="false">
                        <asp:ListItem Text="Elija un tipo de Estado" Value="-1" Selected="True" Disabled="True"></asp:ListItem>
                        <asp:ListItem Value="OK">ok</asp:ListItem>
                        <asp:ListItem Value="Reparacion">Reparación</asp:ListItem>
                        <asp:ListItem Value="Dañado">Dañado</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="mb-3 row align-items-center">
                <div class="col-auto" style="padding-right:78px">
                    <asp:Label ID="Label4" runat="server" Text="Activo:" CssClass="form-label mb-0"></asp:Label>
                </div>
                <div class="col">
                    <asp:DropDownList ID="DropActivo" runat="server" CssClass="form-control" AutoPostBack="false">
                        <asp:ListItem Value="S">Activo</asp:ListItem>
                        <asp:ListItem Value="N">Desactivado</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="mb-3 row">
                <asp:Button ID="Actualizar" runat="server" Text="Actualizar" OnClick="btnEnviar_Click" CssClass="btn btn-primary" Visible="false" />
            </div>

        </div>
    </asp:Panel>


    <asp:GridView ID="GridViewMaestro" runat="server" AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnRowCommand="GridViewServicios_RowCommand" OnRowEditing="GridViewBodega_RowEditing" OnPageIndexChanging="GridViewServicios_PageIndexChanging" DataKeyNames="id_maestro">
        <Columns>
            <asp:BoundField DataField="id_maestro" HeaderText="#" />
            <asp:BoundField DataField="codigo_maestro" HeaderText="Codigo" />
            <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
            <asp:BoundField DataField="unidades" HeaderText="Unidades" />
            <asp:BoundField DataField="formato" HeaderText="Formato" />
            <asp:BoundField DataField="descripcionn" HeaderText="Familia" />
            <asp:BoundField DataField="codigo_subfamilia" HeaderText="SubFamilia" />
            <asp:BoundField DataField="Estado_Item" HeaderText="Estado" />
            <asp:BoundField DataField="activo" HeaderText="Activo" />

            <asp:TemplateField HeaderText="Editar">
                <ItemTemplate>
                    <asp:Button ID="lnkEditar" runat="server" Text="Editar" CommandName="Edit" CommandArgument='<%# Eval("id_maestro") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Ver">
                <ItemTemplate>
                    <asp:Button ID="btVer" runat="server" Text="Ver" CommandName="Ver" CommandArgument='<%# Container.DataItemIndex %>' />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
</asp:Content>