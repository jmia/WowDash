using NUnit.Framework;
using System;
using System.Linq;
using WowDash.ApplicationCore.Entities;
using WowDash.Infrastructure;

namespace WowDash.UnitTests.Common
{
    [TestFixture]
    public abstract class UnitTestBase : IDisposable
    {
        protected readonly ApplicationDbContext Context;
        protected Player DefaultPlayer;

        public UnitTestBase()
        {
            Context = ApplicationContextFactory.Create();
        }

        [SetUp]
        public void RunBeforeEachTest()
        {
            Assume.That(Context.Players.Any(), "The testing database needs at least one user.");
            DefaultPlayer = Context.Players.First();
        }

        public void Dispose()
        {
            ApplicationContextFactory.Destroy(Context);
        }
    }
}
