namespace Fifa_1
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            plantilla plantilla = new plantilla();
            plantilla.Show();
            this.Hide();
        }
    }

}
