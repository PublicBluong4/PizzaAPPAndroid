using Bluong4_Project2V4.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bluong4_Project2V4.Data
{
    interface ITableRepository
    {
        Task<List<Table>> GetTables();
        Task<List<Table>> GetTableByStatus(bool status);
        Task<Table> GetTable(int ID);
        Task UpdateTable(Table tableToUpdate);
    }
}
