using System.IO;
using System;
using FastReport;
using CTe.Classes;
using CTe.Classes.Servicos.Consulta;
using FastReport.Export.PdfSimple;

namespace Cte.Dacte.Fast.Standard
{
    public class DacteFrEvento
    {
        protected Report Relatorio;

        public DacteFrEvento()
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

        public void RegisterData(cteProc proc, procEventoCTe procEventoCTe)
        {
            Relatorio.RegisterData(new[] { proc }, "cteProc", 20);
            Relatorio.GetDataSource("cteProc").Enabled = true;

            Relatorio.RegisterData(new[] { procEventoCTe }, "procEventoCTe", 20);
            Relatorio.GetDataSource("procEventoCTe").Enabled = true;
        }

        public void Configurar(string desenvolvedor = "")
        {
            Relatorio.SetParameterValue("Desenvolvedor", desenvolvedor);
        }

        public DacteFrEvento(cteProc proc, procEventoCTe procEventoCTe, string desenvolvedor = "", string arquivoRelatorio = "")
        {
            Relatorio = new Report();
            RegisterData(proc, procEventoCTe);
            if (!string.IsNullOrEmpty(arquivoRelatorio))
                Relatorio.Load(arquivoRelatorio);
            else
                throw new Exception("Faltou o cte retrato");
                // Relatorio.Load(new MemoryStream(Properties.Resources.CTeEvento));
            Configurar(desenvolvedor: desenvolvedor);
        }


        /// <summary>
        /// Abre a janela de visualização do DAMDFe
        /// </summary>
        /// <param name="modal">Se true, exibe a visualização em Modal. O modo modal está disponível apenas para WinForms</param>
        // public void Visualizar(bool modal = true)
        // {
        //     Relatorio.Show(modal);
        // }

        /// <summary>
        ///  Abre a janela de visualização do design do DAMDFe
        /// Chame esse método se desja fazer alterações no design do DAMDFe em modo run-time
        /// </summary>
        /// <param name="modal">Se true, exibe a visualização em Modal. O modo modal está disponível apenas para WinForms</param>
        // public void ExibirDesign(bool modal = false)
        // {
        //     Relatorio.Design(modal);
        // }

        /// <summary>
        /// Envia a impressão do DAMDFe diretamente para a impressora
        /// </summary>
        /// <param name="exibirDialogo">Se true exibe o diálogo Imprimindo...</param>
        /// <param name="impressora">Passe a string com o nome da impressora para imprimir diretamente em determinada impressora. Caso contrário, a impressão será feita na impressora que estiver como padrão</param>
        // public void Imprimir(bool exibirDialogo = true, string impressora = "")
        // {
        //     Relatorio.PrintSettings.ShowDialog = exibirDialogo;
        //     Relatorio.PrintSettings.Printer = impressora;
        //     Relatorio.Print();
        // }

        /// <summary>
        /// Converte o DAMDFe para PDF e salva-o no caminho/arquivo indicado
        /// </summary>
        /// <param name="arquivo">Caminho/arquivo onde deve ser salvo o PDF do DAMDFe</param>
        public void ExportarPdf(string arquivo)
        {
            Relatorio.Prepare();
            Relatorio.Export(new PDFSimpleExport(), arquivo);
        }

        /// <summary>
        /// Converte o DAMDFe para PDF e copia para o stream
        /// </summary>
        /// <param name="arquivo">Caminho/arquivo onde deve ser salvo o PDF do DAMDFe</param>
        public void ExportarPdf(Stream outputStream)
        {
            Relatorio.Prepare();
            Relatorio.Export(new PDFSimpleExport(), outputStream);
            outputStream.Position = 0;
        }

        /// <summary>
        /// Converte o DAMDFe para PDF e salva-o no caminho/arquivo indicado
        /// </summary>
        /// <param name="arquivo">Caminho/arquivo onde deve ser salvo o PDF do DAMDFe</param>
        /// <param name="exportBase">Instancia do tipo de exportacao do FastReport</param>
        public void ExportarPdf(string arquivo, FastReport.Export.ExportBase exportBase)
        {
            if (exportBase == null)
                throw new NullReferenceException("exportBase deve ter um objeto instanciado, tente 'new PDFExport()'");

            Relatorio.Prepare();
            Relatorio.Export(exportBase, arquivo);
        }

        /// <summary>
        /// Converte o DAMDFe para PDF e copia para o stream
        /// </summary>
        /// <param name="arquivo">Caminho/arquivo onde deve ser salvo o PDF do DAMDFe</param>
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
