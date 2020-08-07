﻿using NUnit.Framework;
using System.Text.Json;

namespace LT.DigitalOffice.ProjectServiceUnitTests.UnitTestLibrary
{
    public static class SerializerAssert
    {
        public static void AreEqual(object expected, object result)
        {
            string expectedJson = JsonSerializer.Serialize(expected);
            string resultJson = JsonSerializer.Serialize(result);

            Assert.AreEqual(expectedJson, resultJson);
        }
    }
}