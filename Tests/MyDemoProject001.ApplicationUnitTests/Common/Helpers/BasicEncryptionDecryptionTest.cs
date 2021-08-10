using NUnit.Framework;
using MyDemoProject001.Application.Common.Helpers;

namespace MyDemoProject001.ApplicationUnitTests.Common.Helpers
{
    public class BasicEncryptionDecryptionTest : TestBase
    {
        [Test]
        public void ShouldEncrypt_Token_Key()
        {// Original Token //b6901f7b-c3b4-4c0d-bef5-daf5341c1607
            var key = "b6901f7b-c3b4-4c0d-bef5-daf5341c1607";
        
            var result = BasicEncryptionDecryption.Base64Encode(key);
        
            var expectedResult = "YjY5MDFmN2ItYzNiNC00YzBkLWJlZjUtZGFmNTM0MWMxNjA3";
            
            Assert.AreEqual(expectedResult, result);
        }


        [Test]
        public void ShouldDecrypt_Token_Key()
        {
            var result = BasicEncryptionDecryption.Base64Decode("YjY5MDFmN2ItYzNiNC00YzBkLWJlZjUtZGFmNTM0MWMxNjA3");
            var expectedResult = "b6901f7b-c3b4-4c0d-bef5-daf5341c1607";
            Assert.AreEqual(expectedResult, result);
        }

    }
}
