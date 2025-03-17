using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GestionTareasBeta
{
    /// <summary>
    /// Formulario de inicio de sesión que permite a los usuarios autenticarse en el sistema.
    /// </summary>
    public partial class LoginForm : Form
    {
        /// <summary>
        /// Constructor de la clase LoginForm.
        /// Inicializa los componentes del formulario y oculta la contraseña en el campo correspondiente.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*'; // Oculta la contraseña
        }

        /// <summary>
        /// Método que se ejecuta al cargar el formulario.
        /// Puede utilizarse para inicializar componentes adicionales si es necesario.
        /// </summary>
        private void Form2_Load(object sender, EventArgs e)
        {
            // Puedes inicializar componentes adicionales aquí si es necesario
        }

        /// <summary>
        /// Método que se ejecuta cuando cambia el texto en el campo de usuario o correo electrónico.
        /// </summary>
        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            // Lógica para cuando cambia el texto del usuario (si es necesaria)
        }

        /// <summary>
        /// Método que se ejecuta al hacer clic en el botón de salida.
        /// Cierra la aplicación.
        /// </summary>
        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Método que se ejecuta cuando cambia el estado del checkbox "Mostrar contraseña".
        /// Muestra u oculta la contraseña en el campo correspondiente.
        /// </summary>
        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
        }

        /// <summary>
        /// Método que se ejecuta al hacer clic en el botón de inicio de sesión.
        /// Valida los campos de entrada y autentica al usuario.
        /// </summary>
        /// <exception cref="Exception">Se lanza si ocurre un error al conectar con la base de datos.</exception>
        private void btnLogIn_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserOrEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                ShowErrorMessage("Please complete all fields.");
                return;
            }

            try
            {
                // Obtiene el ID del usuario utilizando la clase Usuarios
                int userId = Usuarios.GetUserId(txtUserOrEmail.Text, txtPassword.Text);

                if (userId > 0)
                {
                    MessageBox.Show("Welcome!", "Success",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Pasa el ID del usuario al formulario de tareas
                    TasksForms mainForm = new TasksForms(userId);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    ShowErrorMessage("Incorrect username or password. Please try again.");
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error connecting to the database: {ex.Message}");
            }
        }

        /// <summary>
        /// Método auxiliar para mostrar mensajes de error en un MessageBox.
        /// </summary>
        /// <param name="message">Mensaje de error que se desea mostrar.</param>
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}