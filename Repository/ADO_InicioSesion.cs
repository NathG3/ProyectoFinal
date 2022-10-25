using System.Data;
using System.Data.SqlClient;
using ProyectoFinal.Model;
using ProyectoFinal.Repository;

namespace ProyectoFinal.Repository
{
    public class ADO_InicioSesion
    {
        public static Usuario InicioSesion(string nombreUsuario, string contraseña)
        {
            var datosUsuario = new Usuario();

            using (SqlConnection connection = new SqlConnection(General.connectionString()))

            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText =
                    "SELECT * FROM Usuario where NombreUsuario = @NombreUsu and Contraseña = @Contraseña";

                cmd.Parameters.Add(new SqlParameter("NombreUsu", nombreUsuario));
                cmd.Parameters.Add(new SqlParameter("Contraseña", contraseña));

                var reader4 = cmd.ExecuteReader();

                if (reader4.HasRows)
                {

                    datosUsuario = ADO_Usuario.TraerUsuario(nombreUsuario);

                }
                else
                {
                    datosUsuario = new Usuario();
                }

                reader4.Close();
                connection.Close();


                return datosUsuario;
            }

        }
    }
}


