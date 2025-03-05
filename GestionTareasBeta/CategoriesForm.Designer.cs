namespace GestionTareasBeta
{
    partial class CategoriesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.txtCategoryDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtgCategories = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.btnDeleteCategory = new System.Windows.Forms.Button();
            this.btnEditCategory = new System.Windows.Forms.Button();
            this.btnCancelCategory = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtCDCategory = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCategoryID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCategories)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Category Name:";
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Location = new System.Drawing.Point(48, 190);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(298, 22);
            this.txtCategoryName.TabIndex = 1;
            // 
            // txtCategoryDescription
            // 
            this.txtCategoryDescription.Location = new System.Drawing.Point(48, 249);
            this.txtCategoryDescription.Name = "txtCategoryDescription";
            this.txtCategoryDescription.Size = new System.Drawing.Size(298, 22);
            this.txtCategoryDescription.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(45, 220);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description:";
            // 
            // dtgCategories
            // 
            this.dtgCategories.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtgCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgCategories.Location = new System.Drawing.Point(408, 46);
            this.dtgCategories.Name = "dtgCategories";
            this.dtgCategories.RowHeadersWidth = 51;
            this.dtgCategories.RowTemplate.Height = 24;
            this.dtgCategories.Size = new System.Drawing.Size(360, 263);
            this.dtgCategories.TabIndex = 4;
            this.dtgCategories.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgCategories_CellClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(44, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(319, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "Organize Your Task Categories";
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.BackColor = System.Drawing.Color.White;
            this.btnAddCategory.Location = new System.Drawing.Point(49, 343);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(85, 32);
            this.btnAddCategory.TabIndex = 6;
            this.btnAddCategory.Text = "Add";
            this.btnAddCategory.UseVisualStyleBackColor = false;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // btnDeleteCategory
            // 
            this.btnDeleteCategory.BackColor = System.Drawing.Color.White;
            this.btnDeleteCategory.Enabled = false;
            this.btnDeleteCategory.Location = new System.Drawing.Point(262, 343);
            this.btnDeleteCategory.Name = "btnDeleteCategory";
            this.btnDeleteCategory.Size = new System.Drawing.Size(85, 32);
            this.btnDeleteCategory.TabIndex = 7;
            this.btnDeleteCategory.Text = "Delete";
            this.btnDeleteCategory.UseVisualStyleBackColor = false;
            this.btnDeleteCategory.Click += new System.EventHandler(this.btnDeleteCategory_Click);
            // 
            // btnEditCategory
            // 
            this.btnEditCategory.BackColor = System.Drawing.Color.White;
            this.btnEditCategory.Enabled = false;
            this.btnEditCategory.Location = new System.Drawing.Point(156, 343);
            this.btnEditCategory.Name = "btnEditCategory";
            this.btnEditCategory.Size = new System.Drawing.Size(85, 32);
            this.btnEditCategory.TabIndex = 8;
            this.btnEditCategory.Text = "Edit";
            this.btnEditCategory.UseVisualStyleBackColor = false;
            this.btnEditCategory.Click += new System.EventHandler(this.btnEditCategory_Click);
            // 
            // btnCancelCategory
            // 
            this.btnCancelCategory.BackColor = System.Drawing.Color.White;
            this.btnCancelCategory.Location = new System.Drawing.Point(127, 406);
            this.btnCancelCategory.Name = "btnCancelCategory";
            this.btnCancelCategory.Size = new System.Drawing.Size(148, 32);
            this.btnCancelCategory.TabIndex = 9;
            this.btnCancelCategory.Text = "Cancel";
            this.btnCancelCategory.UseVisualStyleBackColor = false;
            this.btnCancelCategory.Click += new System.EventHandler(this.btnCancelCategory_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(51, 42);
            this.button1.TabIndex = 10;
            this.button1.Text = "<--";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtCDCategory
            // 
            this.txtCDCategory.Location = new System.Drawing.Point(48, 305);
            this.txtCDCategory.Name = "txtCDCategory";
            this.txtCDCategory.ReadOnly = true;
            this.txtCDCategory.Size = new System.Drawing.Size(298, 22);
            this.txtCDCategory.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(45, 276);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 21);
            this.label4.TabIndex = 13;
            this.label4.Text = "Creation Date:";
            // 
            // txtCategoryID
            // 
            this.txtCategoryID.Location = new System.Drawing.Point(48, 136);
            this.txtCategoryID.Name = "txtCategoryID";
            this.txtCategoryID.ReadOnly = true;
            this.txtCategoryID.Size = new System.Drawing.Size(298, 22);
            this.txtCategoryID.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(44, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 21);
            this.label5.TabIndex = 11;
            this.label5.Text = "ID:";
            // 
            // CategoriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtCDCategory);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCategoryID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCancelCategory);
            this.Controls.Add(this.btnEditCategory);
            this.Controls.Add(this.btnDeleteCategory);
            this.Controls.Add(this.btnAddCategory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtgCategories);
            this.Controls.Add(this.txtCategoryDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCategoryName);
            this.Controls.Add(this.label1);
            this.Name = "CategoriesForm";
            this.Text = "TaskManager";
            this.Load += new System.EventHandler(this.CategoriesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgCategories)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.TextBox txtCategoryDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dtgCategories;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.Button btnDeleteCategory;
        private System.Windows.Forms.Button btnEditCategory;
        private System.Windows.Forms.Button btnCancelCategory;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtCDCategory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCategoryID;
        private System.Windows.Forms.Label label5;
    }
}