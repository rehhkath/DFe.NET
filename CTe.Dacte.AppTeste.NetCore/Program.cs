using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Cte.Dacte.Base.Standard;
using Cte.Dacte.Fast.Standard;
using CTe.Classes;
using CTe.Classes.Servicos.Consulta;
using CTe.Dacte.AppTeste.NetCore;
using DFe.Utils;

namespace Cte.Dacte.AppTeste.NetCore
{
    public class Program
    {
        // private const string ArquivoConfiguracao = @"\configuracao.xml";
        private static ConfiguracaoConsole _configuracoes;
        private static void Main(string[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            Console.WriteLine("Bem vindo aos teste de CT-e com suporte ao NetStandard 2.0!");
            Console.WriteLine("Este exemplo necesita do arquivo Configuração.xml já criado.");
            Console.WriteLine("Caso necessite criar, utilize o app 'CTe.AppTeste'.");
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
            await GerarDactePdf();
        }

        private static async Task GerarDactePdf()
        {

            string caminho = (@"C:\Users\renat\Documents\27220209005062000403570010000079911005601930-cte.xml"); //retirar

            //busca arquivo xml
            string xml = Funcoes.BuscarArquivoXml(caminho);
            try
            {
                var report = GeraClasseDacte(xml);
                byte[] bytes = report.ExportarPdf();
                Funcoes.SalvaArquivoGerado(caminho, ".pdf", bytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static DacteFrCte GeraClasseDacte(string xml)
        {
            var config = _configuracoes.ConfiguracaoDacte;
            try
            {
                #region Carrega um XML com cteProc para a variável
                cteProc cte = null;
                try
                {
                    cte = FuncoesXml.XmlStringParaClasse<cteProc>(xml);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                #endregion
                DacteFrCte dacte = new DacteFrCte(proc: cte, config: new ConfiguracaoDacte(){
                    Logomarca = File.ReadAllBytes("C:\\Users\\renat\\Pictures\\file-20200309-118956-1cqvm6j.jpg"),
                    DocumentoCancelado = false,
                    Desenvolvedor = "",
                    QuebrarLinhasObservacao = true,
                },
                arquivoRelatorio: "C:\\Users\\renat\\Desktop\\Repositories\\DFe.NET\\Cte.Dacte.Fast.Standard\\Resources\\CTeRetrato.frx");
                
                return dacte;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] ImageToByte(Image img)
        {
            if (img == null)
                return null;
            
            //var converter = new ImageConverter();
            //return (byte[])converter.ConvertTo(img, typeof(byte[]));
            return null;
        }

    }
}