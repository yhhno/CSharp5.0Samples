using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Await
{
    class Program
    {
        static void Main(string[] args)
        {
        }


        static byte[] TryFetch(string url)
        {
            var client=new WebClient();
            try
            {
                //在调用client.DownloadData的大部分时间内，执行此方法的线程将保持静止，不执行任何实际的工作，而只是等待，
                return client.DownloadData(url);
            }
            catch (WebException)
            {
                
                throw;
            }
            return null;
        }

        /***********************顺序组合************************************/

        /***********************连续执行************************************/

        /******************异步编程**********************************/

        //由于方法始终是连续的，因此，您必须将简短的活动（如下载之前和下载之后）划分到多个方法中，要在方法的执行过程中挤出时间，API可提供运行时间较长的方法的异步（非阻塞）版本来启动操作（例如：启动下载），并在操作完成时存储用于执行的传入回调，然后立即返回到调用方，因此是一种很有用的方式，但为了便于调用方提供回调，需要将“后续”活动分解到一个单独的方法。


        //根据以上要求，修改 得到 V2
        static void TryFetchAsyncV1(string url, Action<byte[], Exception> callback)
        {
            var client=new WebClient();
            client.DownloadDataCompleted += (_, args) =>
            {
                if (args.Error == null) callback(args.Result, null);
                else if (args.Error is WebException) callback(null, null);
                else callback(null, args.Error);
            };
            client.DownloadDataAsync(new Uri(url));
        }
        //从中你可以了解到一些传递回调的不同方法：DownloadDataAsync方法要求事件处理程序已注册到DownloadDataCompleted事件，这也是传递该方法“后续”部分所使用的方法，TryFetchAsync本身还需要处理其调用方的回调，
        //你可以采用更加简单的方法，即直接将回调视为参数，而无需自己设置整个事件，
        //好在我们可以将Lambda表达式用于该事件处理程序，以便该处理程序能够直接捕获并使用“回调”参数；
        //如果你尝试使用已命名的方法，则必须考虑以某种方式将回调分派给事件处理程序，
        //请暂停片刻，考虑如何在不使用lambdas的情况下编写这段代码。


       //然而，我们需要关注的重点是控制流的变化情况，您不必使用这种语言的控制结构来表示控制流，相反，您可以模拟它们
          //。return 语句通过调用回调函数来模拟
          //。异常的隐式传播通过调用回调函数来模拟
          //。异常处理使用类型检查来模拟

       //当然这是一个非常简单的实例。随着所需的控制结构变得更加复杂，模拟该结构也会愈加复杂。
        //总体而言，我们实现了非连续，因而能够使线程在“等待”下载事执行其他操作，然而，我们也失去了使用控制结构来表示控制流的遍历。我们放弃了作为结构化式语言的结构

        /************异步方法********************/

        //以这种方式看待问题时，你就会明白下一版本C#如果通过异步方法为我们提供帮助：你可以通过它们来表示非连续顺序代码
        //使用新语法的TryFetch的异步版本

        static async Task<byte[]> TryFetchAsyncV2(string url)
        {
            var client=new WebClient();
            try
            {
                return await client.DownloadDataTaskAsync(url);
            }
            catch (WebException)
            {
                
                throw;
            }
            return null;
        }
        //您可以使用异步方式在执行代码的过程中“稍作休息”：你可以使用您最喜爱的控制结构来表示顺序组合，而且可以使用await表达式在执行过程中挤出时间，以便线程能够在这段时间内执行其他操作。
        //一种不错的思考方式，是想象异步方法也具有“暂停”和“播放”按钮，当正在执行的线程到达await表达式时，将触发“暂停”按钮，于是方法暂停执行，当处于等待状态的任务完成时，将触发“播放”按钮，于是方法恢复执行。
    }
}
