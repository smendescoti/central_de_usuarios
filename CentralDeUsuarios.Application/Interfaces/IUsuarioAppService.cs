﻿using CentralDeUsuarios.Application.Commands;
using CentralDeUsuarios.Domain.Models;
using CentralDeUsuarios.Infra.Logs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Application.Interfaces
{
    /// <summary>
    /// Interface para abstração dos métodos da camada de aplicação para usuário
    /// </summary>
    public interface IUsuarioAppService
    {
        /// <summary>
        /// Método para criar um usuário na aplicação
        /// </summary>
        /// <param name="command">Dados para criação do usuário</param>
        Task CriarUsuario(CriarUsuarioCommand command);

        /// <summary>
        /// Método para autenticar um usuário na aplicação
        /// </summary>
        /// <param name="command">Dados para autenticação do usuário</param>
        Task<AuthorizationModel> AutenticarUsuario(AutenticarUsuarioCommand command);

        /// <summary>
        /// Método para consultar o log de um determinado usuário
        /// </summary>
        /// <param name="email">Email do usuário</param>
        /// <returns>Dados do log do usuário</returns>
        List<LogUsuarioModel> ConsultarLogDeUsuario(string email);
    }
}
