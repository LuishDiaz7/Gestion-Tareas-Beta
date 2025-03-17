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
    public partial class TasksForms : Form
    {
        private readonly string connectionString = "server=localhost; Initial Catalog=ProyectoAdo; Integrated Security=True; trustServerCertificate=True;";
        private int currentUserId;
        private int selectedTaskId = 0;
        private Categories taskCategories;
        private Statuses taskStatuses;
        private Tasks currentTask = new Tasks();
        private Categories categoryManager = new Categories();

        /// <summary>
        /// Constructor de la clase TasksForms.
        /// Inicializa el formulario y valida el ID del usuario.
        /// </summary>
        /// <param name="userId">ID del usuario actual. Debe ser mayor que 0.</param>
        /// <exception cref="ArgumentException">Se lanza si el userId no es válido.</exception>
        public TasksForms(int userId)
        {
            InitializeComponent();

            if (userId <= 0)
            {
                MessageBox.Show("Invalid user ID. Please log in again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close(); // Cierra el formulario si el userId no es válido
                return;
            }

            this.currentUserId = userId;
        }

        /// <summary>
        /// Carga los estados de las tareas en el ComboBox correspondiente.
        /// </summary>
        /// <exception cref="Exception">Se lanza si ocurre un error al cargar los estados.</exception>
        private void LoadTaskStatuses()
        {
            try
            {
                Statuses statuses = new Statuses();
                statuses.ConfigureStatusesComboBox(cbTaskStatus);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading statuses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Carga las tareas del usuario actual en el DataGridView.
        /// </summary>
        /// <exception cref="Exception">Se lanza si ocurre un error al cargar las tareas.</exception>
        private void LoadTasks()
        {
            try
            {
                dgTasks.DataSource = Tasks.DisplayTasksByUser(currentUserId);
                FormatTasksGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tasks: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Formatea las columnas del DataGridView que muestra las tareas.
        /// Ajusta los encabezados y la visibilidad de las columnas.
        /// </summary>
        private void FormatTasksGrid()
        {
            if (dgTasks.Columns.Count > 0)
            {
                dgTasks.Columns["id"].HeaderText = "ID";
                dgTasks.Columns["task_name"].HeaderText = "Task Name";
                dgTasks.Columns["task_description"].HeaderText = "Description";
                dgTasks.Columns["categoryId"].Visible = false;
                dgTasks.Columns["category_name"].HeaderText = "Category";
                dgTasks.Columns["userId"].Visible = false;
                dgTasks.Columns["username"].Visible = false;
                dgTasks.Columns["statusId"].Visible = false;
                dgTasks.Columns["statusName"].HeaderText = "Status";
                dgTasks.Columns["creationDate"].HeaderText = "Creation Date";
                dgTasks.Columns["dueDate"].HeaderText = "Due Date";

                dgTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        /// <summary>
        /// Limpia los campos del formulario y restablece los controles a su estado inicial.
        /// </summary>
        private void ClearFields()
        {
            txtTaskID.Text = string.Empty;
            txtTaskName.Text = string.Empty;
            txtTaskDescription.Text = string.Empty;

            if (cbTaskCategory.Items.Count > 0)
                cbTaskCategory.SelectedIndex = 0;

            if (cbTaskStatus.Items.Count > 0)
                cbTaskStatus.SelectedIndex = 0;

            dtDueDate.Value = DateTime.Now.AddDays(7);

            selectedTaskId = 0;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
            btnAdd.Enabled = true;
        }

        /// <summary>
        /// Maneja el evento de clic en el botón "Agregar".
        /// Valida los campos y agrega una nueva tarea.
        /// </summary>
        /// <exception cref="Exception">Se lanza si ocurre un error al agregar la tarea.</exception>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTaskName.Text))
                {
                    MessageBox.Show("Please enter a task name", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTaskName.Focus();
                    return;
                }

                if (cbTaskCategory.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a category", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbTaskCategory.Focus();
                    return;
                }

                if (cbTaskStatus.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a status", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbTaskStatus.Focus();
                    return;
                }

                currentTask = new Tasks();
                currentTask.TaskName = txtTaskName.Text.Trim();
                currentTask.TaskDescription = txtTaskDescription.Text.Trim();
                currentTask.CategoryId = Convert.ToInt32(cbTaskCategory.SelectedValue);
                currentTask.UserId = currentUserId;
                currentTask.StatusId = Convert.ToInt32(cbTaskStatus.SelectedValue);
                currentTask.CreationDate = DateTime.Now;
                currentTask.DueDate = dtDueDate.Value;

                int newId = currentTask.AddTask();
                bool success = newId > 0;
                if (success)
                {
                    MessageBox.Show("Task created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTasks();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Could not create the task", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving task: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento de clic en el botón "Editar".
        /// Valida los campos y actualiza la tarea seleccionada.
        /// </summary>
        /// <exception cref="Exception">Se lanza si ocurre un error al editar la tarea.</exception>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedTaskId <= 0)
                {
                    MessageBox.Show("Please select a task first", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTaskName.Text))
                {
                    MessageBox.Show("Please enter a task name", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTaskName.Focus();
                    return;
                }

                if (cbTaskCategory.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a category", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbTaskCategory.Focus();
                    return;
                }

                if (cbTaskStatus.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a status", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbTaskStatus.Focus();
                    return;
                }

                currentTask.Id = selectedTaskId;
                currentTask.TaskName = txtTaskName.Text.Trim();
                currentTask.TaskDescription = txtTaskDescription.Text.Trim();
                currentTask.CategoryId = Convert.ToInt32(cbTaskCategory.SelectedValue);
                currentTask.StatusId = Convert.ToInt32(cbTaskStatus.SelectedValue);
                currentTask.DueDate = dtDueDate.Value;

                bool success = currentTask.EditTask();
                if (success)
                {
                    MessageBox.Show("Task updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTasks();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating task: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Limpia el formato de un DateTimePicker para que no muestre una fecha predeterminada.
        /// </summary>
        /// <param name="dtp">DateTimePicker que se desea limpiar.</param>
        private void ClearDateTimePicker(DateTimePicker dtp)
        {
            dtp.CustomFormat = " ";
            dtp.Format = DateTimePickerFormat.Custom;
        }

        /// <summary>
        /// Limpia todos los campos del formulario y restablece los controles a su estado inicial.
        /// </summary>
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

        /// <summary>
        /// Maneja el evento de clic en el botón "Eliminar".
        /// Elimina la tarea seleccionada después de confirmar con el usuario.
        /// </summary>
        /// <exception cref="Exception">Se lanza si ocurre un error al eliminar la tarea.</exception>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedTaskId <= 0)
                {
                    MessageBox.Show("Please select a task first", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure you want to delete this task?",
                                                      "Confirm Deletion",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    currentTask.Id = selectedTaskId;
                    bool success = currentTask.DeleteTask();

                    if (success)
                    {
                        MessageBox.Show("Task deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTasks();
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("Could not delete the task", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting task: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento de clic en el botón "Cancelar".
        /// Limpia los campos del formulario.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        /// <summary>
        /// Maneja el evento de clic en una celda del DataGridView.
        /// Carga los datos de la tarea seleccionada en los controles del formulario.
        /// </summary>
        /// <exception cref="Exception">Se lanza si ocurre un error al seleccionar la tarea.</exception>
        private void dgTasks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgTasks.Rows[e.RowIndex];

                    selectedTaskId = Convert.ToInt32(row.Cells["id"].Value);
                    txtTaskID.Text = selectedTaskId.ToString();
                    txtTaskName.Text = row.Cells["task_name"].Value.ToString();
                    txtTaskDescription.Text = row.Cells["task_description"].Value.ToString();
                    cbTaskCategory.SelectedValue = Convert.ToInt32(row.Cells["categoryId"].Value);
                    cbTaskStatus.SelectedValue = Convert.ToInt32(row.Cells["statusId"].Value);

                    if (row.Cells["DueDate"].Value != DBNull.Value)
                    {
                        dtDueDate.Value = Convert.ToDateTime(row.Cells["dueDate"].Value);
                    }
                    else
                    {
                        dtDueDate.Value = DateTime.Now.AddDays(7);
                    }

                    currentTask = new Tasks();
                    currentTask.Id = selectedTaskId;
                    currentTask.TaskName = txtTaskName.Text;
                    currentTask.TaskDescription = txtTaskDescription.Text;
                    currentTask.CategoryId = Convert.ToInt32(cbTaskCategory.SelectedValue);
                    currentTask.UserId = currentUserId;
                    currentTask.StatusId = Convert.ToInt32(cbTaskStatus.SelectedValue);
                    if (row.Cells["CreationDate"].Value != DBNull.Value)
                        currentTask.CreationDate = Convert.ToDateTime(row.Cells["CreationDate"].Value);
                    if (row.Cells["DueDate"].Value != DBNull.Value)
                        currentTask.DueDate = Convert.ToDateTime(row.Cells["DueDate"].Value);

                    btnDelete.Enabled = true;
                    btnEdit.Enabled = true;
                    btnAdd.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting task: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento de clic en el botón "Mostrar Tareas".
        /// Carga las tareas del usuario en el DataGridView.
        /// </summary>
        private void btnShowTasks_Click_1(object sender, EventArgs e)
        {
            LoadTasks();
        }

        /// <summary>
        /// Maneja el evento de clic en el botón "Categorías".
        /// Abre el formulario de categorías y oculta el formulario actual.
        /// </summary>
        private void lbCategories_Click_1(object sender, EventArgs e)
        {
            CategoriesForm categoriesForm = new CategoriesForm(currentUserId, this);
            categoriesForm.Show();
            this.Hide(); // Ocultar en lugar de cerrar
        }

        /// <summary>
        /// Método que se ejecuta al cargar el formulario.
        /// Carga los estados de las tareas, las categorías y las tareas del usuario.
        /// </summary>
        private void TasksForms_Load(object sender, EventArgs e)
        {
            try
            {
                LoadTaskStatuses();

                Categories categories = new Categories();
                categories.UserId = this.currentUserId;
                categories.LoadCategoriesComboBox(cbTaskCategory);

                LoadTaskStatuses();
                LoadTasks();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las categorías: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Recarga el ComboBox de categorías con las categorías actualizadas.
        /// </summary>
        public void ReloadCategoryComboBox()
        {
            Categories categories = new Categories();
            categories.UserId = currentUserId;
            categories.LoadCategoriesComboBox(cbTaskCategory);
        }
    }
}