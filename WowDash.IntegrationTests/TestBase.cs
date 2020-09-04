using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WowDash.IntegrationTests
{
    using static Testing;

    public class TestBase
    {
        // Does this class need some sort of resolved DbContext?

        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}
