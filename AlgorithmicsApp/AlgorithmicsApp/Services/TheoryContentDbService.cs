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
                    FormulaCountStrings = 13,
                },
                new TheoryContent
                {
                    TheoryId = 5,
                    Formula = @"Теперь последовательно выполняем шаги 3 и 2 (в дальнейшем опускаем показатель основания системы счисления): $$НОД(1110101101, 101101) =$$ $$НОД(1110101101 − 101101, 101101) =$$ $$НОД(1110000000, 101101) =$$ $$НОД(111, 101101) =$$ $$НОД(101101, 111) =$$ $$НОД(101101 − 111, 111) =$$ $$НОД(100110, 111) =$$ $$НОД(10011, 111) =$$ $$НОД(10011 − 111, 111) =$$ $$НОД(1100, 111) =$$ $$НОД(11, 111) =$$ $$НОД(111, 11) =$$ $$НОД(111 − 11, 11) =$$ $$НОД(100, 11) =$$ $$НОД(1, 11) = НОД(11, 1) =$$ $$НОД(11 − 1, 1) = НОД(10, 1) =$$ $$НОД(1, 1) = 1.$$ Значит, $$НОД(111010110100_{2},101101000_{2})=$$ $$d_{0}\cdot 1=100_{2}.$$",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 33,
                },
                new TheoryContent
                {
                    TheoryId = 6,
                    Formula = @"Центрированное деление числа a на число b характеризуется тем, что остаток от деления по абсолютной величине является наименьшим возможным, т. е.$$a=bq+r,\quad где \left | r \right |\leq \frac{b}{2}.$$",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 6,
                },
                new TheoryContent
                {
                    TheoryId = 6,
                    Text1 = "Вспомнить тему: ",
                    Formula = @"Алгоритм Евклида, использующий центрированное деление, в общем случае требует меньшего числа шагов для получения результата.",
                    LinkId = 0,
                    TextCountStrings = 3,
                    FormulaCountStrings = 4,
                },
                new TheoryContent
                {
                    TheoryId = 7,
                    Text1 = "Вспомнить тему: ",
                    Formula = @"Расширенный алгоритм Евклида используется для того, чтобы, кроме наибольшего общего делителя двух чисел a, b, найти представление его в виде: $$НОД(a,b)=au+bv,\quad где u,v\in \mathbb{Z}.$$",
                    LinkId = 1,
                    TextCountStrings = 3,
                    FormulaCountStrings = 5,
                },
                new TheoryContent
                {
                    TheoryId = 7,
                    Formula = @"Для этого в ходе работы алгоритма Евклида дополнительно строятся две числовые последовательности $\left \{ u_{i}\right \}_{i=0,1,...,n+1}$ и $\left \{ v_{i}\right \}_{i=0,1,...,n+1}$, такие что $$r_{i}=au_{i}+bv_{i}\quad (i=0,1,...,n+1).$$",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 6,
                },
                new TheoryContent
                {
                    TheoryId = 7,
                    Formula = @"Если $r_{n}=НОД(a,b),$ то $r_{n}=au_{n}+bv_{n},$ т. е. $u=u_{n},$ $v=v_{n}.$ Числа u и v называются коэффициентами Безу.",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 3,
                },
                new TheoryContent
                {
                    TheoryId = 7,
                    Text1 = "Вспомнить тему: ",
                    Formula = @"Очевидно, $u_{0}=1,$ $v_{0}=1;$ $u_{1}=0,$ $v_{1}=1.$ В дальнейшем элементы последовательностей $\left \{ u_{i}\right \}_{i=0,1,...,n+1}$ и $\left \{ v_{i}\right \}_{i=0,1,...,n+1}$ строятся по рекуррентным формулам: $$u_{i+1}=u_{i-1}-q_{i}u_{i},$$ $$v_{i+1}=v_{i-1}-q_{i}v_{i},\quad (i=1,2,...,n),$$ где $q_{i}$ определено из i-го деления алгоритма Евклида, т. е. $$r_{i-1}=q_{i}r_{i}+r_{i+1},\quad 0<r_{i+1}<r_{i}.$$",
                    LinkId = 0,
                    TextCountStrings = 3,
                    FormulaCountStrings = 10,
                },
                new TheoryContent
                {
                    TheoryId = 8,
                    Formula = @"Диофантовым уравнением первой степени от двух неизвестных называется уравнение $$ax+by=c,\quad (1)$$ где $a,b,c\in \mathbb{Z}$. Решения этого уравнения также ищутся в кольце $\mathbb{Z}.$ Уравнение (1) разрешимо тогда и только тогда, когда $d=НОД(a,b)$ делит c. Поделив обе части уравнения на d, приходим к уравнению $$a_{1}x+b_{1}y=c_{1},\quad (2)$$ где $a_{1}=\frac{a}{d},$ $b_{1}=\frac{b}{d},$ $c_{1}=\frac{c}{d},$ $НОД(a_{1},b_{1})=1.$",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 12,
                },
                new TheoryContent
                {
                    TheoryId = 9,
                    Text1 = "Вспомнить тему: ",
                    Formula = @"С помощью расширенного алгоритма Евклида можно найти такие целые числа u, v, что $$a_{1}u+b_{1}v=1.$$ Умножая полученное равенство на $c_{1}$, получаем $$a_{1}(uc_{1})+b_{1}(vc_{1})=c_{1},$$ т. е. $x_{0}=uc_{1},$ $y_{0}=vc_{1}$ есть целочисленное решение уравнения (1). Остальные целочисленные решения получаются по формулам: $$\left\{\begin{matrix}x=x_{0}-b_{1}t,\\y=y_{0}-a_{1}t,\end{matrix}\right.\quad t\in \mathbb{Z}.$$",
                    LinkId = 2,
                    TextCountStrings = 6,
                    FormulaCountStrings = 12,
                },
                new TheoryContent
                {
                    TheoryId = 10,
                    Text1 = "Вспомнить тему: ",
                    Formula = @"Рассмотрим рациональное число $\frac{a}{b}$ $(b\neq 0,\quad a,b\in \mathbb{Z}).$ Если $\frac{a}{b}<0,$ то всегда считаем $a<0,$ $b>0.$ Применим к числам a и b алгоритм Евклида: $$r_{0}=q_{1}r_{1}+r_{2},\quad 0<r_{2}<r_{1},$$ $$r_{1}=q_{2}r_{2}+r_{3},\quad 0<r_{3}<r_{2},$$ $$(r_{0}=a,\quad r_{1}=b)$$ $$...$$ $$r_{k-2}=q_{k-1}r_{k-1}+r_{k},\quad 0<r_{k}<r_{k-1},$$ $$r_{k-1}=q_{k}r_{k},\quad r_{k+1}=0.$$ Поделив каждое соотношение $r_{i-1}=q_{i}r_{i}+r_{i+1}$ на $r_{i},$ получаем $$\frac{r_{0}}{r_{1}}=q_{1}+\frac{r_{2}}{r_{1}},$$ $$\frac{r_{1}}{r_{2}}=q_{2}+\frac{r_{3}}{r_{2}},$$ $$...$$ $$\frac{r_{k-2}}{r_{k-1}}=q_{k-1}+\frac{r_{k}}{r_{k-1}},$$ $$\frac{r_{k-1}}{r_{k}}=q_{k},$$ откуда $$\frac{a}{b}=\frac{r_{0}}{r_{1}}=$$ $$q_{1}+\frac{1}{q_{2}+\frac{1}{q_{3}+\frac{1}{\ddots +\frac{1}{q_{k}}}}}.\quad (3)$$ Это и есть представление рационального числа $\frac{a}{b}$ в виде непрерывной (цепной) дроби. Для сокращения записи формулу (3) будем записывать в строчку, перечисляя через запятую все последовательные неполные частные $q_{i},$ т. е. в виде $$\frac{a}{b}=\left [ q_{1},q_{2},q_{3},...,q_{k} \right ].$$ В алгоритме Евклида используется классическое деление, поэтому $q_{i}>0$ для $i=2,3,...,k.$ Лишь $q_{1}$ может быть отрицательным в случае, когда $a<0.$ Кроме того, $q_{k}>1.$",
                    LinkId = 0,
                    TextCountStrings = 3,
                    FormulaCountStrings = 41,
                },
                new TheoryContent
                {
                    TheoryId = 10,
                    Formula = @"Справедливо утверждение о том, что любое рациональное число однозначно представляется в виде конечной непрерывной дроби. Oбратно: значением конечной непрерывной дроби является рациональное число.",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 7,
                },
                new TheoryContent
                {
                    TheoryId = 11,
                    Formula = @"Определение. Пусть дана непрерывная дробь $\frac{a}{b}=[q_{1},q_{2},...,q_{n}].$ Рациональное число $\frac{P_{k}}{Q_{k}}=[q_{1},q_{2},...,q_{n}],$ $(1\leq k\leq n)$ называют k-й подходящей дробью к числу $\frac{a}{b}=\frac{P_{n}}{Q_{n}}.$",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 6,
                },
                new TheoryContent
                {
                    TheoryId = 11,
                    Formula = @"Свойства подходящих дробей: $$1.\; \frac{P_{n}}{Q_{n}}=q_{1}+\frac{1}{[q_{1},q_{2},...,q_{n}]};$$ $$2.\; \frac{P_{n}}{Q_{n}}=[q_{1},q_{2},...,q_{n-1}+\frac{1}{q_{n}}];$$ $$3.\; \frac{P_{n}}{Q_{n}}=[q_{1},q_{2},...,q_{s-1}+$$ $$\frac{1}{[q_{1},q_{2},...,q_{n}]}]$$ $$(2\leq s\leq n);$$ $$4.\; P_{k}Q_{k-1}-P_{k-1}Q_{k}=(-1)^{k}\quad (k\geq 1);$$ $$5.\; НОД(P_{k},Q_{k})=1;$$ $$6.\; \frac{P_{k}}{Q_{k}}=\frac{P_{k-1}}{Q_{k-1}}+\frac{(-1)^{k}}{Q_{k}Q_{k-1}}\quad (k\geq 2);$$ $$7.\; P_{k}=q_{k}P_{k-1}+P_{k-2},$$ $$Q_{k}=q_{k}Q_{k-1}+Q_{k-2}\quad (k\geq 2);$$ $$8.\; \frac{P_{k}}{Q_{k}}-\frac{P_{k-2}}{Q_{k-2}}=\frac{q_{k}(-1)^{k-1}}{Q_{k}Q_{k-2}}\quad (k>2);$$ $$9.\; \frac{P_{n}}{Q_{n}}=\frac{P_{1}}{Q_{1}}+\sum_{k=2}^{n}\frac{(-1)^{k}}{\frac{Q_{k}}{Q_{k-1}}};$$ $$10.\; Q_{k}\geq 2^{\frac{k-2}{2}}\quad (k\geq 2).$$",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 29,
                },
                new TheoryContent
                {
                    TheoryId = 12,
                    Formula = @"Для разложения произвольного вещественного числа a в непрерывную дробь следует применить следующий алгоритм. Выделим целую и дробную части числа a: $$a=\left \lfloor a \right \rfloor+\left \{ a \right \}=a_{1}+\alpha ,$$ тогда $a=a_{1}+\frac{1}{\alpha _{1}},$ где $\alpha _{1}=\frac{1}{\alpha }>1.$",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 7,
                },
                new TheoryContent
                {
                    TheoryId = 12,
                    Formula = @"Далее применяем тот же прием к $\alpha _{1};$ $$\alpha _{1}=\left \lfloor \alpha _{1} \right \rfloor+\left \{ \alpha _{1} \right \}=a_{2}+\frac{1}{\alpha _{2}},$$ где $\alpha _{2}=\frac{1}{\left \{ \alpha _{1} \right \}}>1$ и так далее. В результате получаем $$a=a_{1}+\frac{1}{a_{2}+\frac{1}{a_{3}+\frac{1}{\ddots }}}=$$ $$[a_{1},a_{2},a_{3},...],$$ где $a_{1},a_{2},a_{3},...$ - целые числа.",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 10,
                },
                new TheoryContent
                {
                    TheoryId = 12,
                    Formula = @"Теорема. Любое иррациональное вещественное число однозначно представляется в виде бесконечной непрерывной дроби с целыми неполными частными. Обратно: значением всякой бесконечной непрерывной дроби с целыми неполными частными является иррациональное вещественное число.",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 8,
                },
                new TheoryContent
                {
                    TheoryId = 13,
                    Formula = @"Бесконечная непрерывная дробь $[a_{1},a_{2},...,a_{n},...]$ называется периодической, если существуют натуральные числа $k_{0}$ и t такие, что для любого $k>k_{0}$ выполняется равенство $a_{k+t}=a_{k},$ т. е. последовательность $\left \{ a_{k} \right \}_{k=1,2,...}$ начиная с некоторого момента является периодической $$[a_{1},...a_{k_{0}},a_{k_{0}+1},...,$$ $$a_{k_{0}+t},a_{k_{0}+1},...,a_{k_{0}+t},...]=$$ $$[a_{1},...,a_{k_{0}},$$ $$(a_{k_{0}+1},...,a_{k_{0}+t})].$$ t называется длиной периода, $k_{0}$ - индексом вхождения в период.",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 16,
                },
                new TheoryContent
                {
                    TheoryId = 14,
                    Formula = @"Определение. Действительное число называется квадратичной иррациональностью, если оно является корнем квадратного уравнения $ax^{2}+bx+c=0$ с  целыми коэффициентами.\\ Любая квадратичная иррациональность может быть представлена в виде $x+y\sqrt{N},$ где $x,y\in \mathbb{Q},$ а $N\in \mathbb{N}$ не является полным квадратом.",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 9,
                },
                new TheoryContent
                {
                    TheoryId = 14,
                    Formula = @"Теорема (Лагранж). Квадратичные иррациональности и только они могут быть представлены в виде бесконечной периодической непрерывной дроби.",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 4,
                },
                new TheoryContent
                {
                    TheoryId = 15,
                    Formula = @"Лемма. Пусть $\alpha =[c_{1},c_{2},...,c_{n},\alpha _{n}]$ - иррациональное число, где $\alpha _{n} =[c_{n+1},c_{n+2},...],$ $n\geq 1.$ Тогда при $k\geq 1$ $$\frac{1}{2Q_{k+1}Q_{k}}<\left | \alpha -\frac{P_{k}}{Q_{k}} \right |$$ $$< \frac{1}{Q_{k+1}Q_{k}}<\frac{1}{Q_{k}^{2}}.$$",
                    LinkId = -1,
                    TextCountStrings = 0,
                    FormulaCountStrings = 8,
                },
            };

            Link[] links =
            {
                new Link
                {
                    Id = 0,
                    Text = "Алгоритм Евклида",
                    TheoryId = 1,
                    ElementIndex = 0,
                },
                new Link
                {
                    Id = 1,
                    Text = "НОД",
                    TheoryId = 0,
                    ElementIndex = 0,
                },
                new Link
                {
                    Id = 2,
                    Text = "Расширенный алгоритм Евклида",
                    TheoryId = 7,
                    ElementIndex = 0,
                },
            };

            await db.DeleteAllAsync<TheoryContent>();
            await db.DeleteAllAsync<Link>();

            await db.InsertAllAsync(theoryContents);
            await db.InsertAllAsync(links);
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
