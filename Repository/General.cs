using System.Data.SqlClient;


namespace ProyectoFinal.Repository
{
    public class General
    {
        public static string connectionString()

        {
            SqlConnectionStringBuilder conecctionbuilder = new SqlConnectionStringBuilder();
            conecctionbuilder.DataSource = "LAPTOP-OV95KCR8";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;
            var cs = conecctionbuilder.ConnectionString;
            return (cs);

        }
    }
}