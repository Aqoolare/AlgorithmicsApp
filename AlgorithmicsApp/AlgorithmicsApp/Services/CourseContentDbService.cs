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
            await db.CreateTableAsync<Theory>();
            await db.CreateTableAsync<Question>();

            var t0 = new Theory
            {
                Id = 0,
                CourseId = 0,
                Order = 1,
                Title = "Определение НОД и алгоритм нахождения"
            };

            await db.DeleteAllAsync<Theory>();

            await db.InsertAsync(t0);
        }

        public static async Task<IEnumerable<Theory>> GetTheory(int courseId)
        {
            await Init();

            var theory = await db.Table<Theory>().Where(t => t.CourseId == courseId).ToListAsync();
            return theory;
        }

        //public static async Task<IEnumerable<Question>> GetQuestions(int courseId)
        //{
        //    await Init();

        //    var questions = await db.QueryAsync<Question>($"select * from Question where CourseId={courseId}");
        //    return questions;
        //}
    }
}
