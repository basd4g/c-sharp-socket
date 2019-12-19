# Program.csを動かせるようになるまでの記録

## 調査

### まずコンパイルして動かす。

```sh
$ mcs Program.cs

Program.cs(37,17): warning CS0219: The variable `port' is assigned but its value is never used
Compilation succeeded - 1 warning(s)

$ mono Program.exe

1
2
3
4
5
6

Unhandled Exception:
System.Net.Sockets.SocketException (0x80004005): The requested address is not valid in this context
  at System.Net.Sockets.Socket.Bind (System.Net.EndPoint localEP) [0x00043] in <dfffed4750cb49c8ab16f18fc177a88e>:0 
  at ConsoleApp1.Program.Main () [0x00072] in <712b8abf7d1b4ceca17ac9c69535e7df>:0 
[ERROR] FATAL UNHANDLED EXCEPTION: System.Net.Sockets.SocketException (0x80004005): The requested address is not valid in this context
  at System.Net.Sockets.Socket.Bind (System.Net.EndPoint localEP) [0x00043] in <dfffed4750cb49c8ab16f18fc177a88e>:0 
  at ConsoleApp1.Program.Main () [0x00072] in <712b8abf7d1b4ceca17ac9c69535e7df>:0 
```
### ipアドレスを127.0.0.1に変更

### BeginAcceptを繰り返し実行

BeginAccept(listener)は非同期のため、接続要求がなくても値を返してしまう。そこで、ManualResetEventを組み合わせて繰り返し実行し、この行の値が返ってもプログラムを終了させず、繰り返し実行するようにした。


参考: [非同期サーバー ソケットの使用](https://docs.microsoft.com/ja-jp/dotnet/framework/network-programming/using-an-asynchronous-server-socket)
参考: [C# 非同期ソケット通信で簡易サーバーを作成 - エクセレンス★ブログ](https://www.excellence-blog.com/2018/06/08/c-%E9%9D%9E%E5%90%8C%E6%9C%9F%E3%82%BD%E3%82%B1%E3%83%83%E3%83%88%E9%80%9A%E4%BF%A1%E3%81%A7%E7%B0%A1%E6%98%93%E3%82%B5%E3%83%BC%E3%83%90%E3%83%BC%E3%82%92%E4%BD%9C%E6%88%90/)

### (やってない)動いているサンプルプログラムと比較する

参考: [#Cでsocket通信をしてみる。ついでにnetstatでlistenしているポートを調べる。](http://karoten512.hatenablog.com/entry/2018/03/21/234156)
