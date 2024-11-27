using System;
using System.Data;
using System.Windows.Forms;
using BLL;
using ENTITY;
using MaterialSkin;

namespace MaterialDesign.FormulariosChild
{
    public partial class FormTransportista : Form
    {
        private transportistaBLL transportistaBLL = new transportistaBLL();
        private transportista t = new transportista();
        private BLLService bll = new BLLService();
        private readonly MaterialSkinManager materialSkinManager;

        public FormTransportista()
        {
            InitializeComponent();
            Dgtransportistas.CellClick += new DataGridViewCellEventHandler(Dgtransportistas_CellClick);
            //
            LoadData();
        }

        private void Dgtransportistas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = Dgtransportistas.Rows[e.RowIndex];
               txtIdTransportista.Text = row.Cells[0].Value.ToString();
                txtNombreTransportista.Text = row.Cells[1].Value.ToString();
                txtApellidoTransportista.Text = row.Cells[2].Value.ToString();
                txtTelefonoTransportista.Text = row.Cells[4].Value.ToString();
                txtEmailTransportista.Text = row.Cells[3].Value.ToString();
            }
        }

        private void LoadData()
        {
            try
            {
                DataTable data = bll.GetTransportista();
                Dgtransportistas.DataSource = data;
                Dgtransportistas.Refresh();
                Dgtransportistas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                Dgtransportistas.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }

        private void LimpiarFormTransportista()
        {
            txtIdTransportista.Text = "";
            txtNombreTransportista.Text = "";
            txtApellidoTransportista.Text = "";
            txtTelefonoTransportista.Text = "";
            txtEmailTransportista.Text = "";
        }

        private void btnRegistrarTransportista_Click(object sender, EventArgs e)
        {
            transportista t = new transportista();
            {
                t.ID = int.Parse(txtIdTransportista.Text);
                t.Nombre = txtNombreTransportista.Text;
                t.Apellido = txtApellidoTransportista.Text;
                t.Email = txtEmailTransportista.Text;
                t.Telefono = txtTelefonoTransportista.Text;
                t.FechaRegistro = DateTime.Now;
                t.Disponibilidad = true;
                t.EnviosDiario = 0;
                t.Sueldo = 0;
            }
            transportistaBLL.setTransportista(t);
            LoadData();
            LimpiarFormTransportista();
        }

        private void btnEliminarTransportista_Click(object sender, EventArgs e)
        {
                transportistaBLL t = new transportistaBLL();
                transportista transportista = new transportista();
                transportista.ID = int.Parse(txtIdTransportista.Text);
                bool eliminado = t.deleteTransportista(transportista);
                if (eliminado)
                {
                    MessageBox.Show("Transportista eliminado");
                    LoadData();
                    LimpiarFormTransportista();
                }
                else
                {
                    MessageBox.Show("Error al eliminar transportista");
                }
            LimpiarFormTransportista();
        }

        private void btnModificarTransportista_Click(object sender, EventArgs e)
        {
            try
            {
                    t.ID = int.Parse(txtIdTransportista.Text);
                    t.Nombre = txtNombreTransportista.Text;
                    t.Apellido = txtApellidoTransportista.Text;
                    t.Telefono = txtTelefonoTransportista.Text;
                    t.Email = txtEmailTransportista.Text;

                transportistaBLL transportistaBLL = new transportistaBLL();

                bool actualizado = transportistaBLL.updateTransportista(t);
                if (actualizado)
                {
                    MessageBox.Show("Transportista actualizado");
                    LoadData();
                    LimpiarFormTransportista();
                }
                else
                {
                    MessageBox.Show("Error al actualizar transportista");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar transportista: {ex.Message}");
            }
        }
    }
}
