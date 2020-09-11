 using System.Collections.Generic;

 namespace ExampleAssembly
{
    public sealed class GenericClass<T>
    {
        public GenericClass()
        {
        }

        public void Method(int a, ref string b, byte[] c) => b = a + c.ToString();

        public void GenericMethod<TT>(T a, ref T b, TT[] c, out IEnumerable<TT> d)
        {
            b = a;
            d = c;
        }
    }
}