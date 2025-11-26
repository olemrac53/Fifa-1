using System;
using System.Windows.Forms;
using Fifa.Core;
using Fifa.Dapper; 
using Fifa.Core.Repos;
using Microsoft.VisualBasic; 

namespace Fifa_1
{
    public partial class Menu : Form
    {
        private readonly Usuario _usuarioLogueado;
        private readonly Administrador _adminLogueado;

        private Usuario _usuarioConPlantillas;

        public Menu(Usuario usuarioLogueado, Administrador adminLogueado)
        {
            InitializeComponent();
            _usuarioLogueado = usuarioLogueado;
            _adminLogueado = adminLogueado;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            if (_adminLogueado != null)
            {
                // Es Administrador
                lblBienvenida.Text = $"Hola, Admin: {_adminLogueado.Nombre}!";

                // Mostrar botones de Admin
                btnAdminJugadores.Visible = true;
                btnAdminPuntajes.Visible = true;

                // Ocultar controles de Usuario (Plantillas)
                cmbPlantillas.Visible = false;
                btnGestionarPlantilla.Visible = false;
                btnCrearPlantilla.Visible = false;
                btnEliminarPlantilla.Visible = false;
            }
            else if (_usuarioLogueado != null)
            {
                // Es Usuario
                lblBienvenida.Text = $"Hola, {_usuarioLogueado.Nombre}!";

                // Ocultar botones de Admin
                btnAdminJugadores.Visible = false;
                btnAdminPuntajes.Visible = false;

                // Mostrar controles de Usuario y cargar sus plantillas
                cmbPlantillas.Visible = true;
                btnGestionarPlantilla.Visible = true;
                btnCrearPlantilla.Visible = true;
                btnEliminarPlantilla.Visible = true;
                CargarPlantillas();
            }
            else
            {
                // Caso inesperado, volver al Login
                MessageBox.Show("Error de sesión. Intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnCerrarSesion_Click(sender, e);
            }
        }

        public void CargarPlantillas()
        {
            if (_usuarioLogueado == null)
            {
                return;
            }

            try
            {
                using var con = ConexionDB.CrearConexion();
                con.Open();
                var repoUsuario = new RepoUsuario(con);

                _usuarioConPlantillas = repoUsuario.GetUsuarioConPlantillas(_usuarioLogueado.IdUsuario);

                if (_usuarioConPlantillas?.Plantillas == null || _usuarioConPlantillas.Plantillas.Count == 0)
                {
                    cmbPlantillas.DataSource = null;
                    cmbPlantillas.Items.Clear();
                    cmbPlantillas.Items.Add("No tienes plantillas");
                    cmbPlantillas.SelectedIndex = 0;
                    btnGestionarPlantilla.Enabled = false;
                    btnEliminarPlantilla.Enabled = false;
                }
                else
                {
                    cmbPlantillas.DataSource = _usuarioConPlantillas.Plantillas;
                    cmbPlantillas.DisplayMember = "IdPlantilla";
                    cmbPlantillas.ValueMember = "IdPlantilla";
                    btnGestionarPlantilla.Enabled = true;
                    btnEliminarPlantilla.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar plantillas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGestionarPlantilla_Click(object sender, EventArgs e)
        {
            if (cmbPlantillas.SelectedItem == null || !(cmbPlantillas.SelectedItem is Fifa.Core.Plantilla))
            {
                MessageBox.Show("Seleccione una plantilla válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idPlantillaSeleccionada = (int)cmbPlantillas.SelectedValue;

            Plantilla formPlantilla = new Plantilla(idPlantillaSeleccionada);
            formPlantilla.Show();
            this.Hide();
        }

        private void btnCrearPlantilla_Click(object sender, EventArgs e)
        {
            string nombrePlantilla = Interaction.InputBox("Introduce un nombre para tu nueva plantilla:", "Crear Plantilla", "Mi Plantilla");

            if (string.IsNullOrWhiteSpace(nombrePlantilla))
            {
                MessageBox.Show("La creación fue cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var nuevaPlantilla = new Fifa.Core.Plantilla
                {
                    Usuario = _usuarioLogueado,

                    PresupuestoMax = 99999999.99m,
                    CantMaxFutbolistas = 20 // Valor por defecto
                };

                using (var con = ConexionDB.CrearConexion())
                {
                    con.Open();
                    IRepoPlantilla repo = new RepoPlantilla(con);
                    repo.InsertPlantilla(nuevaPlantilla);
                }

                MessageBox.Show($"¡Plantilla (ID: {nuevaPlantilla.IdPlantilla}) creada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarPlantillas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear la plantilla: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarPlantilla_Click(object sender, EventArgs e)
        {
            if (cmbPlantillas.SelectedItem == null || !(cmbPlantillas.SelectedItem is Fifa.Core.Plantilla plantillaSeleccionada))
            {
                MessageBox.Show("Seleccione una plantilla válida para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show(
                $"¿Estás seguro de que quieres eliminar la plantilla ID: {plantillaSeleccionada.IdPlantilla}?\nEsta acción no se puede deshacer.",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmacion == DialogResult.No)
                return;

            try
            {
                using (var con = ConexionDB.CrearConexion())
                {
                    con.Open();
                    IRepoPlantilla repo = new RepoPlantilla(con);
                    repo.DeletePlantilla(plantillaSeleccionada.IdPlantilla);
                }

                MessageBox.Show("Plantilla eliminada correctamente.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarPlantillas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la plantilla: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Inicio_sesion login = new Inicio_sesion();
            login.Show();
            this.Hide();
        }

        private void btnAdminJugadores_Click(object sender, EventArgs e)
        {
            Jugador formJugador = new Jugador();
            formJugador.Show();
        }

        private void btnAdminPuntajes_Click(object sender, EventArgs e)
        {
            Puntuaciones formPuntajes = new Puntuaciones();
            formPuntajes.Show();
        }
    }
}