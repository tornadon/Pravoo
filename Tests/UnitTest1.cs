using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pravoo;

namespace Tests
{
    /// <summary>
    /// Класс тестов
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Проверка функции Replacer на тексте, нуждающемся в преобразовании
        /// </summary>
        [TestMethod]
        public void Invalid_Text()
        {
            Assert.AreEqual("привет здравствуй", Program.Replacer("прu8ет 3дра8ствуй"));
        }

        /// <summary>
        /// Проверка функции rusReplace на слове, нуждающемся в преобразовании
        /// </summary>
        [TestMethod]
        public void Invalid_Word()
        {
            Assert.AreEqual("привет", Program.rusReplace("прu8ет"));
        }

        /// <summary>
        /// Проверка функции textNormalizer на тексте, нуждающемся в преобразовании
        /// </summary>
        [TestMethod]
        public void Normalize_Invalid_Text()
        {
            Assert.AreEqual("привет здравствуй", Program.textNormalizer("ПРИВЕТ, ЗДРАВСТВУЙ.;|/&@#$%^:?<>{}[]"));
        }

        /// <summary>
        /// Проверка функции textNormalizer на тексте, не нуждающемся в преобразовании
        /// </summary>
        [TestMethod]
        public void Normalize_Valid_Text()
        {
            string text = "привет здравствуй";
            Assert.AreEqual(text, Program.textNormalizer(text));
        }

        /// <summary>
        /// Проверка функции Replacer на тексте, не нуждающемся в преобразовании
        /// </summary>
        [TestMethod]
        public void Valid_Text()
        {
            string text = "привет здравствуй";
            Assert.AreEqual(text, Program.Replacer(text));
        }

        /// <summary>
        /// Проверка функции rusReplace на слове, не нуждающемся в преобразовании
        /// </summary>
        [TestMethod]
        public void Valid_Word()
        {
            string word = "привет";
            Assert.AreEqual(word, Program.rusReplace(word));
        }
    }
}
