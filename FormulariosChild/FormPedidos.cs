using BLL;
using ENTITY;
using MaterialSkin;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Drawing.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaterialDesign.FormulariosChild
{
    public partial class FormPedidos : Form
    {   
        private BLLService bll = new BLLService();
        private pedidoBLL pe = new pedidoBLL();
        private transportistaBLL tr = new transportistaBLL();
        private paquete pa = new paquete();
        private envioBLL enB = new envioBLL();
        private envio en = new envio();
        private readonly MaterialSkinManager materialSkinManager;
        public FormPedidos()
        {
            InitializeComponent();
            DgPedidos.CellClick += new DataGridViewCellEventHandler(dgPedidos_CellClick);
            cBoxTransportista.SelectedIndexChanged += new EventHandler(cBoxTransportista_SelectedIndexChanged);
            LoadData();
            LoadComboBox();
        }

        private void dgPedidos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DgPedidos.Rows[e.RowIndex];
                txtIdPedido.Text = row.Cells[0].Value.ToString();
                txtNombrePedido.Text = row.Cells[1].Value.ToString();
                txtApellidoPedido.Text = row.Cells[2].Value.ToString();
                txtEmailPedido.Text = row.Cells[3].Value.ToString();
                txtTelefonoPedido.Text = row.Cells[4].Value.ToString();
                txtDireccionPedido.Text = row.Cells[5].Value.ToString();
                txtEntregaPedidos.Text = row.Cells[6].Value.ToString();
                txtDesscripcionPedido.Text = row.Cells[7].Value.ToString();
            }
        }

        private void LoadData()
        {
            try
            {
                DataTable data = bll.GetPedidos();
                DgPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DgPedidos.DataSource = data;
                DgPedidos.Refresh();
                DgPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                DgPedidos.ReadOnly = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }

        private void LoadComboBox()
        {
            try
            {
                DataTable data = bll.GetTransportistas(); 
                cBoxTransportista.DataSource = data;
                cBoxTransportista.DisplayMember = "NOMBRE"; 
                cBoxTransportista.ValueMember = "ID"; 
                cBoxTransportista.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el transportistas: {ex.Message}");
            }
        }

        private void cBoxTransportista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBoxTransportista.SelectedIndex != -1)
            {
                DataRowView selectedRow = (DataRowView)cBoxTransportista.SelectedItem;
                if (selectedRow != null)
                {
                    txtNombreTransportista.Text = selectedRow["NOMBRE"].ToString();
                }
            }
            else
            {
                txtNombreTransportista.Clear();
            }
        }

        private void LimpiarFormPedidos()
        {
            txtIdPedido.Text = "";
            txtNombrePedido.Text = "";
            txtApellidoPedido.Text = "";
            txtEmailPedido.Text = "";
            txtTelefonoPedido.Text = "";
            txtDireccionPedido.Text = "";
            txtEntregaPedidos.Text = "";
            txtDesscripcionPedido.Text = "";
        }

        private void materialLabel8_Click(object sender, EventArgs e)
        {

        }

        private void materialMultiLineTextBox21_Click(object sender, EventArgs e)
        {

        }

        private void FormPedidos_Load(object sender, EventArgs e)
        {

        }

        private void btnPegarPedido_Click(object sender, EventArgs e)
        {
            // Obtiene el texto del portapapeles
            string datos = Clipboard.GetText();

            if (!string.IsNullOrEmpty(datos))
            {
                // Divide el texto en columnas usando el tabulador como separador
                string[] columnas = datos.Split('\t');

                // Asegura que el formato sea correcto
                if (columnas.Length >= 8)
                {     
                    txtDireccionPedido.Text = columnas[6];  // Dirección
                    txtEntregaPedidos.Text = columnas[7];  // Entrega
                    txtDesscripcionPedido.Text = columnas[8];  // Descripción

                }
                else
                {
                    MessageBox.Show("Formato de datos incorrecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No hay datos en el portapapeles.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAsignarTransportista_Click(object sender, EventArgs e)
        {
            transportista t = new transportista();
            {
                t.ID = Convert.ToInt32(cBoxTransportista.SelectedValue);
            }
            en.ID = Convert.ToInt32(txtIdPedido.Text);
            enB.asignarTransportista(t, en);
            MessageBox.Show("Transportista asignado correctamente.");
        }
    }
}
