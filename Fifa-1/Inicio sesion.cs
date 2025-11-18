using System;
using System.Windows.Forms;
using Fifa.Dapper;
using Fifa.Core; // Necesario para la clase Usuario

namespace Fifa_1
{
    public partial class Inicio_sesion : Form
    {
        public Inicio_sesion()
        {
            InitializeComponent();
        }

        // Tu button1_Click era para saltar el login, lo quitamos para la lógica real.

        // Esto es lo que se ejecuta al hacer clic en "¡Regístrate!"
        private void label4_Click(object sender, EventArgs e)
        {
            Registro registro = new Registro();
            registro.Show();
            this.Hide();
        }

        // Esto es lo que se ejecuta al hacer clic en "Ingresar"
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
                    // Éxito como Usuario
                    // Pasamos el usuario y 'null' como admin
                    var menu = new Menu(usuario, adminLogueado: null);
                    menu.Show();
                    this.Hide();
                    return; // Importante salir del método
                }

                // 2. Si falló, intentar como Administrador
                var repoAdmin = new RepoAdministrador(conexion);
                var admin = repoAdmin.AdministradorPorEmailYPass(email, password);

                if (admin != null)
                {
                    // Éxito como Administrador
                    // Pasamos 'null' como usuario y el admin
                    var menu = new Menu(null, admin);
                    menu.Show();
                    this.Hide();
                    return; // Importante salir del método
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