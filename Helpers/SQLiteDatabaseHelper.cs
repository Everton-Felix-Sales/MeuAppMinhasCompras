using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;
        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);

            _conn.CreateTableAsync<Produto>().Wait();
        }
        public async Task<int> Insert(Produto p)
        {
            return await _conn.InsertAsync(p);
        }
        public async Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";
            return await _conn.QueryAsync<Produto>(
            sql, p.Descricao, p.Quantidade, p.Preco, p.Id
            );
        }
        public async Task<int> Delete(int id)
        {
            return await _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }
        public async  Task<List<Produto>> GetAll()
        {
            return await _conn.Table<Produto>().ToListAsync();
        }
        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%" + q + "%'";
            return _conn.QueryAsync<Produto>(sql);
        }
    }
}
