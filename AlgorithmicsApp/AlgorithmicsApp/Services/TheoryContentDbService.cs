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

            var tc00 = new TheoryContent
            {
                TheoryId = 0,
                Text1 = "Определение 1. Число d > 0 называется наибольшим общим делителем (НОД) двух целых чисел a и b, если оно удовлетворяет следующим " +
                "условиям: 1) d | a и d | b; 2) если c | a и c | b, то c | d.",
                Formula = @"\frac\sqrt0",
                LinkId = 0
            };
            var tc01 = new TheoryContent
            {
                TheoryId = 0,
                Text1 = "Привет1",
                Formula = @"\frac\sqrt1",
            };
            var tc02 = new TheoryContent
            {
                TheoryId = 0,
                Text1 = "Привет2",
                Formula = @"\frac\sqrt2",
            };
            var tc03 = new TheoryContent
            {
                TheoryId = 0,
                Text1 = "Привет3",
                Formula = @"\frac\sqrt3",
            };
            var tc04 = new TheoryContent
            {
                TheoryId = 0,
                Text1 = "Привет4",
                Formula = @"\frac\sqrt4",
            };
            var tc05 = new TheoryContent
            {
                TheoryId = 0,
                Text1 = "Привет5",
                Formula = @"\frac\sqrt5",
            };
            var tc06 = new TheoryContent
            {
                TheoryId = 0,
                Text1 = "Привет6",
                Formula = @"\frac\sqrt6",
            };
            var tc07 = new TheoryContent
            {
                TheoryId = 0,
                Text1 = "Привет7",
                Formula = @"\frac\sqrt7",
            };

            var link0 = new Link
            {
                Id = 0,
                Text = "пися)",
                Page = "TheoryPage",
                ElementIndex = 4
            };

            await db.DeleteAllAsync<TheoryContent>();
            await db.DeleteAllAsync<Link>();

            await db.InsertAsync(tc00);
            await db.InsertAsync(tc01);
            await db.InsertAsync(tc02);
            await db.InsertAsync(tc03);
            await db.InsertAsync(tc04);
            await db.InsertAsync(tc05);
            await db.InsertAsync(tc06);
            await db.InsertAsync(tc07);
            await db.InsertAsync(link0);
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
