using FastReport;
using Mdfe.Damdfe.Base.Standard;
using MDFe.Classes.Retorno;
using System.IO;

namespace Mdfe.Damdfe.Fast.Standard
{
    public class DamdfeHelper
    {
        public static Report GenerateDamdfeReport(MDFeProcMDFe proc, ConfiguracaoDamdfe config, byte[] frx, string arquivoRelatorio)
        {
            Report relatorio = new Report();
            relatorio.RegisterData(new[] { proc }, "MDFeProcMDFe", 20);
            relatorio.GetDataSource("MDFeProcMDFe").Enabled = true;

            if (string.IsNullOrEmpty(arquivoRelatorio))
            {
                relatorio.Load(new MemoryStream(frx));
            }
            else
            {
                relatorio.Load(arquivoRelatorio);
            }
            return relatorio;
        }
    }
}