using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Var
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        //从C#3.0 开始，在方法范围内声明的变量可以具有隐式类型var，隐式类型的本地变量是强类型变量（就好像你已经声明该类型一样），但由编译器确定类型，
        static void vartest()
        {
            //两个语句声明在功能上是等效的
            var i = 10;//implicityly typed
            int ii = 10;//explicitly typed
        }

        //下面的示例演示连个查询表达式，
        //在第一个查询表达式中，允许但不需要使用var，因为可以将查询结构的类型显式声明为IEnumerable<string>.
        //但是，在第二个表达式中必须使用var，因为结果是一个匿名类型集合，而该类型的名称只有编译器本身可以访问，
        //请注意 ，在第二个示例中，foreach迭代标量item也必须转换为隐式变量

        static void vartest1()
        {
            //var is optional because the select clause specifies a string 
            string[] words = { "apple", "strawberry", "grape", "peach", "banana" };
            var wordQuery = from word in words
                where word[0] == 'g'
                select word;
            //Because each element in the sequence is a string ,not an anonymous type,var is optional here alse.
            foreach (string s in wordQuery)
            {
                Console.WriteLine(s);
            }
        }

        static void varetest2()
        {
            ////var is required because the select clause specifieds an anonymous type
            //var custQuery = from cust in customers
            //    where cust.city == "p"
            //    select new {cust.Name, cust.Phone};
            ////var must be used because each item in the sequence is an anonymous type
            //foreach (var item in custQuery)
            //{
            //    Console.WriteLine("Name={0},Phone={1}",item.Name,item.Phone);
            //}
        }
       
    }
}
