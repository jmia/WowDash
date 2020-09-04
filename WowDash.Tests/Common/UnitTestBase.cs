using System;
using WowDash.Infrastructure;

namespace WowDash.UnitTests.Common
{
    public abstract class UnitTestBase : IDisposable
    {
        protected readonly ApplicationDbContext Context;

        public UnitTestBase()
        {
            Context = ApplicationContextFactory.Create();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
