using Mdfe.Damdfe.Base.Standard;

namespace Mdfe.Damdfe.AppTeste.NetCore
{
    public class ConfiguracaoConsole
    {
        public ConfiguracaoConsole()
        {
            ConfiguracaoDamdfe = new ConfiguracaoDamdfe();
        }

        public ConfiguracaoDamdfe ConfiguracaoDamdfe { get; set; }
    }
}