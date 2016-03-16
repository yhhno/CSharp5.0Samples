using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using System.Threading; //thread
using System.Runtime.Remoting.Messaging;//AsyncResult

namespace Delegate
{
    public static partial  class Delegate
    {

    



        #region part 1  Delegate Definition & Introduce Part
        /*
         * 委托是C#最为常见的内容。与类、枚举、结构、接口一样，委托也是一种类型，类是对象的抽象，而委托可以看成是函数的抽象。
         * 一个委托代表了具有相同参数列表和返回值的所有函数。
         * 
         * delegate int GetCalulateValueDelegate(int x, int y);
         *我们定义一个委托，这个委托代表着一类函数，这些函数的第一个参数是整数型的X,第二个参数是整数型的Y，而返回值是一个整数，在这里我们为了描述方便，我们把这一类的函数成为具有相同签名(signature)的函数. 注意：这个签名并不是数字签名中的概念。而是知识表示这类函数具有相同的参数列表和返回值。
         * 
         *既然委托是一种类型，那么它就可以用来定义参数、变量、以及返回值。由委托定义的变量 用于保存具有相同签名的函数实体。
         *C#和C++不同，C++中的函数指针 只能保存全局的或者静态的函数。而C#中的委托实体可以指任何函数。
         *
         * 委托作为变量
         * 
         * 委托作为参数：将委托作为参数，可以把函数本身的处理逻辑 抽象出来，而让决定者决定最终使用什么样的逻辑去处理
         * 
         * 委托作为返回值： 一般会用到在“根据不同情况决定使用不同的委托”这样的情形下，这有点像工厂模式
         * 
         * 
         * 
         * 
         * 委托作为参数，在C#中非常常见，
         * 比如线程的创建，需要一个Threadstart或者ParameterizedThreadStart委托作为参数，而在线程执行的时候，将这个参数所指代的函数用作线程执行体。
         * 
         * 再比如：List<T>类型的Find方法的参数也是一个委托，它把“怎么去查找”或“怎么样才算找到” 这个问题留给开发人员。开发人员只需要定义一个参数为T，返回类型为布尔型的函数，然后实现函数体，并将函数作为参数传给Find方法，就可以完成集合中元素的查找。
         */

        //下面我们来看一个例子
        public delegate int GetCalculateValueDelegate(int x, int y);//创建一个委托实体，使其指代程序中的AddCalculator函数，接下来就可以想使用函数本身一样，使用这个委托实体来获得计算的结果
      public   static int AddCalculator(int x, int y)//委托用来定义变量
        {
            return x + y;
        }

        public static int SubCalculator(int x, int y)//委托来定义变量
        {
            return x - y;
        }




        // 这个函数中，Calculator函数的一个参数就是一个委托，事实上Calcultor对x和y将会做什么处理，它本身并不知道。如何处理x和y由GetCalculateValueDelegate来决定，在调用的方法中，，我们把AddCalculator方法当做参数传递给Calculator，表示让Calculator用AddCalculator的逻辑来处理x和y，这也很形象：Calculator说：我不知道要如何处理x和y，让del去处理吧，于是把x和y扔给了 del。

        //这种做法其实跟“模板方法模式”有点类似，在模板方法中，可以将可变的部分留给子类去重写，而将不变的部分由父类实现，
        //那么在委托作为参数的情况下，Calculator可以自己处理不变的逻辑，而将“具体怎么做”的事情委托给他人去办理
       public  static int Calculator(GetCalculateValueDelegate del, int x, int y)//委托定义参数
        {
            return del(x, y);
        }

        //todo: 委托作为返回值

        #endregion

        //todo:  

        #region part 2  Demo ： AsynChronous  with Delegate

        /*****************************
         * 生活中简单的例子
         * 1.小明在烧水，等水烧开后，将开水灌入开水瓶，然后整理家务。
         * 2.小文在烧水，在烧水的过程中整理家务，等水烧开后，放下手中的家务活，将开水贯入开水瓶，然后继续整理家务。
         * 这是日常生活中很常见的情形，小文的办事效率明显要高于小明，
         * 从C#程序执行的角度考虑，小明使用的是同步处理方式，小文则使用的是异步处理方式。
         * 同步： 事务是按照顺序一件一件处理；而异步：将子操作从住操作中分离出来，主操作继续进行，子操作在完成处理的时候通知主操作。
         * ******/



       public static TimeSpan Boil()
       {
           Console.WriteLine("水壶：开始烧水.....");
           Thread.Sleep(6000);
           Console.WriteLine("水壶：水已经烧开!");
           return TimeSpan.MinValue;
       }

       public static TimeSpan BoilUpdate()
       {
           DateTime begin = DateTime.Now;
           Console.WriteLine("修改后 水壶：开始烧水。。。");
           Thread.Sleep(6000);
           Console.WriteLine("修改后 水壶：水已经烧开！");
           return DateTime.Now - begin;
       }

       public delegate TimeSpan BoilingDelegate();

       public static void BoilingFinishedCallBack(IAsyncResult result)
       {
           AsyncResult asyncResult = (AsyncResult)result;
           //为了得到委托实例以便获取异步处理函数的返回值，采取以下转换，这样才能获得调用异步函数的委托的实体
           BoilingDelegate del = (BoilingDelegate)asyncResult.AsyncDelegate;
           del.EndInvoke(result);// EndInvoke会使得调用线程阻塞，直到异步函数处理完成，显然紧接着BeginInvoke 后面的EndInvoke的使用方法与使用同步方法等价
           Console.WriteLine("小文：将热水灌倒热水瓶");
           Console.WriteLine("小文：继续整理家务");

       }

       public static void BoilingFinishedCallBackUpdate(IAsyncResult result)
       {
           AsyncResult asyncResult = (AsyncResult)result;
           //为了得到委托实例以便获取异步处理函数的返回值，采取以下转换，这样才能获得调用异步函数的委托的实体
           BoilingDelegate del = (BoilingDelegate)asyncResult.AsyncDelegate;
           Console.WriteLine("修改后 烧水一共用去{0}时间", del.EndInvoke(result));
           Console.WriteLine("修改后 小文：将热水灌倒热水瓶");
           Console.WriteLine("修改后 小文：继续整理家务");
       }


        #endregion


        #region part 3 MyRegion

       public  delegate void MethodInvoker();

       public static void Foo()
       {
           int intAvailableThreads, intAvailableAsyncThreads;
           ThreadPool.GetAvailableThreads(out intAvailableThreads, out intAvailableAsyncThreads);
           string strMessage = string.Format(@"Is Thread Pool:{0};Thread ID {1} Free Thread {2}", Thread.CurrentThread.IsThreadPoolThread.ToString(), Thread.CurrentThread.GetHashCode(), intAvailableThreads);
           Console.WriteLine(strMessage);
           Thread.Sleep(10000);
           return;
       }

       public static void CallFoo()
       {
           MethodInvoker simpleDelegate = new MethodInvoker(Foo);
           //MethodInvoker simpleDelegate = Foo;
           for (int i = 0; i < 15; i++)
           {
               simpleDelegate.BeginInvoke(null, null);

           }
       }
        #endregion


    }
}
