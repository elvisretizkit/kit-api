using Dapper;
using Microsoft.Data.SqlClient;
using kit_api.Connection;
using kit_api.Models;

namespace kit_api.Services
{
    public class AsesorService
    {
        private SqlConnection _connection;
        public AsesorService() 
        {
            DBConnection cn = new DBConnection();
            _connection =  new SqlConnection(cn.GetConnectionString());
        }
        public async Task <Asesores> ObtenerAsesor(string @asesor)
        {
            var result = await _connection.QueryAsync<Asesores>(
                sql: "sp_AsesorConsultar",
                param: new { @asesor },
                commandType: System.Data.CommandType.StoredProcedure
                );
            Asesores asesorResult = result.First();
            return asesorResult;
        }

        public async Task <List<Asesores>> ObtenerAsesores(int @activos)
        {
            var result = await _connection.QueryAsync<Asesores>(
                sql: "sp_AsesoresLista", 
                param: new { @activos },
                commandType: System.Data.CommandType.StoredProcedure
                );
            List<Asesores> asesores = result.ToList();
            return asesores;
        }

        public async Task InsertarAsesor(Asesores asesor)
        {
            await _connection.ExecuteAsync(
                sql: "sp_AsesoresInsertar",
                param: new { 
                    @asesor = asesor.Asesor, 
                    @nombre = asesor.Nombre, 
                    @recurso = 0,
                    @activo = asesor.Activo
                    },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task ActualizarAsesor(Asesores asesor)
        {
            await _connection.ExecuteAsync(
                sql: "sp_AsesoresInsertar",
                param: new
                {
                    @asesor = asesor.Asesor,
                    @nombre = asesor.Nombre,
                    //TODO: Revisar que haremos con el campo recurso.
                    @recurso = 0,
                    @activo = asesor.Activo
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task EliminarAsesor(string codigoAsesor)
        {
            await _connection.ExecuteAsync(
                sql: "sp_AsesoresBorrar",
                param: new
                {
                    @asesor = codigoAsesor
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }
    }
}
