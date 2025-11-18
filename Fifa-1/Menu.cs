using System;
using System.Windows.Forms;
using Fifa.Core;
using Fifa.Dapper;
using Fifa.Core.Repos;
using Microsoft.VisualBasic; // Necesario para el InputBox

namespace Fifa_1
{
    public partial class Menu : Form
    {
        private readonly Usuario _usuarioLogueado;
        private Usuario _usuarioConPlantillas;

        public Menu(Usuario usuarioLogueado)
        {
            InitializeComponent();
            _usuarioLogueado = usuarioLogueado;

            // (Opcional) Ocultar botones de admin si el usuario no es admin
            // (Asumimos que un usuario normal no debería ver estos botones)
            // bool esAdmin = ... (necesitarías un login de admin);
            // btnAdminJugadores.Visible = esAdmin;
            // btnAdminPuntajes.Visible = esAdmin;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            lblBienvenida.Text = $"Hola, {_usuarioLogueado.Nombre}!";
            CargarPlantillas();
        }

        public void CargarPlantillas()
        {
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
            if (cmbPlantillas.SelectedItem == null || !(cmbPlantillas.SelectedItem is Plantilla))
            {
                MessageBox.Show("Seleccione una plantilla válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idPlantillaSeleccionada = (int)cmbPlantillas.SelectedValue;

            plantilla formPlantilla = new plantilla(idPlantillaSeleccionada);
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
                var nuevaPlantilla = new Plantilla
                {
                    Usuario = _usuarioLogueado,

                    // --- INICIO DE LA CORRECCIÓN ---
                    // El valor debe ser '99999999.99m' (con 'm' de decimal)
                    // para que entre en la columna DECIMAL(10, 2).
                    PresupuestoMax = 99999999.99m,
                    // --- FIN DE LA CORRECCIÓN ---

                    CantMaxFutbolistas = 20 // Valor por defecto
                };

                using (var con = ConexionDB.CrearConexion())
                {
                    con.Open();
                    IRepoPlantilla repo = new RepoPlantilla(con);
                    // Usamos el SP 'CrearPlantilla'
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
            if (cmbPlantillas.SelectedItem == null || !(cmbPlantillas.SelectedItem is Plantilla plantillaSeleccionada))
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