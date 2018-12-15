using AspectCore.DynamicProxy;
using System;
using System.Threading.Tasks;

class Program
{

    static void Main(string[] args)
    {
        ProxyGeneratorBuilder proxyGeneratorBuilder = new ProxyGeneratorBuilder();
        IProxyGenerator proxyGenerator = proxyGeneratorBuilder.Build();
        SampleInterface sampleInterface = proxyGenerator.CreateInterfaceProxy<SampleInterface>();
        Console.WriteLine(sampleInterface);
        sampleInterface.Foo();
        Console.ReadKey();
    }
}

public class SampleInterceptor : AbstractInterceptorAttribute
{

    public override Task Invoke(AspectContext context, AspectDelegate next)
    {
        Console.WriteLine("call interceptor");
        return context.Invoke(next);
    }
}
public class SampleClass : SampleInterface
{
    public virtual void Foo() { }
}

[SampleInterceptor]
public interface SampleInterface
{
    void Foo();
}