using AlgorithmicsApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Linq.Expressions;

namespace AlgorithmicsApp.Services
{
    class CourseContentDbService
    {
        static SQLiteAsyncConnection db = null;
        static async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);
            //await db.CreateTableAsync<Theory>();
            //await db.CreateTableAsync<Question>();
            //await db.CreateTableAsync<TheoryContent>();

            //var t0 = new Theory
            //{
            //    Id = 0,
            //    CourseId = 0,
            //    Order = 1,
            //    Title = "Определение НОД"
            //};

            //var tc00 = new TheoryContent
            //{
            //    Id = 0,
            //    TheoryId = 0,
            //    Content = "Определение 1. Число d > 0 называется наибольшим общим делителем (НОД) двух целых чисел a и b, если оно удовлетворяет следующим " +
            //    "условиям: 1) d | a и d | b; 2) если c | a и c | b, то c | d.",
            //    ContentType = "Текст"
            //};

            //var t1 = new Theory
            //{
            //    Id = 1,
            //    CourseId = 0,
            //    Order = 2,
            //    Title = "Как найти НОД?"
            //};

            //var tc10 = new TheoryContent
            //{
            //    Id = 1,
            //    TheoryId = 1,
            //    Content = "Наибольший общий делитель двух чисел a и b, a ≥ b > 0, можно найти с помощью алгоритма Евклида, который основан на том, что если " +
            //    "a = bq + r, 0 ≤ r < b, то НОД(a, b) = НОД(r, b).",
            //    ContentType = "Текст"
            //};

            //await db.DeleteAllAsync<Theory>();
            //await db.DeleteAllAsync<TheoryContent>();

            //await db.InsertAsync(t0);
            //await db.InsertAsync(t1);
            //await db.InsertAsync(tc00);
            //await db.InsertAsync(tc10);
        }

        public static async Task<IEnumerable<Theory>> GetTheory(int courseId)
        {
            await Init();

            var theory = await db.Table<Theory>().Where(t => t.CourseId == courseId).ToListAsync();
            return theory;
        }

        public static async Task<IEnumerable<TheoryContent>> GetTheoryContent(int theoryId)
        {
            await Init();

            var theoryContent = await db.Table<TheoryContent>().Where(tc => tc.TheoryId == theoryId).ToListAsync();
            return theoryContent;
        }

        public static async Task<IEnumerable<Question>> GetQuestions(int courseId)
        {
            await Init();

            var questions = await db.QueryAsync<Question>($"select * from Question where CourseId={courseId}");
            return questions;
        }
    }
}
