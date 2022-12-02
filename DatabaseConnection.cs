using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class DatabaseService
    {
        SQLiteAsyncConnection Database;

        public DatabaseService()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<HistoryModel>();
        }

        public async Task<List<HistoryModel>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<HistoryModel>().ToListAsync();
        }

        public async Task<int> SaveItemAsync(HistoryModel item)
        {
            await Init();
            return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteAllAsync()
        {
            await Init();
            return await Database.DeleteAllAsync<HistoryModel>();
        }

    }
}
