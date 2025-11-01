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
        private Futbolista _futbolistaSeleccionado;

        public Jugador()
        {
            InitializeComponent();
        }

        private void Jugador_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCombos();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCombos()
        {
            using (var con = ConexionDB.CrearConexion())
            {
                con.Open();
                // Asumo que RepoTipo y RepoEquipo existen e implementan las interfaces
                IRepoTipo repoTipo = new RepoTipo(con);
                IRepoEquipo repoEquipo = new RepoEquipo(con);

                var equipoSeleccionado = cmbEquipo.SelectedValue;

                // Cargar Tipos
                cmbTipo.DataSource = repoTipo.GetTipos();
                cmbTipo.DisplayMember = "Nombre";
                cmbTipo.ValueMember = "IdTipo";

                // Cargar Equipos
                cmbEquipo.DataSource = repoEquipo.GetEquipos();
                cmbEquipo.DisplayMember = "Nombre";
                cmbEquipo.ValueMember = "IdEquipo";

                if (equipoSeleccionado != null)
                {
                    cmbEquipo.SelectedValue = equipoSeleccionado;
                }
            }
        }

        // --- INICIO DE CORRECCIÓN (BUG 1) ---
        private void CargarGrilla()
        {
            using (var con = ConexionDB.CrearConexion())
            {
                con.Open();
                IRepoFutbolista repoFutbolista = new RepoFutbolista(con);

                dgvFutbolistas.DataSource = null;
                dgvFutbolistas.Columns.Clear();
                dgvFutbolistas.AutoGenerateColumns = false;

                // Columnas manuales
                dgvFutbolistas.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Nombre",
                    HeaderText = "Nombre",
                    DataPropertyName = "Nombre"
                });
                dgvFutbolistas.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Apellido",
                    HeaderText = "Apellido",
                    DataPropertyName = "Apellido"
                });

                // --- CORRECCIÓN AQUÍ ---
                // Antes decía "Tipo.Nombre", ahora usamos la propiedad directa "NombreTipo"
                dgvFutbolistas.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Tipo",
                    HeaderText = "Posición",
                    DataPropertyName = "NombreTipo"
                });

                // --- CORRECCIÓN AQUÍ ---
                // Antes decía "Equipo.Nombre", ahora usamos la propiedad directa "NombreEquipo"
                dgvFutbolistas.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Equipo",
                    HeaderText = "Equipo",
                    DataPropertyName = "NombreEquipo"
                });
                // -----------------------

                dgvFutbolistas.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Cotizacion",
                    HeaderText = "Cotización",
                    DataPropertyName = "Cotizacion",
                    DefaultCellStyle = { Format = "C2" }
                });

                var lista = repoFutbolista.GetFutbolistas();
                dgvFutbolistas.DataSource = lista;
                dgvFutbolistas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        // --- FIN DE CORRECCIÓN (BUG 1) ---

        private void dgvFutbolistas_SelectionChanged(object sender, EventArgs e)
        {
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;
            try
            {
                var futbolista = new Futbolista
                {
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Apodo = txtApodo.Text.Trim(),
                    NumCamisa = txtNumCamisa.Text.Trim(),
                    Cotizacion = decimal.Parse(txtCotizacion.Text),
                    FechaNacimiento = dtpFechaNacimiento.Value.Date,
                    Tipo = cmbTipo.SelectedItem as Tipo ?? throw new InvalidOperationException("Debe seleccionar un Tipo."),
                    Equipo = cmbEquipo.SelectedItem as Equipo ?? throw new InvalidOperationException("Debe seleccionar un Equipo.")
                };

                using (var con = ConexionDB.CrearConexion())
                {
                    con.Open();
                    IRepoFutbolista repoFutbolista = new RepoFutbolista(con);
                    if (_futbolistaSeleccionado == null)
                    {
                        repoFutbolista.InsertFutbolista(futbolista);
                        MessageBox.Show("Futbolista creado exitosamente.", "Nuevo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        futbolista.IdFutbolista = _futbolistaSeleccionado.IdFutbolista;
                        repoFutbolista.UpdateFutbolista(futbolista);
                        MessageBox.Show("Futbolista modificado exitosamente.", "Modificación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                CargarGrilla();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
                    using (var con = ConexionDB.CrearConexion())
                    {
                        con.Open();
                        IRepoFutbolista repoFutbolista = new RepoFutbolista(con);
                        repoFutbolista.DeleteFutbolista(_futbolistaSeleccionado.IdFutbolista);
                        MessageBox.Show("Futbolista eliminado.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    CargarGrilla();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCrearEquipo_Click(object sender, EventArgs e)
        {
            string nombreEquipo = txtNuevoEquipoNombre.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombreEquipo))
            {
                MessageBox.Show("Por favor, ingrese un nombre para el nuevo equipo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var nuevoEquipo = new Equipo
                {
                    Nombre = nombreEquipo,
                    Presupuesto = 0
                };
                using (var con = ConexionDB.CrearConexion())
                {
                    con.Open();
                    IRepoEquipo repoEquipo = new RepoEquipo(con);
                    repoEquipo.InsertEquipo(nuevoEquipo);
                }
                MessageBox.Show($"Equipo '{nombreEquipo}' creado con ID: {nuevoEquipo.IdEquipo}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCombos();
                cmbEquipo.SelectedValue = nuevoEquipo.IdEquipo;
                txtNuevoEquipoNombre.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear el equipo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // --- BOTÓN VOLVER AÑADIDO ---
        private void btnVolverMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}