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

            var t1 = new Theory
            {
                Content = "Определение 1. Число d > 0 называется наибольшим общим делителем (НОД) двух целых чисел a и b, если оно удовлетворяет следующим " +
                "условиям: 1) d | a и d | b; 2) если c | a и c | b, то c | d.",
                CourseId = 0,
                Order = 1
            };
            var t2 = new Theory
            {
                Content = "Наибольший общий делитель двух чисел a и b, a ≥ b > 0, можно найти с помощью алгоритма Евклида, который основан на том, что если " +
                "a = bq + r, 0 ≤ r < b, то НОД(a, b) = НОД(r, b).",
                CourseId = 0,
                Order = 2
            };

            await db.DeleteAllAsync<Theory>();

            await db.InsertAsync(t1);
            await db.InsertAsync(t2);
        }

        public static async Task<IEnumerable<Theory>> GetTheory(int courseId)
        {
            await Init();

            var theory = await db.Table<Theory>().Where(t => t.CourseId == courseId).ToListAsync();
            //var theory = await db.QueryAsync<Theory>($"select * from Theory where CourseId={courseId}");
            return theory;
        }

        public static async Task<IEnumerable<Question>> GetQuestions(int courseId)
        {
            await Init();

            var questions = await db.QueryAsync<Question>($"select * from Question where CourseId={courseId}");
            return questions;
        }
    }
}
