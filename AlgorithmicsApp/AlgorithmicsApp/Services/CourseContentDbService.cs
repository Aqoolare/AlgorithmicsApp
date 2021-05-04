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
            //await db.CreateTableAsync<Question>();

            Theory[] theories = 
            {
                new Theory
                {
                    Id = 0,
                    CourseId = 0,
                    Order = 1,
                    Title = "Определение НОД и алгоритм нахождения"
                },
                new Theory
                {
                    Id = 1,
                    CourseId = 0,
                    Order = 2,
                    Title = "Шаги вычисления Алгоритма Евклида"
                },
                new Theory
                {
                    Id = 2,
                    CourseId = 0,
                    Order = 3,
                    Title = "Каноническое разложение чисел на простые множители"
                },
                new Theory
                {
                    Id = 3,
                    CourseId = 0,
                    Order = 4,
                    Title = "Свойства НОД"
                },
                new Theory
                {
                    Id = 4,
                    CourseId = 0,
                    Order = 5,
                    Title = "Бинарный алгоритм"
                },
                new Theory
                {
                    Id = 5,
                    CourseId = 0,
                    Order = 7,
                    Title = "НОД двух чисел в двоичной системе счисления"
                },
                new Theory
                {
                    Id = 6,
                    CourseId = 0,
                    Order = 9,
                    Title = "Центированное деление"
                },
                new Theory
                {
                    Id = 7,
                    CourseId = 0,
                    Order = 10,
                    Title = "Расширенный алгоритм Евклида"
                },
            };

            var q0 = new Question
            {
                Id = 0,
                CourseId = 0,
                Order = 9,
                Title = "Вопрос 1",
                Formulation = "Да или нет?",
                IsAnswered = false
            };

            await db.DeleteAllAsync<Theory>();
            //await db.DeleteAllAsync<Question>();

            await db.InsertAllAsync(theories);
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
