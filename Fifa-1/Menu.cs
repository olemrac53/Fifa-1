using System;
using System.Windows.Forms;
using Fifa.Core;
using Fifa.Dapper;

namespace Fifa_1
{
    public partial class Menu : Form
    {
        private readonly Usuario _usuarioLogueado;
        private Usuario _usuarioConPlantillas; // Para guardar los datos completos

        // 1. Constructor que recibe el Usuario desde el Login
        public Menu(Usuario usuarioLogueado)
        {
            InitializeComponent();
            _usuarioLogueado = usuarioLogueado;
        }

        // 2. Al cargar el Menú, saludamos y buscamos sus plantillas
        private void Menu_Load(object sender, EventArgs e)
        {
            lblBienvenida.Text = $"Hola, {_usuarioLogueado.Nombre}!";
            CargarPlantillas();
        }

        private void CargarPlantillas()
        {
            try
            {
                using var con = ConexionDB.CrearConexion();
                con.Open();
                var repoUsuario = new RepoUsuario(con);
                
                // Usamos tu método para traer el usuario CON sus plantillas
                _usuarioConPlantillas = repoUsuario.GetUsuarioConPlantillas(_usuarioLogueado.IdUsuario);

                if (_usuarioConPlantillas?.Plantillas == null || _usuarioConPlantillas.Plantillas.Count == 0)
                {
                    // (Lógica opcional) Si no tiene plantillas, podríamos crear una
                    MessageBox.Show("No tienes plantillas. (Aquí iría la lógica para crear una).");
                    btnGestionarPlantilla.Enabled = false;
                }
                else
                {
                    // Llenamos el ComboBox
                    cmbPlantillas.DataSource = _usuarioConPlantillas.Plantillas;
                    cmbPlantillas.DisplayMember = "IdPlantilla"; // Muestra el ID (puedes cambiar esto si Plantilla tuviera un 'Nombre')
                    cmbPlantillas.ValueMember = "IdPlantilla";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar plantillas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. Al hacer clic en "Gestionar", abrimos el formulario 'plantilla'
        private void btnGestionarPlantilla_Click(object sender, EventArgs e)
        {
            if (cmbPlantillas.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una plantilla.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idPlantillaSeleccionada = (int)cmbPlantillas.SelectedValue;

            // Abrimos el formulario 'plantilla' pasándole el ID
            // (Usando el código que te di en la respuesta anterior)
            plantilla formPlantilla = new plantilla(idPlantillaSeleccionada);
            formPlantilla.Show();
            this.Hide();
        }

        // 4. Botón para salir
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Inicio_sesion login = new Inicio_sesion();
            login.Show();
            this.Hide();
        }
    }
}