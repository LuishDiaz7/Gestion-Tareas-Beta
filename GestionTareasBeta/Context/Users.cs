using GestionTareasBeta.Context;
using System;
using System.Data;
using System.Data.SqlClient;

namespace GestionTareasBeta
{
    /// <summary>
    /// Clase que gestiona las operaciones relacionadas con los usuarios.
    /// </summary>
    public class Usuarios
    {
        /// <summary>
        /// Obtiene el ID de un usuario basado en su nombre de usuario o correo electrónico y su contraseña.
        /// </summary>
        /// <param name="userOrEmail">Nombre de usuario o correo electrónico del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <returns>
        /// El ID del usuario si las credenciales son válidas. 
        /// Retorna -1 si no se encuentra un usuario con las credenciales proporcionadas.
        /// </returns>
        /// <exception cref="Exception">
        /// Se lanza si ocurre un error al ejecutar la consulta en la base de datos.
        /// </exception>
        public static int GetUserId(string userOrEmail, string password)
        {
            int userId = -1; // Valor por defecto si no se encuentra el usuario

            using (SqlConnection connection = DatabaseHelper.CreateConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("spGetUserIdByLogin", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros del procedimiento almacenado
                    cmd.Parameters.AddWithValue("@UserOrEmail", userOrEmail);
                    cmd.Parameters.AddWithValue("@Password", password);

                    // Ejecuta el procedimiento almacenado y obtén el resultado
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        userId = Convert.ToInt32(result);
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener el userId: " + ex.Message);
                }
            }

            return userId;
        }
    }
}