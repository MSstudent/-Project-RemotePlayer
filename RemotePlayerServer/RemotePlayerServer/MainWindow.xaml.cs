using Microsoft.Test.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RemotePlayerServer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private static byte[] result = new byte[1024];
        private int myProt;
        //private static Socket serverSocket;
        //private Thread myThread;
        private IPAddress ip = IPAddress.None;
        private UdpReceiver udp;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取本机IPV4地址
        /// </summary>
        /// <returns>IPv4地址</returns>
        private IPAddress GetIPv4Addr()
        {
            string hostName = Dns.GetHostName();
            IPAddress[] addressList = Dns.GetHostAddresses(hostName);

            foreach (IPAddress _ip in addressList)
            {
                if (_ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return _ip;
                }
            }
            return IPAddress.Parse("127.0.0.1");
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            myProt = Convert.ToInt32(hostPort.Text);
            string hostName = Dns.GetHostName();
            ip = GetIPv4Addr();

            hostIP.Text = ip.ToString();
        }

        //private void ListenClientConnect()
        //{
        //    while (true)
        //    {
        //        Socket clientSocket = serverSocket.Accept();
        //        clientSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello"));
        //        Thread receiveThread = new Thread(ReceiveMessage);
        //        receiveThread.Start(clientSocket);
        //    }
        //}

        //private void ReceiveMessage(object clientSocket)
        //{
        //    Socket myClientSocket = (Socket)clientSocket;
        //    while (true)
        //    {
        //        try
        //        {
                    
        //            //通过clientSocket接收数据
        //            myClientSocket.Receive(result);
        //            int receiveContent = Convert.ToInt32(result[0]) - 48;

        //            //新建高优先级线程处理接收数据
        //            Thread dealThread = new Thread(() =>
        //            {
        //                this.Dispatcher.Invoke(DispatcherPriority.Normal,
        //                    new Action(() => this.dealWithReceive(receiveContent)));
        //            });
        //            dealThread.Start();
        //        }
        //        catch (Exception ex)
        //        {
        //            //新建线程输出异常信息
        //            Thread exThread = new Thread(() =>
        //            {
        //                this.Dispatcher.Invoke(DispatcherPriority.Normal,
        //                    new Action(() => hostStatus.Text += "\n" + DateTime.Now.ToString() + "：发生异常 — " + ex.Message));
        //            });
        //            exThread.Start();

        //            myClientSocket.Shutdown(SocketShutdown.Both);
        //            myClientSocket.Close();
        //            break;
        //        }
        //    }
        //}

        /// <summary>
        /// 开始Udp服务
        /// </summary>
        private void startHost_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                udp = new UdpReceiver(myProt);
                udp.StartReceive();
                startHost.IsEnabled = false;
            }
            catch (Exception ee)
            {
                MessageBoxResult msg = MessageBox.Show(ee.Message);
                startHost.IsEnabled = true;

            }
            
            //Thread nThread = new Thread(() =>
            //{
            //    this.Dispatcher.Invoke(DispatcherPriority.Normal,
            //        new Action(() => hostStatus.Text += "\n" + DateTime.Now.ToString() +"：启动监听成功"));
            //});
            //nThread.Start();

            //serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ////serverSocket = new Socket(SocketType.Stream, ProtocolType.Udp);
            //serverSocket.Bind(new IPEndPoint(ip, myProt));  //绑定IP地址：端口
            //serverSocket.Listen(3);    //设定最多10个排队连接请求

            //myThread = new Thread(ListenClientConnect);
            //myThread.Start();
        }

        //private void dealWithReceive(int receive)
        //{
        //    switch (receive)
        //    {
        //        case 0: Keyboard.Release(Key.Space); Keyboard.Release(Key.Down); Keyboard.Press(Key.Up); break; //自动前进
        //        case 1: Keyboard.Release(Key.Up); break; //停止自动前进
        //        case 2: Keyboard.Release(Key.LeftShift); Keyboard.Release(Key.Right); Keyboard.Press(Key.Left); break; //左转
        //        case 3: Keyboard.Release(Key.LeftShift); Keyboard.Release(Key.Left); Keyboard.Press(Key.Right); break; //右转
        //        case 4: Keyboard.Release(Key.Left); Keyboard.Release(Key.Right); break; //不转
        //        case 5: Keyboard.Type(Key.Space); break; //单点手刹
        //        case 6: Keyboard.Press(Key.Space); break; //按住手刹
        //        case 7: Keyboard.Press(Key.Down); break; //按住后退
        //        case 8: Keyboard.Release(Key.Down); break; //停止后退
        //        case 9: Keyboard.Press(Key.LeftShift); break; //氮氧加速
        //    }
        //}
        
    }
}
