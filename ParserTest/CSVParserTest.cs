using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parser;
using System.Linq;
using Parser.Model;

namespace ParserTest
{
    [TestClass]
    public class CSVParserTest
    {
        public class Sample : IMapperEntity
        {
            public string Text { get; set; }
            public string Name { get; set; }
            public int Field { get; set; }
        }

        [TestMethod]
        public void 出力確認()
        {
            var csv = "Text,Name,Field\r\ntest,sample,3\r\n\"te,t\"\"tt\",sample,2";
            var parser = new CSVParser<Sample>();
            var list = parser.ParseCsv(csv);

            Assert.AreEqual(2, list.ToList().Count());

            foreach(var entity in list)
            {
                Assert.IsInstanceOfType(entity, typeof(Sample));
            }

        }
    }
}
