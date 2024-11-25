using MaterialDesign.FormPrincipal;
using System;
using System.Windows.Forms;

namespace MaterialDesign
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Crear y ocultar el FormPrincipalMaterialDesign
            FormPrincipalMaterialDesign mainForm = new FormPrincipalMaterialDesign();
            mainForm.Show();
            mainForm.Hide();

            // Crear y mostrar el FormLogin
            Application.Run(new FormLogin());

        }
    }
}
