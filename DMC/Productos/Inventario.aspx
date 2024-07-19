<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inventario.aspx.cs" EnableEventValidation="false" Inherits="DMC.DMC.Productos.WebForm1" %>
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
    #GridViewInventario {
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
    #GriviewInventarioCss{
        display:flex;
        justify-content:center;
        align-content:center;
    }
</style>




<div id="filters">
        <asp:TextBox ID="txtTextoBusqueda" runat="server" placeholder="Filtrar"></asp:TextBox>
        <asp:Button ID="BtnLogin" runat="server" Text="Buscar" OnClick="ButtonMostrar_Click" />
        <asp:Button ID="DescargarServiciosExcel" runat="server" Text="Excel" onclick="BtnDescargar_Click_Excel"/>
</div>
    <div id="GriviewInventarioCss">
<asp:GridView ID="GridViewInventario" runat="server" AutoGenerateColumns="false" AllowPaging="True" PageSize="20" OnPageIndexChanging="GridViewServicios_PageIndexChanging">
    <Columns>
        <asp:BoundField DataField="codigo_familia" HeaderText="C.Familia" HeaderStyle-Width="40px"/>
        <asp:BoundField DataField="codigo_maestro" HeaderText="C.Maestro" HeaderStyle-Width="40px"/>
        <asp:BoundField DataField="descripcion" HeaderText="Descripción" HeaderStyle-Width="100px"/>
        <asp:BoundField DataField="Saldo" HeaderText="Saldo" HeaderStyle-Width="30px"/>
        <asp:BoundField DataField="Estado_Item" HeaderText="Estado" HeaderStyle-Width="20px" />
    </Columns>
</asp:GridView>
</div>


    <%--         <asp:TemplateField HeaderText="Imagen">
             <ItemTemplate>
                 <asp:Image ID="ImageServicio" runat="server" Width="100" Height="100" onclick="ampliarImagen(this);" />
             </ItemTemplate>
         </asp:TemplateField>--%>

    <%--  <asp:TemplateField HeaderText="Firma Tienda">
             <ItemTemplate>
                 <asp:Button ID="CopyUrlButton" runat="server" Text="Copiar URL" OnClientClick='<%# "copyUrl(\"" + Eval("FirmaTienda") + "\")" %>' />
             </ItemTemplate>
         </asp:TemplateField>

         <asp:TemplateField HeaderText="Firma DMC">
             <ItemTemplate>
                 <asp:Button ID="CopyUrlButton2" runat="server" Text="Copiar URL" OnClientClick='<%# "copyUrl(\"" + Eval("FirmaDMC") + "\")" %>' />
             </ItemTemplate>
         </asp:TemplateField>--%>
</asp:Content>
