using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GestionTareasBeta.Context
{
    class Statuses
    {
        SqlConnection cnn = new SqlConnection("server=localhost; Initial Catalog=ProyectoAdo; Integrated Security=True; trustServerCertificate=True");

        public DataTable GetAllStatuses()
        {
            string query = "SELECT * FROM TaskStatuses";
            DataTable dt = new DataTable();
            cnn.Open();
            SqlDataAdapter data = new SqlDataAdapter(query, cnn);
            data.Fill(dt);

            return dt;
        }

        // Configurar un ComboBox para mostrar estados
        public void ConfigureStatusesComboBox(ComboBox cmb)
        {
            DataTable statuses = GetAllStatuses();
            cmb.DisplayMember = "statusName";
            cmb.ValueMember = "id";
            cmb.DataSource = statuses;

        }
    }
}