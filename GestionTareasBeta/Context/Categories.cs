using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Data;
using System.Windows.Forms;


namespace GestionTareasBeta.Context
{
    class Categories
    {
        private int id;
        private string name;
        private string description;
        private string creationDate;

        SqlConnection cnn = new SqlConnection("server=localhost; Initial Catalog=ProyectoAdo; Integrated Security=True; trustServerCertificate=True");

        public Categories(int id, string name, string description, string creationDate)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.creationDate = creationDate;
        }

        public Categories()
        {

        }

        public Categories(int id)
        {
            this.id = id;

        }

        public int AddCategory()
        {
            cnn.Open();
            SqlCommand query = new SqlCommand("INSERT INTO TaskCategories (name, description) VALUES (@name, @description)", cnn);
            query.Parameters.AddWithValue("name", name);
            query.Parameters.AddWithValue("description", description);

            int affected_Arrows = query.ExecuteNonQuery();
            cnn.Close();

            return affected_Arrows;
        }

        public DataTable GetCategories()
        {
            string query = "SELECT * FROM TaskCategories";
            DataTable dt = new DataTable();
            cnn.Open();
            SqlDataAdapter data = new SqlDataAdapter(query, cnn);
            data.Fill(dt);

            return dt;
        }

        public void DisplayCategories(DataGridView dtg)
        {
            dtg.DataSource = GetCategories();
        }

        public void LoadCategoriesComboBox(ComboBox cmb)
        {
            DataTable categories = GetCategories();
            cmb.DisplayMember = "name";
            cmb.ValueMember = "id";
            cmb.DataSource = categories;
        }

        public int DeleteCategory()
        {
            cnn.Open();
            SqlCommand query = new SqlCommand("DELETE FROM TaskCategories Where id = @code", cnn);
            query.Parameters.AddWithValue("code", id);
            int affected_Rows = query.ExecuteNonQuery();
            cnn.Close();

            return affected_Rows;
        }

        public int EditCategory()
        {
            cnn.Open();
            SqlCommand query = new SqlCommand("UPDATE TaskCategories SET " +
                "name = @name, description = @description, creationDate = @creationDate WHERE id = @id", cnn);
            query.Parameters.AddWithValue("id", id);
            query.Parameters.AddWithValue("name", name);
            query.Parameters.AddWithValue("description", description);
            query.Parameters.AddWithValue("creationDate", DateTime.Parse(creationDate));
            int affected_Rows = query.ExecuteNonQuery();

            return affected_Rows;
            
        }


    }
}
