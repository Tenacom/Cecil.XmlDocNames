using System.IO;
using System.Reflection;
using Mono.Cecil;

namespace Cecil.XmlDocNames.Tests.Internal
{
    static class TestUtility
    {
        public static AssemblyDefinition ReadExampleAssembly()
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var assemblyPath = Path.Combine(Path.GetDirectoryName(location)!, "ExampleAssembly.dll");

            var readerParameters = new ReaderParameters
            {
                ReadingMode = ReadingMode.Deferred,
                ReadWrite = false,
            };

            return AssemblyDefinition.ReadAssembly(assemblyPath, readerParameters);
        }
    }
}