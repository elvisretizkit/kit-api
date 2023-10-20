using Dapper;
using Microsoft.Data.SqlClient;
using kit_api.Connection;
using kit_api.Models;
using kit_api.Controllers;

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

        public async Task InsertarRefreshToken(Refresh refreshToken)
        {
            await _connection.ExecuteAsync(
            sql: "sp_RefreshInsertar",
            param: refreshToken,
            commandType: System.Data.CommandType.StoredProcedure
            );
        }

        public async Task<Refresh> ObtenerRefreshToken(string refreshToken)
        {
            var result = await _connection.QueryAsync<Refresh>(
            sql: "sp_RefreshObtener",
            param: new { 
                @token = refreshToken
            },
            commandType: System.Data.CommandType.StoredProcedure
            );
            var _refreshToken = result.First();
            return _refreshToken;
        }


        public async Task<Usuarios> ObtenerUsuario(string @usuario)
        {
            var result = await _connection.QueryAsync<Usuarios>(
                sql: "sp_UsuarioConsultar",
                param: new { @usuario},
                commandType: System.Data.CommandType.StoredProcedure
                );
            Usuarios usuarioResult = result.First();
            return usuarioResult;

        }
        }
    }
