using Microsoft.AspNetCore.Components;
using RefugeeNavigator.Web.Components;

namespace RefugeeNavigator.Tests;

public class InfrastructureSmokeTests
{
    [Fact]
    public void WebAssemblyContainsRootRazorComponent()
    {
        Assert.IsAssignableFrom<IComponent>(new App());
    }
}
