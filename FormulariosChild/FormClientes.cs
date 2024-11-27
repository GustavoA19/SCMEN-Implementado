using MaterialSkin;
using System;
using System.Windows.Forms;
using BLL;
using ENTITY;
using System.Data;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace MaterialDesign.FormulariosChild
{
    public partial class FormClientes : Form
    {
        private clienteBLL clienteBLL = new clienteBLL();
        private paqueteBLL paqueteBLL = new paqueteBLL();
        //private envioBLL envioBLL = new envioBLL();
        private cliente c = new cliente();
        private paquete p = new paquete();
        private envio en = new envio();
        private BLLService bll = new BLLService();
        private readonly MaterialSkinManager materialSkinManager;

        private static readonly TelegramBotClient Bot = new TelegramBotClient("7935109106:AAHb8DIH5YZYHbS6_UPfC7tYRvXUVYJ5iF0");
        private static Dictionary<long, string> conversationState = new Dictionary<long, string>();
        private static DateTime lastActivatedTime;
        private static bool isServiceAvailable = true;
        private static bool botIniciado = false;

        public FormClientes()
        {
            InitializeComponent();
            InitializeBot();
            dgClientes.CellClick += new DataGridViewCellEventHandler(dgClientes_CellClick);
            dgClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //LoadData();

        }
        // Inside the GuardarDatos method
        public void GuardarDatos()
        {
            try
            {
                var listaDatos = new List<Dictionary<string, object>>();
                foreach (DataGridViewRow fila in dgClientes.Rows)
                {
                    if (!fila.IsNewRow)
                    {
                        var diccionarioFila = new Dictionary<string, object>();
                        foreach (DataGridViewCell celda in fila.Cells)
                        {
                            diccionarioFila[celda.OwningColumn.Name] = celda.Value ?? "";
                        }
                        listaDatos.Add(diccionarioFila);
                    }
                }

                string json = JsonConvert.SerializeObject(listaDatos, Formatting.Indented);
                string rutaJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datos.json");

                System.IO.File.WriteAllText(rutaJson, json);
                Console.WriteLine($"Archivo guardado correctamente en: {rutaJson}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar los datos: {ex.Message}");
            }
        }

        private void FormClientes_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        private void InitializeDataGridView()
        {
            if (dgClientes.Columns.Count == 0)
            {
                dgClientes.Columns.Add("CHAT ID", "CHAT ID");
                dgClientes.Columns.Add("IDENTIFICACION", "IDENTIFICACION");
                dgClientes.Columns.Add("NOMBRE", "NOMBRE");
                dgClientes.Columns.Add("APELLIDOS", "APELLIDOS");
                dgClientes.Columns.Add("EMAIL", "CORREO");
                dgClientes.Columns.Add("TELEFONO", "TELEFONO");
                dgClientes.Columns.Add("DIRECCION DEL PAQUETE", "DIRECCION DEL PAQUETE");
                dgClientes.Columns.Add("DIRECCION DE ENTREGA", "DIRECCION DE ENTREGA");
                dgClientes.Columns.Add("DESCRIPCION", "DESCRIPCION");
            }
        }

        private void dgClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgClientes.Rows[e.RowIndex];
                if (row.Cells[1].Value != null)
                {
                    txtCedulaCliente.Text = row.Cells[1].Value.ToString();
                }
                if (row.Cells[2].Value != null)
                {
                    txtNombreCliente.Text = row.Cells[2].Value.ToString();
                }
                if (row.Cells[3].Value != null)
                {
                    txtApellidoCliente.Text = row.Cells[3].Value.ToString();
                }
                if (row.Cells[4].Value != null)
                {
                    txtEmailCliente.Text = row.Cells[4].Value.ToString();
                }
                if (row.Cells[5].Value != null)
                {
                    txtTelefonoCliente.Text = row.Cells[5].Value.ToString();
                }
                if (row.Cells[6].Value != null)
                {
                    txtDireccionCliente.Text = row.Cells[6].Value.ToString();
                }
                if (row.Cells[7].Value != null)
                {
                    txtEntregaClientes.Text = row.Cells[7].Value.ToString();
                }
                if (row.Cells[8].Value != null)
                {
                    txtDesscripcionCliente.Text = row.Cells[8].Value.ToString();
                }
            }
        }
        private async void InitializeBot()
        {
            if (!botIniciado)
            {
                var cts = new CancellationTokenSource();
                var receiverOptions = new ReceiverOptions { AllowedUpdates = new[] { UpdateType.Message } };
                lastActivatedTime = DateTime.UtcNow;

                Bot.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cts.Token);
                var me = await Bot.GetMeAsync();
                this.Text = $"Bot activo: @{me.Username}";
                botIniciado = true;
            }
        }

        private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message.Type == MessageType.Text)
            {
                var message = update.Message;
                var chatId = message.Chat.Id;
                string userResponse = message.Text;

                // Verifica el estado de la conversación y realiza las acciones correspondientes
                if (conversationState.ContainsKey(chatId))
                {
                    if (conversationState[chatId] == "waiting_for_nit")
                    {
                        // Verifica si el NIT es válido
                        if (IsValidNit(userResponse))
                        {
                            UpdateGridRow(chatId, 1, userResponse); // NIT
                            conversationState[chatId] = "waiting_for_firstname";
                            await Bot.SendTextMessageAsync(chatId, "¡Gracias! Ahora, por favor, ingresa tu nombre.");
                        }
                        else
                        {
                            await Bot.SendTextMessageAsync(chatId, "El ID no es válido. Por favor, intenta de nuevo.");
                        }
                    }
                    else if (conversationState[chatId] == "waiting_for_firstname")
                    {
                        // Guarda el nombre del usuario
                        UpdateGridRow(chatId, 2, userResponse); // Nombre
                        conversationState[chatId] = "waiting_for_lastname";
                        await Bot.SendTextMessageAsync(chatId, "Gracias, ahora ingresa tu apellido.");
                    }
                    else if (conversationState[chatId] == "waiting_for_lastname")
                    {
                        // Guarda el apellido del usuario
                        UpdateGridRow(chatId, 3, userResponse); // Apellido
                        conversationState[chatId] = "waiting_for_email";
                        await Bot.SendTextMessageAsync(chatId, "Por favor, ingresa tu correo electrónico.");
                    }
                    else if (conversationState[chatId] == "waiting_for_email")
                    {
                        // Guarda el correo electrónico
                        UpdateGridRow(chatId, 4, userResponse); // Correo
                        conversationState[chatId] = "waiting_for_phone";
                        await Bot.SendTextMessageAsync(chatId, "¿Cuál es tu número de teléfono?");
                    }
                    else if (conversationState[chatId] == "waiting_for_phone")
                    {
                        // Guarda el teléfono
                        UpdateGridRow(chatId, 5, userResponse); // Teléfono
                        conversationState[chatId] = "waiting_for_address";
                        await Bot.SendTextMessageAsync(chatId, "Por favor, ingresa tu direccion.");
                    }
                    else if (conversationState[chatId] == "waiting_for_address")
                    {
                        // Guarda la dirección del paquete
                        UpdateGridRow(chatId, 6, userResponse); // Dirección del paquete
                        conversationState[chatId] = "waiting_for_delivery_address";
                        await Bot.SendTextMessageAsync(chatId, "Ahora, ingresa la dirección de entrega del paquete.");
                    }
                    else if (conversationState[chatId] == "waiting_for_delivery_address")
                    {
                        // Guarda la dirección de entrega
                        UpdateGridRow(chatId, 7, userResponse); // Dirección de entrega
                        conversationState[chatId] = "waiting_for_package_description";
                        await Bot.SendTextMessageAsync(chatId, "Por favor, ingresa una breve descripción del paquete.");
                    }
                    else if (conversationState[chatId] == "waiting_for_package_description")
                    {
                        // Guarda la descripción del paquete
                        UpdateGridRow(chatId, 8, userResponse); // Descripción del paquete
                        conversationState.Remove(chatId);
                        await Bot.SendTextMessageAsync(chatId, 
                            "¡Gracias! Hemos recibido tus datos. Se enviara la factura al correo electronico registrado" +
                            " y Un Transportista llegara Pronto a tu Domicilo");
                    }
                }
                else
                {
                    // Si no existe una conversación, inicia desde el principio
                    conversationState[chatId] = "waiting_for_nit";
                    await Bot.SendTextMessageAsync(chatId, "¡Hola! Bienvenido a SCMEN, Para Continuar Porfavor Digite su Id");
                }
            }
        }

        private static bool IsValidName(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z\s]+$") && !name.Contains("@");
        }

        private static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

        }
        private static bool IsValidNit(string nit)
        {
            return Regex.IsMatch(nit, @"^\d{9,10}$"); // Validar que el NIT sea un número de 9 o 10 dígitos
        }


        private static void UpdateGridRow(long chatId, int cellIndex, string value)
        {
            if (Application.OpenForms["FormClientes"] is FormClientes form)
            {
                if (form.InvokeRequired)
                {
                    form.Invoke(new Action<long, int, string>(UpdateGridRow), chatId, cellIndex, value);
                    return;
                }

                // Verifica si las columnas ya están definidas
                if (form.dgClientes.Columns.Count == 0)
                {
                    form.InitializeDataGridView();  // Define las columnas si no están definidas
                }

                // Verifica si el chatId ya está presente en alguna fila
                bool found = false;
                foreach (DataGridViewRow row in form.dgClientes.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == chatId.ToString())
                    {
                        // Actualiza la celda específica si el chatId ya existe
                        row.Cells[cellIndex].Value = value;
                        found = true;
                        break;
                    }
                }

                // Si no se encuentra, agrega una nueva fila
                if (!found)
                {
                    int newRowIndex = form.dgClientes.Rows.Add(chatId.ToString(), "", "", "", "", "", "", "");
                    form.dgClientes.Rows[newRowIndex].Cells[cellIndex].Value = value;
                }
            }
        }


        private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            MessageBox.Show(exception.Message);
            return Task.CompletedTask;
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
            txtEntregaClientes.Text = "";
            txtDesscripcionCliente.Text = "";

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void FormClientes_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
        public void CargarDatos()
        {
            try
            {
                string rutaJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datos.json");

                if (System.IO.File.Exists(rutaJson))
                {
                    // Lee el archivo JSON
                    string json = System.IO.File.ReadAllText(rutaJson);

                    // Deserializa el JSON en una lista de diccionarios
                    var listaDatos = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);

                    // Limpia el DataGridView antes de agregar los datos
                    dgClientes.Rows.Clear();
                    dgClientes.Columns.Clear();

                    // Asegúrate de que haya datos para cargar
                    if (listaDatos != null && listaDatos.Count > 0)
                    {
                        // Agrega las columnas al DataGridView
                        foreach (var columna in listaDatos[0].Keys)
                        {
                            dgClientes.Columns.Add(columna, columna);
                        }

                        // Agrega las filas al DataGridView
                        foreach (var fila in listaDatos)
                        {
                            dgClientes.Rows.Add(fila.Values.ToArray());
                        }
                    }
                }
                else
                {
                    Console.WriteLine("El archivo de datos no existe.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar los datos: " + ex.Message);
            }
        }

        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                cliente c = new cliente();
                paquete p = new paquete();
                envio en = new envio();
                p.Descripcion = txtDesscripcionCliente.Text;
                p.Fragil = true;
                c.ID = int.Parse(txtCedulaCliente.Text);
                c.Nombre = txtNombreCliente.Text;
                c.Apellido = txtApellidoCliente.Text;
                c.Email = txtEmailCliente.Text;
                c.Telefono = txtTelefonoCliente.Text;
                c.Direccion = txtDireccionCliente.Text;
                c.Entrega = txtEntregaClientes.Text;
                paqueteBLL.setPaquete(p);
                if (string.IsNullOrEmpty(txtDesscripcionCliente.Text))
                {
                    MessageBox.Show("La descripción del paquete no puede estar vacía.");
                    return;
                }

                // Se guarda el cliente en la base de datos
                clienteBLL.setCliente(c);
                //envioBLL.setEnvio(en);

                // Después de registrar, elimina la fila solo si se guarda en la base de datos
                foreach (DataGridViewRow row in dgClientes.Rows)
                {
                    if (row.Cells[1].Value != null && row.Cells[1].Value.ToString() == txtCedulaCliente.Text)
                    {
                        dgClientes.Rows.Remove(row);  // Elimina la fila que ya existe en el DataGridView
                        break;
                    }
                }

                // Luego, agrega la fila nueva al DataGridView
                //int newRowIndex = dgClientes.Rows.Add(c.ID, c.Nombre, c.Apellido, c.Email, c.Telefono, c.Direccion, "", "", "");
                LimpiarFormCliente(); // Limpia los campos de texto
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el cliente: {ex.Message}");
            }

            //try
            //{
            //    paquete p = new paquete();
            //    p.Descripcion = txtDesscripcionCliente.Text;
            //    p.Fragil = true;

            //    paqueteBLL.setPaquete(p);
            //    if (string.IsNullOrEmpty(txtDesscripcionCliente.Text))
            //    {
            //        MessageBox.Show("La descripción del paquete no puede estar vacía.");
            //        return;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Error al registrar el paquete: {ex.Message}");
            //}
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