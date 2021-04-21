using AlgorithmicsApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AlgorithmicsApp.Services
{
    class TheoryContentDbService
    {
        static SQLiteAsyncConnection db = null;

        static async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<TheoryContent>();
            await db.CreateTableAsync<Link>();

            //var tc00 = new TheoryContent
            //{
            //    TheoryId = 0,
            //    BoldText = "Определение 1.",
            //    Text1 = " Число d > 0 называется наибольшим общим делителем (НОД) двух целых чисел a и b, если оно удовлетворяет следующим " +
            //    "условиям: 1) d | a и d | b; 2) если c | a и c | b, то c | d.",
            //    LinkId = -1
            //};
            //var tc01 = new TheoryContent
            //{
            //    TheoryId = 0,
            //    Text1 = "Наибольший общий делитель двух чисел",
            //    Formula = @"a \quad и  \quad b,\quad a\geq b> 0,",
            //    LinkId = -1
            //};
            //var tc02 = new TheoryContent
            //{
            //    TheoryId = 0,
            //    Text1 = "можно найти с помощью алгоритма Евклида, который основан на том, что если",
            //    Formula = @"a=bq+r,\quad0\leq r<b,",
            //    LinkId = -1
            //};
            //var tc03 = new TheoryContent
            //{
            //    TheoryId = 0,
            //    Text1 = "то НОД(a, b) = НОД(r, b).",
            //    LinkId = -1
            //};

            //var link0 = new Link
            //{
            //    Id = 0,
            //    Text = "Это ссылка",
            //    Page = "TheoryPage",
            //    ElementIndex = 4
            //};

            //await db.DeleteAllAsync<TheoryContent>();
            //await db.DeleteAllAsync<Link>();

            //await db.InsertAsync(tc00);
            //await db.InsertAsync(tc01);
            //await db.InsertAsync(tc02);
            //await db.InsertAsync(tc03);
            //await db.InsertAsync(link0);
        }

        public static async Task<IEnumerable<TheoryContent>> GetTheoryContent(int theoryId)
        {
            await Init();

            var theoryContent = await db.Table<TheoryContent>().Where(tc => tc.TheoryId == theoryId).ToListAsync();
            return theoryContent;
        }

        public static async Task<Link> GetLinkById(int linkId)
        {
            await Init();

            var link = await db.Table<Link>().Where(l => l.Id == linkId).FirstOrDefaultAsync();
            return link;
        }
    }
}
