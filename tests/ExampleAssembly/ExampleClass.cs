using System;
using System.Collections.Generic;

namespace ExampleAssembly
{
    public sealed class ExampleClass
    {
        public int Field;

        public ExampleClass()
        {
            Field = default;
        }

        public int Property { get; set; }

#pragma warning disable CS0067 // Event is never used - accessed only via reflection
        public event EventHandler? Event;
#pragma warning restore CS0067

        public void Method(int a, ref string b, byte[] c) => b = a + c.ToString();

        public void GenericMethod<T>(T a, ref T b, T[] c, out IEnumerable<T> d)
        {
            b = a;
            d = c;
        }
    }
}