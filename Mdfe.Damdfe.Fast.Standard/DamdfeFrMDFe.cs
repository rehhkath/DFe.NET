using FastReport;
using FastReport.Export.PdfSimple;
using Mdfe.Damdfe.Base.Standard;
using MDFe.Classes.Retorno;
using System;
using System.IO;

namespace Mdfe.Damdfe.Fast.Standard
{
    public class DamdfeFrMDFe : DamdfeBase
    {
        public DamdfeFrMDFe(MDFeProcMDFe proc)
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

        public void RegisterData(MDFeProcMDFe proc)
        {
            Relatorio.RegisterData(new[] { proc }, "MDFeProcMDFe", 20);
            Relatorio.GetDataSource("MDFeProcMDFe").Enabled = true;
        }

        public void Configurar(ConfiguracaoDamdfe config)
        {
            Relatorio.SetParameterValue("DoocumentoCancelado", config.DocumentoCancelado);
            Relatorio.SetParameterValue("DocumentoEncerrado", config.DocumentoEncerrado);
            Relatorio.SetParameterValue("Desenvolvedor", config.Desenvolvedor);
            Relatorio.SetParameterValue("QuebrarLinhasObservacao", config.QuebrarLinhasObservacao);
            ((PictureObject)Relatorio.FindObject("poEmitLogo")).Image = config.ObterLogo();
        }
        public DamdfeFrMDFe(MDFeProcMDFe proc, ConfiguracaoDamdfe config, string arquivoRelatorio = "")
        {
            byte[] retrato = null;
            if (string.IsNullOrWhiteSpace(arquivoRelatorio))
            {
                try
                {
                    retrato = Properties.Resources.MDFeRetratoStandard;
                }
                catch (Exception ex)
                {
                    throw new Exception("N�o foi possivel o carregamento do Resource CTeRetrato, utilize o parametro arquivoRelatorio e passe o caminho manualmente.", ex);
                }
            }
            this.Relatorio = DamdfeHelper.GenerateDamdfeReport(proc, config, retrato, arquivoRelatorio);
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
        /// <param name="outputStream">Vari�vel do tipo Stream para output</param>
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
        /// <param name="outputStream">Vari�vel do tipo Stream para output</param>
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