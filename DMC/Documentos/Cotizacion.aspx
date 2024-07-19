<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cotizacion.aspx.cs" Inherits="DMC.DMC.Documentos.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
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
            height: 40px;
            width: 100%; /* Ancho del 100% para dispositivos más pequeños */
            max-width: 280px; /* Máximo ancho para evitar que ocupe toda la pantalla en dispositivos grandes */
            color: black;
            padding: 5px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            border: 1px solid gainsboro;
            background-color: gainsboro;
            margin-top: 5px; /* Añadido espacio entre el input y la tabla en dispositivos pequeños */
        }
            
        input {
            color: black;
            width: 100%; /* Ancho del 100% para dispositivos más pequeños */
            padding: 5px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            border: 1px solid gainsboro;
            background-color: gainsboro;
            margin-top: 5px; /* Añadido espacio entre el input y la tabla en dispositivos pequeños */
        }
            
        #filtroTodo {
            display: flex;
            justify-content: space-between; /* Para distribuir los divs a los lados */
            max-width: 1200px; /* Ancho máximo para contener los divs */
            margin: 0 auto; /* Centrar los divs */
        }
            
        #filtrotienda {
            width: 45%; /* Ancho igual para ambos divs */
            padding: 20px;
            box-sizing: border-box; /* Incluir padding y borde en el ancho */
        }
                
        #filtrocliente {
            width: 45%; /* Ancho igual para ambos divs */
            padding: 10px;
            box-sizing: border-box; /* Incluir padding y borde en el ancho */
        }
            
        #DivCliente, #DivTienda {
            margin-bottom: 20px; /* Espacio entre los elementos dentro de los divs */
        }
            
        .Propiedad label {
            display: inline-block;
            width: 70px;
        }
            
        .Propiedad input[type="text"] {
            display: inline-block;
            width: calc(100% - 70px); /* El ancho del textbox toma el resto del espacio disponible */
        }
            
        .Propiedadtienda label {
            display: inline-block;
            width: 80px;
        }
            
        .Propiedadtienda input[type="text"] {
            display: inline-block;
            width: calc(100% - 80px); /* El ancho del textbox toma el resto del espacio disponible */
        }
        
        .Cotizacion {
            display: flex;
            flex-direction: row;
            justify-content: space-between; /* Alinea los elementos al espacio entre ellos */
            border: 1px solid black; /* Agrega un borde alrededor del contenedor */
            padding: 10px; /* Agrega un espacio interno al contenedor */
        }

        .Cotizacion > div {
           margin-right: 10px; /* Espacio entre los elementos dentro del contenedor */
        }

        .Cotizacion > div:last-child {
               margin-right: 0; /* Elimina el margen derecho del último elemento */
        }

        .Cotizacion label {
           display: block; /* Hace que los labels ocupen una línea completa */
           margin-bottom: 2px; /* Espacio entre los labels y los controles */
        }

        .Cotizacion input[type="text"],
        .Cotizacion select {
           width: 275px; /* Ancho de los campos de texto y select */
           border: 1px solid #6a1b9a; /* Estilo de borde */
           color: #6a1b9a; /* Color de texto */
           background-color: #fff; /* Color de fondo */
        }

        .Cotizacion hr {
                margin: 5px 0; /* Espacio vertical entre las líneas */
        }

        .Cotizacion button {
                margin-right: 5px; /* Espacio entre los botones */
        }

        table {
            width: 100%;
            margin-top: 5px;
            border-collapse: collapse;
            margin-bottom: 10px;
        }

        table .Clientetr,
        table .Clientetd {
            border: 0px;
            text-align: left;
            padding: 0px;
        }

        table .Clientetr {
            color: black;
        }

        .tablaProductos {
            width: 100%;
            margin: auto;
            border-collapse: collapse;
        }
        .tablaProductos th, .tablaProductos td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: center;
        }
        .tablaProductos th {
            background-color: #f2f2f2;
        }
        .tablaProductos input[type="text"], .tablaProductos input[type="number"], .tablaProductos select {
            width: 100%;
            box-sizing: border-box;
        }
        .table-container {
            display: flex;
            justify-content: center;
            align-items: center;
            padding: 20px;
        }

        textarea {
            margin-top: 10px;
            margin-left: 50px;
            width: 700px;
            height: 200px;
            background: none repeat scroll 0 0 rgba(0, 0, 0, 0.07);
            border-radius: 6px 6px 6px 6px;
            border-style: none solid solid none;
            border-width: medium 1px 1px medium;
            box-shadow: 0 1px 2px rgba(0, 0, 0, 0.12) inset;
            color: #555555;
            font-family: "Helvetica Neue",Helvetica,Arial,sans-serif;
            font-size: 1em;
            line-height: 1.4em;
            padding: 5px 8px;
        }

        textarea:focus {
            background: none repeat scroll 0 0 #FFFFFF;
            outline-width: 0;
        }

    </style>

    <asp:Panel ID="InformacionCliente" runat="server" Visible="true">

        <h1>Información del cliente</h1>

        <div id="filtroTodo">
            <div id="filtrocliente">
                <div id="DivCliente">

                    <table>
                        <tr class="Clientetr">
                            <td class="Clientetd">
                                <label for="txtTut" style="display: inline-block">Filtro:</label>
                            </td>
                            <td class="Clientetd">
                                <asp:TextBox ID="txtBusqueda" runat="server" placeholder="Buscar Cliente..." onkeyup="autocompletarProductoscliente()"></asp:TextBox>
                            </td>
                        </tr>

                        <tr class="Clientetr">
                            <td class="Clientetd">
                                <label for="txtTut" style="display: inline-block">Cliente:</label>
                            </td>
                            <td class="Clientetd">
                                <asp:DropDownList ID="ddlCliente" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlCliente_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr class="Clientetr">
                            <td class="Clientetd">
                                <label for="txtTut">RUT:</label>
                            </td>
                            <td class="Clientetd">
                                <asp:TextBox ID="IdRut" runat="server" ReadOnly="TRUE"></asp:TextBox>
                            </td>
                        </tr>

                        <tr class="Clientetr">
                            <td class="Clientetd">
                                <label for="txtTut">Razón Social:</label>
                            </td>
                            <td class="Clientetd">
                                <asp:TextBox ID="idRazonSocial" runat="server" ReadOnly="TRUE"></asp:TextBox>
                            </td>
                        </tr>

                        <tr class="Clientetr">
                            <td class="Clientetd">
                                <label for="txtTut">Dirección:</label>
                            </td>
                            <td class="Clientetd">
                                <asp:TextBox ID="idDireccion" runat="server" ReadOnly="TRUE"></asp:TextBox>
                            </td>
                        </tr>

                        <tr class="Clientetr">
                            <td class="Clientetd">
                                <label for="txtTut">Email:</label>
                            </td>
                            <td class="Clientetd">
                                <asp:TextBox ID="idImail" runat="server" ReadOnly="TRUE"></asp:TextBox>
                            </td>
                        </tr>

                        <tr class="Clientetr">
                            <td class="Clientetd">
                                <label for="txtTut">Región:</label>
                            </td>
                            <td class="Clientetd">
                                <asp:TextBox ID="idRegion" runat="server" ReadOnly="TRUE"></asp:TextBox>
                            </td>
                        </tr>

                        <tr class="Clientetr">
                            <td class="Clientetd">
                                <label for="txtTut">Teléfono Comercial:</label>
                            </td>
                            <td class="Clientetd">
                                <asp:TextBox ID="idTelefonoComercial" runat="server" ReadOnly="TRUE"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="Clientetr">
                            <td class="Clientetd">
                                <label for="txtTut">Teléfono Social:</label>
                            </td>
                            <td class="Clientetd">
                                <asp:TextBox ID="idTelefonoSocial" runat="server" ReadOnly="TRUE"></asp:TextBox>
                            </td>
                        </tr>

                    </table>
                </div>
            </div>



            <%-- --------------------------------------------   Sucursal   ----------------------------------------------------------------- --%>
            <div id="filtrotienda">
                <div id="DivTienda">
                    <table>
                        <tr class="Clientetr">
                            <td class="Clientetd">
                                <label for="txtTut">Sucursal:</label>
                            </td>
                            <td class="Clientetd">
                                <asp:DropDownList ID="ddlTienda" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr class="Clientetr">
                            <td class="Clientetd">
                                <label for="txtTut">Dirección:</label>
                            </td>
                            <td class="Clientetd">
                                <asp:TextBox ID="idDirecciontienda" runat="server" ReadOnly="TRUE"></asp:TextBox>
                            </td>
                        </tr>

                        <tr class="Clientetr">
                            <td class="Clientetd">
                                <label for="txtTut">Región:</label>
                            </td>
                            <td class="Clientetd">
                                <asp:TextBox ID="idRegiontienda" runat="server" ReadOnly="TRUE"></asp:TextBox>
                            </td>
                        </tr>

                        <tr class="Clientetr">
                            <td class="Clientetd">
                                <label for="txtTut">Comuna:</label>
                            </td>
                            <td class="Clientetd">
                                <asp:TextBox ID="idComunatienda" runat="server" ReadOnly="TRUE"></asp:TextBox>
                            </td>
                        </tr>

                        <tr class="Clientetr">
                            <td class="Clientetd">
                                <label for="txtTut">Encargado Local:</label>
                            </td>
                            <td class="Clientetd">
                                <asp:TextBox ID="idEncargadoienda" runat="server" ReadOnly="TRUE"></asp:TextBox>
                            </td>
                        </tr>

                        <tr class="Clientetr">
                            <td class="Clientetd">
                                <label for="txtTut">Teléfono Encargado:</label>
                            </td>
                            <td class="Clientetd">
                                <asp:TextBox ID="idTelefonotienda" runat="server" ReadOnly="TRUE"> </asp:TextBox>
                            </td>
                        </tr>

                        <tr class="Clientetr">
                            <td class="Clientetd">
                                <label for="txtTut">Email Encargado:</label>
                            </td>
                            <td class="Clientetd">
                                <asp:TextBox ID="idEmailtienda" runat="server" ReadOnly="TRUE"></asp:TextBox>
                            </td>
                        </tr>

                        <tr class="Clientetr">
                            <td class="Clientetd">
                                <label for="txtTut">Casa Matriz:</label>
                            </td>
                            <td class="Clientetd">
                                <asp:TextBox ID="idCasaMatriztienda" runat="server" ReadOnly="TRUE"></asp:TextBox>
                            </td>
                    </table>
                </div>
            </div>
        </div>

        <div>
            <asp:Button ID="MostrarProducto" runat="server" Text="Detalle Documento" CssClass="btn btn-primary" OnClick="PanelOcualtaMostrar" Style="background-color: #17A2B8; width: 300px; margin-right: 500px;" />
            <asp:TextBox ID="NombreProyecto" runat="server" placeholder="Ingrese Proyecto"></asp:TextBox>
        </div>

        </asp:Panel>
