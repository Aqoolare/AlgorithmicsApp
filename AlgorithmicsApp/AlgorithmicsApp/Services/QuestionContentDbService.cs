﻿using AlgorithmicsApp.Models;
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

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<Answer>();

            await db.DeleteAllAsync<Answer>();

            var a0 = new Answer
            {
                QuestionId = 0,
                Content = "Ответ 1",
                IsTrue = false
            };

            var a1 = new Answer
            {
                QuestionId = 0,
                Content = "Ответ 2",
                IsTrue = false
            };

            var a2 = new Answer
            {
                QuestionId = 0,
                Content = "Ответ 3",
                IsTrue = true
            };

            var a3 = new Answer
            {
                QuestionId = 0,
                Content = "Ответ 4",
                IsTrue = true
            };

            await db.InsertAsync(a0);
            await db.InsertAsync(a1);
            await db.InsertAsync(a2);
            await db.InsertAsync(a3);
        }

        public static async Task<IEnumerable<Answer>> GetQuestionContent(int questionID)
        {
            await Init();

            var theoryContent = await db.Table<Answer>().Where(a => a.QuestionId == questionID).ToListAsync();
            return theoryContent;
        }
    }
}
