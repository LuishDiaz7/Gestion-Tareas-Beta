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
    public partial class CategoriesForm: Form
    {
        public CategoriesForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TasksForms tasksForms = new TasksForms();
            tasksForms.Show();
            this.Close();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string categoryName = txtCategoryName.Text;
            string categoryDescription = txtCategoryDescription.Text;
            string creationDate = txtCDCategory.Text;

            if (categoryName == "" || categoryDescription == "")
            {
                MessageBox.Show("All fields are required.");
            }
            else
            {
                Categories newCategory = new Categories(0,categoryName, categoryDescription, creationDate);
                int row = newCategory.AddCategory();

                if (row == 1)
                {
                    MessageBox.Show("The category was added successfully.");

                    txtCategoryName.Text = "";
                    txtCategoryDescription.Text = "";
                    ListCategories();
                }

                else
                {
                    MessageBox.Show("The category couldn't be added.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CategoriesForm_Load(object sender, EventArgs e)
        {
            ListCategories();
        }

        public void ListCategories()
        {
            Categories category = new Categories();
            category.DisplayCategories(dtgCategories);
        }

        private void dtgCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                CleanForm();
            }

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

        private void btnCancelCategory_Click(object sender, EventArgs e)
        {
            CleanForm();
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            string code = txtCategoryID.Text;
            int id = Convert.ToInt32(code);

            DialogResult confirm = MessageBox.Show("Do you want to delete this category? ", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (confirm == DialogResult.OK)
            {
                Categories category = new Categories(id);
                int row = category.DeleteCategory();

                if (row == 1)
                {
                    MessageBox.Show("Task successfully deleted.");
                    CleanForm();
                    ListCategories();
                }
                else
                {
                    MessageBox.Show("Could not delete the task.");
                }

            }
            else
            {
                CleanForm();
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCategoryID.Text);
            string name = txtCategoryName.Text;
            string description = txtCategoryDescription.Text;
            string creationDate = txtCDCategory.Text;

            DialogResult confirm = MessageBox.Show("Do you want to apply the changes? ", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (confirm == DialogResult.OK)
            {
                Categories category = new Categories(id, name, description, creationDate);
                int row = category.EditCategory();

                if (row == 1)
                {
                    MessageBox.Show("Changes have been made.");
                    CleanForm();
                    ListCategories();
                }
                else
                {
                    MessageBox.Show("The category could not be updated.");
                }
                
            }
        }
    }
}