<asp:Panel ID="Producto" runat="server" Visible="true">
    <div class="table-container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <table id="tablaProductos" runat="server" class="tablaProductos">
                    <tr>
                        <th>(+/-)
                            <asp:Button ID="Agregar" runat="server" Text="+" OnClick="ButtonAgregar_Click" />
                        </th>
                        <th>N°</th>
                        <th>COD.ARTICULO</th>
                        <th>DESCRIPCIÓN</th>
                        <th>CANTIDAD</th>
                        <th>PRECIO BASE</th>
                        <th>DESCUENTOS (%)</th>
                        <th>PRECIO UNITARIO</th>
                        <th>TOTAL</th>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Agregar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Panel>

<asp:Panel ID="Fecha" runat="server" Visible="false">
    <asp:Label ID="fechaa" runat="server" Text="Fecha:" CssClass="form-label"></asp:Label>
    <asp:TextBox ID="txtfecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
</asp:Panel>

<div style="display: flex; justify-content: center; align-content: center;">
    <div style="display: inline-block; padding-right: 350px;">
        <asp:TextBox ID="txtdescripcion" runat="server" placeholder="Ingresar la Observación" TextMode="MultiLine" Rows="10" Style="width: 1000px;"></asp:TextBox>
    </div>
    <table style="width: 50%; height: 25%;">
        <tr>
            <td style="font-size: 20px; font-family: Platypi, serif; font-weight: bold;">Total Neto:</td>
            <td style="font-size: 15px; font-family: Platypi, serif; font-weight: bold;">
                <asp:TextBox ID="PrecioNeto2" runat="server" TextMode="Number" placeholder="Cantidad" ReadOnly="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-size: 20px; font-family: Platypi, serif; font-weight: bold;">I.V.A (19%):</td>
            <td style="font-size: 15px; font-family: Platypi, serif; font-weight: bold;">
                <asp:TextBox ID="Iva" runat="server" TextMode="Number" placeholder="Cantidad" ReadOnly="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-size: 20px; font-family: Platypi, serif; font-weight: bold;">Total:</td>
            <td style="font-size: 15px; font-family: Platypi, serif; font-weight: bold;">
                <asp:TextBox ID="PrecioTotalConIva2" runat="server" TextMode="Number" placeholder="Cantidad" ReadOnly="false"></asp:TextBox>
            </td>
        </tr>
    </table>
