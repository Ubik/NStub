namespace NStub.CSharp.Tests.BuildContext
{
    using global::MbUnit.Framework;
    using NStub.CSharp.BuildContext;
    using NStub.CSharp.ObjectGeneration;
    using Rhino.Mocks;
    using System.CodeDom;

    public partial class SetupAndTearDownContextBaseTest
    {

        private MockRepository mocks;
        private SetupAndTearDownContextBase testObject;
        BuildDataDictionary buildData;
        CodeNamespace codeNamespace;
        CodeTypeDeclaration testClassDeclaration;
        CodeMemberMethod setUpMethod;
        CodeMemberMethod tearDownMethod;
        ITestObjectComposer creator;

        public SetupAndTearDownContextBaseTest()
        {
        }

        [SetUp()]
        public void SetUp()
        {
            this.mocks = new MockRepository();

            buildData = new BuildDataDictionary();
            codeNamespace = new CodeNamespace();
            testClassDeclaration = new CodeTypeDeclaration();
            setUpMethod = new CodeMemberMethod();
            tearDownMethod = new CodeMemberMethod();
            creator = this.mocks.StrictMock<ITestObjectComposer>();

            this.testObject = this.mocks.StrictMock<SetupAndTearDownContextBase>(buildData, codeNamespace,
                testClassDeclaration, setUpMethod, tearDownMethod, creator);
        }

        [TearDown()]
        public void TearDown()
        {
            this.testObject = null;
        }

        [Test()]
        public void PropertyBuildDataNormalBehavior()
        {
            // Test read access of 'BuildData' Property.
            mocks.ReplayAll();
            var expected = buildData;
            var actual = testObject.BuildData;
            Assert.AreEqual(expected, actual);
            mocks.VerifyAll();
        }

        [Test()]
        public void PropertyCodeNamespaceNormalBehavior()
        {
            // Test read access of 'CodeNamespace' Property.
            mocks.ReplayAll();
            var expected = codeNamespace;
            var actual = testObject.CodeNamespace;
            Assert.AreEqual(expected, actual);
            mocks.VerifyAll();
        }

        [Test()]
        public void PropertyTestClassDeclarationNormalBehavior()
        {
            // Test read access of 'TestClassDeclaration' Property.
            mocks.ReplayAll();
            var expected = testClassDeclaration;
            var actual = testObject.TestClassDeclaration;
            Assert.AreEqual(expected, actual);
            mocks.VerifyAll();
        }

        [Test()]
        public void PropertyNormalBehaviorObjectCreatorNormalBehavior()
        {
            // Test read access of 'TestObjectCreator' Property.
            mocks.ReplayAll();
            var expected = creator;
            var actual = testObject.TestObjectCreator;
            Assert.AreEqual(expected, actual);
            mocks.VerifyAll();
        }

        [Test()]
        public void PropertySetUpMethodNormalBehavior()
        {
            // Test read access of 'SetUpMethod' Property.
            mocks.ReplayAll();
            var expected = setUpMethod;
            var actual = testObject.SetUpMethod;
            Assert.AreEqual(expected, actual);
            mocks.VerifyAll();
        }

        [Test()]
        public void PropertyTearDownMethodNormalBehavior()
        {
            // Test read access of 'TearDownMethod' Property.
            mocks.ReplayAll();
            var expected = tearDownMethod;
            var actual = testObject.TearDownMethod;
            Assert.AreEqual(expected, actual);
            mocks.VerifyAll();
        }
     }
}
