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

        internal static void ModificarProducto(int prd, Producto pr2)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();


                cmd.CommandText = "UPDATE Producto SET Descripcion = @PrdDesc, Costo = @PrdCosto, PrecioVenta = @PrdPrecioVenta," +
                    " Stock = @PrdStock,IdUsuario= @PrdIdUsu WHERE  Id = @prd";


                var paramDescripciones = new SqlParameter();
                paramDescripciones.ParameterName = "PrdDesc";
                paramDescripciones.SqlDbType = SqlDbType.VarChar;
                paramDescripciones.Value = pr2.Descripciones;


                var paramCosto = new SqlParameter();
                paramCosto.ParameterName = "PrdCosto";
                paramCosto.SqlDbType = SqlDbType.VarChar;
                paramCosto.Value = pr2.Costo;

                var paramPrecioVenta = new SqlParameter();
                paramPrecioVenta.ParameterName = "PrdPrecioVenta";
                paramPrecioVenta.SqlDbType = SqlDbType.VarChar;
                paramPrecioVenta.Value = pr2.PrecioVenta;

                var paramStock = new SqlParameter();
                paramStock.ParameterName = "PrdStock";
                paramStock.SqlDbType = SqlDbType.VarChar;
                paramStock.Value = pr2.Stock;

                var paramIdUsuario = new SqlParameter();
                paramIdUsuario.ParameterName = "PrdIdUsu";
                paramIdUsuario.SqlDbType = SqlDbType.VarChar;
                paramIdUsuario.Value = pr2.IdUsuario;

                cmd.Parameters.Add(paramDescripciones);
                cmd.Parameters.Add(paramCosto);
                cmd.Parameters.Add(paramPrecioVenta);
                cmd.Parameters.Add(paramStock);
                cmd.Parameters.Add(paramIdUsuario);

                cmd.ExecuteNonQuery();
                connection.Close();



            }
        }

        internal static void AgregarProducto(Producto pr)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT into Producto (Descripciones, Costo, PrecioVenta, Stock, IdUsuario)" +
                    " VALUES (@PrdDesc, @PrdCosto, @PrdPrecioVenta, @PrdStock, @PrdIdUsu)";


                //cmd.Parameters.Add(new SqlParameter("NombreUsu", nombreUsuario));
                //cmd.Parameters.Add(new SqlParameter("Contraseña", contraseña));


                /////////////falta validar los datos obligatorios
                /////////////agregar validacion para saber si existe el userid 

                var paramDescripciones = new SqlParameter();
                paramDescripciones.ParameterName = "PrdDesc";
                paramDescripciones.SqlDbType = SqlDbType.VarChar;
                paramDescripciones.Value = pr.Descripciones;


                var paramCosto = new SqlParameter();
                paramCosto.ParameterName = "PrdCosto";
                paramCosto.SqlDbType = SqlDbType.VarChar;
                paramCosto.Value = pr.Costo;

                var paramPrecioVenta = new SqlParameter();
                paramPrecioVenta.ParameterName = "PrdPrecioVenta";
                paramPrecioVenta.SqlDbType = SqlDbType.VarChar;
                paramPrecioVenta.Value = pr.PrecioVenta;

                var paramStock = new SqlParameter();
                paramStock.ParameterName = "PrdStock";
                paramStock.SqlDbType = SqlDbType.VarChar;
                paramStock.Value = pr.Stock;

                var paramIdUsuario = new SqlParameter();
                paramIdUsuario.ParameterName = "PrdIdUsu";
                paramIdUsuario.SqlDbType = SqlDbType.VarChar;
                paramIdUsuario.Value = pr.IdUsuario;

                cmd.Parameters.Add(paramDescripciones);
                cmd.Parameters.Add(paramCosto);
                cmd.Parameters.Add(paramPrecioVenta);
                cmd.Parameters.Add(paramStock);
                cmd.Parameters.Add(paramIdUsuario);

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