</div>

<div style="display: flex; justify-content: center; align-content: center;">
    <asp:Button ID="botonEnviar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="BotonEnviar_Click" Style="background-color: #17A2B8; width: 300px;" />
</div>

<script type="text/javascript">

    function autocompletarProductoscliente() {
        let textoBusqueda = document.getElementById("<%= txtBusqueda.ClientID %>").value.toUpperCase();

          let dropdown = document.getElementById("<%= ddlCliente.ClientID %>");
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



    function attachCalculateTotalHandlers() {
        var rows = document.querySelectorAll('.tablaProductos tr');
        rows.forEach((row, index) => {
            if (index === 0) return; // Saltar la fila de encabezado

            let cantidadField = row.querySelector('[id*="IdCantidad"]');
            let precioBaseField = row.querySelector('[id*="IDPrecioBase"]');
            let descuentoField = row.querySelector('[id*="IdDescuentoProducto"]');

            if (cantidadField) {
                cantidadField.addEventListener('keyup', function () { calculateTotal(row); });
                cantidadField.addEventListener('change', function () { calculateTotal(row); });
            }

            if (precioBaseField) {
                precioBaseField.addEventListener('keyup', function () { calculateTotal(row); });
                precioBaseField.addEventListener('change', function () { calculateTotal(row); });
            }

            if (descuentoField) {
                descuentoField.addEventListener('keyup', function () { calculateTotal(row); });
                descuentoField.addEventListener('change', function () { calculateTotal(row); });
            }
        });
    }

    function calculateTotal(row) {
        let cantidadField = document.getElementById(row.querySelector('[id*="IdCantidad"]').id);
        let precioBaseField = document.getElementById(row.querySelector('[id*="IDPrecioBase"]').id);
        let descuentoField = document.getElementById(row.querySelector('[id*="IdDescuentoProducto"]').id);
        let totalField = document.getElementById(row.querySelector('[id*="IdPrecioTotal"]').id);
        let precioUnitarioField = document.getElementById(row.querySelector('[id*="IdPrecioUnitario"]').id);

        if (cantidadField && precioBaseField && descuentoField && totalField && precioUnitarioField) {
            let cantidad = parseFloat(cantidadField.value) || 0;
            let precioBase = parseFloat(precioBaseField.value) || 0;
            let descuento = parseFloat(descuentoField.value) || 0;

            let total = cantidad * precioBase;
            let descuentoFinal = total * (descuento / 100);
            let totalFinal = Math.round(total - descuentoFinal); // Aproximar total a entero
            let precioUnitario = Math.round(totalFinal / cantidad); // Aproximar precio unitario a entero

            totalField.value = totalFinal;
            precioUnitarioField.value = precioUnitario;

            updateTotals(); // Llamar a la función para actualizar el total neto, IVA y total con IVA
        }
    }

    function updateTotals() {
        let rows = document.querySelectorAll('.tablaProductos tr');
        let totalNeto = 0;

        rows.forEach((row, index) => {
            if (index === 0) return; // Saltar la fila de encabezado
            let totalField = row.querySelector('[id*="IdPrecioTotal"]');
            if (totalField) {
                totalNeto += parseFloat(totalField.value) || 0;
            }
        });

        let iva = Math.round(totalNeto * 0.19); // Calcular el IVA (19%) y aproximar a entero
        let totalConIva = Math.round(totalNeto + iva); // Calcular el total con IVA y aproximar a entero

        document.getElementById('<%= PrecioNeto2.ClientID %>').value = Math.round(totalNeto); // Aproximar total neto a entero
        document.getElementById('<%= Iva.ClientID %>').value = iva;
        document.getElementById('<%= PrecioTotalConIva2.ClientID %>').value = totalConIva;
    }

    document.addEventListener('DOMContentLoaded', attachCalculateTotalHandlers);
</script>

    

</asp:Content>