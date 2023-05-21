using CentralDeUsuarios.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Application.Commands
{
    /// <summary>
    /// Modelo de dados para a requisição de autenticação de usuário
    /// </summary>
    public class AutenticarUsuarioCommand : IRequest<AuthorizationModel>
    {
        public string? Email { get; set; }
        public string? Senha { get; set; }
    }
}
