using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    public static partial  class Delegate
    {
        #region  Delegate Definition & Introduce Part
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
    }
}
