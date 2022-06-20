using System.IO;
using FastReport;
using FastReport.Export.Html;
using FastReport.Export.Image;
using FastReport.Export.PdfSimple;

namespace Cte.Dacte.Fast.Standard
{
    public class DacteBase : IDacteBasico
    {
        public Report Relatorio { get; protected set; }
        public byte[] ExportarPdf()
        {
            using (MemoryStream stream = new MemoryStream()) // Create a stream for the report
            {
                try
                {
                    FastReport.Utils.Config.EnableScriptSecurity = false;
                    Relatorio.Prepare();
                    Relatorio.Export(new PDFSimpleExport(), stream);
                    return stream.ToArray();
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}