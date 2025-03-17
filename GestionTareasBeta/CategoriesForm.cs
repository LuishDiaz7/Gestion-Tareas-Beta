using GestionTareasBeta.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionTareasBeta
{
    /// <summary>
    /// Formulario para gestionar las categorías de tareas.
    /// </summary>
    public partial class CategoriesForm : Form
    {
        private int currentUserId; // ID del usuario actual
        private TasksForms parentTasksForm; // Referencia al formulario padre

        /// <summary>
        /// Constructor que inicializa el formulario con el ID del usuario y la referencia al formulario padre.
        /// </summary>
        /// <param name="userId">ID del usuario actualmente autenticado.</param>
        /// <param name="parentForm">Referencia al formulario de tareas padre.</param>
        public CategoriesForm(int userId, TasksForms parentForm)
        {
            InitializeComponent();
            this.currentUserId = userId;
            this.parentTasksForm = parentForm;
        }

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con el diseñador.
        /// </summary>
        public CategoriesForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Maneja el evento de clic en el botón de regresar.
        /// Recarga el ComboBox de categorías en el formulario padre y cierra este formulario.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (parentTasksForm != null)
            {
                parentTasksForm.ReloadCategoryComboBox();
                parentTasksForm.Show();
                this.Close();
            }
            else
            {
                // Fallback en caso de que no se tenga la referencia
                TasksForms newTasksForms = new TasksForms(currentUserId);
                newTasksForms.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Maneja el evento de clic en el botón para añadir una categoría.
        /// Valida los datos ingresados y crea una nueva categoría en la base de datos.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string categoryName = txtCategoryName.Text;
            string categoryDescription = txtCategoryDescription.Text;

            if (string.IsNullOrWhiteSpace(categoryName) || string.IsNullOrWhiteSpace(categoryDescription))
            {
                MessageBox.Show("The name and description are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Categories newCategory = new Categories();
                newCategory.Name = categoryName;
                newCategory.Description = categoryDescription;
                newCategory.UserId = currentUserId;

                int row = newCategory.AddCategory();

                if (row > 0)
                {
                    MessageBox.Show("The category was added successfully.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CleanForm();
                    RefreshDataGridView();
                }
                else
                {
                    MessageBox.Show("Could not add the category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento de carga del formulario.
        /// Refresca el DataGridView para mostrar las categorías existentes.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void CategoriesForm_Load(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        /// <summary>
        /// Actualiza el DataGridView con las categorías del usuario actual.
        /// </summary>
        public void RefreshDataGridView()
        {
            try
            {
                Categories category = new Categories();
                category.UserId = currentUserId;

                // Limpiar el DataGridView antes de cargarlo nuevamente
                dtgCategories.DataSource = null;

                // Crear un nuevo DataTable y asignarlo como DataSource
                DataTable dt = category.GetCategories();
                dtgCategories.DataSource = dt;

                // Formatear columnas
                if (dtgCategories.Columns.Contains("id"))
                    dtgCategories.Columns["id"].HeaderText = "ID";
                if (dtgCategories.Columns.Contains("name"))
                    dtgCategories.Columns["name"].HeaderText = "Nombre";
                if (dtgCategories.Columns.Contains("description"))
                    dtgCategories.Columns["description"].HeaderText = "Descripción";
                if (dtgCategories.Columns.Contains("creationDate"))
                    dtgCategories.Columns["creationDate"].HeaderText = "Fecha de Creación";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve categories: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento de clic en una celda del DataGridView.
        /// Carga los datos de la categoría seleccionada en los campos del formulario.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void dtgCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dtgCategories.Rows.Count)
            {
                CleanForm();
                return;
            }

            try
            {
                DataGridViewRow row = dtgCategories.Rows[e.RowIndex];

                string idCategory = row.Cells["id"].Value?.ToString() ?? "";
                string categoryName = row.Cells["name"].Value?.ToString() ?? "";
                string categoryDescription = row.Cells["description"].Value?.ToString() ?? "";
                string cdCategory = row.Cells["creationDate"].Value?.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(categoryName))
                {
                    CleanForm();
                }
                else
                {
                    txtCategoryName.Text = categoryName;
                    txtCategoryDescription.Text = categoryDescription;
                    txtCategoryID.Text = idCategory;
                    txtCDCategory.Text = cdCategory;

                    btnAddCategory.Enabled = false;
                    btnDeleteCategory.Enabled = true;
                    btnEditCategory.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting the category: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Limpia todos los campos del formulario y restablece los botones a su estado predeterminado.
        /// </summary>
        public void CleanForm()
        {
            txtCategoryName.Clear();
            txtCategoryDescription.Clear();
            txtCategoryID.Clear();
            txtCDCategory.Clear();
            btnAddCategory.Enabled = true;
            btnDeleteCategory.Enabled = false;
            btnEditCategory.Enabled = false;
            txtCategoryName.Focus();
        }

        /// <summary>
        /// Maneja el evento de clic en el botón cancelar.
        /// Limpia el formulario sin realizar ninguna acción en la base de datos.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void btnCancelCategory_Click(object sender, EventArgs e)
        {
            CleanForm();
        }

        /// <summary>
        /// Maneja el evento de clic en el botón eliminar.
        /// Solicita confirmación y elimina la categoría seleccionada de la base de datos.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txtCategoryID.Text);

                DialogResult confirm = MessageBox.Show(
                    "Are you sure you want to delete this category?",
                    "Confirm deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    Categories category = new Categories(id);
                    category.UserId = currentUserId;

                    int row = category.DeleteCategory();

                    if (row > 0)
                    {
                        MessageBox.Show("Category deleted successfully. ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CleanForm();
                        RefreshDataGridView();
                    }
                    else
                    {
                        MessageBox.Show("Could not delete the category. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    CleanForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting the category: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento de clic en el botón editar.
        /// Valida los datos y actualiza la información de la categoría en la base de datos.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txtCategoryID.Text);
                string name = txtCategoryName.Text;
                string description = txtCategoryDescription.Text;
                string creationDate = txtCDCategory.Text;

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
                {
                    MessageBox.Show("The name and description are required. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirm = MessageBox.Show(
                    "Do you want to apply the changes?",
                    "Confirm edit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    Categories category = new Categories(id, name, description, creationDate);
                    category.UserId = currentUserId;

                    int row = category.EditCategory();

                    if (row > 0)
                    {
                        MessageBox.Show("Changes applied successfully. ", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CleanForm();
                        RefreshDataGridView();
                    }
                    else
                    {
                        MessageBox.Show("Could not update the category. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error editing the category: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
