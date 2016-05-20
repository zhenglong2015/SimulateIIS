using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZTBIIS
{
    public partial class IIS : Form
    {
        public IIS()
        {
            InitializeComponent();
        }

        private void btnStar_Click(object sender, EventArgs e)
        {
            //创建Socket
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //绑定端口和IP
            socket.Bind(new IPEndPoint(IPAddress.Parse(txtIP.Text), int.Parse(txtPort.Text)));
            //开始监听
            socket.Listen(10);
            //开启线程，接收用户数据
            ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessRequest), socket);
        }
        //处理Http请求
        private void ProcessRequest(object state)
        {
            Socket socket = state as Socket;
            while (true)
            {
                var proxSocket = socket.Accept();
                byte[] data = new byte[1024 * 1024 * 2];
                int len = proxSocket.Receive(data,0,data.Length,SocketFlags.None);
                string requestText = Encoding.Default.GetString(data,0,len);
                //解析 请求报文 处理请求报文，返回响应内容

                HttpContext context = new HttpContext(requestText);
                IHttpHandler app = new HttpApplication();
                app.ProcessRequest(context);

                proxSocket.Send(context.Response.GetResponseHeader());
                proxSocket.Send(context.Response.Body);
            }
        }
    }
}
