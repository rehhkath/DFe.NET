using System.IO;
using Cte.Dacte.Base.Standard;
using CTe.Classes;
using FastReport;

namespace Cte.Dacte.Fast.Standard
{
    public static class DacteSharedHelper
    {
        public static Report GenerateDacteReport(cteProc proc, ConfiguracaoDacte config, byte[] frx, string arquivoRelatorio)
        {
            Report relatorio = new Report();
            relatorio.RegisterData(new[] { proc }, "cteProc", 20);
            relatorio.GetDataSource("cteProc").Enabled = true;

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