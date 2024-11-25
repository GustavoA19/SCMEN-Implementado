using MaterialSkin;
using System;
using System.Windows.Forms;
using BLL;
using ENTITY;
using System.Data;

namespace MaterialDesign.FormulariosChild
{
    public partial class FormClientesInventario : Form
    {
        private clienteBLL clienteBLL = new clienteBLL();
        private cliente c = new cliente();
        private BLLService bll = new BLLService();
        private readonly MaterialSkinManager materialSkinManager;
        public FormClientesInventario()
        {
            InitializeComponent();
            dgClientes.CellClick += new DataGridViewCellEventHandler(dgClientes_CellClick);
            LoadData();
            dgClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
        private void dgClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgClientes.Rows[e.RowIndex];
                txtCedulaCliente.Text = row.Cells[0].Value.ToString();
                txtNombreCliente.Text = row.Cells[1].Value.ToString();
                txtApellidoCliente.Text = row.Cells[2].Value.ToString();
                txtEmailCliente.Text = row.Cells[3].Value.ToString();
                txtTelefonoCliente.Text = row.Cells[4].Value.ToString();
                txtDireccionCliente.Text = row.Cells[5].Value.ToString();
            }
        }

        private void LoadData()
        {
            try
            {
                DataTable data = bll.GetData();
                dgClientes.DataSource = data;
                dgClientes.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }


        }
        private void LimpiarFormCliente()
        {
            txtCedulaCliente.Text = "";
            txtNombreCliente.Text = "";
            txtApellidoCliente.Text = "";
            txtEmailCliente.Text = "";
            txtTelefonoCliente.Text = "";
            txtDireccionCliente.Text = "";

        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void FormClientes_Load(object sender, EventArgs e)
        {

        }

        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            cliente c = new cliente();
            {
                c.ID = int.Parse(txtCedulaCliente.Text);
                c.Nombre = txtNombreCliente.Text;
                c.Apellido = txtApellidoCliente.Text;
                c.Email = txtEmailCliente.Text;
                c.Telefono = txtTelefonoCliente.Text;
                c.Direccion = txtDireccionCliente.Text;
            }
            clienteBLL.setCliente(c);
            LoadData();
            LimpiarFormCliente();
        }

        private void btnEliminarClientes_Click(object sender, EventArgs e)
        {
            int clienteId;
            if (int.TryParse(txtCedulaCliente.Text, out clienteId))
            {
                clienteBLL c = new clienteBLL();
                bool eliminado = c.deleteCliente(clienteId);
                if (eliminado)
                {
                    MessageBox.Show("Cliente eliminado");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Error al eliminar cliente");
                }
            }
            else
            {
                MessageBox.Show("Ingrese un ID valido");
            }

            LimpiarFormCliente();
        }

        private void btnModificarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                cliente c = new cliente();
                {
                    c.ID = int.Parse(txtCedulaCliente.Text);
                    c.Nombre = txtNombreCliente.Text;
                    c.Apellido = txtApellidoCliente.Text;
                    c.Email = txtEmailCliente.Text;
                    c.Telefono = txtTelefonoCliente.Text;
                    c.Direccion = txtDireccionCliente.Text;
                }

                clienteBLL ClienteBLL = new clienteBLL();

                bool actualizado = ClienteBLL.updateCliente(c);
                if (actualizado)
                {
                    MessageBox.Show("Cliente actualizado");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Error al actualizar cliente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el cliente: {ex.Message}");
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
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
                        // Omite las dos primeras columnas y asigna las restantes a cada TextBox
                        txtCedulaCliente.Text = columnas[1];     // Cédula
                        txtNombreCliente.Text = columnas[2];      // Nombre
                        txtApellidoCliente.Text = columnas[3];    // Apellido
                        txtEmailCliente.Text = columnas[4];       // Email
                        txtTelefonoCliente.Text = columnas[5];    // Teléfono
                        txtDireccionCliente.Text = columnas[6];  // Dirección
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
    }
}
