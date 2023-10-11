using Dapper;
using Microsoft.Data.SqlClient;
using kit_api.Connection;
using kit_api.Models;

namespace kit_api.Services
{
    public class CategoriaService
    {
        private SqlConnection _connection;
        public CategoriaService()
        {
            DBConnection cn = new DBConnection();
            _connection = new SqlConnection(cn.GetConnectionString());
        }
        public async Task<Categorias> ObtenerCategoria(string @categoria)
        {
            var result = await _connection.QueryAsync<Categorias>(
                sql: "sp_CategoriaConsultar",
                param: new { @categoria },
                commandType: System.Data.CommandType.StoredProcedure
                );
            Categorias categoriaResult = result.First();
            return categoriaResult;
        }

        public async Task<List<Categorias>> ObtenerCategorias()
        {
            var result = await _connection.QueryAsync<Categorias>(
                sql: "sp_CategoriaConsultar",
                param: new { @categoria = "" },
                commandType: System.Data.CommandType.StoredProcedure
                );
            List<Categorias> categoria = result.ToList();
            return categoria;
        }

        public async Task InsertarCategoria(Categorias categoria)
        {
            await _connection.ExecuteAsync(
                sql: "sp_CategoriasInsertar",
                param: new
                {
                    @categoria = categoria.Categoria,
                    @descripcion = categoria.Descripcion,
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task ActualizarCategoria(Categorias categoria)
        {
            await _connection.ExecuteAsync(
                sql: "sp_CategoriasInsertar",
                param: new
                {
                    @categoria = categoria.Categoria,
                    @descripcion = categoria.Descripcion,
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task EliminarCategoria(string codigoCategoria)
        {
            await _connection.ExecuteAsync(
                sql: "sp_CategoriasBorrar",
                param: new
                {
                    @categoria = codigoCategoria
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }
    }
}

