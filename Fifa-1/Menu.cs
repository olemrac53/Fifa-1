using System;
using System.Windows.Forms;
using Fifa.Core;
using Fifa.Dapper;
using Fifa.Core.Repos; // Necesario para IRepoPlantilla
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
            // (Opcional) Ocultar botón de admin si el usuario no es admin
            // btnAdminJugadores.Visible = (usuarioLogueado.Rol == "admin");
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
                    cmbPlantillas.DisplayMember = "IdPlantilla"; // Puedes cambiar esto si agregas un 'Nombre' a la Plantilla
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

        // --- LÓGICA "CREATE" AÑADIDA ---
        private void btnCrearPlantilla_Click(object sender, EventArgs e)
        {
            // Pedimos un nombre para la nueva plantilla
            string nombrePlantilla = Interaction.InputBox("Introduce un nombre para tu nueva plantilla:", "Crear Plantilla", "Mi Plantilla");

            if (string.IsNullOrWhiteSpace(nombrePlantilla))
            {
                MessageBox.Show("La creación fue cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Creamos el objeto plantilla
                var nuevaPlantilla = new Plantilla
                {
                    Usuario = _usuarioLogueado,
                    PresupuestoMax = 1000000, // Valor por defecto, se puede cambiar en la otra pantalla
                    CantMaxFutbolistas = 20 // Valor por defecto
                };

                using (var con = ConexionDB.CrearConexion())
                {
                    con.Open();
                    IRepoPlantilla repo = new RepoPlantilla(con);
                    repo.InsertPlantilla(nuevaPlantilla); // El repo actualiza el ID en el objeto
                }

                MessageBox.Show($"¡Plantilla '{nombrePlantilla}' (ID: {nuevaPlantilla.IdPlantilla}) creada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recargamos el ComboBox para que aparezca la nueva plantilla
                CargarPlantillas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear la plantilla: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- LÓGICA "DELETE" AÑADIDA ---
        private void btnEliminarPlantilla_Click(object sender, EventArgs e)
        {
            if (cmbPlantillas.SelectedItem == null || !(cmbPlantillas.SelectedItem is Plantilla plantillaSeleccionada))
            {
                MessageBox.Show("Seleccione una plantilla válida para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pedimos confirmación
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

                // Recargamos el ComboBox
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

        // Botón para abrir el CRUD de Jugadores (Admin)
        private void btnAdminJugadores_Click(object sender, EventArgs e)
        {
            Jugador formJugador = new Jugador();
            formJugador.Show();
            // (Opcional) this.Hide(); si quieres ocultar el menú
        }
    }
}