namespace TuDog.Interfaces.IDialogServers.Impl;

/// <summary>
/// 等待报告进度
/// </summary>
public delegate void ProgressProcess(string header, string subHeader, double? percentage = null);