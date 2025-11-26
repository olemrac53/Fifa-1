using System;
using System.Data;
using System.Windows.Forms;
using Fifa.Core;
using Fifa.Core.Repos;
using Fifa.Dapper;
using MySqlConnector;
using animacion_fifa; // Para acceder a Program

namespace Fifa_1
{
    public partial class Plantilla : Form
    {
        private readonly int _idPlantilla;
        private Fifa.Core.Plantilla _plantillaActual;

        public Plantilla(int idPlantilla)
        {
            InitializeComponent();
            _idPlantilla = idPlantilla;
        }




        private void plantilla_Load(object sender, EventArgs e)
        {

            // CAMBIO 3: Configurar el cursor de mano en todos los botones
            ConfigurarCursores(this.Controls);

            btnFicharTitular.Enabled = false;
            btnFicharSuplente.Enabled = false;
            btnQuitarTitular.Enabled = false;
            btnQuitarSuplente.Enabled = false;

            // CAMBIO 1: Mostrar nombre del equipo del usuario
            if (Program.UsuarioActual != null)
            {
                // Usamos la variable declarada en el Designer
                lblNombreEquipo.Text = "Equipo de " + Program.UsuarioActual.Nombre;
            }

            CargarDatos();
        }

        // CAMBIO 3: Método auxiliar para recorrer controles y cambiar cursor
        private void ConfigurarCursores(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c is Button)
                {
                    c.Cursor = Cursors.Hand;
                }
                // Si tienes paneles dentro de paneles, descomenta la recursividad:
                // if (c.HasChildren) ConfigurarCursores(c.Controls);
            }
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
                    ConfigurarGrilla(dgvMercado);
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

            int fechaActual = 1;
            decimal puntaje = repoPlantilla.CalcularPuntajePlantillaFecha(_idPlantilla, fechaActual);
            lblPuntaje.Text = $"Puntaje Fecha {fechaActual}: {puntaje}";

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
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tipo", HeaderText = "Posición", DataPropertyName = "NombreTipo" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "Equipo", HeaderText = "Equipo", DataPropertyName = "NombreEquipo" });

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

        private void dgvMercado_SelectionChanged(object sender, EventArgs e)
        {
            btnFicharTitular.Enabled = dgvMercado.CurrentRow != null;
            btnFicharSuplente.Enabled = dgvMercado.CurrentRow != null;
        }

        // CAMBIO 2: Evento para actualizar puntajes automáticamente
        private void dgvTitulares_SelectionChanged(object sender, EventArgs e)
        {
            btnQuitarTitular.Enabled = dgvTitulares.CurrentRow != null;

            // Lógica para llenar la tabla de puntajes (dgvPuntajes)
            if (dgvTitulares.CurrentRow != null)
            {
                var futbolista = dgvTitulares.CurrentRow.DataBoundItem as Futbolista;
                if (futbolista != null)
                {
                    try
                    {
                        using (var con = ConexionDB.CrearConexion())
                        {
                            con.Open();
                            var repoPuntuacion = new RepoPuntuacion(con);
                            var listaPuntajes = repoPuntuacion.GetPuntuacionesPorFutbolista(futbolista.IdFutbolista);

                            // Asignamos al grid nuevo (asegúrate de haberlo creado en el Designer)
                            dgvPuntajes.DataSource = listaPuntajes;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Opcional: Manejo silencioso o MessageBox
                    }
                }
            }
        }

        private void dgvSuplentes_SelectionChanged(object sender, EventArgs e)
        {
            btnQuitarSuplente.Enabled = dgvSuplentes.CurrentRow != null;
        }

        private void btnVolverMenu_Click(object sender, EventArgs e)
        {
            Menu menuForm = (Menu)Application.OpenForms["Menu"];
                
            if (menuForm != null)
            {
                menuForm.Show();
                menuForm.CargarPlantillas();
            }
            else
            {
                Inicio_sesion login = new Inicio_sesion();
                login.Show();
            }

            this.Close();
        }


    }
}