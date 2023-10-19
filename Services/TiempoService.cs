using Dapper;
using kit_api.Connection;
using kit_api.Models;
using Microsoft.Data.SqlClient;

namespace kit_api.Services
{
    public class TiempoService
    {
        private SqlConnection _connection;
        public TiempoService()
        {
            DBConnection cn = new DBConnection();
            _connection = new SqlConnection(cn.GetConnectionString());
        }
        public async Task<Tiempos> ObtenerTiempo(int @ticket, int @folio)
        {
            var result = await _connection.QueryAsync<Tiempos>(
                sql: "sp_TiemposConsultar",
                param: new { @ticket, @folio },
                commandType: System.Data.CommandType.StoredProcedure
                );
            Tiempos tiempoResult = result.First();
            return tiempoResult;
        }

        public async Task<List<Tiempos>> ObtenerTiempos(int @ticket, int @folio)
        {
            var result = await _connection.QueryAsync<Tiempos>(
                sql: "sp_TiemposConsultar",
                param: new { @ticket, @folio=0 },
                commandType: System.Data.CommandType.StoredProcedure
                );
            List<Tiempos> tiempoResult = result.ToList();
            return tiempoResult;
        }


        public async Task InsertarTiempo(Tiempos tiempo)
        {
            await _connection.ExecuteAsync(
                sql: "sp_TiemposInsertar",
                param: new
                {
                    @ticket = tiempo.Ticket,
                    @folio = tiempo.Folio,
                    @asesor = tiempo.Asesor,
                    @fechaSoporte = tiempo.FechaSoporte,
                    @tiempo = tiempo.Tiempo,
                    @descripcion = tiempo.Descripcion
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task ActualizarTiempo(Tiempos tiempo)
        {
            await _connection.ExecuteAsync(
                sql: "sp_TiemposInsertar",
                param: new
                {
                    @ticket = tiempo.Ticket,
                    @folio = tiempo.Folio,
                    @asesor = tiempo.Asesor,
                    @fechaSoporte = tiempo.FechaSoporte,
                    @tiempo = tiempo.Tiempo,
                    @descripcion = tiempo.Descripcion
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task EliminarTiempo(int codigoTicket, int codigoFolio)
        {
            await _connection.ExecuteAsync(
                sql: "sp_TiemposBorrar",
                param: new
                {
                    @ticket = codigoTicket,
                    @folio = codigoFolio
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }
    }
}
