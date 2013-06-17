using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Test.Input;

namespace RemotePlayerServer
{
    public class UdpReceiver
    {
        private UdpClient _udpClient;
        private Thread _recvThread;

        
        private void InitKeyChange()
        {
            
        }

        /// <summary>
        /// 用端口号实例化UdpReceiver
        /// </summary>
        /// <param name="port">监听端口号</param>
        public UdpReceiver(int port)
        {
            _udpClient = new UdpClient(port);
        }

        /// <summary>
        /// 新建线程开始监听UDP
        /// </summary>
        public void StartReceive()
        {
            _recvThread = new Thread(new ThreadStart(Receive));
            _recvThread.Start();
        }

        /// <summary>
        /// 处理接收到的数据
        /// </summary>
        /// <param name="receive">接收到的数据</param>
        private void dealWithReceive(int receive)
        {
            switch (receive)
            {
                case 0: Keyboard.Release(Key.Space); Keyboard.Release(Key.Down); Keyboard.Press(Key.Up); break; //自动前进
                case 1: Keyboard.Release(Key.Up); break; //停止自动前进
                case 2: Keyboard.Release(Key.LeftShift); Keyboard.Release(Key.Right); Keyboard.Press(Key.Left); break; //左转
                case 3: Keyboard.Release(Key.LeftShift); Keyboard.Release(Key.Left); Keyboard.Press(Key.Right); break; //右转
                case 4: Keyboard.Release(Key.Left); Keyboard.Release(Key.Right); break; //不转
                case 5: Keyboard.Type(Key.Space); break; //单点手刹
                case 6: Keyboard.Press(Key.Space); break; //按住手刹
                case 7: Keyboard.Press(Key.Down); break; //按住后退
                case 8: Keyboard.Release(Key.Down); break; //停止后退
                case 9: Keyboard.Press(Key.LeftShift); break; //氮氧加速
            }
        }

        /// <summary>
        /// 监听端口接受数据
        /// </summary>
        private void Receive()
        {
            //int count = 0;
            while (true)
            {
                try
                {
                    byte[] recvBytes = new byte[1024];
                    IPEndPoint point = new IPEndPoint(IPAddress.Any, 0);
                    recvBytes = _udpClient.Receive(ref point);
                    int recv = recvBytes[0] - 48;
                    //Console.WriteLine("I'm Receiver:" + DateTime.Now.ToString() + "--->接收消息：" + Encoding.Default.GetString(recvBytes));
                    //if (recvBytes != null)
                    //{
                    //    string sts = "北平地铁又瘫痪了";
                    //    byte[] bs = Encoding.Default.GetBytes(sts);
                    //    if (count == 10)
                    //    {
                    //        Thread.Sleep(11000);
                    //    }
                    //    _udpClient.Send(bs, bs.Length, point);
                    //    Console.WriteLine("I'm Receiver:我已发送消息--" + sts + "第" + count + "次");
                    //}
                    //count++;
                    dealWithReceive(recv);
                    
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
