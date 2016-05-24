/*******************Delegate ********************
 ********************* 2015-12-10: ********
 * 添加文件:  Delegate.cs， Delegate_Test.cs
 * 内    容:  Delgate基本概念，使用情况，添加简单的单元测试
 * 遗    留:  委托作为返回值 案例
 * 
 * *********************2015-12-15**********************
 * 添加文件:  无
 * 内    容:  part 2（Async with Delegate），part 3（）
 * 遗    留:  无
 * 
  ***/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading; //thread

namespace Delegate
{
    public static partial class Delegate
    {

        static void Main(string[] args)
        {
            #region   Call part 1: Delegate Definition &Introduce Part
            ////针对Main函数，本来需要调用AddCalculator函数，却通过d调用了，也就是后续对AddCalculator的操作有d代劳，
            ////对比生活：本来要小明去办公室去拿粉笔盒，由于小明和小文是好朋友，因此小明要小文代他去拿，于是小文成了小明的代理，小明委托小文去拿粉笔盒


            ////委托定义变量
            //GetCalculateValueDelegate d = AddCalculator;
            //Console.WriteLine("委托是一种类型，那么它就可以用来定义参数、变量、以及返回值");
            //Console.WriteLine("委托定义变量begin");
            //Console.WriteLine(d(10, 20));
            //Console.WriteLine("委托定义变量end");

            //Console.WriteLine("************************");
            ////委托定义参数
            ////
            //Console.WriteLine("委托定义参数Begion");
            //Console.WriteLine(Calculator(AddCalculator, 10, 20));
            //Console.WriteLine("委托定义参数End");
            //Console.ReadKey();
            #endregion

            #region  call part 2: Asyns with Delegate

            /***************************
            * 这是一个最简单的异步调用的例子，没有对异步调用函数做任何参数传递以及返回值校验。
            * 这个例子反应小文烧水的流程，
            * 首先，小文将水壶放在炉子上，
            * 在定义好委托以后，就是用BeginInvoke方法开始异步调用，既让水壶开始烧水，
            * 于是小文开始整理家务，水烧开后，C#的异步模型会触发由BenginInvoke方式所指定的回调函数，
            * 也就是水烧开后的处理逻辑由这个回调函数定义，
            * 此时小文将水灌入开水瓶，并继续整理家务。
            * 
            * 由此可见，在C#中实现异步调用并不复杂，
            * 首先创建一个异步处理函数，并针对其定义一个委托，
            * 然后在调用函数的时候，使用委托的BeginInvoke方法，
            * 制定在函数处理完成时的回调函数（如果不需要对完成事件做处理，可以给null值），
            * 并制定所需的函数（如果没有参数，也可以给null值),最后在回调函数中完成事件
            * bug: EndInvoke会使得调用线程阻塞，直到异步函数处理完成，显然紧接着BeginInvoke 后面的EndInvoke的使用方法与使用同步方法等价
             * EndInvoke调用的返回值就是异步处理函数的返回值
             * 
             * 
             * 然后对程序进行修改
             * 
             * 我们就可以在EndInvoke的时候获得由BoilUpdate异步函数返回的时间值，
             * 事实上如果我们定义的BoilingDelegate 委托有参数列表，我们也可以在BeginInvoke的时候，将所需的参数传递给异步处理函数，
             * 
             * 
             * BeginInvoke/EndInvoke函数的签名与定义它们的委托签名有关
             * 
             * 
             * net 处理异步函数的调用，事实上通过线程来完成 ，这个过程有以下几个特点哦
             *   1异步函数由线程完成，这个线程是net线程池中的线程，
             *   2通常情况下，NET线程池拥有500个线程（当然这个数字可以设置），每当调用BeginInvoke开始出来异步时，
             *   异步处理函数就有线程池中的某个线程负责执行，而用户无法控制具体由那个线程执行
             *   3 由于线程池中的线程数量有限，因此当线程池中的线程被完全占用时，新的调用请求将使函数 不得不等待空余线程的出现，此时程序的效率会有所下降
             *   
             * 为了验证这些特点   part 3
             * 
            * ***/
            Console.WriteLine("小文：把水壶放在炉子上");
            //BoilingDelegate d = new BoilingDelegate(Boil);
            //BoilingDelegate d = Boil;
            //IAsyncResult result = d.BeginInvoke(BoilingFinishedCallBack, null);//AsyncCallBack is Delegate ，委托做参数

            BoilingDelegate d = BoilUpdate;
            IAsyncResult result = d.BeginInvoke(BoilingFinishedCallBackUpdate, null);//AsyncCallBack is Delegate ，委托做参数
            Console.WriteLine("小文：开始整理家务。。。。");
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("小文：整理第{0}项家务。。。", i + 1);
                Thread.Sleep(1000);
            }

            Console.ReadKey();



           
            #endregion

            #region call part 3
            /****************
             * 程序在开始的时候，将线程池中的最大线程数设置为10，然后做15异步调用，每个调用都停留10秒当做处理本身要消耗的时间，
             * 从程序的执行我们可以看到，当前10个异步调用完全开始后，新的异步调用就会等待（注意不是主线程在等待），直到线程池中有线程空闲出来*******************/
                    //ThreadPool.SetMaxThreads(10, 10);
                    //CallFoo();
                    //Console.ReadKey();



           
            #endregion
        }

    }
}