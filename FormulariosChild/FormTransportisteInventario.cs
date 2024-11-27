using System.Data;
using BLL;
using System;
using System.Windows.Forms;

namespace MaterialDesign.FormulariosChild
{
    public partial class FormTransportisteInventario : Form
    {
        private BLLService bll = new BLLService();
        public FormTransportisteInventario()
        {
            InitializeComponent();
            LoadData();
            dgTransportistas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void LoadData()
        {
            try
            {
                DataTable data = bll.GetTransportistas();
                dgTransportistas.DataSource = data;
                dgTransportistas.Refresh();
                dgTransportistas.CellFormatting += DgTransportistas_CellFormatting;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }

        private void DgTransportistas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgTransportistas.Columns[e.ColumnIndex].Name == "DISPONIBILIDAD" && e.Value != null)
            {
                // Convertir 1 -> "Disponible", 0 -> "No disponible"
                e.Value = e.Value.ToString() == "1" ? "Disponible" : "No disponible";
                e.FormattingApplied = true;
            }
        }
    }
}
