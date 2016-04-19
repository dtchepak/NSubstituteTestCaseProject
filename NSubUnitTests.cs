using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace NSubstituteTestCase
{
    [TestClass]
    public class NSubUnitTests
    {
        [TestMethod]
        public void TestSubstituteFailsWithoutEquals()
        {
            // Fails because CompositeKeyFail does NOT override Equals(object obj)
            // It does implement IEquatable<CompositeKeyFail>
            // and overrides GetHashCode()

            ICompositeKeyFailCache cacheSub = Substitute.For<ICompositeKeyFailCache>();
            cacheSub.Get(new CompositeKeyFail(Constants.KeyPart1, Constants.KeyPart2)).Returns(Constants.ReturnValue);

            string retVal = cacheSub.Get(new CompositeKeyFail(Constants.KeyPart1, Constants.KeyPart2));

            Assert.IsNotNull(retVal);
            Assert.AreEqual(Constants.ReturnValue, retVal);
        }

        [TestMethod]
        public void TestSubstituteOkWithEqualsAndEquatable()
        {
            // Succeeds because CompositeKeyOk does override Equals(object obj)
            // It does implement IEquatable<CompositeKeyFail>
            // and overrides GetHashCode()

            ICompositeKeyOkCache cacheSub = Substitute.For<ICompositeKeyOkCache>();
            cacheSub.Get(new CompositeKeyOk(Constants.KeyPart1, Constants.KeyPart2)).Returns(Constants.ReturnValue);

            string retVal = cacheSub.Get(new CompositeKeyOk(Constants.KeyPart1, Constants.KeyPart2));

            Assert.IsNotNull(retVal);
            Assert.AreEqual(Constants.ReturnValue, retVal);
        }

        [TestMethod]
        public void TestSubstituteOkWithOnlyEquals()
        {
            // Succeeds because CompositeKeyAlsoOk does override Equals(object obj)
            // It does NOT implement IEquatable<CompositeKeyFail>
            // and overrides GetHashCode()

            ICompositeKeyAlsoOkCache cacheSub = Substitute.For<ICompositeKeyAlsoOkCache>();
            cacheSub.Get(new CompositeKeyAlsoOk(Constants.KeyPart1, Constants.KeyPart2)).Returns(Constants.ReturnValue);

            string retVal = cacheSub.Get(new CompositeKeyAlsoOk(Constants.KeyPart1, Constants.KeyPart2));

            Assert.IsNotNull(retVal);
            Assert.AreEqual(Constants.ReturnValue, retVal);
        }

        [TestMethod]
        public void TestActualOkWithoutEquals()
        {
            // Succeeds because real dictionary work with either overriden Equals or implemented IEquatable<> or both

            ICompositeKeyFailCache cache = new CompositeKeyFailCache();
            cache.Add(new CompositeKeyFail(Constants.KeyPart1, Constants.KeyPart2), Constants.ReturnValue);

            string retVal = cache.Get(new CompositeKeyFail(Constants.KeyPart1, Constants.KeyPart2));

            Assert.IsNotNull(retVal);
            Assert.AreEqual(Constants.ReturnValue, retVal);
        }

        [TestMethod]
        public void TestActualOkWithEqualsAndEquatable()
        {
            // Succeeds because real dictionary work with either overriden Equals or implemented IEquatable<> or both

            ICompositeKeyOkCache cache = new CompositeKeyOkCache();
            cache.Add(new CompositeKeyOk(Constants.KeyPart1, Constants.KeyPart2), Constants.ReturnValue);

            string retVal = cache.Get(new CompositeKeyOk(Constants.KeyPart1, Constants.KeyPart2));

            Assert.IsNotNull(retVal);
            Assert.AreEqual(Constants.ReturnValue, retVal);
        }

        [TestMethod]
        public void TestActualOkWithOnlyEquals()
        {
            // Succeeds because real dictionary work with either overriden Equals or implemented IEquatable<> or both

            ICompositeKeyAlsoOkCache cache = new CompositeKeyAlsoOkCache();
            cache.Add(new CompositeKeyAlsoOk(Constants.KeyPart1, Constants.KeyPart2), Constants.ReturnValue);

            string retVal = cache.Get(new CompositeKeyAlsoOk(Constants.KeyPart1, Constants.KeyPart2));

            Assert.IsNotNull(retVal);
            Assert.AreEqual(Constants.ReturnValue, retVal);
        }

    }



}
