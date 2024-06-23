using HtmlToJsonConverterWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HtmlToJsonConverterWeb.Controllers
{
    public class ConverterController : Controller
    {
        public IActionResult Index()
        {
            return View(new ConversionModel());
        }

        [HttpPost]
        public async Task<IActionResult> Convert([FromBody] ConversionInputModel input)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { success = false, error = string.Join(", ", errors) });
            }

            try
            {
                var options = new HtmlToJsonParser.ParserOptions
                {
                    AttributePrefix = input.AttributePrefix,
                    TextPropertyName = input.TextPropertyName,
                    ValueNewLineConversion = input.ValueNewLineConversion,
                    Indent = input.Indent,
                    UnescapeJson = input.UnescapeJson,
                    TrimInsideWords = input.TrimInsideWords,
                    ConvertAllTables = input.ConvertAllTables
                };

                string jsonOutput = await HtmlToJsonParser.ParseHtmlToJson(input.HtmlInput, input.ParserMode, options);
                return Json(new { success = true, result = jsonOutput });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }

    public class ConversionInputModel
    {
        [Required(ErrorMessage = "HTML input is required")]
        public string HtmlInput { get; set; }

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
