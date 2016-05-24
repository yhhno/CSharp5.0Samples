using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //int i = 1;
            //int j = 2;
            //int k = 3;
            //string l = "4";
            //string answer = i + j + k + l;
            //Console.WriteLine(answer);
            //string answer2 = l + i + j + k;
            //Console.WriteLine(answer2);

            //faseter();

            //test();


            testawait();
            Console.ReadKey();

        }

        public static void test()
        {
            int j = 5;
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("{0}:{1}:{2}",i,i%j,(float)i/j);
            }
        }



        public static  void testawait()
        {
            Console.WriteLine("1.1");
             sleep();
            Console.WriteLine("1.2");
            Console.WriteLine("1.3");
        }

        static async Task sleep()
        {
             await  Task.Factory.StartNew(() =>
            {
                Console.WriteLine("2.1");
                Thread.Sleep(4000);
                Console.WriteLine("2.2");
            });
        }



        public static void faseter()
        {

            Console.WriteLine("第一次开始");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 10000000; i++)
            {
                string str = "a";
                str += "b";
                str += "c";
            }
            //Thread.Sleep(1000);
            watch.Stop();
            Console.WriteLine("第一次消耗时间{0}", watch.Elapsed);

            Console.WriteLine("第二次开始");
            watch.Reset();
            watch.Start();
            for (int i = 0; i < 10000000; i++)
            {
                string a = "a";
                string b = "b";
                string c = "c";
                string str2 = a + b + c;

            }
            Console.WriteLine("第二次消耗时间{0}", watch.Elapsed);

            watch.Reset();
            watch.Start();
            Console.WriteLine("第二次开始");
            for (int i = 0; i < 10000000; i++)
            {
                string str3 = "a" + "b" + "c";
            }
            Console.WriteLine("第三次消耗时间{0}", watch.Elapsed);
        }

        

    //     Console.WriteLine("\n第" + i + "次比较：");
    //            Stopwatch watch = new Stopwatch();
    //            watch.Start();
    //            var result = list.OrderBy(ss => ss).ToList();
    //            watch.Stop();
    //            Console.WriteLine("\n快速排序消耗时间：" + watch.ElapsedMilliseconds);
    //            Console.WriteLine("输出前十个数：" + string.Join(",", result.Take(10).ToList()));

    //            watch.Reset();
    //            watch.Start();
    //            result = Sort.BubbleSort(list);
    //            watch.Stop();
    //            Console.WriteLine("\n冒泡排序耗费时间：" + watch.ElapsedMilliseconds);
    //            Console.WriteLine("输出前十个数：" + string.Join(",", result.Take(10).ToList()));
    }
}
