using AlgorithmicsApp.Models;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AlgorithmicsApp.Services
{
    public static class CoursesDbService
    {
        static SQLiteAsyncConnection db = null;
        static async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Course>();

            Course[] courses =
            {
                new Course
                {
                    Name = "Наибольший общий делитель и алгоритмы его нахождения",
                    Id = 0
                },
                new Course
                {
                    Name = "Диофантовы уравнения первой степени",
                    Id = 1
                },
                new Course
                {
                    Name = "Непрерывные (цепные) дроби",
                    Id = 2
                },
                new Course
                {
                    Name = "Кольцо целых гауссовых чисел",
                    Id = 3
                },
                new Course
                {
                    Name = "Сравнения и системы сравнений",
                    Id = 4
                },
            };

            await db.DeleteAllAsync<Course>();

            await db.InsertAllAsync(courses);
        }

        public static async Task<IEnumerable<Course>> GetCourses()
        {
            await Init();

            var courses = await db.Table<Course>().ToListAsync();
            return courses;
        }

        public static async Task<Course> GetCourseById(int courseId)
        {
            await Init();

            var courses = await db.Table<Course>().Where(c => c.Id == courseId).FirstOrDefaultAsync();
            return courses;
        }
    }
}
