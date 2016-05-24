using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
    class Program
    {
        static void Main(string[] args)
        {
            ////part1 
            //Heater ht = new Heater();
            //ht.BoilWater();

            //part 3
            HeaterV3 heaterv3=new HeaterV3();
            AlarmV3 alarmv3=new AlarmV3();

            heaterv3.BoilEvent += alarmv3.MakeAlert;//注册方法
            heaterv3.BoilEvent += (new AlarmV3()).MakeAlert;//为匿名对象注册方法
            heaterv3.BoilEvent += DisplayV3.ShowMsg;//注册静态方法
            heaterv3.BoilEvent += DisplayV3.ShowMsg;//注册静态方法
            heaterv3.BoilEvent +=ss=>Console.WriteLine("这是什么鬼");

       

            heaterv3.BoilWater();//烧水，会自动调用注册过对象的方法



        }

        

        #region Part 1:提出问题及初步解决方案

        //part 1   
        //假设有个高档的热水器，当水温超过95度的时候：
        //一、扬声器会开始发出语音，告诉你水的温度；
        //二、液晶屏也会改变水温的显示，以提示水已经快烧开了。
        //现在需要写个程序来模拟这个烧水的过程，
        //我们将定义一个类来代表热水器，将它取名为Heater，它有代表水温的字段temperature，
        //当然，还有必不可少的水加热方法BoilWater()，
        //一个发出语音警报的方法MakeAlert()，
        //一个显示水温的方法，ShowMsg()。
        public class Heater
        {
            private int temperature;//水温

            //烧水
            public void BoilWater()
            {
                for (int i = 0; i < 100; i++)
                {
                    temperature = i;
                    if (temperature > 95)
                    {
                        MakeAlert(temperature);
                        ShowMsg(temperature);
                    }
                }
            }

            //发出语音警报
            public void MakeAlert(int temperature)
            {
                Console.WriteLine("Display:水快开了额，当前温度为：{0}度", temperature);
            }

            //显示水温
            public void ShowMsg(int temperature)
            {
                Console.WriteLine("Display谁快开了,当前的温度为：{0}度", temperature);
            }
        }
        #endregion

        //上面的例子显然能完成之前描述的工作，但是并不够好。
        //我们假设热水器由三部分组成： 热水器，警报器，显示器。他们来自于不同厂商并进行组装。
        //那么，热水器应该负责烧水，它不能发出警报也不能显示水温；在水烧开时，由警报器发出警报、显示器显示水温。


        #region Part 2 针对Part1的实现 提出问题 、分析及解决方案

        //热水器
        public class HeaterV2
        {
            private int temperature;
            //烧水
            private void BoilWater()
            {
                for (int i = 0; i < 100; i++)
                {
                    temperature = i;
                }
            }
        }

        //警报器
        public class Alarm
        {
            private void MakeAlert(int temperature)
            {
                Console.WriteLine("Alarm:嘀嘀嘀，水已经{0}度：", temperature);
            }
        }

        //显示器
        public class Dispaly
        {
            private void ShowMsg(int temperature)
            {
                Console.WriteLine("Dispaly:水已烧开，当前温度：{0}度", temperature);
            }
        }

        //这时候就出现了一个问题：如何在水烧开的时候通知报警器和显示器？
        //在继续进行之前，先了解一下Observer模式，
        //Oberver设计模式中主要包括如下两类对象
        //.Subject,即被监视对象，它往往包含着其他对象所感兴趣的内容，在本例中，热水器就是一个被监视对象，它包含的其他对象所感兴趣的内容，就是temperature字段，当这个值快到100时，会不断把数据发给监视它的对象。
        //.Observer,即监视者，它监视Subject，当Subject中的某个事发生的时候，会告知Observer，而Observer则会采取相应的行动。在本例中，Observer有警报器和显示器，它们分别采取的行动是发出警报和显示水温。

        //在本例中，事情发生的顺序是这样的：
        //1) 显示器和警报器告诉热水器，它们对热水器的温度比较感兴趣（注册）.
        //2) 热水器知道后保留对热水器和显示器的引用。
        //3) 热水器进行烧水这一动作，当水温超过95时，通过显示器和警报器的引用，自动调用警报器的MakeAlert()方法和显示器的ShowMsg（）方法。

        //类似这样的例子很多，GOF对它进行了抽象，成为Observer设计模式：
        //Observer设计模式是为了定义对象间的一种一对多的依赖关系，以便于当一个对象的状态改变时，其他依赖于它的对象会被自动告知并更新。
        //Observer设计模式是一种松耦合的设计模式。


        #endregion


        #region Part 3 实现实例的Observer设计模式

        //热水器
        public class HeaterV3
        {
            private int temperature;

            public delegate void BoilHanlder(int temperature); //声明委托
            public event BoilHanlder BoilEvent; //声明事件

            //烧水
            public void BoilWater()
            {
                for (int i = 0; i < 100; i++)
                {
                    temperature = i;
                    if (temperature > 95)
                    {
                        if (BoilEvent != null)//如果有对象注册
                        {
                            BoilEvent(temperature);//则调用所有注册对象的方法。
                        }
                    }
                }
            }
        }

        //警报器
        public class AlarmV3
        {
            public  void MakeAlert(int temperature)
            {
                Console.WriteLine("Alarm: 嘀嘀嘀，水已经{0}度：",temperature);
            }
        }
        //显示器
        public class DisplayV3
        {
            public static void ShowMsg(int temperature)
            {
                Console.WriteLine("Display:水已经烧开了，当前温度为：{0}度",temperature);
            }
        }
        #endregion

        //尽管part3 中的实例很好地完成了我们想要完成的工作，
        //但是还有一个问题没有解决：为什么.NET Framework 中的事件模型和前面的不同?为什么有很多的EventArgs参数：

        //在回答上述问题之前，首先弄清楚.NET Framework 的编码规范
        //。委托类型的名称都应该以EventHanlder结束
        //。委托的原型定义一个void返回值，并接受两个输入参数：一个Object类型，一个EventArgs类型（或者继承自EventArgs）
        //。事件的命名为委托去掉EventHanlder之后剩余的部分。
        //。继承自EventArgs的类型应该以EventArgs结尾

        //再做一下说明：
        //1. 委托声明原型中的object类型的参数代表了Subject，也就是监视对象，在本例中是Heater（热水器），回调函数（比如Alarm的MakeAlert）可以通过它访问触发事件的对象（heater)
        //2. EventArgs 对象包含了Observer所感兴趣的数据，在本例中为temperature。

        //


    }
}
