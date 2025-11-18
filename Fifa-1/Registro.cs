using System;
using System.Windows.Forms;
using Fifa.Core;
using Fifa.Dapper;
using System.Data;

namespace Fifa_1
{
    public partial class Registro : Form
    {
        public Registro()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            // --- INICIO DE LA CORRECCIÓN ---
            string confirmPassword = txtConfirmPassword.Text;

            if (password != confirmPassword)
            {
                MessageBox.Show("Las contraseñas no coinciden. Por favor, inténtelo de nuevo.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Detiene la ejecución
            }
            // --- FIN DE LA CORRECCIÓN ---

            if (string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(apellido) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Complete todos los campos.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var usuario = new Usuario()
            {
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                FechaNacimiento = dtpFechaNacimiento.Value.Date
            };

            try
            {
                using var conexion = Fifa.Dapper.ConexionDB.CrearConexion();
                conexion.Open();
                var repo = new RepoUsuario(conexion);
                repo.InsertUsuario(usuario, password);

                MessageBox.Show("Registro exitoso.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var login = new Inicio_sesion();
                login.Show();
                this.Hide();
            }
            catch (ConstraintException ce)
            {
                MessageBox.Show(ce.Message, "Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar: {ex.Message}", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llblVolverLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var login = new Inicio_sesion();
            login.Show();
            this.Hide();
        }
    }
}