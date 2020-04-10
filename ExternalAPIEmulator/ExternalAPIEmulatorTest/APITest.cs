using ExternalAPIEmulator;
using Infrastructure.Models;
using NUnit.Framework;

namespace ExternalAPIEmulatorTest
{
    [TestFixture]
    public class APITest
    {
        [Test]
        public void TestRandomEntity()
        {
            ParamsEntity entity = EntityService.GetNewRandomEntity();
            Assert.IsNotNull(entity);
        }

        [Test]
        public void TestRandomNumber()
        {
            double result = EntityService.GetRandomNumber(-1, 1);
            Assert.LessOrEqual(result, 1);
            Assert.GreaterOrEqual(result, -1);
            Assert.IsInstanceOf(typeof(double), result);
        }
    }
}