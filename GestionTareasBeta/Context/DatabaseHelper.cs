using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GestionTareasBeta.Context
{
    /// <summary>
    /// Proporciona métodos de utilidad para gestionar la conexión con la base de datos.
    /// </summary>
    class DatabaseHelper
    {
        /// <summary>
        /// Cadena de conexión para establecer comunicación con la base de datos.
        /// </summary>
        private static readonly string ConnectionString = "server=localhost; Initial Catalog=ProyectoAdo; Integrated Security=True; trustServerCertificate=True";

        /// <summary>
        /// Crea una nueva instancia de <see cref="SqlConnection"/> con la cadena de conexión configurada.
        /// </summary>
        /// <returns>Un objeto <see cref="SqlConnection"/> listo para ser utilizado.</returns>
        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}

