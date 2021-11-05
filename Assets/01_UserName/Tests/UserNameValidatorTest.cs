using NUnit.Framework;
using Project.Scripts;

namespace Tests
{
    public class UserNameValidatorTest
    {
        [TestCase("1")]
        [TestCase("12345678")]
        public void 名前は1文字から8文字以内を許容する(string expect)
        {
            Assert.That(UserNameValidator.Validate(expect), Is.True);
        }

        [TestCase("", TestName = "0文字")]
        [TestCase("123456789", TestName = "9文字")]
        public void 名前はの文字数は1から8文字以外許容しない(string invalidName)
        {
            Assert.That(UserNameValidator.Validate(invalidName), Is.False);
        }

        [Test]
        public void 名前はアルファベットのみ許容する()
        {
            Assert.That(UserNameValidator.Validate("English"), Is.True);
        }

        [TestCase("a b")]
        [TestCase("a_b")]
        [TestCase("aあb")]
        public void 名前はアルファベット以外許容しない(string invalidName)
        {
            Assert.That(UserNameValidator.Validate(invalidName), Is.False);
        }
    }
}
