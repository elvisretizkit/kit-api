using Dapper;
using kit_api.Connection;
using kit_api.Models;
using Microsoft.Data.SqlClient;

namespace kit_api.Services;

public static class AuthService
{
    public static async Task<Auth> Login(Auth credentials)
    {
        try
        {
            DBConnection cn = new DBConnection();
            SqlConnection sql = new SqlConnection(cn.GetConnectionString());
            Auth user = sql.Query<Auth>("EXEC sp_APILoginApi @usuario, @clave", credentials).First();
            return user;
        }
        catch (Exception ex)
        {

        }
    }
}