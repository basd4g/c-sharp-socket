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

### 動いているサンプルプログラムと比較する

参考: [#Cでsocket通信をしてみる。ついでにnetstatでlistenしているポートを調べる。](http://karoten512.hatenablog.com/entry/2018/03/21/234156)
