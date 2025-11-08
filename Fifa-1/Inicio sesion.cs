using System;
using System.Windows.Forms;
using Fifa.Dapper;

namespace Fifa_1
{
    public partial class Inicio_sesion : Form
    {
        public Inicio_sesion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Menu Menu = new Menu();
            Menu.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Registro registro = new Registro();
            registro.Show();
            this.Hide();
        }

        // Asume controles txtEmail y txtPassword y botón btnLogin (aquí button2)
        private void button2_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Ingrese email y contraseña.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var conexion = ConexionDB.CrearConexion();
                conexion.Open();
                var repo = new RepoUsuario(conexion);
                var usuario = repo.UsuarioPorEmailYPass(email, password);

                if (usuario is null)
                {
                    MessageBox.Show("Email o contraseña incorrectos.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Iniciar sesión: abrir menú principal o pantalla de usuario
                var menu = new Menu();
                menu.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al autenticar: {ex.Message}", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}