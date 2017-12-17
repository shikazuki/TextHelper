using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.Configuration
{
    public class CSVConfiguration
    {
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        public FieldType TextFieldType { get; set; } = FieldType.Delimited;
        public bool TrimWhiteSpace { get; set; } = false;
        public string Delimiter { get; set; } = ",";
    }
}
