using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRagChatPoc.Kernel.Services.TextExtraction
{
    public interface ITextExtractorService
    {
        string ExtractTextFromPdf(Stream pdf);
    }
}