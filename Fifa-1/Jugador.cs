namespace Fifa_1
{
    public partial class Jugador : Form
    {
        public Jugador()
        {
            InitializeComponent();
        }

        private void Salir_Click(object sender, EventArgs e)
        {
            Menu Menu = new Menu();
            Menu.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Jugador_Load(object sender, EventArgs e)
        {

        }
    }
}
