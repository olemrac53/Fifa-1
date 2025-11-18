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

        // Tu lógica de registro (¡está perfecta!)
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

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

                // Después de registrar, volver al login
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

        // *** AÑADIR ESTE MÉTODO ***
        // Para que funcione el link "Ya tienes cuenta? Inicia Sesión"
        private void llblVolverLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var login = new Inicio_sesion();
            login.Show();
            this.Hide();
        }
    }
}