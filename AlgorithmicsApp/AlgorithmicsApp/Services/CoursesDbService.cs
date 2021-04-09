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

            var course1 = new Course
            {
                Name = "Наибольший общий делитель и алгоритмы его нахождения",
                Image = "https://www.yesplz.coffee/app/uploads/2020/11/emptybag-min.png",
                Id = 0
            };

            await db.DeleteAllAsync<Course>();

            await db.InsertAsync(course1);
        }

        public static async Task<IEnumerable<Course>> GetCourses()
        {
            await Init();

            var courses = await db.Table<Course>().ToListAsync();
            return courses;
        }
    }
}
