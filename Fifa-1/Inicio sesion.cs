
namespace Fifa_1
{
    public partial class Inicio_sesion : Form
    {
        public Inicio_sesion()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            Menu Menu = new Menu();
            Menu.Show();
            this.Hide();
        }

      
    }
}
