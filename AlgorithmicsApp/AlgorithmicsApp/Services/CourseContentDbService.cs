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
            await db.CreateTableAsync<TheoryContent>();

            var t0 = new Theory
            {
                Id = 0,
                CourseId = 0,
                Order = 1,
                Title = "Определение НОД и алгоритм нахождения"
            };

            var tc00 = new TheoryContent
            {
                TheoryId = 0,
                Text = "Определение 1. Число d > 0 называется наибольшим общим делителем (НОД) двух целых чисел a и b, если оно удовлетворяет следующим " +
                "условиям: 1) d | a и d | b; 2) если c | a и c | b, то c | d.",
            };

            var tc01 = new TheoryContent
            {
                TheoryId = 0,
                Text = "Наибольший общий делитель двух чисел a и b, a ≥ b > 0, можно найти с помощью алгоритма Евклида, который основан на том, что если " +
                "a = bq + r, 0 ≤ r < b, то НОД(a, b) = НОД(r, b).",
            };

            var tc02 = new TheoryContent
            {
                TheoryId = 0,
                Text = "Алгоритм Евклида состоит из следующих шагов вычисления",
            };

            var tc03 = new TheoryContent
            {
                TheoryId = 0,
                Formula = "Здесь надо задать формулу на латехе"
            };

            await db.DeleteAllAsync<Theory>();
            await db.DeleteAllAsync<TheoryContent>();

            await db.InsertAsync(t0);
            await db.InsertAsync(tc00);
            await db.InsertAsync(tc01);
            await db.InsertAsync(tc02);
            await db.InsertAsync(tc03);
            await db.InsertAsync(tc00);
            await db.InsertAsync(tc00);
            await db.InsertAsync(tc00);
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
