
using CefSharp.WinForms;
using CefSharp;
using FontAwesome.Sharp;
using MaterialDesign.FormulariosChild;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using ENTITY;
using MaterialDesign.FormPrincipal;
using System.Linq;




namespace MaterialDesign
{
    public partial class FormPrincipalMaterialDesign : Form
    {
        //Fields
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        private FormUsuarios FormUsuarios;
        private FormClientes FormClientes;
        private FormTransportista FormTransportista;
        private FormPedidos FormPedidos;
        private FormSheets FormWhataspp;
        private FormPendiente FormPendiente;
        private Dictionary<string, Form> activeChildForms;
        public void DesactivarBotonRegistrarUsuario()
        {
            btnRegistrarUsuario.Enabled = false;
        }

        public FormPrincipalMaterialDesign()
        {
            InitializeComponent();
            customizeDesings();
            
            Cef.Initialize(new CefSettings()); // Inicializa CefSharp aquí una vez
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            PanelMenu.Controls.Add(leftBorderBtn);
            //Form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;


        }

        //Structs
        

        private void customizeDesings()
        {
            PanelSubMenuregistro.Visible = false;
            PanelSubInventario.Visible = false;

        }
        private void hideSubMenu()
        {
            if (PanelSubMenuregistro.Visible == true)
                PanelSubMenuregistro.Visible = false;
            if (PanelSubInventario.Visible == true)
                PanelSubInventario.Visible = false;

        }
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(15, 128, 225);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }

        //Methods
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(58, 76, 100);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //Left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                //Current Child Form Icon
                iconFormularioActual.IconChar = currentBtn.IconChar;

            }
        }
        private void ActivateSubButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Button
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //Left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                //Current Child Form Icon
                iconFormularioActual.IconChar = currentBtn.IconChar;

            }
        }
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(49, 64, 84);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }
        private void OpenChildForm(Form childForm)
        {
            // Si el formulario actual es FormClientes, guarda los datos antes de cambiar
            if (currentChildForm is FormClientes formClientes)
            {
                formClientes.GuardarDatos();
            }
            //open only form
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            PanelContenedor.Controls.Add(childForm);
            PanelContenedor.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            Lb_Titulo.Text = childForm.Text;
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconFormularioActual.IconChar = IconChar.Home;
            Lb_Titulo.Text = "Home";
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            showSubMenu(PanelSubMenuregistro);
            //OpenChildForm(new Form1MaterialDesign());
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            //OpenChildForm(new Two_Photon_Absortioncs());
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            //OpenChildForm(new Hiperpolarizabilidad());
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            //OpenChildForm(new PropiedadesTermodinamicas());

        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            // OpenChildForm(new FormDashboard());

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            Reset();
            hideSubMenu();
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Btn_Cerrar_Click(object sender, EventArgs e)
        {
            //this.Close();
            //Application.Exit();

            if (MessageBox.Show("DESEA CERRAR SESION?", "Warning",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                //this.Close();
                Application.Exit();

        }


        private void btn_Maximizar_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void Btn_Minimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void FechayHora_Tick(object sender, EventArgs e)
        {

        }

        private void Btn_Whatsapp_Click(object sender, EventArgs e)
        {
            //ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new FormSheets());
        }

        private void Btn_Correo_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void iconFormularioActual_Click(object sender, EventArgs e)
        {

        }

        private void FormPrincipalMaterialDesign_Load(object sender, EventArgs e)
        {

            FormUsuarios = new FormUsuarios();
            FormClientes = new FormClientes();
            FormTransportista = new FormTransportista();
            FormPedidos = new FormPedidos();
            FormWhataspp = new FormSheets();
            FormPendiente = new FormPendiente();


        }

        private void iconButton2_Click_1(object sender, EventArgs e)
        {
            ActivateSubButton(sender, RGBColors.color1);
            OpenChildForm(new FormUsuarios());
            hideSubMenu();
        }

        private void iconButton3_Click_1(object sender, EventArgs e)
        {
            ActivateSubButton(sender, RGBColors.color1);
            OpenChildForm(new FormTransportista());
            hideSubMenu();
        }

        private void iconButton4_Click_1(object sender, EventArgs e)
        {
            ActivateSubButton(sender, RGBColors.color1);
            OpenChildForm(new FormClientes());
            hideSubMenu();
        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new FormPedidos());
            hideSubMenu();
        }

        private void btnPendiente_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new FormPendiente());
            hideSubMenu();
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            showSubMenu(PanelSubInventario);
            // OpenChildForm(new formPrueba());

        }

        private void btnInventarioEnvios_Click(object sender, EventArgs e)
        {
            ActivateSubButton(sender, RGBColors.color1);
            //OpenChildForm(new FormUsuarios());

            hideSubMenu();
        }

        private void btnInventarioTransportistas_Click(object sender, EventArgs e)
        {
            ActivateSubButton(sender, RGBColors.color1);
            //CODE
            OpenChildForm(new FormTransportisteInventario());
            hideSubMenu();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void iconButton2_Click_2(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new FormUsuarios());
        }

        private void iconButton1_Click_2(object sender, EventArgs e)
        {
            OpenChildForm(new FormClientesInventario());
        }
        //Events
        //Reset

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Check if activeChildForms is null
            if (activeChildForms != null)
            {
                // Cerrar y liberar todos los formularios
                foreach (var form in activeChildForms.Values)
                {
                    form.Close();
                    form.Dispose();
                }

                activeChildForms.Clear();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FormPrincipalMaterialDesign_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Mostrar de nuevo el FormLogin
            FormLogin loginForm = Application.OpenForms.OfType<FormLogin>().FirstOrDefault();
            if (loginForm != null)
            {
                loginForm.Show();
            }
        }

    }
}