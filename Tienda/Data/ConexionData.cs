using System.Data.SqlClient;
using System.Data;
using Tienda.Models;

namespace Tienda.Data
{
    public class ConexionData
    {
        private readonly string connectionSQL;
        string conexionAlter = "Data Source=EDRE11;Initial Catalog =SistemaVentas;Integrated Security=true";
        private readonly IConfiguration configuration;
        ConfigurationManager ConfigurationManager { get; set; }
        public ConexionData()
        {
            if (connectionSQL is null)
            {
                connectionSQL = ConfigurationManager.GetConnectionString("conexionProducto");
            }
        }
        public ConexionData(IConfiguration Config)
        {
            configuration = Config;
            connectionSQL = configuration.GetConnectionString("ConexionAuto");
        }
        public async Task<ResponseModel> GetAllProductAsync()
        {
            try
            {
                
                List<ProductoModel> productos = new List<ProductoModel>();
                using (SqlConnection connection = new SqlConnection(!string.IsNullOrEmpty(connectionSQL) ? connectionSQL : conexionAlter))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_Lista_Producto", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rd = await cmd.ExecuteReaderAsync();
                    if (rd.HasRows)
                    {
                        while (await rd.ReadAsync())
                        {
                            productos.Add(new ProductoModel()
                            {
                                Id = !rd.IsDBNull(0)? rd.GetInt32(0) : 0,
                                CodigoBarra = !rd.IsDBNull(1) ? rd.GetInt64(1) : 0,
                                Nombre = !rd.IsDBNull(2) ? rd.GetString(2) : "",
                                Marca = !rd.IsDBNull(3) ? rd.GetString(3) : "",
                                Categoria = !rd.IsDBNull(4) ? rd.GetString(4) : "",
                                Precio = (decimal)(!rd.IsDBNull(5) ? rd.GetSqlDecimal(5) : 0),
                            });
                        }
                    }
                }
                return new ResponseModel()
                {
                    IsSuccess = true,
                    ObjectResponse = productos,
                    Message = "OK"
                };
            }
            catch (Exception e)
            {
                return new ResponseModel()
                {
                    IsSuccess = false,
                    MessageException = e.Message,
                    MessageStackTrace = e.StackTrace
                };
            }
            
        }
        public async Task<ResponseModel> GetProductAsync(int id)
        {
            try
            {

                ProductoModel producto = new ProductoModel();
                using (SqlConnection connection = new SqlConnection(!string.IsNullOrEmpty(connectionSQL) ? connectionSQL : conexionAlter))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_Producto", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdProducto", SqlDbType.Int).Value = id;
                    SqlDataReader rd = await cmd.ExecuteReaderAsync();
                    if (rd.HasRows)
                    {
                        while (await rd.ReadAsync())
                        {
                            producto.Id = !rd.IsDBNull(0) ? rd.GetInt32(0) : 0;
                            producto.CodigoBarra = !rd.IsDBNull(1) ? rd.GetInt64(1) : 0;
                            producto.Nombre = !rd.IsDBNull(2) ? rd.GetString(2) : "";
                            producto.Marca = !rd.IsDBNull(3) ? rd.GetString(3) : "";
                            producto.Categoria = !rd.IsDBNull(4) ? rd.GetString(4) : "";
                            producto.Precio = (decimal)(!rd.IsDBNull(5) ? rd.GetSqlDecimal(5) : 0);
                        };
                    }
                }
                return new ResponseModel()
                {
                    IsSuccess = true,
                    ObjectResponse = producto,
                    Message = "OK"
                };
            }
            catch (Exception e)
            {
                return new ResponseModel()
                {
                    IsSuccess = false,
                    MessageException = e.Message,
                    MessageStackTrace = e.StackTrace
                };
            }

        }
        public async Task<ResponseModel> PostProducto(ProductoModel producto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(!string.IsNullOrEmpty(connectionSQL) ? connectionSQL : conexionAlter))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar_Producto", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@codigobarra", SqlDbType.BigInt).Value = producto.CodigoBarra;
                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = producto.Nombre;
                    cmd.Parameters.Add("@marca", SqlDbType.VarChar).Value = producto.Marca;
                    cmd.Parameters.Add("@categoria", SqlDbType.VarChar).Value = producto.Categoria;
                    cmd.Parameters.Add("@precio", SqlDbType.Decimal).Value = producto.Precio;
                    SqlDataReader rd = await cmd.ExecuteReaderAsync();  
                    if (rd.RecordsAffected>0)
                    {

                    }
                }
                return new ResponseModel()
                {
                    IsSuccess = true,
                    ObjectResponse = producto,
                    Message = "OK"
                };
            }
            catch (Exception e)
            {
                return new ResponseModel()
                {
                    IsSuccess = false,
                    MessageException = e.Message,
                    MessageStackTrace = e.StackTrace
                };
            }

        }
        public async Task<ResponseModel> EditProduct(ProductoModel producto)
        {
            try
            {
                bool success = false;
                using (SqlConnection connection = new SqlConnection(!string.IsNullOrEmpty(connectionSQL) ? connectionSQL : conexionAlter))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_Editar_Producto", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdProducto", SqlDbType.Int).Value = producto.Id == 0 ? DBNull.Value : producto.Id;
                    cmd.Parameters.Add("@codigobarra", SqlDbType.BigInt).Value = producto.CodigoBarra == 0 ? DBNull.Value : producto.CodigoBarra;
                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = string.IsNullOrEmpty(producto.Nombre) ? DBNull.Value : producto.Nombre;
                    cmd.Parameters.Add("@marca", SqlDbType.VarChar).Value = string.IsNullOrEmpty(producto.Marca) ? DBNull.Value: producto.Marca;
                    cmd.Parameters.Add("@categoria", SqlDbType.VarChar).Value = string.IsNullOrEmpty(producto.Categoria) ? DBNull.Value : producto.Categoria;
                    cmd.Parameters.Add("@precio", SqlDbType.Decimal).Value = producto.Precio == 0 ? DBNull.Value : producto.Precio;
                    SqlDataReader rd = await cmd.ExecuteReaderAsync();
                    if (rd.RecordsAffected > 0)
                        success = true;
                }
                return new ResponseModel()
                {
                    IsSuccess = success,
                    Message = "OK"
                };
            }
            catch (Exception e)
            {
                return new ResponseModel()
                {
                    IsSuccess = false,
                    MessageException = e.Message,
                    MessageStackTrace = e.StackTrace
                };
            }
        }
        public async Task<ResponseModel> DeleteProduct(int Id)
        {
            try
            {
                bool success = false;
                using (SqlConnection connect = new SqlConnection(String.IsNullOrEmpty(connectionSQL) ? conexionAlter : connectionSQL))
                {
                    await connect.OpenAsync();
                    SqlCommand cmd = new SqlCommand("sp_Eliminar_Producto", connect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdProducto", SqlDbType.Int).Value = Id;
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    if (dr.RecordsAffected > 0)
                        success = true;
                }
                return new ResponseModel()
                {
                    IsSuccess = success,
                    Message = "OK"
                };
            }
            catch (Exception e)
            {
                return new ResponseModel()
                {
                    IsSuccess = false,
                    MessageException = e.Message,
                    MessageStackTrace = e.StackTrace
                };
            }
        }
    }
}
