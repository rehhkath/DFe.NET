﻿using DFeFacadeBase;
using NFe.Utils;

namespace DFeFacadeZeus
{
    public class ZeusWSFacade : IWSFacade<ConfiguracaoServico>
    {
        private readonly IWStatusServico<ConfiguracaoServico> _wStatusServico;

        public ZeusWSFacade()
        {
            _wStatusServico = new ZeusWSFacadeStatusServico();
        }

        public IConsultaStatusRetorno ConsultaStatus(DFeBase<ConfiguracaoServico> dfeBase)
        {
            return _wStatusServico.ConsultaStatus(dfeBase);
        }
    }
}