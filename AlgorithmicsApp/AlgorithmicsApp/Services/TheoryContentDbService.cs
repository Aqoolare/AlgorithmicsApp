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
                    Formula = @"Число d > 0 называется наибольшим общим делителем (НОД) двух целых чисел a и b, если оно удовлетворяет следующим " +
                    "условиям: 1) d | a и d | b; 2) если c | a и c | b, то c | d.",
                    LinkId = 0,
                    TextCountStrings = 0,
                    FormulaCountStrings = 5
                },
                new TheoryContent
                {
                    TheoryId = 0,
                    Formula = @" Наибольший общий делитель двух чисел a и b, $a\geq b> 0,$ можно найти с помощью алгоритма Евклида, который основан на том, что если $$a=bq+r,\quad0\leq r<b,$$ то $$НОД(a, b) = НОД(r, b).$$",
                    LinkId = -1,
                    FormulaCountStrings = 6,
                    TextCountStrings = 0,
                },
                new TheoryContent
                {
                    TheoryId = 1,
                    Formula = @"Алгоритм Евклида состоит из следующих шагов вычисления: $$(r_{0}=a,\quad r_{1}=b):\quad r_{i-1}=r_{i}q_{i}+r_{i+1},$$ $$0<r_{i+1}<r_{i},\quad (i=1,2,...).$$ Если $r_{n+1}=0$ – первый нулевой остаток, то $$НОД(a,b)=r_{n}.$$",
                    LinkId = -1,
                    FormulaCountStrings = 7,
                    TextCountStrings = 0
                },
                new TheoryContent
                {
                    TheoryId = 2,
                    Formula = @"Еще один способ нахождения наибольшего общего делителя двух чисел: найти каноническое разложение этих чисел на простые множители. Пусть $$a={p_{1}}^{\alpha _{1}}{p_{2}}^{\alpha _{2}}...{p_{s}}^{\alpha _{s}},\quad \alpha _{i}\geq 0,$$ $$b={p_{1}}^{\beta  _{1}}{p_{2}}^{\beta  _{2}}...{p_{s}}^{\beta _{s}},\quad \beta _{i}\geq 0,$$ $$(i=1,...,s),$$ где $p_{1},p_{2},...,p_{s}$ – попарно различные простые целые числа.",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 10,
                },
                new TheoryContent
                {
                    TheoryId = 2,
                    Formula = @"Тогда $$НОД(a,b)={p_{1}}^{\gamma  _{1}}{p_{2}}^{\gamma \alpha _{2}}...{p_{s}}^{\gamma \alpha _{s}},$$ где $\gamma _{i}=min\left \{ \alpha _{i},\beta _{i} \right \}\quad(i=1,2,...,s).$",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 5,
                },
                new TheoryContent
                {
                    TheoryId = 3,
                    Formula = @"Можно также искать наибольший общий делитель двух чисел с помощью бинарного алгоритма, который основан на следующих свойствах наибольшего общего делителя: $$НОД(2n,2m)=2НОД(n,m);$$ $$НОД(2n+1,2m)=НОД(2n+1,m);$$ $$НОД(n,m)=НОД(n-m,m)\quad(n\geq m);$$ $$НОД(n,m)=НОД(m,n);$$ $$НОД(n,0)=n.$$",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 13,
                },
                new TheoryContent
                {
                    TheoryId = 4,
                    Formula = @"С учетом этих свойств получаем следующий алгоритм:\\1. Если $2^{k}|a\quadи \quad 2^{k}|b,$ a $2^{k+1}$ не делит одно из этих чисел, то запомнить $d_{0}=2^{k}$ и положить $a=\frac{a}{2^{k}},\quad b=\frac{b}{2^{k}}$ (при этом одно из чисел a и b обязательно является нечетным);",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 7,
                },
                new TheoryContent
                {
                    TheoryId = 4,
                    Formula = @"2. Если одно из чисел a и b четно, то поделить его на наибольшую возможную степень двойки так, чтобы оба числа a и b стали нечетными. Если оба числа нечетны, то сразу переходим к следующему шагу;",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 6,
                },
                new TheoryContent
                {
                    TheoryId = 4,
                    Formula = @"3. Сравним полученные числа, пусть $a\geq b.$ Тогда положим $a=a-b.$ Если $a\neq 0,$ то возвращаемся на шаг 2, иначе переходим к шагу 4.",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 4,
                },
                new TheoryContent
                {
                    TheoryId = 4,
                    Formula = @"4. Положить $НОД(a,b)=d_{0}\cdot b.$",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 1,
                },
                new TheoryContent
                {
                    TheoryId = 5,
                    Formula = @"Рассмотрим пример по нахождению НОД двух чисел, записанных в двоичной системе счисления: $$a=111010110100_{2},\quad b=101101000_{2}.$$ Очевидно оба числа делятся на $100_{2},$ поэтому $d_{0}=100_{2}.$ Делим оба числа на $d_{0},$ т.е. сдвигаем оба числа на два разряда вправо (отсекаем два последних нуля), получаем числа $$1110101101_{2}, 10110100_{2}.$$ Второе число является чётным, сдвигаем его вправо на два разряда, получаем $101101_{2}.$",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 12,
                },
                new TheoryContent
                {
                    TheoryId = 5,
                    Formula = @"Теперь последовательно выполняем шаги 3 и 2 (в дальнейшем опускаем показатель основания системы счисления): $$НОД(1110101101, 101101) =$$ $$НОД(1110101101 − 101101, 101101) =$$ $$НОД(1110000000, 101101) =$$ $$НОД(111, 101101) =$$ $$НОД(101101, 111) =$$ $$НОД(101101 − 111, 111) =$$ $$НОД(100110, 111) =$$ $$НОД(10011, 111) =$$ $$НОД(10011 − 111, 111) =$$ $$НОД(1100, 111) =$$ $$НОД(11, 111) =$$ $$НОД(111, 11) =$$ $$НОД(111 − 11, 11) =$$ $$НОД(100, 11) =$$ $$НОД(1, 11) = НОД(11, 1) =$$ $$НОД(11 − 1, 1) = НОД(10, 1) =$$ $$НОД(1, 1) = 1.$$ Значит, $$НОД(111010110100_{2},101101000_{2})=$$ $$d_{0}\cdot 1=100_{2}.$$",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 33,
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
