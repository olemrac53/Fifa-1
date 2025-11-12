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
                var repo = new RepoUsuario(conexion);
                var usuario = repo.UsuarioPorEmailYPass(email, password);

                if (usuario is null)
                {
                    MessageBox.Show("Email o contraseña incorrectos.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // *** ¡CAMBIO IMPORTANTE! ***
                // Pasamos el objeto 'usuario' al Menú
                var menu = new Menu(usuario); // Asumimos que Menu.cs tiene un constructor que acepta un Usuario
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