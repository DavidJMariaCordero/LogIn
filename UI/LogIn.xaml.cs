using LogIn.BLL;
using LogIn.UI.Registros;
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

namespace LogIn.UI
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void IniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            var paso = UsuarioBLL.Validar(UsuarioTextBox.Text.Trim(), UsuarioBLL.getHashSha256(ClaveTextBox.Password));
            if (paso)
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("El usuario o la cotraseña son incorrectos");
            }
        }

        private void Registrarse_Click(object sender, RoutedEventArgs e)
        {
            rUsuario usuario = new rUsuario();
            usuario.Show();
        }
    }
}
