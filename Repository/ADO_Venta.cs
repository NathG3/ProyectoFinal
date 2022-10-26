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

        public static void CargarVenta(List<Producto> pv, int idUsuario)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT into Venta (Comentarios, IdUsuario) values ('', @idUsuario); select @@identity";

                var paramIdUsuario = new SqlParameter();
                paramIdUsuario.ParameterName = "idUsuario";
                paramIdUsuario.SqlDbType = SqlDbType.BigInt;
                paramIdUsuario.Value = idUsuario;

                cmd.Parameters.Add(paramIdUsuario);
                int ventaId = Convert.ToInt32(cmd.ExecuteScalar());
                
                List<string> productoSinStock = new List<string>();
                string prodDesc = string.Empty;
                List<string> prodDescList = new List<string>();


                foreach (var Producto in pv)
                {

                    cmd.Parameters.Add(new SqlParameter("Stock", Producto.Stock));
                    cmd.Parameters.Add(new SqlParameter("IdProd", Producto.Id));
                    cmd.Parameters.Add(new SqlParameter("IdVent", ventaId));

                    cmd.CommandText = "SELECT Stock From Producto where Id = @IdProd ";
                    int stockExistente = Convert.ToInt32(cmd.ExecuteScalar());


                    if (stockExistente >= Producto.Stock)

                    {
                        cmd.CommandText = "INSERT into ProductoVendido (Stock, IdProducto, IdVenta) " +
                          "values (@Stock, @IdProd, @IdVent)";


                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "UPDATE Producto set Stock = Stock - @Stock where Id = @IdProd";

                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "SELECT Descripciones From Producto where Id = @IdProd";
                        prodDesc = Convert.ToString(cmd.ExecuteScalar());
                        prodDescList.Add(prodDesc.ToString());
                        productoSinStock.Add(Producto.Id.ToString());

                        cmd.CommandText = "UPDATE Venta set Comentarios = @Comentarios where Id = @IdVent";

                        string comentario = "Productos vendidos: " + string.Join(", ", prodDescList);
                        cmd.Parameters.Add(new SqlParameter("Comentarios", comentario));
                        cmd.ExecuteNonQuery();
                    }

                    cmd.Parameters.Clear();
                }

                connection.Close();
            }

        }
    }
}