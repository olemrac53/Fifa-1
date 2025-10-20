using Fifa_1;

namespace animacion_fifa
{
    public partial class Animacion : Form
    {
        public Animacion()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
        }

        private void tmrInicio_Tick(object sender, EventArgs e)
        {
            tmrInicio.Stop();
            pictureBox1.Visible = false;
            pictureBox2.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //SoundPlayer Sonido = new SoundPlayer();
            //Sonido.SoundLocation = "C:\\Users\\Lab20-PC02\\Documents\\GitHub\\Fifa-1\\Fifa-1\\Resources\\Musica.wav";
            //Sonido.Play();
            Inicio_sesion Inicio_sesion = new Inicio_sesion();
            Inicio_sesion.Show();
            this.Hide();
        }
    }


    

}
