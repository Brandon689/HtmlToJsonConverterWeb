using System.ComponentModel.DataAnnotations;

namespace HtmlToJsonConverterWeb.Models
{
    public class ConversionModel
    {
        [Required(ErrorMessage = "HTML input is required")]
        public string HtmlInput { get; set; }

        public string JsonOutput { get; set; }

        [Required(ErrorMessage = "Parser mode is required")]
        public HtmlToJsonParser.ParserMode ParserMode { get; set; }

        public string AttributePrefix { get; set; } = "@";

        public string TextPropertyName { get; set; } = "#text";

        public HtmlToJsonParser.NewLineConversion ValueNewLineConversion { get; set; }

        public bool Indent { get; set; } = true;

        public bool UnescapeJson { get; set; }

        public bool TrimInsideWords { get; set; }

        public bool ConvertAllTables { get; set; }
    }
}
