using Dapper;
using Microsoft.Data.SqlClient;
using kit_api.Connection;
using kit_api.Models;

namespace kit_api.Services
{
    public class ClienteService
    {
        private SqlConnection _connection;
        public ClienteService()
        {
            DBConnection cn = new DBConnection();
            _connection = new SqlConnection(cn.GetConnectionString());
        }
        public async Task<Clientes> ObtenerCliente(string @cliente)
        {
            var result = await _connection.QueryAsync<Clientes>(
                sql: "sp_ClienteConsultar",
                param: new { @cliente },
                commandType: System.Data.CommandType.StoredProcedure
                );
            Clientes clienteResult = result.First();
            return clienteResult;
        }

        public async Task<List<Categorias>> ObtenerClientes()
        {
            var result = await _connection.QueryAsync<Clientes>(
                sql: "sp_ClienteConsultar",
                param: new { @cliente = 0 },
                commandType: System.Data.CommandType.StoredProcedure
                );
            List<Clientes> cliente = result.ToList();
            return cliente;
        }

        public async Task InsertarCliente(Clientes cliente)
        {
            await _connection.ExecuteAsync(
                sql: "sp_ClientesInsertar",
                param: new
                {
                    @cliente = cliente.Cliente,
                    @razonSocial = cliente.RazonSocial,
                    @activo = cliente.Activo,
                    @polizaanual = cliente.PolizaAnual,
                    @contacto = cliente.Contacto,
                    @correo = cliente.Correo,
                    @nombreCorto = cliente.NombreCorto,
                    @nombreCotizacion = cliente.NombreCotizacion,
                    @categoria = cliente.Categoria,
                    @direccion = cliente.Direccion,
                    @colonia = cliente.Colonia,
                    @ciudad = cliente.Ciudad,
                    @estado = cliente.Estado,
                    @version = cliente.Version,
                    @horasContrato = cliente.HorasContrato,
                    @telefono = cliente.Telefono,
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task ActualizarClientes(Clientes cliente)
        {
            await _connection.ExecuteAsync(
                sql: "sp_ClientesInsertar",
                param: new
                {
                    @cliente = cliente.Cliente,
                    @razonSocial = cliente.RazonSocial,
                    @activo = cliente.Activo,
                    @polizaanual = cliente.PolizaAnual,
                    @contacto = cliente.Contacto,
                    @correo = cliente.Correo,
                    @nombreCorto = cliente.NombreCorto,
                    @nombreCotizacion = cliente.NombreCotizacion,
                    @categoria = cliente.Categoria,
                    @direccion = cliente.Direccion,
                    @colonia = cliente.Colonia,
                    @ciudad = cliente.Ciudad,
                    @estado = cliente.Estado,
                    @version = cliente.Version,
                    @horasContrato = cliente.HorasContrato,
                    @telefono = cliente.Telefono,
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task EliminarCliente(string codigoCliente)
        {
            await _connection.ExecuteAsync(
                sql: "sp_ClientesBorrar",
                param: new
                {
                    @cliente = codigoCliente
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }
    }
}
