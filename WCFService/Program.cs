using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;

namespace WCFService
{
	static class Program
	{

		/// <summary>
		/// 接口声明
		/// </summary>
		[ServiceContract]
		public interface IClass
		{
			[OperationContract]
			int Add(int A, int B);
		}

		/// <summary>
		/// 代码实现
		/// </summary>
		public class SClass : IClass
		{
			public int Add(int A, int B)
			{
				return A + B;
			}
		}//End Class

		[STAThread]
		static void Main()
		{
			//两个地址,前面的是Pipe管道.在本机使用.  后面的是TCP协议,可跨网
			Uri[] URI = new Uri[] { new Uri("net.pipe://127.0.0.1/SClass"), new Uri("net.tcp://127.0.0.1:4255/SClass"), new Uri("HTTP://127.0.0.1:4255/SClass") };

			ServiceHost sh = ZZJCore.SuanFa.WCFInvoke.CreateService(URI[2], typeof(SClass), typeof(IClass));
			sh.Opening += new EventHandler((object sender, EventArgs e) => { });
			sh.Opened += new EventHandler((object sender, EventArgs e) => { });
			sh.Open();

			while (true)
			{
				Thread.Sleep(100);
			}

			sh.Close();
		}

	}//End Class
}