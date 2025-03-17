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
    /// <summary>
    /// Clase que representa las categorías de tareas y proporciona métodos para su gestión.
    /// </summary>
    class Categories
    {
        private int id;
        private string name;
        private string description;
        private string creationDate;
        private int userId;

        /// <summary>
        /// Constructor que inicializa una categoría con sus atributos principales.
        /// </summary>
        /// <param name="id">Identificador único de la categoría.</param>
        /// <param name="name">Nombre de la categoría.</param>
        /// <param name="description">Descripción de la categoría.</param>
        /// <param name="creationDate">Fecha de creación de la categoría.</param>
        public Categories(int id, string name, string description, string creationDate)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.creationDate = creationDate;
        }

        /// <summary>
        /// Constructor por defecto para la creación de instancias vacías.
        /// </summary>
        public Categories()
        {
        }

        /// <summary>
        /// Constructor que inicializa una categoría solo con su ID.
        /// </summary>
        /// <param name="id">Identificador único de la categoría.</param>
        public Categories(int id)
        {
            this.id = id;
        }

        // Propiedades para acceder a los atributos privados
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public int UserId { get => userId; set => userId = value; }

        /// <summary>
        /// Añade una nueva categoría a la base de datos.
        /// </summary>
        /// <returns>Número de filas afectadas por la operación.</returns>
        /// <exception cref="Exception">Se lanza cuando ocurre un error al añadir la categoría.</exception>
        public int AddCategory()
        {
            SqlConnection cnn = null;
            try
            {
                cnn = DatabaseHelper.CreateConnection();
                cnn.Open();
                SqlCommand cmd = new SqlCommand("spAddTaskCategory", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@userId", userId);
                int affected_Rows = cmd.ExecuteNonQuery();
                return affected_Rows;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error adding category: " + ex.Message);
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
        }

        /// <summary>
        /// Obtiene todas las categorías asociadas al usuario actual.
        /// </summary>
        /// <returns>DataTable con las categorías del usuario.</returns>
        /// <exception cref="Exception">Se lanza cuando ocurre un error al recuperar las categorías.</exception>
        public DataTable GetCategories()
        {
            DataTable dt = new DataTable();
            SqlConnection cnn = null;
            try
            {
                cnn = DatabaseHelper.CreateConnection();
                cnn.Open();
                SqlCommand cmd = new SqlCommand("spDisplayAllTaskCategoriesByUser", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                SqlDataAdapter data = new SqlDataAdapter(cmd);
                data.Fill(dt);
                return dt;
            }
            catch (SqlException ex)
            {
                throw new Exception("Failed to retrieve categories: " + ex.Message);
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
        }

        /// <summary>
        /// Carga las categorías del usuario en un ComboBox.
        /// </summary>
        /// <param name="cmb">ComboBox donde se cargarán las categorías.</param>
        /// <exception cref="Exception">Se lanza cuando ocurre un error al cargar las categorías.</exception>
        public void LoadCategoriesComboBox(ComboBox cmb)
        {
            SqlConnection cnn = null;
            try
            {
             
                cnn = DatabaseHelper.CreateConnection();
                cnn.Open();
                SqlCommand cmd = new SqlCommand("spGetCategoriesForComboBox", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);

                DataTable categories = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(categories);

                // Si no hay categorías, añadir una opción "Sin categoría"
                if (categories.Rows.Count == 0)
                {
                    // Crear una nueva tabla con la estructura correcta
                    DataTable defaultCategories = new DataTable();
                    defaultCategories.Columns.Add("id", typeof(int));
                    defaultCategories.Columns.Add("name", typeof(string));

                    // Añadir una fila con valores predeterminados
                    // Usamos -1 como ID para indicar que es una categoría temporal
                    defaultCategories.Rows.Add(-1, "Uncategorized");

                    cmb.DisplayMember = "name";
                    cmb.ValueMember = "id";
                    cmb.DataSource = defaultCategories;
                }
                else
                {

                    // Asigna los datos al combobox
                    cmb.DisplayMember = "name";
                    cmb.ValueMember = "id";
                    cmb.DataSource = categories;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error loading categories into the ComboBox: " + ex.Message);
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
        }

        /// <summary>
        /// Elimina una categoría de la base de datos.
        /// </summary>
        /// <returns>Número de filas afectadas por la operación.</returns>
        /// <exception cref="Exception">Se lanza cuando ocurre un error al eliminar la categoría.</exception>
        public int DeleteCategory()
        {
            SqlConnection cnn = null;
            try
            {
                cnn = DatabaseHelper.CreateConnection();
                cnn.Open();
                SqlCommand cmd = new SqlCommand("spDeleteTaskCategory", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@userId", userId);
                int affected_Rows = cmd.ExecuteNonQuery();
                return affected_Rows;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error deleting category: " + ex.Message);
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
        }

        /// <summary>
        /// Actualiza la información de una categoría existente.
        /// </summary>
        /// <returns>Número de filas afectadas por la operación.</returns>
        /// <exception cref="Exception">Se lanza cuando ocurre un error al editar la categoría.</exception>
        public int EditCategory()
        {
            SqlConnection cnn = null;
            try
            {
                cnn = DatabaseHelper.CreateConnection();
                cnn.Open();
                SqlCommand cmd = new SqlCommand("spEditTaskCategory", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@userId", userId);
                int affected_Rows = cmd.ExecuteNonQuery();
                return affected_Rows;
            }
            catch (SqlException ex)
            {
                throw new Exception("Failed to edit category: " + ex.Message);
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
        }
    }
}