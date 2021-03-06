namespace NStub.CSharp.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::MbUnit.Framework;
    using NStub.CSharp;
    using System.CodeDom;


    public partial class NamespaceDetectorTest
    {

        private NamespaceDetector testObject;
        private System.CodeDom.CodeTypeDeclarationCollection typeDeclarations;

        [SetUp()]
        public void SetUp()
        {
            this.typeDeclarations = new System.CodeDom.CodeTypeDeclarationCollection();
            typeDeclarations.Add(new CodeTypeDeclaration("Jedzia.Loves.Testing.TheClassToTest"));
            this.testObject = new NamespaceDetector(this.typeDeclarations);
        }

        [Test()]
        public void ConstructWithParametersTypeDeclarationsTest()
        {
            this.typeDeclarations = new System.CodeDom.CodeTypeDeclarationCollection();
            this.testObject = new NamespaceDetector(this.typeDeclarations);
            Assert.Throws<ArgumentNullException>(() => new NamespaceDetector(null));
        }

        [Test()]
        public void PropertyShortestNamespaceNormalBehavior()
        {
            // Test read access of 'ShortestNamespace' Property.
            var expected = "Jedzia.Loves.Testing";
            var actual = testObject.ShortestNamespace;
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void PropertyTypeDeclarationsNormalBehavior()
        {
            // Test read access of 'TypeDeclarations' Property.
            var expected = this.typeDeclarations;
            var actual = testObject.TypeDeclarations;
            Assert.AreSame(expected, actual);
        }

        [Test()]
        public void InsertAfterShortestNamespaceTest()
        {
            //typeDeclarations.Add(new CodeTypeDeclaration("Jedzia.Loves.Testing.TheClassToTest"));
            var type = new CodeTypeDeclaration("Jedzia.Loves.Testing.TheClassToTest");

            var expected = "Jedzia.Loves.Testing.Tests.TheClassToTest";
            var actual = testObject.InsertAfterShortestNamespace(type, ".Tests");
            Assert.AreEqual(expected, actual);

        }

        [Test()]
        public void GetDifferingTypeFullnameTest()
        {
            var type = new CodeTypeDeclaration("Jedzia.Loves.Testing.SubNamespace.TheClassToTest");

            var expected = ".Tests.SubNamespace";
            var actual = testObject.GetDifferingTypeFullname(type, ".Tests");
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void PrepareNamespaceImportsTest()
        {
            var imports = new[] { "System","System.Linq", "MbUnit.Framework" };
            var expected = ".Tests.SubNamespace";
            var actual = testObject.PrepareNamespaceImports(imports);
            Assert.AreElementsEqual(imports, actual.Select(e => e.Namespace));

            var expectedPrepared = new[] { "System", "System.Linq", "global::MbUnit.Framework" };
            typeDeclarations.Add(new CodeTypeDeclaration("Jedzia.Loves.Testing.MbUnit.SubClass"));
            this.testObject = new NamespaceDetector(this.typeDeclarations);
            actual = testObject.PrepareNamespaceImports(imports);
            Assert.AreElementsEqual(expectedPrepared, actual.Select(e => e.Namespace));
        }
    }
}
