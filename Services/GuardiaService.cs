using Dapper;
using Microsoft.Data.SqlClient;
using kit_api.Connection;
using kit_api.Models;

namespace kit_api.Services
{
    public class GuardiaService
    {
        private SqlConnection _connection;
        public GuardiaService()
        {
            DBConnection cn = new DBConnection();
            _connection = new SqlConnection(cn.GetConnectionString());
        }
        public async Task<Guardias> ObtenerGuardia(int @guardia)
        {
            var result = await _connection.QueryAsync<Guardias>(
                sql: "sp_GuardiasConsultar",
                param: new { @guardia },
                commandType: System.Data.CommandType.StoredProcedure
                );
            Guardias guardiaResult = result.First();
            return guardiaResult;
        }

        public async Task<List<Guardias>> ObtenerGuardias()
        {
            var result = await _connection.QueryAsync<Guardias>(
                sql: "sp_GuardiasConsultar",
                param: new { @guardia = 0 },
                commandType: System.Data.CommandType.StoredProcedure
                );
            List<Guardias> guardias = result.ToList();
            return guardias;
        }

        public async Task<List<Guardias>> ObtenerGuardiasDeCliente(int @cliente)
        {
            var result = await _connection.QueryAsync<Guardias>(
                sql: "sp_GuardiasDeClienteConsultar",
                param: new { @cliente },
                commandType: System.Data.CommandType.StoredProcedure
                );
            List<Guardias> guardias = result.ToList();
            return guardias;
        }

        public async Task InsertarGuardia(Guardias guardia)
        {
            await _connection.ExecuteAsync(
                sql: "sp_GuardiasInsertar",
                param: guardia,
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task ActualizarGuardia(Guardias guardia)
        {
            await _connection.ExecuteAsync(
                sql: "sp_GuardiasInsertar",
                param: guardia,
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task EliminarGuardia(int codigoGuardia)
        {
            await _connection.ExecuteAsync(
                sql: "sp_GuardiasBorrar",
                param: new
                {
                    @guardia = codigoGuardia
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }
    }
}
