namespace TuDog.IocAttribute;

public enum ServiceLifetime
{
    //
    // 摘要:
    //     Specifies that a single instance of the service will be created.
    Singleton,
    //
    // 摘要:
    //     Specifies that a new instance of the service will be created for each scope.
    //
    //
    // 言论：
    //     In ASP.NET Core applications a scope is created around each server request.
    Scoped,
    //
    // 摘要:
    //     Specifies that a new instance of the service will be created every time it is
    //     requested.
    Transient
}