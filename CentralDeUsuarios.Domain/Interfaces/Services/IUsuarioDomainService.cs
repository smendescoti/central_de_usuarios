using CentralDeUsuarios.Domain.Entities;
using CentralDeUsuarios.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Domain.Interfaces.Services
{
    /// <summary>
    /// Interface para serviços de domínio de usuário
    /// </summary>
    public interface IUsuarioDomainService : IDisposable
    {
        /// <summary>
        /// Método para criar um usuário na aplicação
        /// </summary>
        /// <param name="usuario">Entidade de domínio</param>
        void CriarUsuario(Usuario usuario);

        /// <summary>
        /// Método para autenticar o usuário na aplicação
        /// </summary>
        AuthorizationModel AutenticarUsuario(string email, string senha);
    }
}
