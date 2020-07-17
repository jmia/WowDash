using System;
using wow_dashboard.Data;

namespace wow_dashboard_tests.Common
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
