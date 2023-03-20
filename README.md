# AnyCAD Rapid .NET Sample


## 分支说明:
 - R2021: 2021 branch
 - R2022: 2022 branch
 - R2023: master

## 1 环境准备

### 1.1 Windows
#### 1.1.1 Microsoft Visual C++ Runtime Library

低于VS2022的版本需要下载C++运行时库，下载地址: 
- [vc_redist.x64](https://aka.ms/vs/17/release/vc_redist.x64.exe)
- [vc_redist.x86](https://aka.ms/vs/17/release/vc_redist.x86.exe)

#### 1.2.2 .NET Framework
支持 .Net Framework 4.5.2、4.8
#### 1.3.2 .NET 6.0
推荐使用


### 1.2 Linux
### 1.2.1 .NET 6.0
```
sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-6.0
```
### 1.2.2 编译
```
dotnet msbuild AnyCAD.Rapid.Avalonia.sln
```

![linux.loft](showcase/linux.loft.png)
![linux.loft](showcase/linux.pyramid.png)
![linux.loft](showcase/linux.sweep.png)
## 2 程序示例

### 2.1 建模
![sweep](showcase/slice.png)
![sweep](showcase/sweeploft.png)

![pipe](showcase/pipe.png)

![hole](showcase/holes.png)

![boolean](showcase/boolean.png)

![spring](showcase/spring.png)


### 2.2 显示

![dimension](showcase/dimension.png)

![matplot](showcase/matplot.png)

![CAE](showcase/cae.png)

![Robot](showcase/robot2.png)

![Tag](showcase/tag.png)

### 2.3 交互
![move](showcase/move.png)
![move](showcase/rotate.png)

### 2.4 Featured App
[Robot.NET](https://gitee.com/anycad/anycad.rapid.net.sample/AnyRobot.NET)
![Robot](showcase/robot.png)

## 3 Documentation

- [API](http://www.anycad.cn/api/classes.html)
- [Guide](http://www.anycad.cn/guide/)

## 4 更多示例
### 入门示例：
https://gitee.com/anycad/rapid.net.starter
### 高级示例：
https://gitee.com/anycad/RapidCAX

## 5 关于
http://www.anycad.cn

![Weixin](weixin.jpg)