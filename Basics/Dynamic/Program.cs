using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic
{
    class Program
    {
        static void Main(string[] args)
        {
            //part1
            //dynamicAndObject();


            ////part 2  下面的实例中以多个声明使用dynamic，main也用运行时类型检查对比编译时类型检查
            //ExampleClass ex = new ExampleClass();
            //Console.WriteLine(ex.exampleMethod(10));
            //Console.WriteLine(ex.exampleMethod("Value"));

            ////The Following line cause a compiler error because exampleMethod takes onle one argument.
            ////Console.WriteLine(ex.exampleMethod(10,4));

            //dynamic dynamic_ec=new ExampleClass();
            //Console.WriteLine(dynamic_ec.exampleMethod(10));


            ////Because dynamic_ec is dynamic,The following call to exampleMethod withe two arguments does not product an error at compile time,however ,it does cause a run-time error.
            ////Console.WriteLine(dynamic_ec.exampleMethod(10,4));



            //part 3
            //在下面的代码中实例方法exampleMethod1只有一个形参，则编译器会将对该方法的第一个调用ec.exampleMethod1（10，4）识别为无效，原因是它包含两个实参，该调用将导致编译器错误，编译器不会检查对该方法的第二个调用dynamic_ec.exampleMethod1（10,4），原因是dynamic_ec的类型为dynamic，因此，不会报告编译器错误，但是该错误不会被无限期疏忽，它将在运行时捕获，并导致运行时异常。

            //在这些示例中，编译器的作用是将有关每个语句的预期作用的信息一起打包到类型化为dynamic的对象或表达式。在运行时，将对存储的信息进行检查，并且任何无效的语句将导致运行时异常
           //大多数动态操作的结果是其本身dynamic。例如，如果将树胶指针放在以下实例中的testSum上，则IntelliSense将显示烈性（局部变量）dynamic testSum。

            dynamic dd = 1;
            var testSum = dd + 3;
            //Reset the mouse pointer over testSum in the following statement.
            Console.WriteLine(testSum);
            //结果不是dynamic的操作包括从dynamic到另一类型的转换，以及包括类型为dynamic的参数的构造函数调用，例如，以下声明的testInstance的类型为ExampleClass，而不是dynamic
            var testInstance=new ExampleClassV2();

            ExampleClassV2 ec = new ExampleClassV2();
            //The following call to exampleMethod1 causes a compiler error if exampleMehod1 has only one parameter, Uncomment the line to see the errro.
            //ec.exampleMethod(10, 3);


            dynamic dynamic_ec = new ExampleClassV2();
            //The following line is not identified as an error by the compiler, but it causes a run-time exception.
            dynamic_ec.exampleMethod1(10, 4);

            //The following calls also do not cause comiler errors .whether appropriates exist or not
            dynamic_ec.someMethod("some argument", 7, null);
            dynamic_ec.nonexistMethod();


            //tip 3.2 使用类型为dynamic的参数重载决策
            //如果方法调用中的一个或多个参数具有类型dynamic，或者方法调用的接收方的类型为dynamic，则会在运行时（而不是在编译时）进行重载决策，在下面的示例中，，如果唯一可访问的exampleMethod2方法定义为接收字符串参数，则将d1作为参数发送为不会导致编译器错误，但却会导致运行时错误，重载决策之所以会在运行是错误，原因是d1的运行时类型为int，而exampleMethod2需要字符串
            //Valid
            ec.exampleMethod2("a string");

            //The following statement does not cause a compiler error,even though ex is not dyanmic ,A run-time exception is raised because the run-time type of d1 is int
            ec.exampleMethod1(1);

           //The following statement does cause a compiler error.
            ec.exampleMethod2("3");



            Console.ReadKey();
        }

        //在通过dynamic类型实现的操作中，该类型的作用是绕过编译时类型检查，改为在运行时解析这些操作。
        //dynamic类型简化了对COM API(例如Office Automation API),动态API（例如IronPython库）和HTML文档对象模型（DOM）的访问。
        //大多数情况下，dynamic类型和object类型的行为是一样的，但是，不会用编译器对包含dynamic类型表达式的操作进行解析或类型检查，编译器将有关该操作信息打包在一起，并且该信息以后用于计算运行时操作，在此过程中，类型dynamic的变量会编译到object的变量中，因此，类型dynamic只在编译时存在，在运行时则不存在

        //将类型为dynamic的变量和类型为object的变量对比，若要在编译时验证每个标量的类型，请将鼠标指针放在WriteLine语句的dyn或obj上，intelliSense(智能提示)显示了dyn的“dynamic"和obj的"object"
        static void dynamicAndObject()
        {
            dynamic dyn = 1;
            object obj = 1;

            //1若要查看dyn和obj之间的差异，添加下面两行代码
            //1.1 下面语句不会报错，编译时不会检查包含dyn的表达式，原因是dyn的类型是dynamic
            dyn = dyn + 3;
            //1.2 下面语句会报错，object和int
            //obj = obj + 3;


            //WriteLine语句显示dyn和obj的运行时类型，此时两者具有相同的整数类型，将都生成System.Int32
            Console.WriteLine(dyn.GetType());
            //若将鼠标放在dyn上，将显示dynamic类型
            Console.WriteLine(obj.GetType());
        }


        /************tip2上下文*************/
        //dynamic 关键在可以直接出现或作为构造类型的组件在下列情况中出现
        //2.1 在声明中，作为属性，字段、索引器、参数、返回值或类型约束的类型，下面的类定义在几个不同的声明中使用dynamic
        public class ExampleClass
        {
            // a dynamic field
            static dynamic field;

            //a dynamic property
            dynamic prop { get; set; }

            // a dynamic return type and a dynamic parameter type.
            public dynamic exampleMethod(dynamic d)
            {
                // A dynamic local variable
                dynamic local = "Local variable";
                int two = 2;
                if (d is int)
                {
                    return local;
                }
                else
                {
                    return two;
                }
            }

        }

        //2.2 在显式类型转换中，作为转换的目标类型

        static void convertToDynamic()
        {
            dynamic d;
            int i = 20;
            d = (dynamic) i;
            Console.WriteLine(d);

            string s = "Example string";
            d = (dynamic) s;
            Console.WriteLine(d);

            DateTime dt = DateTime.Today;
            d = (dynamic)dt;

            Console.WriteLine(d);

        }

        //2.3 在以类型充当值（如is运算符或as运算符右侧）或者作为typeof的参数成为构造类型的一部分的任何上下文，例如：可以在下列表达式中使用dynamic

       static void test23()
       {
           int i = 8;
           dynamic d="22";
           //with the is operator. The dynamic type behaves like object.The following expression returns true unless someVar has the value null.

           if (d is dynamic)
           {
           }

           //with the as operator
           d = i as dynamic;

           //with typeof , as part of a constructed type.
           Console.WriteLine(typeof(List<dynamic>));

           //the following statement causes a compiler error.
           //Console.WriteLine(typeof(dynamic));
       }




        //part 3
        //新类型dynamic，该类型是一个静态类型，但是类型为dynamic的对象会跳过静态类型检查，大多数情况下，该对象就像类型object一样，在编译时，将假定类型化为dynamic的元素支持的的任何操作，因此，你不必考虑对象时从COM API、从动态语言（例如IronPython），从HTML文档对象模型（DOM），从反射还是从程序中的其他位置获取自己的值，但是，如果代码无效，则在运行时会捕获到错误。



        
        class ExampleClassV2
        {

            
            public ExampleClassV2()
            {
            }

            public ExampleClassV2(int id)
            {
                
            }

            public void exampleMethod1(int i)
            {
                
            }
            public void exampleMethod2(string str)
            {
                
            }
        }


        //tip 3.1 转换
        //动态对象和其他类型之间的转换非常简单，这样，开发人员将能够在动态行为和非动态行为之间切换
        //任何对象都可隐式转换为动态类型

        dynamic d1 = 7;
        dynamic d2 = "String";
        dynamic d3 = System.DateTime.Today;
        dynamic d4 = Process.GetProcesses();

        //反之，隐式转换也可动态地应用于类型Wiedynamic的任何表达式
        int i = d1;
        string str = d2;
        DateTime dt = d3;
        Process[] procs = d4;


  


    }
}
