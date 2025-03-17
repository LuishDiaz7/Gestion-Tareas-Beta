using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GestionTareasBeta.Context
{
    public class Statuses
    {
        private int id;
        private string statusName;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Statuses"/>.
        /// </summary>
        public Statuses()
        {
        }

        /// <summary>
        /// Configura un ComboBox con los estados de las tareas obtenidos desde la base de datos.
        /// </summary>
        /// <param name="cmb">ComboBox que será configurado con los estados de las tareas.</param>
        /// <exception cref="Exception">Se lanza cuando no se encuentran estados de tareas en la base de datos
        /// o cuando ocurre un error al cargar los datos en el ComboBox.</exception>
        public void ConfigureStatusesComboBox(ComboBox cmb)
        {
            SqlConnection cnn = null;

            try
            {
                // Crear y abrir conexión a la base de datos.
                cnn = DatabaseHelper.CreateConnection();
                cnn.Open();

                // Ejecutar procedimiento almacenado para obtener los estados de tareas.
                SqlCommand cmd = new SqlCommand("spGetAllTaskStatuses", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Cargar los resultados en un DataTable.
                DataTable statuses = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(statuses);

                // Verificar que el DataTable tenga datos.
                if (statuses.Rows.Count == 0)
                {
                    throw new Exception("No task statuses found in the database.");
                }

                // Configurar las propiedades del ComboBox.
                cmb.DisplayMember = "statusName";
                cmb.ValueMember = "id";
                cmb.DataSource = statuses;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error loading statuses into ComboBox: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión si está abierta.
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
        }
    }
}
