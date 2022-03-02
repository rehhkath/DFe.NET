using DFe.Utils;
using Mdfe.Damdfe.Fast.Standard;
using MDFe.Classes.Retorno;
using System.Net;

namespace Mdfe.Damdfe.AppTeste.NetCore
{
    public class Program
    {
        private static ConfiguracaoConsole _configuracoes;
        private static void Main(string[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            Console.WriteLine("Bem vindo aos teste de MDFe com suporte ao NetStandard 2.0!");
            Console.WriteLine("Este exemplo necesita do arquivo Configuração.xml já criado.");
            Console.WriteLine("Caso necessite criar, utilize o app 'MDFe.AppTeste'.");
            Console.WriteLine("Em seguida copie o Configuração.xml para a pasta bin\\Debug\\netcoreapp2.2 deste projeto.\n");
            try
            {
                _configuracoes = new ConfiguracaoConsole();
                Menu();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private static async void Menu()
        {
            await GerarDamdfePdf();
        }
        private static async Task GerarDamdfePdf()
        {
            string caminho = (@"caminho.xml");
            string xml = Funcoes.BuscarArquivoXml(caminho);
            try
            {
                var report = GerarClasseDamdfe(xml);
                byte[] bytes = report.ExportarPdf();
                Funcoes.SalvaArquivoGerado(caminho, ".pdf", bytes);
            }
            catch (Exception ex)
            {
                throw ex;   
            }
        }
        private static DamdfeFrMDFe GerarClasseDamdfe(string xml)
        {
            var config = _configuracoes.ConfiguracaoDamdfe;
            try
            {
                #region Carrega um XML com MDFeProc para a variável
                MDFeProcMDFe mdfe = null;
                try
                {
                    mdfe = FuncoesXml.XmlStringParaClasse<MDFeProcMDFe>(xml);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                #endregion
                DamdfeFrMDFe damdfe = new DamdfeFrMDFe(proc: mdfe, config: new Base.Standard.ConfiguracaoDamdfe()
                {
                    Logomarca = File.ReadAllBytes("colocar o caminho da imagem"),
                    DocumentoCancelado = false,
                    DocumentoEncerrado = false,
                    Desenvolvedor = "Teste",
                    QuebrarLinhasObservacao = true,
                },
                arquivoRelatorio:""
                );
                return damdfe;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
