using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace EasyAbp.IkapatigiCapstone1.Pages;

public class Index_Tests : IkapatigiCapstone1WebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
