using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using GestionTareasBeta.Context;

namespace GestionTareasBeta
{
    /// <summary>
    /// Clase que representa las tareas del usuario y proporciona métodos para su gestión.
    /// </summary>
    public class Tasks
    {
        /// <summary>
        /// Identificador único de la tarea.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la tarea.
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// Descripción detallada de la tarea.
        /// </summary>
        public string TaskDescription { get; set; }

        /// <summary>
        /// Identificador de la categoría a la que pertenece la tarea.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Identificador del usuario propietario de la tarea.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Identificador del estado actual de la tarea.
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// Fecha de creación de la tarea.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Fecha límite para completar la tarea. Puede ser nula si no hay fecha límite.
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Constructor por defecto para la creación de instancias vacías.
        /// </summary>
        public Tasks()
        {
            // Constructor vacío
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado y devuelve un valor escalar.
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento almacenado a ejecutar.</param>
        /// <param name="parameters">Parámetros para el procedimiento almacenado.</param>
        /// <param name="commandType">Tipo de comando a ejecutar.</param>
        /// <returns>Valor escalar devuelto por el procedimiento almacenado.</returns>
        private static object ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            using (SqlConnection connection = DatabaseHelper.CreateConnection())
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, connection))
                {
                    cmd.CommandType = commandType;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado que no devuelve resultados.
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento almacenado a ejecutar.</param>
        /// <param name="parameters">Parámetros para el procedimiento almacenado.</param>
        /// <returns>Número de filas afectadas por la operación.</returns>
        private static int ExecuteNonQueryStoredProcedure(string procedureName, SqlParameter[] parameters)
        {
            using (SqlConnection connection = DatabaseHelper.CreateConnection())
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Añade una nueva tarea a la base de datos.
        /// </summary>
        /// <returns>ID de la tarea añadida.</returns>
        /// <exception cref="Exception">Se lanza cuando ocurre un error al añadir la tarea.</exception>
        public int AddTask()
        {
            try
            {
                // Crear parámetros para el procedimiento almacenado
                SqlParameter[] parameters = {
                    new SqlParameter("@task_name", TaskName),
                    new SqlParameter("@task_description", TaskDescription),
                    new SqlParameter("@categoryId", CategoryId),
                    new SqlParameter("@userId", UserId),
                    new SqlParameter("@statusId", StatusId),
                    DueDate.HasValue
                        ? new SqlParameter("@dueDate", DueDate.Value)
                        : new SqlParameter("@dueDate", DBNull.Value)
                };

                // Ejecutar el procedimiento almacenado
                object result = ExecuteStoredProcedure("spAddTask", parameters);
                int idInsertado = Convert.ToInt32(result);
                this.Id = idInsertado;
                return idInsertado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error, the task couldn't be added: " + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza una tarea existente en la base de datos.
        /// </summary>
        /// <returns>True si la tarea se actualizó correctamente, false en caso contrario.</returns>
        /// <exception cref="Exception">Se lanza cuando ocurre un error al actualizar la tarea.</exception>
        public bool EditTask()
        {
            try
            {
                // Crear parámetros para el procedimiento almacenado
                SqlParameter[] parameters = {
                    new SqlParameter("@id", Id),
                    new SqlParameter("@task_name", TaskName),
                    new SqlParameter("@task_description", TaskDescription),
                    new SqlParameter("@categoryId", CategoryId),
                    new SqlParameter("@statusId", StatusId),
                    DueDate.HasValue
                        ? new SqlParameter("@dueDate", DueDate.Value)
                        : new SqlParameter("@dueDate", DBNull.Value)
                };

                // Ejecutar el procedimiento almacenado
                int rowsAffected = ExecuteNonQueryStoredProcedure("spUpdateTask", parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error, The task could not be updated: " + ex.Message);
            }
        }

        /// <summary>
        /// Elimina una tarea de la base de datos.
        /// </summary>
        /// <returns>True si la tarea se eliminó correctamente, false en caso contrario.</returns>
        /// <exception cref="Exception">Se lanza cuando ocurre un error al eliminar la tarea.</exception>
        public bool DeleteTask()
        {
            try
            {
                // Crear parámetros para el procedimiento almacenado
                SqlParameter[] parameters = {
                    new SqlParameter("@id", Id)
                };

                // Ejecutar el procedimiento almacenado
                int rowsAffected = ExecuteNonQueryStoredProcedure("spDeleteTask", parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error, The task could not be deleted: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todas las tareas asociadas a un usuario específico.
        /// </summary>
        /// <param name="userId">Identificador del usuario.</param>
        /// <returns>DataTable con las tareas del usuario.</returns>
        /// <exception cref="Exception">Se lanza cuando ocurre un error al recuperar las tareas.</exception>
        public static DataTable DisplayTasksByUser(int userId)
        {
            try
            {
                string query = "spDisplayTasksByUser";

                SqlParameter[] parameters = {
                    new SqlParameter("@userId", userId)
                };

                using (SqlConnection connection = DatabaseHelper.CreateConnection())
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddRange(parameters);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error displaying user's tasks: " + ex.Message);
            }
        }
    }
}