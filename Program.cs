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
            string ipString = "127.0.0.1";

            System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(ipString);

            int port = 2001;
            IPEndPoint localEndPoint = new IPEndPoint(ipAdd, port); 

            Socket listener = new Socket(ipAdd.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);
            listener.Listen(100);
            StartAccept(listener);
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
