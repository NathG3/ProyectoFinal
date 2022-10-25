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

                return usuario;


            }
        }

        internal static void AgregarUsuario(Usuario us)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT into Usuario (Nombre, Apellido, NombreUsuario, Contraseña, Mail)" +
                    " VALUES (@Nombre, @Apellido, @NombreUsuario, @Contraseña, @Mail)";

                //cmd.Parameters.Add(new SqlParameter("NombreUsu", nombreUsuario));
                //cmd.Parameters.Add(new SqlParameter("Contraseña", contraseña));


                var paramNombre = new SqlParameter();
                paramNombre.ParameterName = "Nombre";
                paramNombre.SqlDbType = SqlDbType.VarChar;
                paramNombre.Value = us.Nombre;


                var paramApelido = new SqlParameter();
                paramApelido.ParameterName = "Apellido";
                paramApelido.SqlDbType = SqlDbType.VarChar;
                paramApelido.Value = us.Apellido;

                var paramNombreUsuario = new SqlParameter();
                paramNombreUsuario.ParameterName = "NombreUsuario";
                paramNombreUsuario.SqlDbType = SqlDbType.VarChar;
                paramNombreUsuario.Value = us.NombreUsuario;

                var paramContraseña = new SqlParameter();
                paramContraseña.ParameterName = "Contraseña";
                paramContraseña.SqlDbType = SqlDbType.VarChar;
                paramContraseña.Value = us.Contraseña;

                var paramMail = new SqlParameter();
                paramMail.ParameterName = "Mail";
                paramMail.SqlDbType = SqlDbType.VarChar;
                paramMail.Value = us.Mail;

                cmd.Parameters.Add(paramNombre);
                cmd.Parameters.Add(paramApelido);
                cmd.Parameters.Add(paramNombreUsuario);
                cmd.Parameters.Add(paramContraseña);
                cmd.Parameters.Add(paramMail);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void ModificarUsuario(Usuario us)

            //////////Falta validacion si existe usuario o no
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

