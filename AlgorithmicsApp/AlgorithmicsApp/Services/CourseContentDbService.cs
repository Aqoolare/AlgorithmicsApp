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

            Theory[] theories = 
            {
                new Theory
                {
                    Id = 1,
                    CourseId = 0,
                    Order = 1,
                    Title = "Определение НОД и алгоритм нахождения"
                },
                new Theory
                {
                    Id = 2,
                    CourseId = 0,
                    Order = 2,
                    Title = "Шаги вычисления Алгоритма Евклида"
                },
                new Theory
                {
                    Id = 3,
                    CourseId = 0,
                    Order = 4,
                    Title = "Каноническое разложение чисел на простые множители"
                },
                new Theory
                {
                    Id = 4,
                    CourseId = 0,
                    Order = 5,
                    Title = "Свойства НОД"
                },
                new Theory
                {
                    Id = 5,
                    CourseId = 0,
                    Order = 6,
                    Title = "Бинарный алгоритм"
                },
                new Theory
                {
                    Id = 6,
                    CourseId = 0,
                    Order = 7,
                    Title = "НОД двух чисел в двоичной системе счисления"
                },
                new Theory
                {
                    Id = 7,
                    CourseId = 0,
                    Order = 9,
                    Title = "Центрированное деление"
                },
                new Theory
                {
                    Id = 8,
                    CourseId = 0,
                    Order = 11,
                    Title = "Расширенный алгоритм Евклида"
                },
                new Theory
                {
                    Id = 9,
                    CourseId = 1,
                    Order = 1,
                    Title = "Определение. Условие разрешимости"
                },
                new Theory
                {
                    Id = 10,
                    CourseId = 1,
                    Order = 2,
                    Title = "Решение диофантова уравнения"
                },
                new Theory
                {
                    Id = 11,
                    CourseId = 2,
                    Order = 1,
                    Title = "Определение непрерывной дроби"
                },
                new Theory
                {
                    Id = 12,
                    CourseId = 2,
                    Order = 2,
                    Title = "Свойства подходящих дробей"
                },
                new Theory
                {
                    Id = 13,
                    CourseId = 2,
                    Order = 5,
                    Title = "Разложение произвольного вещественного числа в непрерывную дробь"
                },
                new Theory
                {
                    Id = 14,
                    CourseId = 2,
                    Order = 6,
                    Title = "Периодические непрерывные дроби"
                },
                new Theory
                {
                    Id = 15,
                    CourseId = 2,
                    Order = 8,
                    Title = "Теорема Лагранжа"
                },
                new Theory
                {
                    Id = 16,
                    CourseId = 2,
                    Order = 10,
                    Title = "Лемма о погрешности приближения"
                },
                new Theory
                {
                    Id = 17,
                    CourseId = 3,
                    Order = 1,
                    Title = "Определение кольца целых гауссовых чисел"
                },
                new Theory
                {
                    Id = 18,
                    CourseId = 3,
                    Order = 2,
                    Title = "Норма элемента"
                },
                new Theory
                {
                    Id = 19,
                    CourseId = 3,
                    Order = 3,
                    Title = "Операция деления с остатком"
                },
                new Theory
                {
                    Id = 20,
                    CourseId = 3,
                    Order = 5,
                    Title = "Факториальность кольца"
                },
                new Theory
                {
                    Id = 21,
                    CourseId = 3,
                    Order = 6,
                    Title = "Теорема"
                },
                new Theory
                {
                    Id = 22,
                    CourseId = 4,
                    Order = 1,
                    Title = "Определение сравнения по модулю"
                },
                new Theory
                {
                    Id = 23,
                    CourseId = 4,
                    Order = 2,
                    Title = "Свойства сравнений"
                },
                new Theory
                {
                    Id = 24,
                    CourseId = 4,
                    Order = 3,
                    Title = "Теорема (разрешимость сравнения)"
                },
                new Theory
                {
                    Id = 25,
                    CourseId = 4,
                    Order = 7,
                    Title = "Решение системы сравнений"
                },
            };

            Question[] questions =
            {
                new Question
                {
                    Id = 0,
                    CourseId = 0,
                    Order = 3,
                    Title = "Вопрос 1",
                    Formulation = "Для чисел 126 и 21, выбрать частное, полученное на последнем шаге алгоритма Евклида.",
                    IsAnswered = false
                },
                new Question
                {
                    Id = 1,
                    CourseId = 0,
                    Order = 12,
                    Title = "Вопрос 4",
                    Formulation = @"Выбрать два числа: u и v, найденные расширенным алгоритмом Евклида для чисел a и b. $(НОД(136\quad ,51)=136u+51v).$ Порядок выбора чисел не важен.",
                    IsAnswered = false
                },
                new Question
                {
                    Id = 2,
                    CourseId = 0,
                    Order = 8,
                    Title = "Вопрос 2",
                    Formulation = @"Используя бинарный алгоритм найти НОД двух данных чисел ($a=1000100_{2}$ и $b=110011_{2}$).",
                    IsAnswered = false
                },
                new Question
                {
                    Id = 3,
                    CourseId = 0,
                    Order = 10,
                    Title = "Вопрос 3",
                    Formulation = "Найти НОД двух чисел (297 и 84), используя центрированное деление, выбрать неполное частное, полученное на последнем шаге, и НОД этих чисел.",
                    IsAnswered = false
                },
                new Question
                {
                    Id = 4,
                    CourseId = 1,
                    Order = 3,
                    Title = "Вопрос 1",
                    Formulation = "Условие разрешимости диофантова уравнения.",
                    IsAnswered = false
                },
                new Question
                {
                    Id = 5,
                    CourseId = 1,
                    Order = 4,
                    Title = "Вопрос 2",
                    Formulation = @"Решить в целых числах уравнение $27x-12y=84$",
                    IsAnswered = false
                },
                new Question
                {
                    Id = 6,
                    CourseId = 2,
                    Order = 9,
                    Title = "Вопрос 4",
                    Formulation = @"Выберите верные утверждения:",
                    IsAnswered = false
                },
                new Question
                {
                    Id = 7,
                    CourseId = 2,
                    Order = 3,
                    Title = "Вопрос 1",
                    Formulation = @"Свернуть конечную непрерывную (цепную) дробь: $[3, 2, 1, 4, 5]$",
                    IsAnswered = false
                },
                new Question
                {
                    Id = 8,
                    CourseId = 2,
                    Order = 4,
                    Title = "Вопрос 2",
                    Formulation = @"Разложить в непрерывную дробь число $-\frac{795}{79}$",
                    IsAnswered = false
                },
                new Question
                {
                    Id = 9,
                    CourseId = 2,
                    Order = 7,
                    Title = "Вопрос 3",
                    Formulation = @"Разложить в непрерывную дробь иррациональное число $\sqrt{148}$",
                    IsAnswered = false
                },
                new Question
                {
                    Id = 10,
                    CourseId = 3,
                    Order = 4,
                    Title = "Вопрос 1",
                    Formulation = @"В кольце $\mathbb{Z}[i]$ найти $НОД$ чисел $z_{1}=18-4i$ и $z_{2}=2-11i$",
                    IsAnswered = false
                },
                new Question
                {
                    Id = 11,
                    CourseId = 3,
                    Order = 7,
                    Title = "Вопрос 2",
                    Formulation = @"Выберите верные утверждения:",
                    IsAnswered = false
                },
                new Question
                {
                    Id = 12,
                    CourseId = 4,
                    Order = 4,
                    Title = "Вопрос 1",
                    Formulation = @"С помощью расширенного алгоритма Евклида, решить сравнение: $5x\equiv 3(mod\: 11)$",
                    IsAnswered = false
                },
                new Question
                {
                    Id = 13,
                    CourseId = 4,
                    Order = 5,
                    Title = "Вопрос 2",
                    Formulation = @"С помощью расширенного алгоритма Евклида, решить сравнение: $42x\equiv 9(mod\: 81)$ Найти все решения по данному модулю(1 ответ - 1 решение):",
                    IsAnswered = false
                },
                new Question
                {
                    Id = 14,
                    CourseId = 4,
                    Order = 6,
                    Title = "Вопрос 3",
                    Formulation = @"С помощью расширенного алгоритма Евклида найти $a^{-1}$ в кольце $\mathbb{Z}_{m}$ $(a=17,\quad m=38):$",
                    IsAnswered = false
                },
                new Question
                {
                    Id = 15,
                    CourseId = 4,
                    Order = 8,
                    Title = "Вопрос 4",
                    Formulation = @"Решить систему сравнений $$\left\{\begin{matrix}2x\equiv 2(mod\: 5)\\ 3x\equiv 2(mod\: 7)\\ 5x\equiv 11(mod\: 13)\end{matrix}\right.$$, используя формулу $x\equiv \sum_{i=1}^{k}a_{i}M_{i}N_{i}\; (mod\: M):$",
                    IsAnswered = false
                },
            };

            //await db.DeleteAllAsync<Theory>();
            //await db.DeleteAllAsync<Question>();

            if (await db.Table<Theory>().CountAsync() == 0)
            {
                await db.InsertAllAsync(theories);
            }
            if (await db.Table<Question>().CountAsync() == 0)
            {
                await db.InsertAllAsync(questions);
            }
        }

        public static async Task<IEnumerable<Theory>> GetTheory(int courseId)
        {
            await Init();

            var theory = await db.Table<Theory>().Where(t => t.CourseId == courseId).ToListAsync();
            return theory;
        }

        public static async Task<Theory> GetTheoryById(int theoryId)
        {
            await Init();

            var theory = await db.Table<Theory>().Where(t => t.Id == theoryId).FirstOrDefaultAsync();
            return theory;
        }

        public static async Task<Question> GetQuestionById(int questionId)
        {
            await Init();

            var question = await db.Table<Question>().Where(q => q.Id == questionId).FirstOrDefaultAsync();
            return question;
        }

        public static async Task<IEnumerable<Question>> GetQuestions(int courseId)
        {
            await Init();

            var questions = await db.Table<Question>().Where(q => q.CourseId == courseId).ToListAsync();
            return questions;
        }

        public static async Task<int> GetTotalQuestionsCount()
        {
            await Init();

            var totalCount = await db.Table<Question>().CountAsync();
            return totalCount;
        }

        public static async Task<int> GetAnsweredQuestionsCount()
        {
            await Init();

            var answeredCount = await db.Table<Question>().Where(q => q.IsTrue).CountAsync();
            return answeredCount;
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
