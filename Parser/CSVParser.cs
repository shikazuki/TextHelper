using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using System.Data;
using Parser.Model;
using Parser.Mapper;

namespace Parser
{
    public class CSVParser<T> where T : IMapperEntity
    {
        public IMapper<T> Mapper { get; set; } = new BaseMapper<T>();

        public CSVParser()
        {
        }

        public CSVParser(IMapper<T> mapper)
        {
            Mapper = mapper;
        }

        public IEnumerable<T> ParseCsv(string csv)
        {
            var table = new DataTable();
            using (var stream = new MemoryStream(Encoding.Default.GetBytes(csv.ToCharArray())))
            using (var parser = new TextFieldParser(stream))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                try
                {
                    var header = parser.ReadFields();

                    foreach(var h in header)
                    {
                        var column = new DataColumn(h, typeof(string));
                        table.Columns.Add(column);
                    }

                    while (!parser.EndOfData)
                    {
                        string[] data = parser.ReadFields();

                        var row = table.NewRow();
                        foreach (var d in data.Select((d, i) => new { v = d,  i = i }))
                        {
                            row.SetField(d.i, d.v);
                        }
                        table.Rows.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return Mapper.Map(table);
        }
        
    }
}
