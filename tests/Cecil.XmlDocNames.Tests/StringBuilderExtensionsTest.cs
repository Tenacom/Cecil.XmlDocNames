// -----------------------------------------------------------------------------------
// Copyright (C) Tenacom. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
//
// Part of this file may be third-party code, distributed under a compatible license.
// See THIRD-PARTY-NOTICES file in the project root for third-party copyright notices.
// -----------------------------------------------------------------------------------

using System;
using System.Text;
using Cecil.XmlDocNames.Tests.Internal;
using NUnit.Framework;

namespace Cecil.XmlDocNames.Tests
{
    [TestFixture]
    public class StringBuilderExtensionsTest
    {
        [Test]
        public void AppendXmlDocName_WithNullMember_ThrowsArgumentNullException()
        {
            var sb = new StringBuilder();
            _ = Assert.Throws<ArgumentNullException>(() => sb.AppendXmlDocName(null!));
        }

        [Test]
        public void AppendXmlDocName_WithNonGenericType_ReturnsCorrectValue()
        {
            var sb = new StringBuilder();
            using var assembly = TestUtility.ReadExampleAssembly();
            var type = assembly.GetTypeThatStartsWith("Example");
            var result = sb.AppendXmlDocName(type).ToString();
            Assert.AreEqual("T:ExampleAssembly.ExampleClass", result);
        }

        [Test]
        public void AppendXmlDocName_WithGenericType_ReturnsCorrectValue()
        {
            var sb = new StringBuilder();
            using var assembly = TestUtility.ReadExampleAssembly();
            var type = assembly.GetTypeThatStartsWith("Generic");
            var result = sb.AppendXmlDocName(type).ToString();
            Assert.AreEqual("T:ExampleAssembly.GenericClass`1", result);
        }

        [Test]
        public void AppendXmlDocName_WithField_ReturnsCorrectValue()
        {
            var sb = new StringBuilder();
            using var assembly = TestUtility.ReadExampleAssembly();
            var type = assembly.GetTypeThatStartsWith("Example");
            var field = type.GetFieldThatStartsWith("F");

            var result = sb.AppendXmlDocName(field).ToString();
            Assert.AreEqual("F:ExampleAssembly.ExampleClass.Field", result);
        }

        [Test]
        public void AppendXmlDocName_WithEvent_ReturnsCorrectValue()
        {
            var sb = new StringBuilder();
            using var assembly = TestUtility.ReadExampleAssembly();
            var type = assembly.GetTypeThatStartsWith("Example");
            var evnt = type.GetEventThatStartsWith("E");

            var result = sb.AppendXmlDocName(evnt).ToString();
            Assert.AreEqual("E:ExampleAssembly.ExampleClass.Event", result);
        }

        [Test]
        public void AppendXmlDocName_WithProperty_ReturnsCorrectValue()
        {
            var sb = new StringBuilder();
            using var assembly = TestUtility.ReadExampleAssembly();
            var type = assembly.GetTypeThatStartsWith("Example");
            var property = type.GetPropertyThatStartsWith("P");

            var result = sb.AppendXmlDocName(property).ToString();
            Assert.AreEqual("P:ExampleAssembly.ExampleClass.Property", result);
        }

        [Test]
        public void AppendXmlDocName_WithConstructorOfNonGenericType_ReturnsCorrectValue()
        {
            var sb = new StringBuilder();
            using var assembly = TestUtility.ReadExampleAssembly();
            var type = assembly.GetTypeThatStartsWith("Example");
            var method = type.GetMethodThatStartsWith(".c");

            var result = sb.AppendXmlDocName(method).ToString();
            Assert.AreEqual("M:ExampleAssembly.ExampleClass.#ctor", result);
        }

        [Test]
        public void AppendXmlDocName_WithConstructorOfGenericType_ReturnsCorrectValue()
        {
            var sb = new StringBuilder();
            using var assembly = TestUtility.ReadExampleAssembly();
            var type = assembly.GetTypeThatStartsWith("Generic");
            var method = type.GetMethodThatStartsWith(".c");

            var result = sb.AppendXmlDocName(method).ToString();
            Assert.AreEqual("M:ExampleAssembly.GenericClass`1.#ctor", result);
        }

        [Test]
        public void AppendXmlDocName_WithNonGenericMethodOfNonGenericType_ReturnsCorrectValue()
        {
            var sb = new StringBuilder();
            using var assembly = TestUtility.ReadExampleAssembly();
            var type = assembly.GetTypeThatStartsWith("Example");
            var method = type.GetMethodThatStartsWith("M");

            var result = sb.AppendXmlDocName(method).ToString();
            Assert.AreEqual("M:ExampleAssembly.ExampleClass.Method(System.Int32,System.String@,System.Byte[])", result);
        }

        [Test]
        public void AppendXmlDocName_WithGenericMethodOfNonGenericType_ReturnsCorrectValue()
        {
            var sb = new StringBuilder();
            using var assembly = TestUtility.ReadExampleAssembly();
            var type = assembly.GetTypeThatStartsWith("Example");
            var method = type.GetMethodThatStartsWith("G");

            var result = sb.AppendXmlDocName(method).ToString();
            Assert.AreEqual("M:ExampleAssembly.ExampleClass.GenericMethod``1(``0,``0@,``0[],System.Collections.Generic.IEnumerable{``0}@)", result);
        }

        [Test]
        public void AppendXmlDocName_WithNonGenericMethodOfGenericType_ReturnsCorrectValue()
        {
            var sb = new StringBuilder();
            using var assembly = TestUtility.ReadExampleAssembly();
            var type = assembly.GetTypeThatStartsWith("Generic");
            var method = type.GetMethodThatStartsWith("M");

            var result = sb.AppendXmlDocName(method).ToString();
            Assert.AreEqual("M:ExampleAssembly.GenericClass`1.Method(System.Int32,System.String@,System.Byte[])", result);
        }

        [Test]
        public void AppendXmlDocName_WithGenericMethodOfGenericType_ReturnsCorrectValue()
        {
            var sb = new StringBuilder();
            using var assembly = TestUtility.ReadExampleAssembly();
            var type = assembly.GetTypeThatStartsWith("Generic");
            var method = type.GetMethodThatStartsWith("G");

            var result = sb.AppendXmlDocName(method).ToString();
            Assert.AreEqual("M:ExampleAssembly.GenericClass`1.GenericMethod``1(`0,`0@,``0[],System.Collections.Generic.IEnumerable{``0}@)", result);
        }
    }
}