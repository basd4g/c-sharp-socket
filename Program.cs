using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        

        static void Main()
        {
            
            /*
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            
            Socket listener = new Socket(ipAddress.AddressFamily,SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(100);
            */
            Console.WriteLine("1");

            string ipString = "172.16.37.233";
            string Endport = "100";
            Console.WriteLine("2");

            System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(ipString);
            Console.WriteLine("3");

            int port = 2001;
            Console.WriteLine("4");

            IPEndPoint localEndPoint = new IPEndPoint(ipAdd, int.Parse(Endport));
            Console.WriteLine("5");

            Socket listener = new Socket(ipAdd.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("6");

            listener.Bind(localEndPoint);
            Console.WriteLine("7");
            listener.Listen(100);
            Console.WriteLine("8");
            StartAccept(listener);
            Console.WriteLine("9");
            System.Threading.Thread.Sleep(2000);

        }

      


        //クライアントの接続待ちスタート
        private static void StartAccept(System.Net.Sockets.Socket server)
        {
            //接続要求待機を開始する
            Console.WriteLine("10");
            System.Threading.Thread.Sleep(2000);
            server.BeginAccept(
                new System.AsyncCallback(AcceptCallback), server);
            Console.WriteLine("11");
            System.Threading.Thread.Sleep(2000);
        }


        //BeginAcceptのコールバック
        private static void AcceptCallback(System.IAsyncResult ar)
        {
            Console.WriteLine("12");
            //サーバーSocketの取得

            System.Net.Sockets.Socket server =
                (System.Net.Sockets.Socket)ar.AsyncState;

            //接続要求を受け入れる
            System.Net.Sockets.Socket client = null;
            try
            {
                //クライアントSocketの取得
                client = server.EndAccept(ar);
            }
            catch
            {
                System.Console.WriteLine("閉じました。");
                return;
            }

            //クライアントが接続した時の処理をここに書く
            //ここでは文字列を送信して、すぐに閉じている
            client.Send(System.Text.Encoding.UTF8.GetBytes("こんにちは。"));
            client.Shutdown(System.Net.Sockets.SocketShutdown.Both);
            client.Close();

            //接続要求待機を再開する
            server.BeginAccept(
                new System.AsyncCallback(AcceptCallback), server);
        }
    }
}
