using System;

namespace TuDog.IocAttribute;

/// <summary>
/// 指定与弹框视图模型关联的视图类没有视图。
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class NoViewAttribute:Attribute
{
    
}