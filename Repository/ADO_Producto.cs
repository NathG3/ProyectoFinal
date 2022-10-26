using System.Data;
using System.Data.SqlClient;
using ProyectoFinal.Model;
using ProyectoFinal.Repository;

namespace ProyectoFinal.Repository
{
 
    public class ADO_Producto
    {

        public static List<Producto> DevolverProducto(int idUsuario)
        {
            var listaProducto = new List<Producto>();
            using (SqlConnection connection = new SqlConnection(General.connectionString()))

            {

                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Producto where IdUsuario = @idUs";

                var parametro = new SqlParameter();
                parametro.ParameterName = "idUs";
                parametro.SqlDbType = SqlDbType.BigInt;
                parametro.Value = idUsuario;

                cmd.Parameters.Add(parametro);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var producto = new Producto();

                    producto.Id = Convert.ToInt32(reader.GetValue(0));
                    producto.Descripciones = reader.GetValue(1).ToString();
                    producto.Costo = Convert.ToDouble(reader.GetValue(2));
                    producto.PrecioVenta = Convert.ToDouble(reader.GetValue(3));
                    producto.Stock = Convert.ToInt32(reader.GetValue(4));
                    producto.IdUsuario = Convert.ToInt32(reader.GetValue(5));

                    listaProducto.Add(producto);
                }

                reader.Close();
                connection.Close();

                return listaProducto;

            }

        }

        public static void ModificarProducto(Producto pr)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = "UPDATE Producto SET Descripciones = @PrdDesc, Costo = @PrdCosto, PrecioVenta = @PrdPrecioVenta," +
                    " Stock = @PrdStock,IdUsuario= @PrdIdUsu WHERE  Id = @PrdId";

                cmd.Parameters.Add(new SqlParameter("PrdId", pr.Id));
                cmd.Parameters.Add(new SqlParameter("PrdDesc", pr.Descripciones));
                cmd.Parameters.Add(new SqlParameter("PrdCosto", pr.Costo));
                cmd.Parameters.Add(new SqlParameter("PrdPrecioVenta", pr.PrecioVenta));
                cmd.Parameters.Add(new SqlParameter("PrdStock", pr.Descripciones));
                cmd.Parameters.Add(new SqlParameter("PrdIdUsu", pr.IdUsuario));

                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void AgregarProducto(Producto pr)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT into Producto (Descripciones, Costo, PrecioVenta, Stock, IdUsuario)" +
                    " VALUES (@PrdDesc, @PrdCosto, @PrdPrecioVenta, @PrdStock, @PrdIdUsu)";

                /////////////revisar falta validar los datos obligatorios

                cmd.Parameters.Add(new SqlParameter("PrdDesc", pr.Descripciones));
                cmd.Parameters.Add(new SqlParameter("PrdCosto", pr.Costo));
                cmd.Parameters.Add(new SqlParameter("PrdPrecioVenta", pr.PrecioVenta));
                cmd.Parameters.Add(new SqlParameter("PrdStock", pr.Descripciones));
                cmd.Parameters.Add(new SqlParameter("PrdIdUsu", pr.IdUsuario));

                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void EliminarProducto(int id)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {


                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM ProductoVendido where IdProducto = @idPrd";
                

                var param = new SqlParameter();
                param.ParameterName = "idPrd";
                param.SqlDbType = SqlDbType.BigInt;
                param.Value = id;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();

                cmd.CommandText = "DELETE FROM Producto where Id = @idPrd";
                cmd.ExecuteNonQuery();

                connection.Close();


            }
        }
    }
}


