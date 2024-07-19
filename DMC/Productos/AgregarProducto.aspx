<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgregarProducto.aspx.cs" Inherits="DMC.DMC.Productos.WebForm6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        #GridViewServicios {
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
            background-color: #f8f8f8;
            color: #333;
            margin: 0;
            font-size:13px;
        }

        h1 {
            color: #6a1b9a;
            text-align: center;
            margin-top: 15px;
            font-size: 30px;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .container {
            max-width: 600px;
            margin: 0 auto;
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

        select {
            height: 50px;
            width: 100%;
            max-width: 280px;
            background-color: #fff;
            color: #6a1b9a;
            border: 1px solid #6a1b9a;
            border-radius: 5px;
            margin-bottom: 10px;
        }

        input {
            background-color: #6a1b9a;
            color: #fff;
            width: 100%;
            padding: 5px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin-top: 5px;
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

        .panel-container {
            margin-top: 5px;
        }

        .panel-top-margin {
            margin-top: 5px;
        }

        @media screen and (max-width: 600px) {
            #GridViewServicios th, #GridViewServicios td {
                max-width: 200px;
                white-space: nowrap;
            }
        }

        .form-group {
            display: flex;
            align-items: center;
            margin-bottom: 10px;
        }

        .form-group label {
            flex: 0 0 150px;
            margin-right: 10px;
        }

        .form-group input,
        .form-group select,
        .form-group .aspNet-TextBox {
            flex: 1;
        }

        .container {
            max-width: 600px;
            margin: 0 auto;
        }

        .mb-3 {
            margin-bottom: 1rem;
        }
    </style>

    <h1>Agregar Productos</h1>

    <asp:Panel ID="PanelAgregarCliente" runat="server" Visible="true" CssClass="panel-top-margin">
        <div class="container mt-4 border p-4 rounded " style="max-width: 600px; margin: 0 auto;">

            <!-- Campo oculto con la letra 'S' -->
            <asp:Label ID="lblHiddenS" runat="server" Text="1" Visible="false"></asp:Label>

            <div class="form-row">
                <asp:Label ID="codigomaestro" runat="server" Text="Codigo Maestro:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtcodigo" runat="server" CssClass="form-control" placeholder="Ingresar un codigo"></asp:TextBox>
            </div>

            <div class="form-row">
                <asp:Label ID="descripcion" runat="server" Text="Descripción:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtdescripcion" runat="server" CssClass="form-control" placeholder="Ingresar una Descripción"></asp:TextBox>
            </div>

            <div class="form-row">
                <asp:Label ID="unidades" runat="server" Text="Unidades:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtunidades" runat="server" CssClass="form-control" placeholder="Ingresar Unidades"></asp:TextBox>
            </div>

            <div class="form-row">
                <asp:Label ID="formato" runat="server" Text="Formato:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtformato" runat="server" CssClass="form-control" placeholder="Formato"></asp:TextBox>
            </div>

            <div class="form-row">
                <asp:Label ID="familia" runat="server" Text="Familia:" CssClass="form-label"></asp:Label>
                <asp:DropDownList ID="ddlfamilia" runat="server" CssClass="form-control" AutoPostBack="false" placeholder="Elije una Familia"></asp:DropDownList>
            </div>

            <div class="form-row">
                <asp:Label ID="subfamilia" runat="server" Text="SubFamilia:" CssClass="form-label"></asp:Label>
                <asp:DropDownList ID="ddlsubfamilia" runat="server" CssClass="form-control" placeholder="Elije una SubFamilia"></asp:DropDownList>
            </div>

            <div class="form-row">
                <asp:Label ID="estado" runat="server" Text="ESTADO ITEM:" CssClass="form-label"></asp:Label>
                <asp:DropDownList ID="ddlestado" runat="server" CssClass="form-control">
                    <asp:ListItem Text="OK" Value="OK"></asp:ListItem>
                    <asp:ListItem Text="Reparación" Value="Reparación"></asp:ListItem>
                    <asp:ListItem Text="Dañado" Value="Dañado"></asp:ListItem>
                </asp:DropDownList>

            </div>

            <div class="form-row">
                <asp:Label ID="lblImagen" runat="server" Text="IMG:" CssClass="form-label"></asp:Label>
                <asp:FileUpload ID="fileUploadImagen" runat="server" CssClass="form-control" />
            </div>

            <div class="form-row">
                <asp:Button ID="Guardar" runat="server" Text="Guardar" OnClick="Guardar_Click" />
            </div>

        </div>
    </asp:Panel>
</asp:Content>