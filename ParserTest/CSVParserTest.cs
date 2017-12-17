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
            public int Number { get; set; }
            public DateTime Date { get; set; }
            public bool Bool { get; set; }
        }

        [TestMethod]
        public void 出力確認()
        {
            var csv = "Text,Number,Date,Bool\r\ntest,1,2017-01-10,true\r\n\"te,t\"\"tt\",2,2017-01-21,false";
            var parser = new CSVParser<Sample>();
            var list = parser.ParseCsv(csv);

            Assert.AreEqual(2, list.ToList().Count());

            foreach(var entity in list)
            {
                Assert.IsInstanceOfType(entity, typeof(Sample));
            }
        }

        [TestMethod]
        public void 空確認()
        {
            var csv = "Text,Number,Date,Bool\r\n,,,";
            var parser = new CSVParser<Sample>();
            var list = parser.ParseCsv(csv);

            Assert.AreEqual(1, list.ToList().Count());

            var entity = list.First();

            Assert.AreEqual(default(string), entity.Text);
            Assert.AreEqual(default(int), entity.Number);
            Assert.AreEqual(default(DateTime), entity.Date);
            Assert.AreEqual(default(bool), entity.Bool);
        }

        [TestMethod]
        public void 空文字確認()
        {
            var csv = "Text,Number,Date,Bool\r\n 　,,,";
            var parser = new CSVParser<Sample>();
            var list = parser.ParseCsv(csv);

            Assert.AreEqual(1, list.ToList().Count());

            var entity = list.First();

            Assert.AreEqual(" 　", entity.Text);

        }
    }
}
