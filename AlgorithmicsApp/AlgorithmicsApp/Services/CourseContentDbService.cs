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

        public static CourseItem[] CourseItems { get; set; }
        public static List<CourseItem> CourseItemsHistory { get; set; }

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

            var q0 = new Question
            {
                Id = 0,
                CourseId = 0,
                Order = 2,
                Title = "Вопросик :3",
                Formulation = "Да или нет?",
                IsAnswered = false
            };

            //await db.DeleteAllAsync<Theory>();
            //await db.DeleteAllAsync<Question>();

            //await db.InsertAsync(t0);
            //await db.InsertAsync(q0);
        }

        public static async Task<IEnumerable<Theory>> GetTheory(int courseId)
        {
            await Init();

            var theory = await db.Table<Theory>().Where(t => t.CourseId == courseId).ToListAsync();
            return theory;
        }

        public static async Task<IEnumerable<Question>> GetQuestions(int courseId)
        {
            await Init();

            var questions = await db.Table<Question>().Where(q => q.CourseId == courseId).ToListAsync();
            return questions;
        }

        public static async Task<Question> GetQuestion(int questionId)
        {
            await Init();

            var question = await db.Table<Question>().Where(q => q.Id == questionId).FirstOrDefaultAsync();
            return question;
        }

        public static async Task<int> UpdateQuestion(Question question)
        {
            await Init();

            return await db.UpdateAsync(question);
        }
    }
}
