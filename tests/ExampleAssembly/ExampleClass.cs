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

        public event EventHandler? Event;

        public void Method(int a, ref string b, byte[] c) => b = a + c.ToString();

        public void GenericMethod<T>(T a, ref T b, T[] c, out IEnumerable<T> d)
        {
            b = a;
            d = c;
        }
    }
}