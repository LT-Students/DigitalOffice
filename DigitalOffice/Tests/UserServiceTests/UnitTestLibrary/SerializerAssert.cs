using System.Text.Json;
using NUnit.Framework;

namespace LT.DigitalOffice.UserServiceUnitTests.UnitTestLibrary
{
    public static class SerializerAssert
    {
        public static void AreEqual(object expected, object result)
        {
            var expectedJson = JsonSerializer.Serialize(expected);
            var resultJson = JsonSerializer.Serialize(result);

            Assert.AreEqual(expectedJson, resultJson);
        }
    }
}