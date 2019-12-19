# Program.csを動かせるようになるまでの記録

## 調査

### まずコンパイルして動かす。

```sh
$ mcs Program.cs

Program.cs(37,17): warning CS0219: The variable `port' is assigned but its value is never used
Compilation succeeded - 1 warning(s)

$ mono Program.exe
Cannot open assembly 'Program.cs': File does not contain a valid CIL image.
```

