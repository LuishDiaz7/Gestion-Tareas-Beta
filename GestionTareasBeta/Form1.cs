using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionTareasBeta
{
    public partial class Form1: Form
    {
        private readonly string connectionString = "server=localhost; Initial Catalog=ProyectoAdo; Integrated Security=True; trustServerCertificate=True;";


        public Form1()
        {
            InitializeComponent();

            dtCreatedDate.CustomFormat = " ";
            dtCompletedDate.CustomFormat = " ";
            dtCreatedDate.Format = DateTimePickerFormat.Custom;
            dtCompletedDate.Format = DateTimePickerFormat.Custom;
            dtCreatedDate.ValueChanged += new EventHandler(dtCreatedDate_ValueChanged);
            dtCompletedDate.ValueChanged += new EventHandler(dtCompletedDate_ValueChanged);
        }


        private void dtCreatedDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtCreatedDate.CustomFormat == " ")
            {
                dtCreatedDate.CustomFormat = "dd/MM/yyyy";
            }
        }

        private void dtCompletedDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtCompletedDate.CustomFormat == " ")
            {
                dtCompletedDate.CustomFormat = "dd/MM/yyyy";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
        }
        private void dgListaTareas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnMostrarTareas_Click(object sender, EventArgs e)
        {
            ListTasks();
        }
       

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void BntTaskName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtTaskName.Text) ||
            string.IsNullOrWhiteSpace(txtTaskDescription.Text) ||
            dtCreatedDate.CustomFormat == " " ||
            dtCompletedDate.CustomFormat == " " ||
            cbTaskStatus.SelectedItem == null)
            {
                string mensaje = string.IsNullOrWhiteSpace(txtTaskName.Text) || string.IsNullOrWhiteSpace(txtTaskDescription.Text)
                    ? "Todos los campos son obligatorios."
                    : dtCreatedDate.CustomFormat == " " || dtCompletedDate.CustomFormat == " "
                        ? "Por favor seleccione una fecha válida."
                        : "Por favor seleccione un estado válido.";

                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
               
                string task_name = txtTaskName.Text.Trim();
                string task_description = txtTaskDescription.Text.Trim();
                DateTime created_date = dtCreatedDate.Value;
                DateTime completed_date = dtCompletedDate.Value;
                bool is_active = cbTaskStatus.SelectedItem.ToString().Equals("TRUE", StringComparison.OrdinalIgnoreCase);

                Tasks newTask = new Tasks(0, task_name, task_description, created_date, completed_date, is_active);
                int row = newTask.AddTask();

                if (row == 1)
                {
                    MessageBox.Show("The task was added successfully.");
                    txtTaskName.Text = string.Empty;
                    txtTaskDescription.Text = string.Empty;

                    dtCreatedDate.Value = DateTime.Today;  // Restablecer la fecha actual
                    dtCreatedDate.CustomFormat = "dd/MM/yyyy"; // Asegurar que se muestre correctamente

                    dtCompletedDate.Value = DateTime.Today;
                    dtCompletedDate.CustomFormat = "dd/MM/yyyy";

                    cbTaskStatus.SelectedIndex = -1;
                }

                else
                {
                    MessageBox.Show("The task couldn't be added.");
                }

            }


        }

        public void ListTasks()
        {
            Tasks task = new Tasks();
            task.DisplayTasks(dgTasks);
        }

        private void dgTasks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if(index == -1 || dgTasks.SelectedCells[1].Value.ToString() == "")
            {
                ClearForm();

            }
            else
            {
                DataGridViewRow selectedRow = dgTasks.Rows[index];
                if (selectedRow.Cells[5].Value == null || selectedRow.Cells[0].Value == null)
                {
                    ClearForm();
                    return;
                }
                txtTaskID.Text = dgTasks.SelectedCells[5].Value?.ToString() ?? string.Empty;
                txtTaskName.Text = dgTasks.SelectedCells[0].Value?.ToString() ?? string.Empty;
                txtTaskDescription.Text = dgTasks.SelectedCells[1].Value?.ToString() ?? string.Empty;


                DateTime tempDate;

                var createdValue = dgTasks.SelectedCells[2].Value;
                if (createdValue is DateTime dateTime)
                    dtCreatedDate.Value = dateTime;
                else if (DateTime.TryParse(createdValue?.ToString(), out tempDate))
                {
                    dtCreatedDate.CustomFormat = "dd/MM/yyyy";
                    dtCreatedDate.Value = tempDate;
                }

                var completedValue = dgTasks.SelectedCells[3].Value;
                if (completedValue is DateTime dateTime2)
                    dtCompletedDate.Value = dateTime2;
                else if (DateTime.TryParse(completedValue?.ToString(), out tempDate))
                {
                    dtCompletedDate.CustomFormat = "dd/MM/yyyy";
                    dtCompletedDate.Value = tempDate;
                }

                var statusValue = dgTasks.SelectedCells[4].Value;
                bool isActive = false;

                if (statusValue is bool b)
                    isActive = b;
                else if (bool.TryParse(statusValue?.ToString(), out bool result))
                    isActive = result;
                else
                    isActive = statusValue?.ToString().Trim().ToLower() == "1" ||
                               statusValue?.ToString().Trim().ToLower() == "true";

                cbTaskStatus.SelectedIndex = isActive ?
                    cbTaskStatus.FindStringExact("TRUE") :
                    cbTaskStatus.FindStringExact("FALSE");

                btnAdd.Enabled = false;
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
            }

        }

        private void ClearDateTimePicker(DateTimePicker dtp)
        {
            dtp.CustomFormat = " ";
            dtp.Format = DateTimePickerFormat.Custom;
        }

        public void ClearForm()
        {
            txtTaskID.Clear();
            txtTaskName.Clear();
            txtTaskDescription.Clear();
            ClearDateTimePicker(dtCreatedDate);
            ClearDateTimePicker(dtCompletedDate);
            cbTaskStatus.SelectedIndex = -1;
            btnAdd.Enabled = true;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;

            txtTaskName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void cbTaskStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string code = txtTaskID.Text;
            int id = Convert.ToInt32(code);

            DialogResult confirm = MessageBox.Show("Do you want to delete this task?", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            
            if (confirm == DialogResult.OK)
            {
                Tasks task = new Tasks(id);
                int row = task.DeleteTask();

                if (row == 1)
                {
                    MessageBox.Show("Task successfully deleted.");
                    ClearForm();
                    ListTasks();
                }
                else
                {
                    MessageBox.Show("Could not delete the task.");
                }
            }
            else
            {
                ClearForm();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtTaskID.Text);
            string taskName = txtTaskName.Text;
            string taskDescription = txtTaskDescription.Text;
            DateTime createdDate = dtCreatedDate.Value;
            DateTime completedDate = dtCompletedDate.Value;
            Boolean taskStatus = cbTaskStatus.SelectedItem.ToString().ToUpper() == "TRUE";

            DialogResult confirm = MessageBox.Show("Do you want to apply the changes? ",
                "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (confirm == DialogResult.OK)
            {
                Tasks task = new Tasks(id, taskName, taskDescription, createdDate, completedDate, taskStatus);       
                int row = task.EditTask();
                if (row == 1)
                {
                    MessageBox.Show("Changes have been made.");
                    ClearForm();
                    ListTasks();
                }
                else
                {
                    MessageBox.Show("The task could not be updated.");
                }
            }
            else
            {
                ClearForm();
            }

        }

        private void cbTaskStatus_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void dtCreatedDate_VisibleChanged(object sender, EventArgs e)
        {

        }

        
    }
}
