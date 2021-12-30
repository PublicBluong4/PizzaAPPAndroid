using Bluong4_Project2V4.Models;
using NuGet.Common;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bluong4_Project2V4.Data
{
    public class WallpaperDatabase
    {
        static SQLiteAsyncConnection Database;
        public static readonly AsyncLazy<WallpaperDatabase> Instance = new AsyncLazy<WallpaperDatabase>(async () =>
        {
            var instance = new WallpaperDatabase();
            CreateTableResult result = await Database.CreateTableAsync<Wallpaper>();
            return instance;
        });
        public WallpaperDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }
        public Task<List<Wallpaper>> GetItemsAsync()
        {
            return Database.Table<Wallpaper>().ToListAsync();
        }

        public Task<List<Wallpaper>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<Wallpaper>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<Wallpaper> GetItemAsync(int id)
        {
            return Database.Table<Wallpaper>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Wallpaper item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Wallpaper item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
