using System;

public class Client
{
  public static void Main()
  {
    Console.WriteLine("入力してください");
    string sendMsg = Console.ReadLine();

    if (sendMsg == null || sendMsg.Length == 0)
    {
      return;
    }

    string ipOrHost = "127.0.0.1";
    int port = 2001;


    System.Net.Sockets.TcpClient tcp =
      new System.Net.Sockets.TcpClient(ipOrHost, port);
    Console.WriteLine("サーバー({0}:{1})と接続しました({2}:{3})。",
        ((System.Net.IPEndPoint)tcp.Client.RemoteEndPoint).Address,
        ((System.Net.IPEndPoint)tcp.Client.RemoteEndPoint).Port,
        ((System.Net.IPEndPoint)tcp.Client.LocalEndPoint).Address,
        ((System.Net.IPEndPoint)tcp.Client.LocalEndPoint).Port);
System.Net.Sockets.NetworkStream ns = tcp.GetStream();

    ns.ReadTimeout = 10000;
    ns.WriteTimeout = 10000;


    System.Text.Encoding enc = System.Text.Encoding.UTF8;
    byte[] sendBytes = enc.GetBytes(sendMsg + '\n');

    ns.Write(sendBytes, 0, sendBytes.Length);
    Console.WriteLine(sendMsg);


    System.IO.MemoryStream ms = new System.IO.MemoryStream();
    byte[] resBytes = new byte[256];
    int resSize = 0;
    do
    {
      resSize = ns.Read(resBytes, 0, resBytes.Length);

      if (resSize == 0)
      {
        Console.WriteLine("サーバーが切断しました。");
        break;
      }

      ms.Write(resBytes, 0, resSize);

    } while (ns.DataAvailable || resBytes[resSize - 1] != '\n');

    string resMsg = enc.GetString(ms.GetBuffer(), 0, (int)ms.Length);

    ms.Close();

    resMsg = resMsg.TrimEnd('\n');
    Console.WriteLine(resMsg);
    ns.Close();
    tcp.Close();
    Console.WriteLine("切断しました。");

    Console.ReadLine();
  }
}

