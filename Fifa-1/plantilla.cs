using System;
using System.Windows.Forms;
using Fifa.Core;

namespace Fifa_1
{
    public partial class Plantilla : Form
    {
        private int _idPlantilla;
        private Usuario _usuarioLogueado;

        public Plantilla(int idPlantilla, Usuario usuarioLogueado)
        {
            InitializeComponent();
            _idPlantilla = idPlantilla;
            _usuarioLogueado = usuarioLogueado;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu(_usuarioLogueado);
            menu.Show();
            this.Hide();
        }
    }
}
