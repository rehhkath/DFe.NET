using Cte.Dacte.Base.Standard;

namespace CTe.Dacte.AppTeste.NetCore
{
    public class ConfiguracaoConsole
    {
        public ConfiguracaoConsole()
        {
            ConfiguracaoDacte = new ConfiguracaoDacte();            
        }

        public ConfiguracaoDacte ConfiguracaoDacte { get; set; }
    }
}