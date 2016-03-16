/*******************Data Type ********************
 ********************* 2015-12-18: ********
 * 添加文件:  Program.cs
 * 内    容:  ref 关键字 以及 值类型和引用类型调用
 * 遗    留: 无
 * 

 * 
 * */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataType
{
    class Program
    {
        static void Main(string[] args)
        {

            #region  call value type
            //int b = 1;
            //ValueTypeTest(b);
            //Console.WriteLine(b);
            //Console.ReadKey();
            #endregion

            #region call  reference  type

            //bb为引用类型，在只修改b的成员的时候，加不加ref 都一样，引用类型本身并不包含数据，仅仅维持着对数据的引用 
            TestClass bb = new TestClass();
            bb.b = 1;
            ReferenceTypeTest(bb);
            Console.WriteLine(bb.b);
            Console.ReadKey();
            
            #endregion

        }

        #region part1  ref 关键字
        //C#中 ref的意思就是按引用传递，可以参考C++
        //在C#中 ref 对于值类型对象的作用显而易见，
        //而对于引用类型，如需修改应用类型内部的数据，则不需使用ref关键字。否则当被调用函数内部需要更改引用本身时，比如在函数内部重新定位对象的引用时，需要ref关键字

        // 值类型 
        // 1:如果简单的调用这个swap函数，比如swap(a,b)那么你根本没有办法交换a、b这两个变量的值， 因为x和y都是形参，在swap返回的时候，x和y都被释放了
        int a = 10, b = 20;
        void swap(int x, int y)
        {
            int temp = x;
            x = y;
            y = temp;
        }

        //1.1：这样x与a、b与y指向的是同一个内存地址，相当于对x的操作也就相当于对a的操作
        void swap2(ref int x, ref int y)
        {
            int temp = x;
            x = y;
            y = temp;
        }

        #endregion



        #region part2 value type & reference type


        

        //值类型
        static void ValueTypeTest(int b)
        {
            b = 2;
        }

        class TestClass
        {
            public int b;
        }

        //引用类型
        static void ReferenceTypeTest(TestClass b)//b为引用类型
        {
            b.b = 2;
        }

        #endregion

    }
}
