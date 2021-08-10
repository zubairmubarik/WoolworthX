using NUnit.Framework;
using System.Threading.Tasks;

namespace MyDemoProject001.ApplicationUnitTests
{
    using static Testing;
    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}
