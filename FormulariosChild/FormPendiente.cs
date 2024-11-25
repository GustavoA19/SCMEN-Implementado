using BLL;
using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using ENTITY;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaterialDesign.FormulariosChild
{
    public partial class FormPendiente : Form
    {
        private BLLService bll = new BLLService();
        private factura f = new factura();
        private facturaBLL fb = new facturaBLL();
        public FormPendiente()
        {
            InitializeComponent();
            InitializeChromium();
            dgPedidos.CellClick += new DataGridViewCellEventHandler(dgPedidos_CellClick);
            LoadData();
        }
        private void InitializeChromium()
        {
            // Aquí ya no necesitas inicializar Cef, solo cargar la URL.

            WebBrowser1.Load("https://www.google.com/maps/@10.4497152,-73.2630616,14z?entry=ttu&g_ep=EgoyMDI0MTAyNy4wIKXMDSoASAFQAw%3D%3D"); // Cargar la URL de WhatsApp Web directamente.
        }
        private void dgPedidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgPedidos.Rows[e.RowIndex];
                txtIdPendiente.Text = row.Cells[1].Value.ToString();
                txtNombrePendiente.Text = row.Cells[2].Value.ToString();
                txtDireccionPendiente.Text = row.Cells[6].Value.ToString();
                txtEntregaPendiente.Text = row.Cells[7].Value.ToString();
            }
        }
        private void LoadData()
        {
            try
            {
                DataTable data = bll.GetPedidos();
                dgPedidos.DataSource = data;
                dgPedidos.Refresh();
                foreach (DataGridViewColumn column in dgPedidos.Columns)
                {
                    if (column.HeaderText != "ID_Envio" && column.HeaderText != "NOMBRE_CLIENTE")
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                }
                dgPedidos.Columns["ID_Envio"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgPedidos.Columns["NOMBRE_CLIENTE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; 
                dgPedidos.ReadOnly = true;
                dgPedidos.ScrollBars = ScrollBars.None;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Apaga CefSharp al cerrar el formulario
            base.OnFormClosing(e);
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, System.EventArgs e)
        {

        }

        private async void materialButton1_Click(object sender, System.EventArgs e)
        {
            string direccionOrigen = $"{txtDireccionPendiente.Text} Valledupar Cesar";
            string direccionDestino = $"{txtEntregaPendiente.Text} Valledupar Cesar";


            // Crea la URL de Google Maps con las direcciones especificadas
            string urlMapa = $"https://www.google.com/maps/dir/?api=1&origin={Uri.EscapeDataString(direccionOrigen)}&destination={Uri.EscapeDataString(direccionDestino)}";

            // Navega a la URL en el navegador Chromium
            WebBrowser1.Load(urlMapa);
        }

        private void FormPendiente_Load(object sender, EventArgs e)
        {
            dgPedidos.DataSource = bll.GetPedidos();
        }

        private void btnEnviarFactura_Click(object sender, EventArgs e)
        {
            if (dgPedidos.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgPedidos.SelectedRows[0];
                    f.IdEnvio = int.Parse(selectedRow.Cells["ID_Envio"].Value.ToString());
                    f.IdCliente = int.Parse(txtIdPendiente.Text);

                try
                {
                    // 2. Llamar al BLL para procesar el insert
                    bool success =  fb.InsertarFactura(f);

                    // 3. Mensaje de éxito o error
                    if (success)
                    {
                        MessageBox.Show("Factura enviada correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error al enviar la factura.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al procesar la factura: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un pedido de la lista.");
            }
        }
    }
}
