using System;
using System.Windows.Forms;
using Fifa.Core; 

namespace animacion_fifa
{
    internal static class Program
    {
        public static Usuario? UsuarioActual { get; set; }

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Animacion());
        }
    }
}