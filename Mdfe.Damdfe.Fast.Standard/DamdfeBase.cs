using FastReport;
using FastReport.Export.PdfSimple;
using System.IO;

namespace Mdfe.Damdfe.Fast.Standard
{
    public class DamdfeBase : IDamdfeBasico
    {
        public Report Relatorio { get; protected set; }
        public byte[] ExportarPdf()
        {
            using (MemoryStream stream = new MemoryStream()) // Create a stream for the report
            {
                try
                {
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