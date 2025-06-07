
<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/Snsaiu/TuDog">
    <img src="ReadMeAssets/TuDog.png" alt="Logo" width="80" height="80">
  </a>

<h3 align="center">中华田园犬-Avalonia-ToolKit</h3>

  <p align="center">
    针对Avalonia框架适配的MVVM工具包
    <br />
    <a href="https://github.com/Snsaiu/TuDog/wiki"><strong>查看文档 »</strong></a>
    <br />
    <br />
    <a href="https://github.com/Snsaiu/TabbyCat_OpenSource">查看示例项目</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>目录列表</summary>
  <ol>
    <li>
      <a href="#关于项目">关于项目</a>
      <ul>
        <li><a href="#构建清单">构建清单</a></li>
      </ul>
    </li>
    <li>
      <a href="#开始">开始</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## 关于项目
[中华田园犬](https://github.com/Snsaiu/TuDog "中华田园犬")是一个框架，用在[Avalonia](https://github.com/AvaloniaUI/Avalonia)中构建一个松耦合，可维护的XAML应用程序。[中华田园犬](https://github.com/Snsaiu/TuDog "中华田园犬")提供了包括MVVM，依赖注入，页面跳转等常用的功能。

[中华田园犬](https://github.com/Snsaiu/TuDog "中华田园犬")目的是服务于[狸花猫项目](https://github.com/Snsaiu/TabbyCat_OpenSource "狸花猫")，目的是构建一个类似[Prism库](https://github.com/PrismLibrary/Prism)方便用户专注业务并且能够快速开发的框架。



### 构建清单

[中华田园犬](https://github.com/Snsaiu/TuDog "中华田园犬")主要使用*Avalonia*和 *.NET 9* 以及*源代码生成器*

- <a href="https://github.com/AvaloniaUI/Avalonia">
    <img src="https://upload.wikimedia.org/wikipedia/commons/b/bc/Avalonia_logo.svg" alt="Avalonia" height="40">
  </a>

- <a href="https://github.com/dotnet/core">
    <img src="https://upload.wikimedia.org/wikipedia/commons/e/ee/.NET_Core_Logo.svg" alt=".NET CORE" height="40">
  </a>
  
- <a href="https://github.com/dotnet/roslyn">
    <img src="https://user-images.githubusercontent.com/46729679/109719841-17b7dd00-7b5e-11eb-8f5e-87eb2d4d1be9.png" alt="roslyn" height="40">
  </a>

<!-- GETTING STARTED -->
## 开始

### 最低要求

Avalonia (>= 11.2.6)  
Avalonia.Xaml.Behaviors (>= 11.2.0.14)  
CommunityToolkit.Mvvm (>= 8.4.0)  
FluentAvaloniaUI (>= 2.3.0)  
Microsoft.Extensions.DependencyInjection (>= 10.0.0-preview.1.25080.5)  
Newtonsoft.Json (>= 13.0.3)  
Xamarin.Essentials (>= 1.8.1)  

### 引入包

如果您使用的Avalonia项目模版是混合的（包含了Desktop,Android,IOS,共享项目），那么请将下面的包引入到共享项目中

若要使用该框架，您需要在您的Avalonia项目首先在使用[Nuget](https://www.nuget.org/)上引入[中华田园犬](https://github.com/Snsaiu/TuDog "中华田园犬")相关的包

* ```dotnet add package TuDog --version 1.0.0```
* ```dotnet add package TuDog.IocAutoRegisterSourceGenerator --version 1.0.0```
* ```dotnet add package TuDog.IocAttribute --version 1.0.0```
* ```dotnet add package TuDog.LoggerMetrics --version 1.0.1``` _可选_

### 项目配置

如果您使用的Avalonia项目模版是混合的（包含了Desktop,Android,IOS,共享项目），那么请修改共享项目的.csproj。  

* 在您的项目中添加标记
```
    <ItemGroup>
        <AdditionalFiles Include="$(MSBuildProjectFile)"/>
    </ItemGroup>
```
该标记主要让源代码生成器能够找到库，并且将库所依赖的库全部找到。

* 在您的项目添加标记
```
    <PropertyGroup>
        <ScanAssemblyRule>^你的项目名称</ScanAssemblyRule>
    </PropertyGroup>

```
`^你的项目名称` 是一个正则表达式，主要目的是告诉源代码生成器生成依赖注入的时候，需要对哪些库中的类进行搜索，大部分情况下您只会对您写的类进行依赖注入。  
`^TabbyCat` 表示对以TabbyCat开头的库进行搜索。

* 在`App.xaml.cs`文件中，将`App`的基类设置为`TuDogApplication`.
* 一旦您将`App`的基类设置为`TuDogApplication`，那么您需要重写`public abstract object CreateShell()`方法。  
该方法要求您返回一个`object`对象，针对桌面端，您只需要返回`window`对象，其实就是您应用的主窗口对象；针对移动端，您需要返回
`UserControl`对象，其实就是您应用启动的第一个页面。

**一旦您完成上述操作，那么您的配置已经结束了，您可以编译一下项目保证没有问题后就可以继续您的下一步了！**

<!-- USAGE EXAMPLES -->
## 用法
[中华田园犬](https://github.com/Snsaiu/TuDog "中华田园犬")的具体用法，请查阅[Wiki](https://github.com/Snsaiu/TuDog/wiki)。

<!-- LICENSE -->
## 开源许可

关于开源许可 请查阅 `LICENSE`

<!-- CONTACT -->
## 联系我们

snsaiu@outlook.com

项目链接: [中华田园犬](https://github.com/Snsaiu/TuDog "中华田园犬")

