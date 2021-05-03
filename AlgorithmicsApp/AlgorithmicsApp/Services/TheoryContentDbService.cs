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
        static SQLiteAsyncConnection db = null;

        static async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<TheoryContent>();
            await db.CreateTableAsync<Link>();

            TheoryContent[] theoryContents =
            {
                new TheoryContent
                {
                    TheoryId = 0,
                    BoldText = "Определение 1.",
                    Text1 = " Число d > 0 называется наибольшим общим делителем (НОД) двух целых чисел a и b, если оно удовлетворяет следующим " +
                    "условиям: 1) d | a и d | b; 2) если c | a и c | b, то c | d.",
                    LinkId = 0
                },
                new TheoryContent
                {
                    TheoryId = 0,
                    Text1 = "Наибольший общий делитель двух чисел",
                    Formula = @"$$a \quad и  \quad b,\quad a\geq b> 0,$$",
                    LinkId = -1,
                    CountStrings = 1
                },
                new TheoryContent
                {
                    TheoryId = 0,
                    Text1 = "можно найти с помощью алгоритма Евклида, который основан на том, что если",
                    Formula = @"$$a=bq+r,\quad0\leq r<b,$$",
                    LinkId = -1,
                    CountStrings = 1
                },
                new TheoryContent
                {
                    TheoryId = 0,
                    Text1 = "то НОД(a, b) = НОД(r, b).",
                    LinkId = -1
                },
                new TheoryContent
                {
                    TheoryId = 1,
                    Text1 = "Алгоритм Евклида состоит из следующих шагов вычисления",
                    Formula = @"$$\\(r_{0}=a,\quad r_{1}=b):\\r_{i-1}=r_{i}q_{i}+r_{i+1},\\0<r_{i+1}<r_{i},\\(i=1,2,...).$$",
                    LinkId = -1,
                    CountStrings = 4
                },
                new TheoryContent
                {
                    TheoryId = 1,
                    Text1 = "Если",
                    Formula = @"$$r_{n+1}=0$$",
                    LinkId = -1,
                    CountStrings = 1
                },
                new TheoryContent
                {
                    TheoryId = 1,
                    Text1 = "– первый нулевой остаток, то",
                    Formula = @"$$НОД(a,b)=r^{_{n}}.$$",
                    LinkId = -1,
                    CountStrings = 1
                },
                new TheoryContent
                {
                    TheoryId = 2,
                    Text1 = "Еще один способ нахождения наибольшего общего делителя двух чисел: найти каноническое разложение этих чисел на простые множители. Пусть",
                    Formula = @"$$\\a={p_{1}}^{\alpha _{1}}{p_{2}}^{\alpha _{2}}...{p_{s}}^{\alpha _{s}},\\\alpha _{i}\geq 0,\\b={p_{1}}^{\beta  _{1}}{p_{2}}^{\beta  _{2}}...{p_{s}}^{\beta _{s}},\\\beta _{i}\geq 0,\\(i=1,...,s),$$",
                    LinkId = -1,
                    CountStrings = 5
                },
                new TheoryContent
                {
                    TheoryId = 2,
                    Text1 = "где",
                    Formula = @"$$p_{1},p_{2},...,p_{s}$$",
                    LinkId = -1,
                    CountStrings = 1
                },
                new TheoryContent
                {
                    TheoryId = 2,
                    Text1 = "– попарно различные простые целые числа. Тогда",
                    Formula = @"$$НОД(a,b)={p_{1}}^{\gamma  _{1}}{p_{2}}^{\gamma \alpha _{2}}...{p_{s}}^{\gamma \alpha _{s}},$$",
                    LinkId = -1,
                    CountStrings = 1
                },
                new TheoryContent
                {
                    TheoryId = 2,
                    Text1 = "где",
                    Formula = @"$$\gamma _{i}=min\left \{ \alpha _{i},\beta _{i} \right \}\quad(i=1,2,...,s)$$",
                    LinkId = -1,
                    CountStrings = 1
                },
                new TheoryContent
                {
                    TheoryId = 3,
                    Text1 = "Можно также искать наибольший общий делитель двух чисел с помощью бинарного алгоритма, который основан на следующих свойствах наибольшего общего делителя:",
                    Formula = @"$$\\НОД(2n,2m)=2НОД(n,m);\\НОД(2n+1,2m)=НОД(2n+1,m);\\НОД(n,m)=НОД(n-m,m)\\\quad(n\geq m);\\НОД(n,m)=НОД(m,n)\\НОД(n,0)=n.$$",
                    LinkId = -1,
                    CountStrings = 6
                },
                new TheoryContent
                {
                    TheoryId = 4,
                    Text1 = "С учетом этих свойств получаем следующий алгоритм:",
                    LinkId = -1,
                },
                new TheoryContent
                {
                    TheoryId = 4,
                    BoldText = "1. ",
                    Text1 = "Если",
                    Formula = @"$$2^{k}|a\quadи \quad 2^{k}|b,$$",
                    LinkId = -1,
                    CountStrings = 1
                },
                new TheoryContent
                {
                    TheoryId = 4,
                    Text1 = "a",
                    Formula = @"$$2^{k+1}$$",
                    LinkId = -1,
                    CountStrings = 1
                },
                new TheoryContent
                {
                    TheoryId = 4,
                    Text1 = "не делит одно из этих чисел, то запомнить",
                    Formula = @"$$d_{0}=2^{k}$$",
                    LinkId = -1,
                    CountStrings = 1
                },
                new TheoryContent
                {
                    TheoryId = 4,
                    Text1 = "и положить",
                    Formula = @"$$a=\frac{a}{2^{k}},\quad b=\frac{b}{2^{k}}$$",
                    LinkId = -1,
                    CountStrings = 2
                },
                new TheoryContent
                {
                    TheoryId = 4,
                    Text1 = "(при этом одно из чисел a и b обязательно является нечетным);",
                    LinkId = -1,
                },
                new TheoryContent
                {
                    TheoryId = 5,
                    BoldText = "2. ",
                    Text1 = "Если одно из чисел a и b четно, то поделить его на наибольшую возможную степень двойки так, чтобы оба числа a и b стали нечетными. Если оба числа нечетны, то сразу переходим к следующему шагу;",
                    LinkId = -1,
                },
                new TheoryContent
                {
                    TheoryId = 5,
                    BoldText = "3. ",
                    Text1 = "Сравним полученные числа, пусть",
                    Formula = @"$$a\geq b.$$",
                    LinkId = -1,
                    CountStrings = 1
                },
                new TheoryContent
                {
                    TheoryId = 5,
                    Text1 = "Тогда положим",
                    Formula = @"$$a=a-b.$$",
                    LinkId = -1,
                    CountStrings = 1
                },
                new TheoryContent
                {
                    TheoryId = 5,
                    Text1 = "Если",
                    Formula = @"$$a\neq 0,$$",
                    LinkId = -1,
                    CountStrings = 1
                },
                new TheoryContent
                {
                    TheoryId = 5,
                    Text1 = "то возвращаемся на шаг 2, иначе переходим к шагу 4.",
                    LinkId = -1,
                },
                new TheoryContent
                {
                    TheoryId = 5,
                    BoldText = "4. ",
                    Text1 = "Положить",
                    Formula = @"$$НОД(a,b)=d_{0}\cdot b.$$",
                    LinkId = -1,
                    CountStrings = 1
                },
            };

            var link0 = new Link
            {
                Id = 0,
                Text = "Это ссылка",
                TheoryId = 2,
                ElementIndex = 3,
                TheoryTitle = "Каноническое разложение чисел на простые множители"
            };

            await db.DeleteAllAsync<TheoryContent>();
            await db.DeleteAllAsync<Link>();

            await db.InsertAllAsync(theoryContents);
            await db.InsertAsync(link0);
        }

        public static async Task<IEnumerable<TheoryContent>> GetTheoryContent(int theoryId)
        {
            await Init();

            var theoryContent = await db.Table<TheoryContent>().Where(tc => tc.TheoryId == theoryId).ToListAsync();
            return theoryContent;
        }

        public static async Task<Link> GetLinkById(int linkId)
        {
            await Init();

            var link = await db.Table<Link>().Where(l => l.Id == linkId).FirstOrDefaultAsync();
            return link;
        }
    }
}
