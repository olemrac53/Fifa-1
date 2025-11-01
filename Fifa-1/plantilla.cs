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
        private Plantilla _plantillaActual;

        public plantilla(int idPlantilla)
        {
            InitializeComponent();
            _idPlantilla = idPlantilla;
        }

        private void plantilla_Load(object sender, EventArgs e)
        {
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
                using (var con = ConexionDB.CrearConexion())
                {
                    con.Open();
                    IRepoPlantilla repoPlantilla = new RepoPlantilla(con);
                    IRepoFutbolista repoFutbolista = new RepoFutbolista(con);

                    // Cargar Mercado
                    dgvMercado.DataSource = null;
                    ConfigurarGrilla(dgvMercado); // Configurar ANTES de cargar datos
                    dgvMercado.DataSource = repoFutbolista.GetFutbolistas();

                    // Cargar Plantilla
                    _plantillaActual = repoPlantilla.GetPlantillaCompleta(_idPlantilla);
                    if (_plantillaActual == null)
                    {
                        MessageBox.Show("No se pudo cargar la plantilla.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }

                    // Cargar Titulares
                    dgvTitulares.DataSource = null;
                    ConfigurarGrilla(dgvTitulares);
                    dgvTitulares.DataSource = _plantillaActual.Titulares;

                    // Cargar Suplentes
                    dgvSuplentes.DataSource = null;
                    ConfigurarGrilla(dgvSuplentes);
                    dgvSuplentes.DataSource = _plantillaActual.Suplentes;

                    // Cargar Configuración
                    txtPresupuesto.Text = _plantillaActual.PresupuestoMax.ToString();
                    txtCantJugadores.Text = _plantillaActual.CantMaxFutbolistas.ToString();

                    // Actualizar Labels
                    ActualizarPresupuestoLabel(repoPlantilla);
                    ActualizarLabelsInformativos(repoPlantilla);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarPresupuestoLabel(IRepoPlantilla repoPlantilla)
        {
            if (_plantillaActual == null) return;
            decimal presupuestoUsado = repoPlantilla.CalcularPresupuestoPlantilla(_idPlantilla);
            lblPresupuestoActual.Text = $"Presupuesto: {presupuestoUsado:C} / {_plantillaActual.PresupuestoMax:C}";
        }

        private void ActualizarLabelsInformativos(IRepoPlantilla repoPlantilla)
        {
            if (_plantillaActual == null) return;

            // 1. Lógica de Puntaje
            int fechaActual = 1;
            decimal puntaje = repoPlantilla.CalcularPuntajePlantillaFecha(_idPlantilla, fechaActual);
            lblPuntaje.Text = $"Puntaje Fecha {fechaActual}: {puntaje}";

            // 2. LÓGICA DE VALIDACIÓN DE FORMACIÓN AÑADIDA
            bool esValida = repoPlantilla.PlantillaEsValida(_idPlantilla);

            if (esValida)
            {
                lblFormacionValida.Text = "Formación: 1-4-4-2 VÁLIDA";
                lblFormacionValida.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblFormacionValida.Text = "Formación: INVÁLIDA (Requiere 1-4-4-2)";
                lblFormacionValida.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void ConfigurarGrilla(DataGridView dgv)
        {
            dgv.Columns.Clear();
            dgv.AutoGenerateColumns = false;

            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nombre", HeaderText = "Nombre", DataPropertyName = "Nombre" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Apellido", HeaderText = "Apellido", DataPropertyName = "Apellido" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tipo", HeaderText = "Posición", DataPropertyName = "Tipo.Nombre" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Equipo", HeaderText = "Equipo", DataPropertyName = "Equipo.Nombre" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cotizacion",
                HeaderText = "Cotización",
                DataPropertyName = "Cotizacion",
                DefaultCellStyle = { Format = "C2" }
            });

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

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
                using (var con = ConexionDB.CrearConexion())
                {
                    con.Open();
                    IRepoPlantilla repoPlantilla = new RepoPlantilla(con);
                    repoPlantilla.UpdatePlantilla(_plantillaActual);
                    ActualizarPresupuestoLabel(repoPlantilla);
                }
                MessageBox.Show("Configuración de la plantilla actualizada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la configuración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                using (var con = ConexionDB.CrearConexion())
                {
                    con.Open();
                    IRepoPlantilla repoPlantilla = new RepoPlantilla(con);
                    if (esTitular)
                        repoPlantilla.AgregarTitular(_idPlantilla, futbolista.IdFutbolista);
                    else
                        repoPlantilla.AgregarSuplente(_idPlantilla, futbolista.IdFutbolista);
                }
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
                using (var con = ConexionDB.CrearConexion())
                {
                    con.Open();
                    IRepoPlantilla repoPlantilla = new RepoPlantilla(con);
                    if (esTitular)
                        repoPlantilla.EliminarTitular(_idPlantilla, futbolista.IdFutbolista);
                    else
                        repoPlantilla.EliminarSuplente(_idPlantilla, futbolista.IdFutbolista);
                }
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al quitar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFicharTitular_Click(object sender, EventArgs e) { FicharJugador(esTitular: true); }
        private void btnFicharSuplente_Click(object sender, EventArgs e) { FicharJugador(esTitular: false); }

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

        private void dgvMercado_SelectionChanged(object sender, EventArgs e) { btnFicharTitular.Enabled = dgvMercado.CurrentRow != null; btnFicharSuplente.Enabled = dgvMercado.CurrentRow != null; }
        private void dgvTitulares_SelectionChanged(object sender, EventArgs e) { btnQuitarTitular.Enabled = dgvTitulares.CurrentRow != null; }
        private void dgvSuplentes_SelectionChanged(object sender, EventArgs e) { btnQuitarSuplente.Enabled = dgvSuplentes.CurrentRow != null; }

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