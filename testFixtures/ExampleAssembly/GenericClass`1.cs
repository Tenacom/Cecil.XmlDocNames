// ---------------------------------------------------------------------------------------
// Copyright (C) Riccardo De Agostini and Tenacom. All rights reserved.
// Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.
//
// Part of this file may be third-party code, distributed under a compatible license.
// See the THIRD-PARTY-NOTICES file in the project root for third-party copyright notices.
// ---------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace ExampleAssembly;

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
