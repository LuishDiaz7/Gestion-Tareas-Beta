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
        private string task_category;
        private string task_status;
        private DateTime due_Date;

        SqlConnection cnn = new SqlConnection("server=localhost; Initial Catalog=ProyectoAdo; Integrated Security=True; trustServerCertificate=True");

        public Tasks(int id, string task_name, string task_description, string task_category, string task_status, DateTime due_Date)
        {
            this.id = id;
            this.task_name = task_name;
            this.task_description = task_description;
            this.task_category = task_category;
            this.task_status = task_status;
            this.due_Date = due_Date;
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
            SqlCommand query = new SqlCommand("INSERT INTO tasks VALUES (@task_name, @task_description, @task_category, @task_status, @dueDate)", cnn);
            query.Parameters.AddWithValue("task_name", task_name);
            query.Parameters.AddWithValue("task_description", task_description);
            query.Parameters.AddWithValue("task_category", task_category);
            query.Parameters.AddWithValue("task_status", task_status);
            query.Parameters.AddWithValue("dueDate", due_Date);

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
                " task_name = @task_name, task_description = @task_description, task_category = @task_category, task_status = @task_status, dueDate = @dueDate" +
                " WHERE id = @task_id", cnn);
            query.Parameters.AddWithValue("task_id", id);
            query.Parameters.AddWithValue("task_name", task_name);
            query.Parameters.AddWithValue("task_description", task_description);
            query.Parameters.AddWithValue("task_category", task_category);
            query.Parameters.AddWithValue("task_status", task_status);
            query.Parameters.AddWithValue("dueDate", due_Date);

            int affected_Rows = query.ExecuteNonQuery();
            cnn.Close();

            return affected_Rows;

        }
    }
    
}
