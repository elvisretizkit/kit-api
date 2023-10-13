using Dapper;
using Microsoft.Data.SqlClient;
using kit_api.Connection;
using kit_api.Models;

namespace kit_api.Services
{
    public class ContactoService
    {
        private SqlConnection _connection;
        public ContactoService()
        {
            DBConnection cn = new DBConnection();
            _connection = new SqlConnection(cn.GetConnectionString());
        }
        public async Task<Contactos> ObtenerContacto(int @cliente, int @contacto, int @activo)
        {
            var result = await _connection.QueryAsync<Contactos>(
                sql: "sp_ContactosConsultar",
                param: new 
                { 
                    @cliente ,
                    @contacto ,
                    @activo 
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
            Contactos contactoResult = result.First();
            return contactoResult;
        }

        public async Task<List<Contactos>> ObtenerContactos(int @cliente, int @contacto, int @activo)
        {
            var result = await _connection.QueryAsync<Contactos>(
                sql: "sp_ContactosConsultar",
                param: new
                {
                    @cliente,
                    @contacto,
                    @activo,
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
            List<Contactos> contactos = result.ToList();
            return contactos;
        }

        public async Task InsertarContacto(Contactos contacto)
        {
            await _connection.ExecuteAsync(
                sql: "sp_ContactosInsertar",
                param: new
                {
                    @cliente = contacto.Cliente,
                    @contacto = contacto.Contacto,
                    @nombre = contacto.Nombre,
                    @telefono = contacto.Telefono,
                    @celular = contacto.Celular,
                    @correo = contacto.Correo,
                    @puesto = contacto.Puesto,
                    @activo = contacto.Activo,
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task ActualizarContacto(Contactos contacto)
        {
            await _connection.ExecuteAsync(
                sql: "sp_ContactosInsertar",
                param: new
                {
                    @cliente = contacto.Cliente,
                    @contacto = contacto.Contacto,
                    @nombre = contacto.Nombre,
                    @telefono = contacto.Telefono,
                    @celular = contacto.Celular,
                    @correo = contacto.Correo,
                    @puesto = contacto.Puesto,
                    @activo = contacto.Activo,
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task EliminarContacto(int codigoCliente, int codigoContacto)
        {
            await _connection.ExecuteAsync(
                sql: "sp_ContactosBorrar",
                param: new
                {
                    @cliente = codigoCliente,
                    @contacto = codigoContacto
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }
    }
}
