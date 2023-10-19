using Dapper;
using Microsoft.Data.SqlClient;
using kit_api.Connection;
using kit_api.Models;

namespace kit_api.Services
{
    public class UsuarioService
    {
        private SqlConnection _connection;
        public UsuarioService()
        {
            DBConnection cn = new DBConnection();
            _connection = new SqlConnection(cn.GetConnectionString());
        }

        public async Task<Usuarios> Login(string @usuario, string @password) {
            var result = await _connection.QueryAsync<Usuarios>(
                sql: "sp_APILogin",
                param: new { @usuario, @password },
                commandType: System.Data.CommandType.StoredProcedure
                );
            Usuarios usuarioResult = result.First();
            return usuarioResult;
        }

        public async Task InsertarUsuario(Usuarios usuario)
        {
            await _connection.ExecuteAsync(
            sql: "sp_WUsuariosInsertar",
            param: new
            {
                @usuario = usuario.Usuario,
                @nombre = usuario.Nombre,
                @tipo = usuario.Tipo,
                @cliente = usuario.Cliente,
                @password = usuario.Password,
                @asesor = usuario.Asesor,
                @contacto = usuario.Contacto,
                @activo = usuario.Activo
            },
            commandType: System.Data.CommandType.StoredProcedure
            );
        }
    }
}
