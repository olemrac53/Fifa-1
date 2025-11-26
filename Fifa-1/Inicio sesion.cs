using System;
using System.Windows.Forms;
using Fifa.Dapper;
using Fifa.Core;
using animacion_fifa; // Necesario para acceder a Program

namespace Fifa_1
{
    public partial class Inicio_sesion : Form
    {
        public Inicio_sesion()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Registro registro = new Registro();
            registro.Show();
            this.Hide();
        }

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

                // 1. Intentar como Usuario
                var repoUsuario = new RepoUsuario(conexion);
                var usuario = repoUsuario.UsuarioPorEmailYPass(email, password);

                if (usuario != null)
                {
                    // CAMBIO 1: Guardamos el usuario en la variable global
                    Program.UsuarioActual = usuario;

                    // Éxito como Usuario
                    var menu = new Menu(usuario, adminLogueado: null);
                    menu.Show();
                    this.Hide();
                    return;
                }

                // 2. Si falló, intentar como Administrador
                var repoAdmin = new RepoAdministrador(conexion);
                var admin = repoAdmin.AdministradorPorEmailYPass(email, password);

                if (admin != null)
                {
                    // Éxito como Administrador
                    var menu = new Menu(null, admin);
                    menu.Show();
                    this.Hide();
                    return;
                }

                // 3. Si ambos fallan
                MessageBox.Show("Email o contraseña incorrectos.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al autenticar: {ex.Message}", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}