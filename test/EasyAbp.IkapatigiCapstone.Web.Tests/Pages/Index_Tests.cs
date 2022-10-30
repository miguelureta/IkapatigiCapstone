using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace EasyAbp.IkapatigiCapstone.Pages;

public class Index_Tests : IkapatigiCapstoneWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
