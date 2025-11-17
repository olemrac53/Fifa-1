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
        private IRepoPlantilla _repoPlantilla;
        private IRepoFutbolista _repoFutbolista;
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
                using var con = ConexionDB.CrearConexion();
                con.Open();
                _repoPlantilla = new RepoPlantilla(con);
                _repoFutbolista = new RepoFutbolista(con); 

                CargarMercado();
                CargarDatosPlantilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarMercado()
        {
            dgvMercado.DataSource = null;
            dgvMercado.DataSource = _repoFutbolista.GetFutbolistas();
            ConfigurarGrilla(dgvMercado);
        }

        private void CargarDatosPlantilla()
        {
            _plantillaActual = _repoPlantilla.GetPlantillaCompleta(_idPlantilla);

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

            // --- LÓGICA "UPDATE" (Cargar datos en el GroupBox) ---
            txtPresupuesto.Text = _plantillaActual.PresupuestoMax.ToString();
            txtCantJugadores.Text = _plantillaActual.CantMaxFutbolistas.ToString();
            // --- FIN LÓGICA "UPDATE" ---

            ActualizarPresupuestoLabel();
            ActualizarPuntajeLabel();
        }

        private void ActualizarPresupuestoLabel()
        {
            if (_plantillaActual == null) return;
            // Actualizar presupuesto usando la función de la DB
            decimal presupuestoUsado = _repoPlantilla.CalcularPresupuestoPlantilla(_idPlantilla);
            lblPresupuestoActual.Text = $"Presupuesto: {presupuestoUsado:C} / {_plantillaActual.PresupuestoMax:C}";
        }

        private void ActualizarPuntajeLabel()
        {
            if (_plantillaActual == null) return;
            int fechaActual = 1; // Necesitarías una forma de obtener la fecha/jornada
            decimal puntaje = _repoPlantilla.CalcularPuntajePlantillaFecha(_idPlantilla, fechaActual);
            lblPuntaje.Text = $"Puntaje Fecha {fechaActual}: {puntaje}";
        }

        private void ConfigurarGrilla(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dgv.Columns.Contains("IdFutbolista"))
                dgv.Columns["IdFutbolista"].Visible = false;
            if (dgv.Columns.Contains("FechaNacimiento"))
                dgv.Columns["FechaNacimiento"].Visible = false;
                
            if (dgv.Columns.Contains("Tipo"))
                dgv.Columns["Tipo"].DisplayIndex = 3;
            if (dgv.Columns.Contains("Equipo"))
                dgv.Columns["Equipo"].DisplayIndex = 4;
            if (dgv.Columns.Contains("Cotizacion"))
            {
                dgv.Columns["Cotizacion"].DisplayIndex = 5;
                dgv.Columns["Cotizacion"].DefaultCellStyle.Format = "C2";
            }
        }

        // --- LÓGICA "UPDATE" (Guardar Cambios) AÑADIDA ---
        private void btnGuardarConfig_Click(object sender, EventArgs e)
        {
            // Validar
            if (!decimal.TryParse(txtPresupuesto.Text, out decimal nuevoPresupuesto) ||
                !int.TryParse(txtCantJugadores.Text, out int nuevaCantidad))
            {
                MessageBox.Show("Por favor, ingrese valores numéricos válidos para presupuesto y cantidad.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Actualizar el objeto
            _plantillaActual.PresupuestoMax = nuevoPresupuesto;
            _plantillaActual.CantMaxFutbolistas = nuevaCantidad;

            try
            {
                // Llamar al repositorio
                _repoPlantilla.UpdatePlantilla(_plantillaActual);
                MessageBox.Show("Configuración de la plantilla actualizada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Recargar los labels
                ActualizarPresupuestoLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la configuración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Revertir cambios en los textbox si falla
                txtPresupuesto.Text = _plantillaActual.PresupuestoMax.ToString();
                txtCantJugadores.Text = _plantillaActual.CantMaxFutbolistas.ToString();
            }
        }


        // --- Lógica de Fichajes (Sin cambios) ---
        private void btnFicharTitular_Click(object sender, EventArgs e)
        {
            FicharJugador(esTitular: true);
        }

        private void btnFicharSuplente_Click(object sender, EventArgs e)
        {
            FicharJugador(esTitular: false);
        }

        private void FicharJugador(bool esTitular)
        {
            if (dgvMercado.CurrentRow == null) return;
            var futbolista = (Futbolista)dgvMercado.CurrentRow.DataBoundItem;

            try
            {
                if (esTitular)
                    _repoPlantilla.AgregarTitular(_idPlantilla, futbolista.IdFutbolista);
                else
                    _repoPlantilla.AgregarSuplente(_idPlantilla, futbolista.IdFutbolista);

                CargarDatosPlantilla(); 
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

        private void QuitarJugador(Futbolista futbolista, bool esTitular)
        {
            try
            {
                if (esTitular)
                    _repoPlantilla.EliminarTitular(_idPlantilla, futbolista.IdFutbolista);
                else
                    _repoPlantilla.EliminarSuplente(_idPlantilla, futbolista.IdFutbolista);
                
                CargarDatosPlantilla(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al quitar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            // Buscamos el menú que ya debe estar en memoria (o lo creamos)
            // Esto evita crear un menú nuevo cada vez.
            Menu menuForm = (Menu)Application.OpenForms["Menu"];
            if (menuForm == null)
            {
                // Si el menú no existe (improbable), creamos uno.
                // Necesitamos el usuario logueado, que no tenemos aquí.
                // Solución simple: forzar un nuevo login.
                Inicio_sesion login = new Inicio_sesion();
                login.Show();
            }
            else
            {
                menuForm.Show();
                menuForm.CargarPlantillas(); // Forzamos recarga del ComboBox
            }
            this.Close(); // Cerramos este formulario de plantilla
        }
    }
}