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
    public static class QuestionContentDbService
    {
        static SQLiteAsyncConnection db = null;

        static async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData1.db");

            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<Answer>();


            Answer[] answers =
            {
                new Answer
                {
                    QuestionId = 0,
                    Content = "5",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 0
                },
                new Answer
                {
                    QuestionId = 0,
                    Content = "6",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 1
                },
                new Answer
                {
                    QuestionId = 0,
                    Content = "4",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 2
                },
                new Answer
                {
                    QuestionId = 0,
                    Content = "7",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 3
                },
                new Answer
                {
                    QuestionId = 1,
                    Content = "2",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 4
                },
                new Answer
                {
                    QuestionId = 1,
                    Content = "3",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 5
                },
                new Answer
                {
                    QuestionId = 1,
                    Content = "4",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 6
                },
                new Answer
                {
                    QuestionId = 1,
                    Content = "-1",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 7
                },
                new Answer
                {
                    QuestionId = 2,
                    Content = @"$10001_{2}$",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 8
                },
                new Answer
                {
                    QuestionId = 2,
                    Content = "$10101_{2}$",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 9
                },
                new Answer
                {
                    QuestionId = 2,
                    Content = "$10000_{2}$",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 10
                },
                new Answer
                {
                    QuestionId = 2,
                    Content = "$11001_{2}$",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 11
                },
                new Answer
                {
                    QuestionId = 3,
                    Content = "3",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 12
                },
                new Answer
                {
                    QuestionId = 3,
                    Content = "18",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 13
                },
                new Answer
                {
                    QuestionId = 3,
                    Content = "5",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 14
                },
                new Answer
                {
                    QuestionId = 3,
                    Content = "-7",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 15
                },
                new Answer
                {
                    QuestionId = 4,
                    Content = @"$\Leftrightarrow НОД(a,\, b)<=c$",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 16
                },
                new Answer
                {
                    QuestionId = 4,
                    Content = @"$\Leftrightarrow НОД(a,\, b)\, |\, c$",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 17
                },
                new Answer
                {
                    QuestionId = 4,
                    Content = @"$\Leftrightarrow НОК(a,\, b)>=c$",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 18
                },
                new Answer
                {
                    QuestionId = 5,
                    Content = @"$\left\{\begin{matrix}x=-56+9t,\\y=28+4t,\end{matrix}\right.\: t\in \mathbb{Z}$",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 19
                },
                new Answer
                {
                    QuestionId = 5,
                    Content = @"$\left\{\begin{matrix}x=24+4t,\\y=-56+13t,\end{matrix}\right.\: t\in \mathbb{Z}$",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 20
                },
                new Answer
                {
                    QuestionId = 5,
                    Content = @"$\left\{\begin{matrix}x=28+4t,\\y=-56+9t,\end{matrix}\right.\: t\in \mathbb{Z}$",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 21
                },
                new Answer
                {
                    QuestionId = 5,
                    Content = @"$\left\{\begin{matrix}x=28+4t,\\y=-56+9t,\end{matrix}\right.\: t\in \mathbb{N}$",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 22
                },
                new Answer
                {
                    QuestionId = 6,
                    Content = @"Любое рациональное число однозначно представляется в виде конечной непрерывной дроби.",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 23
                },
                new Answer
                {
                    QuestionId = 6,
                    Content = @"$Q_{0}=0$",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 24
                },
                new Answer
                {
                    QuestionId = 6,
                    Content = @"Любое иррациональное вещественное число однозначно представляется в виде бесконечной непрерывной дроби с целыми неполными частными.",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 25
                },
                new Answer
                {
                    QuestionId = 6,
                    Content = @"Квадратичные иррациональности и только они могут быть представлены в виде бесконечной периодической непрерывной дроби.",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 26
                },
                new Answer
                {
                    QuestionId = 7,
                    Content = @"$\frac{245}{73}$",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 27
                },
                new Answer
                {
                    QuestionId = 7,
                    Content = @"$\frac{247}{73}$",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 28
                },
                new Answer
                {
                    QuestionId = 7,
                    Content = @"$\frac{23}{3}$",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 29
                },
                new Answer
                {
                    QuestionId = 7,
                    Content = @"$\frac{241}{73}$",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 30
                },
                new Answer
                {
                    QuestionId = 8,
                    Content = @"$\left [ -13,1,14,1,4 \right ]$",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 31
                },
                new Answer
                {
                    QuestionId = 8,
                    Content = @"$\left [ -11,1,14,1,5 \right ]$",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 32
                },
                new Answer
                {
                    QuestionId = 8,
                    Content = @"$\left [ -11,1,14,1,4 \right ]$",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 33
                },
                new Answer
                {
                    QuestionId = 8,
                    Content = @"$\left [ -11,1,14,1 \right ]$",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 34
                },
                new Answer
                {
                    QuestionId = 9,
                    Content = @"$\left [ 12, (6, 6, 24) \right ]$",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 35
                },
                new Answer
                {
                    QuestionId = 9,
                    Content = @"$\left [ 12, (6, 6, 24, 1) \right ]$",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 36
                },
                new Answer
                {
                    QuestionId = 9,
                    Content = @"$\left [ 12, (6, 24) \right ]$",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 37
                },
                new Answer
                {
                    QuestionId = 10,
                    Content = @"$2+i$",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 38
                },
                new Answer
                {
                    QuestionId = 10,
                    Content = @"$-2+i$",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 39
                },
                new Answer
                {
                    QuestionId = 10,
                    Content = @"$2-i$",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 40
                },
                new Answer
                {
                    QuestionId = 11,
                    Content = @"$15-4i$ неприводимый элемент кольца $\mathbb{Z}[i]$",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 41
                },
                new Answer
                {
                    QuestionId = 11,
                    Content = @"$9+5i$ неприводимый элемент кольца $\mathbb{Z}[i]$",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 42
                },
                new Answer
                {
                    QuestionId = 11,
                    Content = @"$617$ приводимый элемент кольца $\mathbb{Z}[i]$",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 43
                },
                new Answer
                {
                    QuestionId = 12,
                    Content = @"7",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 44
                },
                new Answer
                {
                    QuestionId = 12,
                    Content = @"3",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 45
                },
                new Answer
                {
                    QuestionId = 12,
                    Content = @"5",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 46
                },
                new Answer
                {
                    QuestionId = 13,
                    Content = @"60",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 47
                },
                new Answer
                {
                    QuestionId = 13,
                    Content = @"3",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 48
                },
                new Answer
                {
                    QuestionId = 13,
                    Content = @"33",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 49
                },
                new Answer
                {
                    QuestionId = 13,
                    Content = @"31",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 50
                },
                new Answer
                {
                    QuestionId = 13,
                    Content = @"6",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 51
                },
                new Answer
                {
                    QuestionId = 14,
                    Content = @"9",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 52
                },
                new Answer
                {
                    QuestionId = 14,
                    Content = @"7",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 53
                },
                new Answer
                {
                    QuestionId = 14,
                    Content = @"17",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 54
                },
                new Answer
                {
                    QuestionId = 15,
                    Content = @"101(mod 455)",
                    IsTrue = true,
                    AnswerColor = "White",
                    Id = 55
                },
                new Answer
                {
                    QuestionId = 15,
                    Content = @"111(mod 455)",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 56
                },
                new Answer
                {
                    QuestionId = 15,
                    Content = @"101(mod 457)",
                    IsTrue = false,
                    AnswerColor = "White",
                    Id = 57
                },
            };

            //await db.DeleteAllAsync<Answer>();

            if (await db.Table<Answer>().CountAsync() == 0)
                await db.InsertAllAsync(answers);
        }

        public static async Task<IEnumerable<Answer>> GetQuestionContent(int questionID)
        {
            await Init();

            var theoryContent = await db.Table<Answer>().Where(a => a.QuestionId == questionID).ToListAsync();
            await db.CloseAsync();
            return theoryContent;
        }

        public static async Task<int> UpdateQuestionContent(Answer answer)
        {
            await Init();
            var res = await db.UpdateAsync(answer);
            await db.CloseAsync();
            return res;
        }
    }
}
