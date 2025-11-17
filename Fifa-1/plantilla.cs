using System;
using System.Linq;
using System.Windows.Forms;
using Fifa.Core;
using Fifa.Dapper;
using System.Collections.Generic;

namespace Fifa_1
{
    public partial class Plantilla : Form
    {
        private int _idPlantilla;
        private Usuario _usuarioLogueado;
        private RepoPlantilla? _repoPlantilla;
        private RepoFutbolista? _repoFutbolista;
        private RepoEquipo? _repoEquipo;
        private Fifa.Core.Plantilla? _plantillaActual;
        A
        public Plantilla(int idPlantilla, Usuario usuarioLogueado)
        {
            InitializeComponent();
            _idPlantilla = idPlantilla;
            _usuarioLogueado = usuarioLogueado;
            InicializarRepos();
            CargarPlantilla();
            CargarFutbolistasDisponibles();
            CargarEquipos();
        }

        private void InicializarRepos()
        {
            var con = Fifa.Dapper.ConexionDB.CrearConexion();
            con.Open();
            _repoPlantilla = new RepoPlantilla(con);
            _repoFutbolista = new RepoFutbolista(con);
            _repoEquipo = new RepoEquipo(con);
        }

        private void CargarPlantilla()
        {
            _plantillaActual = _repoPlantilla?.GetPlantillaCompleta(_idPlantilla);

            if (_plantillaActual == null)
            {
                MessageBox.Show("No se pudo cargar la plantilla.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Mostrar titulares y suplentes en el DataGridView principal
            var todos = _plantillaActual.Titulares.Concat(_plantillaActual.Suplentes).ToList();
            dataGridViewPlantilla.DataSource = null;
            dataGridViewPlantilla.DataSource = todos;
        }

        private void CargarFutbolistasDisponibles()
        {
            var todosFutbolistas = _repoFutbolista?.GetFutbolistas() ?? new List<Futbolista>();
            var idsEnPlantilla = _plantillaActual?.Titulares.Select(f => f.IdFutbolista)
                .Concat(_plantillaActual?.Suplentes.Select(f => f.IdFutbolista) ?? Enumerable.Empty<int>())
                .ToHashSet() ?? new HashSet<int>();

            var disponibles = todosFutbolistas.Where(f => !idsEnPlantilla.Contains(f.IdFutbolista)).ToList();

            dataGridViewDisponibles.DataSource = null;
            dataGridViewDisponibles.DataSource = disponibles;
        }

        private void CargarEquipos()
        {
            var equipos = _repoEquipo?.GetEquipos() ?? new List<Equipo>();
            comboBoxEquipos.DataSource = equipos;
            comboBoxEquipos.DisplayMember = "Nombre";
            comboBoxEquipos.ValueMember = "IdEquipo";
        }

        private void buttonSalir_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu(_usuarioLogueado);
            menu.Show();
            this.Hide();
        }

        private int ObtenerIdFutbolistaPlantillaSeleccionado()
        {
            if (dataGridViewPlantilla.CurrentRow?.DataBoundItem is Futbolista futbolista)
                return futbolista.IdFutbolista;
            throw new InvalidOperationException("No hay futbolista seleccionado en la plantilla.");
        }

        private int ObtenerIdFutbolistaDisponibleSeleccionado()
        {
            if (dataGridViewDisponibles.CurrentRow?.DataBoundItem is Futbolista futbolista)
                return futbolista.IdFutbolista;
            throw new InvalidOperationException("No hay futbolista disponible seleccionado.");
        }

        private void btnAgregarTitular_Click(object sender, EventArgs e)
        {
            if (_repoPlantilla == null || _plantillaActual == null)
            {
                MessageBox.Show("La plantilla no está cargada correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int idFutbolista = ObtenerIdFutbolistaDisponibleSeleccionado();
            try
            {
                _repoPlantilla.AgregarTitular(_idPlantilla, idFutbolista);
                CargarPlantilla();
                CargarFutbolistasDisponibles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar titular: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarTitular_Click(object sender, EventArgs e)
        {
            if (_repoPlantilla == null || _plantillaActual == null)
            {
                MessageBox.Show("La plantilla no está cargada correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int idFutbolista = ObtenerIdFutbolistaPlantillaSeleccionado();
            try
            {
                _repoPlantilla.EliminarTitular(_idPlantilla, idFutbolista);
                CargarPlantilla();
                CargarFutbolistasDisponibles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar titular: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarSuplente_Click(object sender, EventArgs e)
        {
            if (_repoPlantilla == null || _plantillaActual == null)
            {
                MessageBox.Show("La plantilla no está cargada correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int idFutbolista = ObtenerIdFutbolistaDisponibleSeleccionado();
            try
            {
                _repoPlantilla.AgregarSuplente(_idPlantilla, idFutbolista);
                CargarPlantilla();
                CargarFutbolistasDisponibles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar suplente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarSuplente_Click(object sender, EventArgs e)
        {
            if (_repoPlantilla == null || _plantillaActual == null)
            {
                MessageBox.Show("La plantilla no está cargada correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int idFutbolista = ObtenerIdFutbolistaPlantillaSeleccionado();
            try
            {
                _repoPlantilla.EliminarSuplente(_idPlantilla, idFutbolista);
                CargarPlantilla();
                CargarFutbolistasDisponibles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar suplente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Navegación a otros formularios
        private void btnVerFutbolistas_Click(object sender, EventArgs e)
        {
            Jugador formFutbolista = new Jugador();
            formFutbolista.Show();
            this.Hide();
        }

        private void btnVerEquipos_Click(object sender, EventArgs e)
        {
            // Asumiendo que tienes un formulario llamado Equipo
            EquipoForm formEquipo = new EquipoForm();
            formEquipo.Show();
            this.Hide();
        }
    }
}
