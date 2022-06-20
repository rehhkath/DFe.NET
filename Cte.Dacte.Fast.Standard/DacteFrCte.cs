using System.IO;
using System;
using CTe.Classes;
using Cte.Dacte.Base.Standard;
using FastReport;
using FastReport.Export.PdfSimple;

namespace Cte.Dacte.Fast.Standard
{
    public class DacteFrCte : DacteBase
    {
        // protected Report Relatorio;

        public DacteFrCte()
        {
            Relatorio = new Report();
        }

        public void LoadReport(string arquivoRelatorio)
        {
            Relatorio.Load(arquivoRelatorio);
        }

        public void LoadReport(MemoryStream stream)
        {
            Relatorio.Load(stream);
        }

        public void RegisterData(cteProc proc)
        {
            Relatorio.RegisterData(new[] { proc }, "cteProc", 20);
            Relatorio.GetDataSource("cteProc").Enabled = true;            
        }

        public void Configurar(ConfiguracaoDacte config)
        {
            Relatorio.SetParameterValue("DoocumentoCancelado", config.DocumentoCancelado);
            Relatorio.SetParameterValue("Desenvolvedor", config.Desenvolvedor);
            Relatorio.SetParameterValue("QuebrarLinhasObservacao", config.QuebrarLinhasObservacao);
            if (Relatorio.FindObject("poEmitLogo") != null)
                ((PictureObject)Relatorio.FindObject("poEmitLogo")).Image = config.ObterLogo();
        }

        public DacteFrCte(cteProc proc, ConfiguracaoDacte config, string arquivoRelatorio = "")
        {
            byte[] retrato = null;
            if(string.IsNullOrWhiteSpace(arquivoRelatorio))
            {
                try
                {
                    retrato = Properties.Resources.CTeRetratoPadrao;
                }
                catch (Exception ex)
                {
                    throw new Exception("Não foi possivel o carregamento do Resource CTeRetrato, utilize o parametro arquivoRelatorio e passe o caminho manualmente.", ex);
                }
            }
            this.Relatorio = DacteSharedHelper.GenerateDacteReport(proc, config, retrato, arquivoRelatorio);
            Configurar(config);
        }
        
        /// <summary>
        /// Converte o DACTe para PDF e salva-o no caminho/arquivo indicado
        /// </summary>
        /// <param name="arquivo">Caminho/arquivo onde deve ser salvo o PDF do DACTe</param>
        public void ExportarPdf(string arquivo)
        {
            Relatorio.Prepare();
            Relatorio.Export(new PDFSimpleExport(), arquivo);
        }

        /// <summary>
        /// Converte o DACTe para PDF e copia para o stream
        /// </summary>
        /// <param name="outputStream">Variável do tipo Stream para output</param>
        public void ExportarPdf(Stream outputStream)
        {
            Relatorio.Prepare();
            Relatorio.Export(new PDFSimpleExport(), outputStream);
            outputStream.Position = 0;
        }

        /// <summary>
        /// Converte o DACTe para PDF e salva-o no caminho/arquivo indicado
        /// </summary>
        /// <param name="arquivo">Caminho/arquivo onde deve ser salvo o PDF do DACTe</param>
        /// <param name="exportBase">Instancia do tipo de exportacao do FastReport</param>
        public void ExportarPdf(string arquivo, FastReport.Export.ExportBase exportBase)
        {
            if (exportBase == null)
                throw new NullReferenceException("exportBase deve ter um objeto instanciado, tente 'new PDFExport()'");

            Relatorio.Prepare();
            Relatorio.Export(exportBase, arquivo);
        }

        /// <summary>
        /// Converte o DACTe para PDF e copia para o stream
        /// </summary>
        /// <param name="outputStream">Variável do tipo Stream para output</param>
        /// <param name="exportBase">Instancia do tipo de exportacao do FastReport</param>
        public void ExportarPdf(Stream outputStream, FastReport.Export.ExportBase exportBase)
        {
            if (exportBase == null)
                throw new NullReferenceException("exportBase deve ter um objeto instanciado, tente 'new PDFExport()'");

            Relatorio.Prepare();
            Relatorio.Export(exportBase, outputStream);
            outputStream.Position = 0;
        }
    }
}