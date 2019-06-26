using NLog.Config;
using NUnit.Framework;

namespace BackupManager.DomainTests
{
    [SetUpFixture]
    public class StartupTests
    {
        [OneTimeSetUp]
        public void RunBeforeAllTestsOnNamespace()
        {
            SimpleConfigurator.ConfigureForConsoleLogging();
        }
    }
}