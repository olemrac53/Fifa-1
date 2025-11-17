using System;
using System.Data;
using System.Windows.Forms;
using Fifa.Core;
using Fifa.Core.Repos;
using Fifa.Dapper;
using MySqlConnector;

namespace Fifa_1
{
    public partial class plantilla : Form
    {
        private readonly int _idPlantilla;

        // --- CORRECCIÓN 1: Eliminar los campos de repositorio ---
        // private IRepoPlantilla _repoPlantilla;  <-- ELIMINADO
        // private IRepoFutbolista _repoFutbolista; <-- ELIMINADO

        private Plantilla _plantillaActual; // El objeto de datos SÍ se puede guardar

        public plantilla(int idPlantilla)
        {
            InitializeComponent();
            _idPlantilla = idPlantilla;
        }

        private void plantilla_Load(object sender, EventArgs e)
        {
            // Deshabilitar botones al inicio
            btnFicharTitular.Enabled = false;
            btnFicharSuplente.Enabled = false;
            btnQuitarTitular.Enabled = false;
            btnQuitarSuplente.Enabled = false;

            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                // --- CORRECCIÓN 2: Los repos se crean y se usan solo localmente ---
                using (var con = ConexionDB.CrearConexion())
                {
                    con.Open();
                    IRepoPlantilla repoPlantilla = new RepoPlantilla(con);
                    IRepoFutbolista repoFutbolista = new RepoFutbolista(con);

                    // Cargamos el mercado
                    dgvMercado.DataSource = null;
                    dgvMercado.DataSource = repoFutbolista.GetFutbolistas();
                    ConfigurarGrilla(dgvMercado);

                    // Cargamos la plantilla actual
                    _plantillaActual = repoPlantilla.GetPlantillaCompleta(_idPlantilla);

                    if (_plantillaActual == null)
                    {
                        MessageBox.Show("No se pudo cargar la plantilla.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }

                    // Enlazar datos a las grillas
                    dgvTitulares.DataSource = null;
                    dgvTitulares.DataSource = _plantillaActual.Titulares;
                    ConfigurarGrilla(dgvTitulares);

                    dgvSuplentes.DataSource = null;
                    dgvSuplentes.DataSource = _plantillaActual.Suplentes;
                    ConfigurarGrilla(dgvSuplentes);

                    // Cargar datos en el GroupBox de Configuración
                    txtPresupuesto.Text = _plantillaActual.PresupuestoMax.ToString();
                    txtCantJugadores.Text = _plantillaActual.CantMaxFutbolistas.ToString();

                    // Actualizar Labels (con la conexión aún abierta)
                    ActualizarPresupuestoLabel(repoPlantilla);
                    ActualizarPuntajeLabel(repoPlantilla);
                }
                // --- La conexión y los repos se desechan aquí. ¡Eso está bien! ---
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- CORRECCIÓN 3: Los métodos ahora crean su propia conexión/repo ---
        private void ActualizarPresupuestoLabel(IRepoPlantilla repoPlantilla)
        {
            if (_plantillaActual == null) return;
            decimal presupuestoUsado = repoPlantilla.CalcularPresupuestoPlantilla(_idPlantilla);
            lblPresupuestoActual.Text = $"Presupuesto: {presupuestoUsado:C} / {_plantillaActual.PresupuestoMax:C}";
        }

        private void ActualizarPuntajeLabel(IRepoPlantilla repoPlantilla)
        {
            if (_plantillaActual == null) return;
            int fechaActual = 1;
            decimal puntaje = repoPlantilla.CalcularPuntajePlantillaFecha(_idPlantilla, fechaActual);
            lblPuntaje.Text = $"Puntaje Fecha {fechaActual}: {puntaje}";
        }

        // ... (ConfigurarGrilla no necesita conexión, está bien como está)
        private void ConfigurarGrilla(DataGridView dgv)
        {
            // ... (código sin cambios)
        }


        // --- CORRECCIÓN 4: Añadir 'using' a TODOS los métodos de Clic ---

        private void btnGuardarConfig_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtPresupuesto.Text, out decimal nuevoPresupuesto) ||
                !int.TryParse(txtCantJugadores.Text, out int nuevaCantidad))
            {
                MessageBox.Show("Por favor, ingrese valores numéricos válidos.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _plantillaActual.PresupuestoMax = nuevoPresupuesto;
            _plantillaActual.CantMaxFutbolistas = nuevaCantidad;

            try
            {
                // Creamos una NUEVA conexión y repo solo para esta operación
                using (var con = ConexionDB.CrearConexion())
                {
                    con.Open();
                    IRepoPlantilla repoPlantilla = new RepoPlantilla(con);
                    repoPlantilla.UpdatePlantilla(_plantillaActual);

                    // Actualizamos el label de presupuesto (reutilizando el repo)
                    ActualizarPresupuestoLabel(repoPlantilla);
                }

                MessageBox.Show("Configuración de la plantilla actualizada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la configuración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Revertir cambios
                txtPresupuesto.Text = _plantillaActual.PresupuestoMax.ToString();
                txtCantJugadores.Text = _plantillaActual.CantMaxFutbolistas.ToString();
            }
        }

        private void FicharJugador(bool esTitular)
        {
            if (dgvMercado.CurrentRow == null) return;
            var futbolista = (Futbolista)dgvMercado.CurrentRow.DataBoundItem;

            try
            {
                // Creamos una NUEVA conexión y repo solo para esta operación
                using (var con = ConexionDB.CrearConexion())
                {
                    con.Open();
                    IRepoPlantilla repoPlantilla = new RepoPlantilla(con);

                    if (esTitular)
                        repoPlantilla.AgregarTitular(_idPlantilla, futbolista.IdFutbolista);
                    else
                        repoPlantilla.AgregarSuplente(_idPlantilla, futbolista.IdFutbolista);
                }

                // Recargamos TODO (esto creará sus propias conexiones nuevas)
                CargarDatos();
            }
            catch (MySqlException mex)
            {
                MessageBox.Show($"No se pudo fichar: {mex.Message}", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void QuitarJugador(Futbolista futbolista, bool esTitular)
        {
            try
            {
                // Creamos una NUEVA conexión y repo solo para esta operación
                using (var con = ConexionDB.CrearConexion())
                {
                    con.Open();
                    IRepoPlantilla repoPlantilla = new RepoPlantilla(con);

                    if (esTitular)
                        repoPlantilla.EliminarTitular(_idPlantilla, futbolista.IdFutbolista);
                    else
                        repoPlantilla.EliminarSuplente(_idPlantilla, futbolista.IdFutbolista);
                }

                CargarDatos(); // Recargamos
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al quitar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- (El resto de los métodos de clic no necesitan cambios) ---
        private void btnFicharTitular_Click(object sender, EventArgs e)
        {
            FicharJugador(esTitular: true);
        }

        private void btnFicharSuplente_Click(object sender, EventArgs e)
        {
            FicharJugador(esTitular: false);
        }

        private void btnQuitarTitular_Click(object sender, EventArgs e)
        {
            if (dgvTitulares.CurrentRow == null) return;
            var futbolista = (Futbolista)dgvTitulares.CurrentRow.DataBoundItem;
            QuitarJugador(futbolista, esTitular: true);
        }

        private void btnQuitarSuplente_Click(object sender, EventArgs e)
        {
            if (dgvSuplentes.CurrentRow == null) return;
            var futbolista = (Futbolista)dgvSuplentes.CurrentRow.DataBoundItem;
            QuitarJugador(futbolista, esTitular: false);
        }

        private void dgvMercado_SelectionChanged(object sender, EventArgs e)
        {
            btnFicharTitular.Enabled = dgvMercado.CurrentRow != null;
            btnFicharSuplente.Enabled = dgvMercado.CurrentRow != null;
        }

        private void dgvTitulares_SelectionChanged(object sender, EventArgs e)
        {
            btnQuitarTitular.Enabled = dgvTitulares.CurrentRow != null;
        }

        private void dgvSuplentes_SelectionChanged(object sender, EventArgs e)
        {
            btnQuitarSuplente.Enabled = dgvSuplentes.CurrentRow != null;
        }

        private void btnVolverMenu_Click(object sender, EventArgs e)
        {
            Menu menuForm = (Menu)Application.OpenForms["Menu"];
            if (menuForm == null)
            {
                Inicio_sesion login = new Inicio_sesion();
                login.Show();
            }
            else
            {
                menuForm.Show();
                menuForm.CargarPlantillas();
            }
            this.Close();
        }
    }
}