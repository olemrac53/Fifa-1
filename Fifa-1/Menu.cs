using System;
using System.Windows.Forms;
using Fifa.Core;
using Fifa.Dapper;

namespace Fifa_1
{
    public partial class Menu : Form
    {
        private readonly Usuario _usuarioLogueado;
        private Usuario? _usuarioConPlantillas; // Para guardar los datos completos

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

                _usuarioConPlantillas = repoUsuario.GetUsuarioConPlantillas(_usuarioLogueado.IdUsuario);

                if (_usuarioConPlantillas?.Plantillas == null || _usuarioConPlantillas.Plantillas.Count == 0)
                {
                    var crear = MessageBox.Show(
                        "No tienes plantillas. ¿Deseas crear una ahora?", 
                        "Plantillas", 
                        MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Question);

                    btnGestionarPlantilla.Enabled = false;

                    if (crear == DialogResult.Yes)
                    {
                        CrearPlantillaInteractiva();
                    }
                }
                else
                {
                    cmbPlantillas.DataSource = _usuarioConPlantillas.Plantillas;
                    cmbPlantillas.DisplayMember = "IdPlantilla";
                    cmbPlantillas.ValueMember = "IdPlantilla";
                    btnGestionarPlantilla.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar plantillas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para crear la plantilla pidiendo datos al usuario
        private void CrearPlantillaInteractiva()
        {
            // Aquí puedes usar un formulario modal para pedir datos, o usar valores fijos:
            decimal presupuestoMax = 5000000;
            int cantMaxFutbolistas = 25;

            using var con = Fifa.Dapper.ConexionDB.CrearConexion();
            con.Open();
            var repoPlantilla = new RepoPlantilla(con);

            var nuevaPlantilla = new Fifa.Core.Plantilla
            {
                Usuario = _usuarioLogueado,
                PresupuestoMax = presupuestoMax,
                CantMaxFutbolistas = cantMaxFutbolistas
            };

            repoPlantilla.InsertPlantilla(nuevaPlantilla);

            MessageBox.Show($"Plantilla #{nuevaPlantilla.IdPlantilla} creada correctamente.", "Plantilla", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Actualiza la lista de plantillas del usuario
            CargarPlantillas();
        }

        private int GetSelectedValue()
        {
            // Fix CS8605: Unboxing a posiblemente null value.
            // Asegurarse de que SelectedValue no sea nulo antes de deserializar.
            if (cmbPlantillas.SelectedValue is int value)
            {
                return value;
            }
            else
            {
                throw new InvalidOperationException("No se seleccionó ninguna plantilla o SelectedValue es nulo.");
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

            int idPlantillaSeleccionada = GetSelectedValue();

            Plantilla formPlantilla = new Plantilla(idPlantillaSeleccionada, _usuarioLogueado);
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

        private void btnCrearPlantilla_Click(object sender, EventArgs e)
        {
            // Puedes pedir estos datos al usuario con un formulario modal, aquí ejemplo fijo:
            decimal presupuestoMax = 5000000;
            int cantMaxFutbolistas = 25;

            // Si quieres pedir al usuario, usa un formulario modal personalizado aquí

            using var con = Fifa.Dapper.ConexionDB.CrearConexion();
            con.Open();
            var repoPlantilla = new RepoPlantilla(con);

            var nuevaPlantilla = new Fifa.Core.Plantilla
            {
                Usuario = _usuarioLogueado,
                PresupuestoMax = presupuestoMax,
                CantMaxFutbolistas = cantMaxFutbolistas
            };

            repoPlantilla.InsertPlantilla(nuevaPlantilla);

            MessageBox.Show($"Plantilla #{nuevaPlantilla.IdPlantilla} creada correctamente.", "Plantilla", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Actualiza la lista de plantillas del usuario
            CargarPlantillas();
        }
    }
}