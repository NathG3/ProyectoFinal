using System.Data;
using System.Data.SqlClient;
using ProyectoFinal.Model;
using ProyectoFinal.Repository;

namespace ProyectoFinal.Repository
{
    public class ADO_Usuario
    {

        public static Usuario TraerUsuario(string nombreUsuario)
        {
            var usuario = new Usuario();

            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Usuario where NombreUsuario = @IDUsu";

                var parametro = new SqlParameter();
                parametro.ParameterName = "IDUsu";
                parametro.SqlDbType = SqlDbType.VarChar;
                parametro.Value = nombreUsuario;

                cmd.Parameters.Add(parametro);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    usuario.Id = Convert.ToInt32(reader.GetValue(0));
                    usuario.Nombre = reader.GetValue(1).ToString();
                    usuario.Apellido = reader.GetValue(2).ToString();
                    usuario.NombreUsuario = reader.GetValue(3).ToString();
                    usuario.Contraseña = reader.GetValue(4).ToString();
                    usuario.Mail = reader.GetValue(5).ToString();

                }

                reader.Close();
                connection.Close();

                if (usuario.Id == 0) usuario = null;

                return usuario;

            }
        }

        internal static void AgregarUsuario(Usuario us)

        {
            if (TraerUsuario(us.NombreUsuario) == null)
            {
                using (SqlConnection connection = new SqlConnection(General.connectionString()))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "INSERT into Usuario (Nombre, Apellido, NombreUsuario, Contraseña, Mail)" +
                      " VALUES (@Nombre, @Apellido, @NombreUsuario, @Contraseña, @Mail)";

                    cmd.Parameters.Add(new SqlParameter("Nombre", us.Nombre));
                    cmd.Parameters.Add(new SqlParameter("Apellido", us.Apellido));
                    cmd.Parameters.Add(new SqlParameter("NombreUsuario", us.Nombre));
                    cmd.Parameters.Add(new SqlParameter("Contraseña", us.Contraseña));
                    cmd.Parameters.Add(new SqlParameter("Mail", us.Mail));

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public static void ModificarUsuario(Usuario us)

        ///////// revisar agregar para modificar los demas campos
        {

            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = "UPDATE Usuario SET Contraseña = @UsContraseña, Mail = @usMail WHERE NombreUsuario = 'string'";

                var paramContraseña = new SqlParameter();
                paramContraseña.ParameterName = "UsContraseña";
                paramContraseña.SqlDbType = SqlDbType.VarChar;
                paramContraseña.Value = us.Contraseña;

                var paramMail = new SqlParameter();
                paramMail.ParameterName = "usMail";
                paramMail.SqlDbType = SqlDbType.VarChar;
                paramMail.Value = us.Mail;

                cmd.Parameters.Add(paramContraseña);
                cmd.Parameters.Add(paramMail);

                cmd.ExecuteNonQuery();
                connection.Close();

            }
        }

    }
}