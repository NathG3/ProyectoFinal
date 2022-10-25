using System.Data;
using System.Data.SqlClient;
using ProyectoFinal.Model;
using ProyectoFinal.Repository;

namespace ProyectoFinal.Repository
{
    public class ADO_Venta
    {

        public static List<Venta> TraerVentaPorUsuario(int idUsuario)
        {
            var listaVentas = new List<Venta>();

            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {

                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Venta where IdUsuario = @idUs";

                var parametro = new SqlParameter();
                parametro.ParameterName = "idUs";
                parametro.SqlDbType = SqlDbType.BigInt;
                parametro.Value = idUsuario;

                cmd.Parameters.Add(parametro);

                var reader3 = cmd.ExecuteReader();

                while (reader3.Read())
                {
                    var venta = new Venta();

                    venta.Id = Convert.ToInt32(reader3.GetValue(0));
                    venta.Comentarios = reader3.GetValue(1).ToString();
                    venta.IdUsuario = Convert.ToInt32(reader3.GetValue(2));

                    listaVentas.Add(venta);

                }


                reader3.Close();
                connection.Close();

                return listaVentas;

            }

        }
    }
}


