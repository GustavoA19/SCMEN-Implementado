using System.Data;
using System;
using System.Windows.Forms;
using BLL;
using ENTITY;

namespace MaterialDesign.FormulariosChild
{
    public partial class FormUsuarios : Form
    {
        private BLLService bll = new BLLService();
        private usuarioBLL usuarioBLL = new usuarioBLL();
        private usuario u = new usuario();
        public FormUsuarios()
        {
            InitializeComponent();
            dgUsuario.CellClick += new DataGridViewCellEventHandler(dgUsuario_CellClick);
            dgUsuario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LoadUsuario();
        }

        private void FormUsuarios_Load(object sender, System.EventArgs e)
        {

        }

        private void dgUsuario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgUsuario.Rows[e.RowIndex];
                txtCedulaUsuario.Text = row.Cells[0].Value.ToString();
                txtNombreUsuario.Text = row.Cells[1].Value.ToString();
                txtApellidoUsuario.Text = row.Cells[2].Value.ToString();
                txtEmailUsuario.Text = row.Cells[3].Value.ToString();
                txtTelefonoUsuario.Text = row.Cells[4].Value.ToString();
                txtUserUsuario.Text = row.Cells[5].Value.ToString();
                txtContraseñaUsuario.Text = row.Cells[6].Value.ToString();
                string rolUsuario = dgUsuario.CurrentRow.Cells[7].Value.ToString();
                cbRolUsuario.Text = rolUsuario;
                cbRolUsuario.Refresh();
            }
        }

        private void LimpiarFormUsuario() {
            txtCedulaUsuario.Text = "";
            txtNombreUsuario.Text = "";
            txtApellidoUsuario.Text = "";
            txtEmailUsuario.Text = "";
            txtTelefonoUsuario.Text = "";
            txtUserUsuario.Text = "";
            txtContraseñaUsuario.Text = "";
            cbRolUsuario.Text = "";

        }

        private void LoadUsuario()
        {
            try
            {
                DataTable data = bll.GetUsuarios();
                dgUsuario.DataSource = data;
                dgUsuario.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void materialLabel10_Click(object sender, EventArgs e)
        {

        }

        private void cbRolUsuario_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void btnRegistrarUsuario_Click(object sender, EventArgs e)
        {
            usuario u = new usuario();
            {
                u.ID = int.Parse(txtCedulaUsuario.Text);
                u.Nombre = txtNombreUsuario.Text;
                u.Apellido = txtApellidoUsuario.Text;
                u.Email = txtEmailUsuario.Text;
                u.Telefono = txtTelefonoUsuario.Text;
                u.NombreUsuario = txtNombreUsuario.Text;
                u.Contraseña =  txtContraseñaUsuario.Text;
                u.Rol = cbRolUsuario.Text;
            }
            usuarioBLL.setUsuario(u);
            LoadUsuario();
            LimpiarFormUsuario();
        }

        private void btnModificarUsuarios_Click(object sender, EventArgs e)
        {
            try
            {
                usuario u = new usuario();
                {
                    u.ID = int.Parse(txtCedulaUsuario.Text);
                    u.Nombre = txtNombreUsuario.Text;
                    u.Apellido = txtApellidoUsuario.Text;
                    u.Email = txtEmailUsuario.Text;
                    u.Telefono = txtTelefonoUsuario.Text;
                    u.NombreUsuario = txtNombreUsuario.Text;
                    u.Contraseña = txtContraseñaUsuario.Text;
                    u.Rol = cbRolUsuario.Text;
                }

                clienteBLL ClienteBLL = new clienteBLL();

                bool actualizado = usuarioBLL.updateUsuario(u);
                if (actualizado)
                {
                    MessageBox.Show("Cliente actualizado");
                    LoadUsuario();
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

        private void btnEliminarUsuario_Click(object sender, EventArgs e)
        {

            int usuarioId;
            if (int.TryParse(txtCedulaUsuario.Text, out usuarioId))
            {
                usuarioBLL u = new usuarioBLL();
                bool eliminado = u.deleteUsuario(usuarioId);
                if (eliminado)
                {
                    MessageBox.Show("Cliente eliminado");
                    LoadUsuario();
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

            LimpiarFormUsuario();

        }
    }
}
