using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackupManager.Domain.Services;
using NUnit.Framework;

namespace BackupManager.DomainTests.Services
{
    [TestFixture]
    public sealed class HashServicesTests
    {

        [Test]
        public async Task Test()
        {
            //var senha1 = HashService.GenerateSaltedHash("123456");

            //var senha3 = HashService.GenerateSaltedHash("123Qazwsx");


            await new AuthenticationService().AuthenticateUser("16741790000104", "123Qazwsx");

            

        }
    }
}
