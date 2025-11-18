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
        private PuntuacionFutbolista _puntuacionSeleccionada;

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
            // Necesitamos un método en RepoPuntuacion para traer todas
            // (Asumimos que GetPuntuaciones() existe y devuelve una Lista)
            var lista = repoPuntuacion.GetPuntuaciones();
            dgvPuntuaciones.DataSource = lista;
            dgvPuntuaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CargarComboFutbolistas(IRepoFutbolista repoFutbolista)
        {
            cmbFutbolista.DataSource = repoFutbolista.GetFutbolistas();
            cmbFutbolista.DisplayMember = "Nombre"; // Muestra "Lionel Messi"
            cmbFutbolista.ValueMember = "IdFutbolista"; // Guarda el ID
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
            if (dgvPuntuaciones.CurrentRow == null) return;

            _puntuacionSeleccionada = (PuntuacionFutbolista)dgvPuntuaciones.CurrentRow.DataBoundItem;

            // Poblar formulario
            cmbFutbolista.SelectedValue = _puntuacionSeleccionada.IdFutbolista;
            numFecha.Value = _puntuacionSeleccionada.Fecha;
            txtPuntaje.Text = _puntuacionSeleccionada.Puntuacion.ToString();

            // No se puede editar la clave primaria (Futbolista y Fecha)
            cmbFutbolista.Enabled = false;
            numFecha.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbFutbolista.SelectedItem == null || !decimal.TryParse(txtPuntaje.Text, out decimal puntaje))
            {
                MessageBox.Show("Seleccione un futbolista e ingrese un puntaje válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var con = ConexionDB.CrearConexion())
                {
                    con.Open();
                    var repo = new RepoPuntuacion(con);

                    if (_puntuacionSeleccionada == null) // Es NUEVO
                    {
                        var nuevaPuntuacion = new PuntuacionFutbolista
                        {
                            IdFutbolista = (int)cmbFutbolista.SelectedValue,
                            Fecha = (int)numFecha.Value,
                            Puntuacion = puntaje
                        };
                        repo.InsertPuntuacion(nuevaPuntuacion);
                        MessageBox.Show("Puntuación guardada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else // Es MODIFICACIÓN
                    {
                        _puntuacionSeleccionada.Puntuacion = puntaje;
                        repo.UpdatePuntuacion(_puntuacionSeleccionada);
                        MessageBox.Show("Puntuación actualizada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    CargarGrilla(repo); // Recargamos
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
                        repo.DeletePuntuacion(_puntuacionSeleccionada.IdPuntuacion);
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
    }
}