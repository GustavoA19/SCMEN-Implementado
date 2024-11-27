using CefSharp;
using CefSharp.WinForms;
using System;
using System.Windows.Forms;

namespace MaterialDesign.FormulariosChild
{
    public partial class FormSheets : Form
    {
        public FormSheets()
        {
            InitializeComponent();
            InitializeChromium();
        }

        private void FormWhatsapp_Load(object sender, EventArgs e)
        {
 
        }
 
        private void InitializeChromium()
        {
            // Aquí ya no necesitas inicializar Cef, solo cargar la URL.
            WebBrowser.Dock = DockStyle.Fill;
            WebBrowser.Load("https://docs.google.com/spreadsheets/d/12k-ILq0B3eDphZRShCuvVXdN9LlXWW_CnDOLsBk_6M4/edit?resourcekey=&gid=1417698977#gid=1417698977"); // Cargar la URL de WhatsApp Web directamente.
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Apaga CefSharp al cerrar el formulario
            base.OnFormClosing(e);
        }
    }

}

