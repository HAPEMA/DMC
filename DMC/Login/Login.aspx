<%@ Page Title="" Language="C#" MasterPageFile="~/AntesLogin.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DMC.DMC.Login.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <style>
      body {
          font-family: Arial, sans-serif;
          background-color: #f2f2f2;
          margin: 0;
          padding: 0;
      }

      img {
          width: 100px;
          height: auto;
          margin-right: 10px;
      }

      #formContainer {
          text-align: center;
          margin: 50px auto;
          padding: 20px;
          background-color: white;
          border-radius: 10px;
          box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
          max-width: 400px;
      }

      label {
          display: block;
          margin: 10px 0 5px;
          color: #663399;
      }

      input {
          max-width: 500px;
          padding: 8px;
          margin-bottom: 10px;
          box-sizing: border-box;
      }

      button {
          background-color: #663399;
          color: white;
          padding: 10px;
          border: none;
          border-radius: 5px;
          cursor: pointer;
      }

      .titulo {
          display: flex;
          justify-content: center;
          align-content: center;
          color: #8E44AD
      }

  </style>
  <body>


      <div class="titulo">

          <h1>Inicio de sesión:</h1>
      </div>

      <div id="formContainer">
          <div id="form1" runat="server">

              <label for="txtusuario">Usuario:</label>
              <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control"></asp:TextBox>

              <label for="txtpassword">Contraseña:</label>
              <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>

                <asp:Button ID="BtnLogin" runat="server" Text="Iniciar Sesión" style="background-color: #6a1b9a; color: #fff; width: 100%; padding: 10px; border: none; border-radius: 5px; cursor: pointer;" OnClick="BtnLogin_Click" OnClientClick="return validateFields();" />

              <asp:Label ID="Mensaje" runat="server"></asp:Label>
              <asp:Label ID="lblMensaje" runat="server" Text="" ForeColor="Red"></asp:Label>

              <script type="text/javascript">
                  function validateFields() {
                      var usuario = document.getElementById('<%=txtUsuario.ClientID%>').value.trim();
                      var contrasena = document.getElementById('<%=txtContrasena.ClientID%>').value.trim();

                      if (usuario === '' || contrasena === '') {
                          alert('Por favor, complete ambos campos antes de continuar.');
                          return false; // Detiene la acción del botón si los campos no están llenos
                      }

                      return true; // Permite la acción del botón si los campos están llenos
                  }
              </script>

              <script>
                  // Función para detectar si el usuario está en un dispositivo móvil
                  function isMobileDevice() {
                      return (typeof window.orientation !== "undefined") || (navigator.userAgent.indexOf('IEMobile') !== -1);
                  }

                  // Función para activar automáticamente el "Switch to Desktop"
                  function activateSwitchToDesktop() {
                      if (isMobileDevice()) {
                          // Simular clic en el botón de "Switch to Desktop"
                          var switchToDesktopButton = document.getElementById('switchToDesktopButton');
                          if (switchToDesktopButton) {
                              switchToDesktopButton.click();
                          }
                      }
                  }

                  // Llamar a la función al cargar la página
                  window.onload = function () {
                      activateSwitchToDesktop();
                  };
              </script>
              <asp:Button ID="switchToDesktopButton" runat="server" Text="Switch to Desktop" Style="display: none;" />

          </div>
      </div>
  </body>


</asp:Content>
