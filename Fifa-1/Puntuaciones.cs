using System;
using System.Data;
using System.Windows.Forms;
using Fifa.Core;
using Fifa.Core.Repos;
using Fifa.Dapper;

namespace Fifa_1
{
    public partial class Puntuaciones : Form
    {
        private PuntuacionFutbolista? _puntuacionSeleccionada;

        public Puntuaciones()
        {
            InitializeComponent();
        }

        private void Puntuaciones_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                using (var con = ConexionDB.CrearConexion())
                {
                    con.Open();
                    CargarGrilla(new RepoPuntuacion(con));
                    CargarComboFutbolistas(new RepoFutbolista(con));
                }
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarGrilla(IRepoPuntuacion repoPuntuacion)
        {
            dgvPuntuaciones.DataSource = null;
            dgvPuntuaciones.Columns.Clear();
            dgvPuntuaciones.AutoGenerateColumns = false;

            // Columnas manuales
            dgvPuntuaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Futbolista",
                HeaderText = "Futbolista",
                DataPropertyName = "NombreFutbolista" 
            });
            dgvPuntuaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                HeaderText = "Fecha",
                DataPropertyName = "Fecha"
            });
            dgvPuntuaciones.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Puntuacion",
                HeaderText = "Puntaje",
                DataPropertyName = "Puntuacion"
            });

            var lista = repoPuntuacion.GetPuntuaciones();
            dgvPuntuaciones.DataSource = lista;
            dgvPuntuaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CargarComboFutbolistas(IRepoFutbolista repoFutbolista)
        {
            var futbolistas = repoFutbolista.GetFutbolistas();

            cmbFutbolista.DataSource = futbolistas;
            cmbFutbolista.DisplayMember = "NombreCompleto"; 
            cmbFutbolista.ValueMember = "IdFutbolista";
        }

        private void LimpiarFormulario()
        {
            _puntuacionSeleccionada = null;
            cmbFutbolista.SelectedIndex = -1;
            numFecha.Value = 1;
            txtPuntaje.Clear();
            cmbFutbolista.Enabled = true;
            numFecha.Enabled = true;
            dgvPuntuaciones.ClearSelection();
        }

        private void dgvPuntuaciones_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPuntuaciones.CurrentRow == null || dgvPuntuaciones.CurrentRow.DataBoundItem == null)
            {
                LimpiarFormulario();
                return;
            }

            _puntuacionSeleccionada = (PuntuacionFutbolista)dgvPuntuaciones.CurrentRow.DataBoundItem;

            cmbFutbolista.SelectedValue = _puntuacionSeleccionada.IdFutbolista;
            numFecha.Value = _puntuacionSeleccionada.Fecha;
            txtPuntaje.Text = _puntuacionSeleccionada.Puntuacion.ToString();

            cmbFutbolista.Enabled = false;
            numFecha.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbFutbolista.SelectedItem == null || !decimal.TryParse(txtPuntaje.Text, out decimal puntaje))
            {
                MessageBox.Show("Seleccione un futbolista e ingrese un puntaje válido (ej. 8,5).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (puntaje < 1 || puntaje > 10)
            {
                MessageBox.Show("El puntaje debe estar entre 1.0 y 10.0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var con = ConexionDB.CrearConexion())
                {
                    con.Open();
                    var repo = new RepoPuntuacion(con);

                    if (_puntuacionSeleccionada == null) 
                    {
                        var nuevaPuntuacion = new PuntuacionFutbolista
                        {
                            IdFutbolista = cmbFutbolista.SelectedValue is int idFutbolista ? idFutbolista : throw new InvalidOperationException("No futbolista seleccionado."),
                            Fecha = (int)numFecha.Value,
                            Puntuacion = puntaje
                        };
                        repo.AltaPuntuacion(nuevaPuntuacion.IdFutbolista, nuevaPuntuacion.Fecha, nuevaPuntuacion.Puntuacion);
                        MessageBox.Show("Puntuación guardada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else 
                    {
                        _puntuacionSeleccionada.Puntuacion = puntaje;
                        repo.ModificarPuntuacion(_puntuacionSeleccionada.IdPuntuacion, puntaje);
                        MessageBox.Show("Puntuación actualizada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    CargarGrilla(repo);
                }
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_puntuacionSeleccionada == null)
            {
                MessageBox.Show("Seleccione una puntuación de la grilla.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Seguro que desea eliminar esta puntuación?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (var con = ConexionDB.CrearConexion())
                    {
                        con.Open();
                        var repo = new RepoPuntuacion(con);
                        repo.EliminarPuntuacion(_puntuacionSeleccionada.IdPuntuacion);
                        MessageBox.Show("Puntuación eliminada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarGrilla(repo);
                    }
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnVolverMenu_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}