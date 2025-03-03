using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace GestionTareasBeta
{
    class Tasks
    {
        private int id;
        private string task_name;
        private string task_description;
        private DateTime created_date;
        private DateTime completed_date;
        private Boolean is_active;

        SqlConnection cnn = new SqlConnection("server=localhost; Initial Catalog=ProyectoAdo; Integrated Security=True; trustServerCertificate=True");

        public Tasks(int id, string task_name, string task_description, DateTime created_date, DateTime completed_date, bool is_active)
        {
            this.id = id;
            this.task_name = task_name;
            this.task_description = task_description;
            this.created_date = created_date;
            this.completed_date = completed_date;
            this.is_active = is_active;
        }

        public Tasks()
        {

        }

        public Tasks(int id)
        {
            this.id = id;
        }

        public int AddTask()
        {
            cnn.Open();
            SqlCommand query = new SqlCommand("INSERT INTO tasks VALUES (@task_name, @task_description, @created_date, @completed_date, @is_active)", cnn);
            query.Parameters.AddWithValue("task_name", task_name);
            query.Parameters.AddWithValue("task_description", task_description);
            query.Parameters.AddWithValue("created_date", created_date);
            query.Parameters.AddWithValue("completed_date", completed_date);
            query.Parameters.AddWithValue("is_active", is_active);

            int affected_Rows = query.ExecuteNonQuery();
            cnn.Close();

            return affected_Rows;
        }

        public void DisplayTasks(DataGridView dtg)
        {
            string query = "SELECT * FROM tasks";
            cnn.Open();
            SqlDataAdapter data = new SqlDataAdapter(query, cnn);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dtg.DataSource = dt;
        }

        public int DeleteTask()
        {
            cnn.Open();
            SqlCommand query = new SqlCommand("DELETE FROM tasks WHERE id = @codigo", cnn);
            query.Parameters.AddWithValue("codigo", id);

            int affected_Rows = query.ExecuteNonQuery();
            cnn.Close();

            return affected_Rows;
        }

        public int EditTask()
        {
            cnn.Open();
            SqlCommand query = new SqlCommand("UPDATE tasks SET" +
                " task_name = @task_name, task_description = @task_description, created_date = @created_date, completed_date = @completed_date, is_active = @task_status" +
                " WHERE id = @task_id", cnn);
            query.Parameters.AddWithValue("task_id", id);
            query.Parameters.AddWithValue("task_name", task_name);
            query.Parameters.AddWithValue("task_description", task_description);
            query.Parameters.AddWithValue("created_date", created_date);
            query.Parameters.AddWithValue("completed_date", completed_date);
            query.Parameters.AddWithValue("task_status", is_active);

            int affected_Rows = query.ExecuteNonQuery();
            cnn.Close();

            return affected_Rows;

        }
    }
    
}
