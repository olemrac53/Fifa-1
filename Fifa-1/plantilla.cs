namespace Fifa_1
{
    public partial class plantilla : Form
    {
        public plantilla()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Menu Menu = new Menu();
            Menu.Show();
            this.Hide();

        }
    }
}
