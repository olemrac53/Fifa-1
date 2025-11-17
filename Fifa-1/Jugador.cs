using System;
using System.Data;
using System.Windows.Forms;
using Fifa.Core;
using Fifa.Core.Repos;
using Fifa.Dapper;

namespace Fifa_1
{
    public partial class Jugador : Form
    {
        // --- CORRECCIÓN 1: Eliminar los campos de repositorio ---
        // private IRepoFutbolista _repoFutbolista;  <-- ELIMINADO
        // private IRepoTipo _repoTipo;            <-- ELIMINADO
        // private IRepoEquipo _repoEquipo;          <-- ELIMINADO

        // SÍ podemos guardar el futbolista seleccionado (es solo un objeto de datos)
        private Futbolista _futbolistaSeleccionado;

        public Jugador()
        {
            InitializeComponent();
        }

        private void Jugador_Load(object sender, EventArgs e)
        {
            try
            {
                // --- CORRECCIÓN 2: Crear conexión y repos SOLO para cargar ---
                using (var con = ConexionDB.CrearConexion())
                {
                    con.Open();
                    // Pasamos los repos a los métodos
                    CargarCombos(new RepoTipo(con), new RepoEquipo(con));
                    CargarGrilla(new RepoFutbolista(con));
                }
                // --- La conexión se cierra aquí ---
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCombos(IRepoTipo repoTipo, IRepoEquipo repoEquipo)
        {
            // Cargar Tipos
            cmbTipo.DataSource = repoTipo.GetTipos();
            cmbTipo.DisplayMember = "Nombre";
            cmbTipo.ValueMember = "IdTipo";

            // Cargar Equipos
            cmbEquipo.DataSource = repoEquipo.GetEquipos();
            cmbEquipo.DisplayMember = "Nombre";
            cmbEquipo.ValueMember = "IdEquipo";
        }

        private void CargarGrilla(IRepoFutbolista repoFutbolista)
        {
            dgvFutbolistas.DataSource = null;
            var lista = repoFutbolista.GetFutbolistas();
            dgvFutbolistas.DataSource = lista;

            // Configurar grilla
            dgvFutbolistas.Columns["IdFutbolista"].Visible = false;
            dgvFutbolistas.Columns["FechaNacimiento"].Visible = false;
            dgvFutbolistas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dgvFutbolistas.Columns.Contains("Cotizacion"))
                dgvFutbolistas.Columns["Cotizacion"].DefaultCellStyle.Format = "C2";
        }

        private void dgvFutbolistas_SelectionChanged(object sender, EventArgs e)
        {
            // Este método no usa la BD, así que no necesita cambios.
            if (dgvFutbolistas.CurrentRow == null || dgvFutbolistas.CurrentRow.DataBoundItem == null)
            {
                LimpiarFormulario();
                return;
            }

            _futbolistaSeleccionado = (Futbolista)dgvFutbolistas.CurrentRow.DataBoundItem;
            PoblarFormulario(_futbolistaSeleccionado);
        }

        private void PoblarFormulario(Futbolista fut)
        {
            txtNombre.Text = fut.Nombre;
            txtApellido.Text = fut.Apellido;
            txtApodo.Text = fut.Apodo;
            txtNumCamisa.Text = fut.NumCamisa;
            txtCotizacion.Text = fut.Cotizacion.ToString();
            dtpFechaNacimiento.Value = fut.FechaNacimiento;
            cmbTipo.SelectedValue = fut.Tipo.IdTipo;
            cmbEquipo.SelectedValue = fut.Equipo.IdEquipo;
        }

        private void LimpiarFormulario()
        {
            _futbolistaSeleccionado = null;
            txtNombre.Clear();
            txtApellido.Clear();
            txtApodo.Clear();
            txtNumCamisa.Clear();
            txtCotizacion.Clear();
            dtpFechaNacimiento.Value = DateTime.Now;
            if (cmbTipo.Items.Count > 0) cmbTipo.SelectedIndex = 0;
            if (cmbEquipo.Items.Count > 0) cmbEquipo.SelectedIndex = 0;
            dgvFutbolistas.ClearSelection();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        // --- CORRECCIÓN 3: 'btnGuardar' debe crear su propia conexión ---
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                // Crear el objeto Futbolista desde el formulario
                var futbolista = new Futbolista
                {
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Apodo = txtApodo.Text.Trim(),
                    NumCamisa = txtNumCamisa.Text.Trim(),
                    Cotizacion = decimal.Parse(txtCotizacion.Text),
                    FechaNacimiento = dtpFechaNacimiento.Value.Date,
                    Tipo = (Tipo)cmbTipo.SelectedItem,
                    Equipo = (Equipo)cmbEquipo.SelectedItem
                };

                // Abrir una NUEVA conexión solo para esta operación
                using (var con = ConexionDB.CrearConexion())
                {
                    con.Open();
                    IRepoFutbolista repoFutbolista = new RepoFutbolista(con);

                    if (_futbolistaSeleccionado == null) // Es Nuevo
                    {
                        repoFutbolista.InsertFutbolista(futbolista);
                        MessageBox.Show("Futbolista creado exitosamente.", "Nuevo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else // Es Modificación
                    {
                        futbolista.IdFutbolista = _futbolistaSeleccionado.IdFutbolista;
                        repoFutbolista.UpdateFutbolista(futbolista);
                        MessageBox.Show("Futbolista modificado exitosamente.", "Modificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Recargamos la grilla (reutilizando el repo y la conexión)
                    CargarGrilla(repoFutbolista);
                }
                // --- La conexión se cierra aquí ---

                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- CORRECCIÓN 4: 'btnEliminar' debe crear su propia conexión ---
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_futbolistaSeleccionado == null)
            {
                MessageBox.Show("Seleccione un futbolista de la lista.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show($"¿Está seguro de que desea eliminar a {_futbolistaSeleccionado.Nombre} {_futbolistaSeleccionado.Apellido}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    // Abrir una NUEVA conexión solo para esta operación
                    using (var con = ConexionDB.CrearConexion())
                    {
                        con.Open();
                        IRepoFutbolista repoFutbolista = new RepoFutbolista(con);

                        repoFutbolista.DeleteFutbolista(_futbolistaSeleccionado.IdFutbolista);
                        MessageBox.Show("Futbolista eliminado.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Recargamos la grilla (reutilizando el repo y la conexión)
                        CargarGrilla(repoFutbolista);
                    }
                    // --- La conexión se cierra aquí ---

                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("Nombre y Apellido son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!decimal.TryParse(txtCotizacion.Text, out _))
            {
                MessageBox.Show("La cotización debe ser un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cmbTipo.SelectedItem == null || cmbEquipo.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un Tipo y un Equipo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // Tus botones originales (button1_Click y button2_Click) no
        // tenían lógica, así que los he omitido por los nuevos botones del CRUD.
    }
}