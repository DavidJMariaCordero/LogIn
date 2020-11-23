using LogIn.BLL;
using LogIn.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LogIn.UI.Registros
{
    /// <summary>
    /// Interaction logic for rUsuario.xaml
    /// </summary>
    public partial class rUsuario : Window
    {
        private Usuario usuario;
        public rUsuario()
        {
            InitializeComponent();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;
            usuario = new Usuario();
            usuario.Nombre = UsuarioTextBox.Text.Trim();
            usuario.Clave = UsuarioBLL.getHashSha256(ContraseñaTextBox.Password.Trim());
            var paso = UsuarioBLL.Guardar(usuario);
            if (paso)
            {
                MessageBox.Show("Guardado con Exito", "Exito!!", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Error al guardar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private bool Validar()
        {
            bool esValido = true;
            if (String.IsNullOrWhiteSpace(UsuarioTextBox.Text))
            {
                esValido = false;
                MessageBox.Show("Le falta incluir el nombre de usuario", "Error", MessageBoxButton.OKCancel);
            }
            if (String.IsNullOrWhiteSpace(ContraseñaTextBox.Password))
            {
                esValido = false;
                MessageBox.Show("Le falta incluir la contraseña", "Error", MessageBoxButton.OKCancel);
            }
            return esValido;
        }
    }
}
