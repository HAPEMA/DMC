<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IngresarMovimiento.aspx.cs" Inherits="DMC.DMC.Productos.WebForm7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        body {
            font-family: Arial, sans-serif;
            font-size:15px;
        }
        .container {
            width: 100%;
            max-width: 800px;
            margin: 0 auto;
            padding: 20px;
             border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .row {
            display: flex;
            align-items: center;
            margin-bottom: 15px;
        }

        .row label {
            width: 150px;
            margin-right: 10px;
            text-align: right;
        }

        .row input[type="text"],
        .row select, input[type="date"], .row DropDownList, input[type="number"] {
            flex: 1;
            padding: 5px;
            border: 1px solid #ccc;
                border-radius: 3px;
        }

        .row .btn-green {
            background-color: #28a745;
            color: white;
            border: none;
            padding: 10px 20px;
                cursor: pointer;
                border-radius: 3px;
        }

        .row .btn-green:hover {
            background-color: #218838;
        }
    </style>
        <div runat="server" id="divMensajeValidacion" style="color: red; font-weight: bold; text-align: center;"></div>

    <h1>Movimiento Inventario</h1>


    <div class="container">
        <div class="row">
            <label for="fecha">Fecha</label>
            <asp:TextBox ID="Idfecha" runat="server" TextMode="Date" placeholder="Ingresar la salida"></asp:TextBox>
        </div>
        <div class="row">
            <label for="bodega">Bodega</label>
            <asp:DropDownList ID="Idbodega" runat="server"></asp:DropDownList>
        </div>
        <div class="row">
            <label for="producto">Producto</label>
            <asp:TextBox ID="txtBusqueda" runat="server" Style="max-width: 200px; margin-right: 30px;" placeholder="Buscar producto..." onkeyup="autocompletarProductos()"></asp:TextBox>

            <asp:DropDownList ID="IDProducto" runat="server"></asp:DropDownList>

        </div>
        <div class="row">
            <label for="documento">Documento</label>

            <asp:DropDownList ID="DropDocumento" runat="server">
                <asp:ListItem Text="Tipo de Documento" Value="-1" Selected="True" Disabled="True"></asp:ListItem>
                <asp:ListItem Value="-1">S/D</asp:ListItem>
                <asp:ListItem Value="0002">Guia Despacho</asp:ListItem>
                <asp:ListItem Value="0042">Nota de Venta</asp:ListItem>
                <asp:ListItem Value="0052">Factura</asp:ListItem>
            </asp:DropDownList>
            <label for="noDocumento">Nro. Documento</label>
            <asp:TextBox ID="IDNumDocumento" runat="server" TextMode="Number" placeholder="Ingresar el N° Documento"></asp:TextBox>

        </div>
        <div class="row">
            <label for="entradas">Entradas</label>
            <asp:TextBox ID="IDEntrada" runat="server" TextMode="Number" placeholder="Ingresar la Entrada"></asp:TextBox>
            <label for="salidas">Salidas</label>
            <asp:TextBox ID="IDSalida" runat="server" TextMode="Number" placeholder="Ingresar la salida"></asp:TextBox>
        </div>
        <div class="row">
            <label for="observaciones">Observaciones</label>
            <asp:TextBox ID="IDObservacion" runat="server" placeholder="Ingresar la Observación"></asp:TextBox>
        </div>
        <div class="row">
                <asp:Button ID="Guardar" runat="server" Text="Guardar" OnClick="botonEnviar_Click" Visible="true" />
        </div>
    </div>
                <asp:TextBox ID="IDUsu" runat="server" Visible="false"></asp:TextBox>


    <script type="text/javascript">
        function autocompletarProductos() {
            var textoBusqueda = document.getElementById("<%= txtBusqueda.ClientID %>").value.toUpperCase();
            var dropdown = document.getElementById("<%= IDProducto.ClientID %>");
            var opciones = dropdown.getElementsByTagName("option");

            var coincidencias = false;

            // Iterar sobre las opciones y mostrar/ocultar según la coincidencia con el texto de búsqueda
            for (var i = 0; i < opciones.length; i++) {
                var textoOpcion = opciones[i].text.toUpperCase();
                if (textoOpcion.indexOf(textoBusqueda) > -1) {
                    opciones[i].style.display = "";
                    coincidencias = true;
                } else {
                    opciones[i].style.display = "none";
                }
            }

            // Mostrar u ocultar el DropDownList según si hay coincidencias o no
            dropdown.style.display = coincidencias ? "block" : "none";
        }
    </script>

</asp:Content>
