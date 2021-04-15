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
        static SQLiteConnection db = null;

        static void Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteConnection(databasePath);
            db.CreateTable<TheoryContent>();

            var tc00 = new TheoryContent
            {
                TheoryId = 0,
                Type = false,
                Text = "Определение 1. Число d > 0 называется наибольшим общим делителем (НОД) двух целых чисел a и b, если оно удовлетворяет следующим " +
                "условиям: 1) d | a и d | b; 2) если c | a и c | b, то c | d.",
            };

            var tc01 = new TheoryContent
            {
                TheoryId = 0,
                Type = false,
                Text = "Наибольший общий делитель двух чисел a и b, a ≥ b > 0, можно найти с помощью алгоритма Евклида, который основан на том, что если " +
                "a = bq + r, 0 ≤ r < b, то НОД(a, b) = НОД(r, b).",
            };

            var tc02 = new TheoryContent
            {
                TheoryId = 0,
                Type = false,
                Text = "Алгоритм Евклида состоит из следующих шагов вычисления",
                LinkPage = "CourseContentPage"
            };

            var tc03 = new TheoryContent
            {
                TheoryId = 0,
                Type = true,
                Text = @"\frac\sqrt23"
            };

            db.DeleteAll<TheoryContent>();

            db.Insert(tc00);
            db.Insert(tc01);
            db.Insert(tc02);
            db.Insert(tc03);
            db.Insert(tc00);
        }

        public static IEnumerable<TheoryContent> GetTheoryContent(int theoryId)
        {
            Init();

            var theoryContent = db.Table<TheoryContent>().Where(tc => tc.TheoryId == theoryId).ToList();
            return theoryContent;
        }
    }
}
