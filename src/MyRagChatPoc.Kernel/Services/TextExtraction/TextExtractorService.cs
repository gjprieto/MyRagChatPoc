using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace MyRagChatPoc.Kernel.Services.TextExtraction
{
    public class TextExtractorService : ITextExtractorService
    {
        public string ExtractTextFromPdf(Stream pdf)
        {
            var text = new StringBuilder();

            pdf.Seek(0, SeekOrigin.Begin);

            using (PdfDocument document = PdfDocument.Open(pdf))
            {
                foreach (Page page in document.GetPages())
                {
                    var pageText = ContentOrderTextExtractor.GetText(page, true);
                    text.Append(pageText);
                }
            }

            return text.ToString();
        }
    }
}