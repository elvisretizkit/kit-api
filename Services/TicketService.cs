using Dapper;
using Microsoft.Data.SqlClient;
using kit_api.Connection;
using kit_api.Models;

namespace kit_api.Services
{
    public class TicketService
    {
        private SqlConnection _connection;
        public TicketService()
        {
            DBConnection cn = new DBConnection();
            _connection = new SqlConnection(cn.GetConnectionString());
        }
        public async Task<Tickets> ObtenerTicket(int @ticket)
        {
            var result = await _connection.QueryAsync<Tickets>(
                sql: "sp_TicketConsultar",
                param: new { @ticket },
                commandType: System.Data.CommandType.StoredProcedure
                );
            Tickets clienteResult = result.First();
            return clienteResult;
        }

        public async Task InsertarTicket(Tickets ticket)
        {
            await _connection.ExecuteAsync(
                sql: "sp_TicketInsertar",
                param: new
                {
                    @ticket = ticket.Ticket,
                    @fechaRegistro = ticket.FechaRegistro,
                    @tema = ticket.Tema,
                    @estatus = ticket.Estatus,
                    @cliente = ticket.Cliente,
                    @contacto = ticket.Contacto,
                    @prioridad = ticket.Prioridad,
                    @asesor = ticket.Asesor,
                    @categoria = ticket.Categoria,
                    @facturable = ticket.Facturable,
                    @fechaProgramado = ticket.FechaProgramado
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task ActualizarTicket(Tickets ticket)
        {
            await _connection.ExecuteAsync(
                sql: "sp_TicketInsertar",
                param: new
                {
                    @ticket = ticket.Ticket,
                    @fechaRegistro = ticket.FechaRegistro,
                    @tema = ticket.Tema,
                    @estatus = ticket.Estatus,
                    @cliente = ticket.Cliente,
                    @contacto = ticket.Contacto,
                    @prioridad = ticket.Prioridad,
                    @asesor = ticket.Asesor,
                    @categoria = ticket.Categoria,
                    @facturable = ticket.Facturable,
                    @fechaProgramado = ticket.FechaProgramado
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task EliminarTicket(int codigoTicket)
        {
            await _connection.ExecuteAsync(
                sql: "sp_TicketBorrar",
                param: new
                {
                    @ticket = codigoTicket
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }
    }
}
