<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MovimientoPorProducto.aspx.cs" Inherits="DMC.DMC.Productos.WebForm3" %>
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

    input {
        background-color: #6a1b9a;
        color: #fff;
        width: 100%; 
        padding: 10px;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        margin-top: 10px;
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

    footer {
        background-color: #663399;
        color: white;
        text-align: left;
        padding: 10px;
        position: fixed;
        bottom: 0;
        width: 100%;
    }


    @media screen and (max-width: 600px) {
        #GridViewServicios th, #GridViewServicios td {
            max-width: 200px;
            white-space: nowrap;
        }
    }
</style>   
    <%--  Estilo Tecnico--%>

    <div>

        <div>
            <asp:TextBox ID="txtBusquedaProdu" style="background-color:#E8F0FE; color:black;" runat="server" placeholder="Buscar Producto" onkeyup="autocompletarProductos()"></asp:TextBox>
<%--            <asp:Label ID="Txtproducto" runat="server" Text="Nombre Producto:" CssClass="form-label"></asp:Label>--%>
            <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-control" AutoPostBack="false">
            </asp:DropDownList>
        </div>

        <div>
            <%--            <asp:TextBox ID="txtBusqueda" runat="server" placeholder="Buscar Bodega..." onkeyup="autocompletarBodega()"></asp:TextBox>
            <asp:Label ID="TxtBodega" runat="server" Text="Nombre Bodega:" CssClass="form-label"></asp:Label>--%>
            <asp:DropDownList ID="ddlBodega" runat="server" CssClass="form-control" AutoPostBack="false">
            </asp:DropDownList>
        </div>

        <div>
            <asp:Button ID="ButtonMostrar" runat="server" Text="Mostrar" OnClick="ButtonMostrar_Click" />
        </div>

    </div>
    <script>
<%--        function autocompletarBodega() {
            let textoBusqueda = document.getElementById("<%= txtBusqueda.ClientID %>").value.toUpperCase();

              let dropdown = document.getElementById("<%= ddlBodega.ClientID %>");
              let opciones = dropdown.getElementsByTagName("option");


              var coincidencias                             // Iterar sobre las opciones y mostrar/ocultar según la coincidencia con el texto de búsqueda
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
        }--%>

        function autocompletarProductos() {
            let textoBusqueda = document.getElementById("<%= txtBusquedaProdu.ClientID %>").value.toUpperCase();

              let dropdown = document.getElementById("<%= ddlProducto.ClientID %>");
              let opciones = dropdown.getElementsByTagName("option");


              var coincidencias                             // Iterar sobre las opciones y mostrar/ocultar según la coincidencia con el texto de búsqueda
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

    <asp:GridView ID="GridViewMovimiento" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="codigo_maestro" HeaderText="Codigo" />
            <asp:BoundField DataField="Nombre_Bodega" HeaderText="Bodega" />
            <asp:BoundField DataField="numero_documento" HeaderText="Documento" />
            <asp:BoundField DataField="fecha_documento" HeaderText="Fecha" />
            <asp:BoundField DataField="entradas" HeaderText="Entrada" />
            <asp:BoundField DataField="salidas" HeaderText="Salida" />
            <asp:BoundField DataField="observacion" HeaderText="Observaciones" />
            <asp:BoundField DataField="nombre_usuario" HeaderText="Usuario" />
        </Columns>
    </asp:GridView>
</asp:Content>