using GestionTareasBeta.Context;
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
    public partial class TasksForms: Form
    {
        private readonly string connectionString = "server=localhost; Initial Catalog=ProyectoAdo; Integrated Security=True; trustServerCertificate=True;";

        private Categories taskCategories;
        private Statuses taskStatuses;

        public TasksForms()
        {
            InitializeComponent();

            dtDueDate.CustomFormat = " ";
            dtDueDate.Format = DateTimePickerFormat.Custom;
            dtDueDate.ValueChanged += new EventHandler(dtCompletedDate_ValueChanged);

            taskCategories = new Categories();
            taskStatuses = new Statuses();
        }


        private void dtCompletedDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtDueDate.CustomFormat == " ")
            {
                dtDueDate.CustomFormat = "dd/MM/yyyy";
            }
        }

        private void btnMostrarTareas_Click(object sender, EventArgs e)
        {
            ListTasks();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtTaskName.Text) ||
            string.IsNullOrWhiteSpace(txtTaskDescription.Text) ||
            dtDueDate.CustomFormat == " " ||
            cbTaskStatus.SelectedItem == null)
            {
                string mensaje = string.IsNullOrWhiteSpace(txtTaskName.Text) || string.IsNullOrWhiteSpace(txtTaskDescription.Text)
                    ? "Todos los campos son obligatorios."
                    : dtDueDate.CustomFormat == " "
                        ? "Por favor seleccione una fecha válida."
                        : "Por favor seleccione un estado válido.";

                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
               
                string task_name = txtTaskName.Text.Trim();
                string task_description = txtTaskDescription.Text.Trim();
                string task_category = cbTaskCategory.Text.Trim();
                string task_status = cbTaskStatus.Text.Trim();
                DateTime dueDate = dtDueDate.Value;
                bool is_active = cbTaskStatus.SelectedItem.ToString().Equals("TRUE", StringComparison.OrdinalIgnoreCase);

                Tasks newTask = new Tasks(0, task_name, task_description, task_category, task_status, dueDate);
                int row = newTask.AddTask();

                if (row == 1)
                {
                    MessageBox.Show("The task was added successfully.");
                    txtTaskName.Text = string.Empty;
                    txtTaskDescription.Text = string.Empty;
                    cbTaskCategory.Text = string.Empty;
                    cbTaskStatus.Text = string.Empty;

                    dtDueDate.Value = DateTime.Today;
                    dtDueDate.CustomFormat = "dd/MM/yyyy";

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
                cbTaskCategory.Text = dgTasks.SelectedCells[2].ToString() ?? string.Empty;
                cbTaskStatus.Text = dgTasks.SelectedCells[3].ToString() ?? string.Empty;

                DateTime tempDate;

                var completedValue = dgTasks.SelectedCells[4].Value;
                if (completedValue is DateTime dateTime2)
                    dtDueDate.Value = dateTime2;
                else if (DateTime.TryParse(completedValue?.ToString(), out tempDate))
                {
                    dtDueDate.CustomFormat = "dd/MM/yyyy";
                    dtDueDate.Value = tempDate;
                }

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
            cbTaskCategory.SelectedIndex = -1; 
            cbTaskStatus.SelectedIndex = -1;
            ClearDateTimePicker(dtDueDate);

            btnAdd.Enabled = true;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;

            txtTaskName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
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
            string taskCategory = cbTaskCategory.Text;
            string taskStatus = cbTaskStatus.Text;
            DateTime dueDate = dtDueDate.Value;

            DialogResult confirm = MessageBox.Show("Do you want to apply the changes? ",
                "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (confirm == DialogResult.OK)
            {
                Tasks task = new Tasks(id, taskName, taskDescription, taskCategory, taskStatus, dueDate);       
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

        private void lbCategories_Click(object sender, EventArgs e)
        {
            CategoriesForm categoriesForm = new CategoriesForm();
            categoriesForm.Show();
            this.Hide();
        }

        private void TasksForms_Load(object sender, EventArgs e)
        {
            taskCategories.LoadCategoriesComboBox(cbTaskCategory);
            taskStatuses.ConfigureStatusesComboBox(cbTaskStatus);

        }
    }
}
