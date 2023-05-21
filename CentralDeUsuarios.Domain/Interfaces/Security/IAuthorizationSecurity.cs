using CentralDeUsuarios.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Domain.Interfaces.Security
{
    /// <summary>
    /// Interface para definir o contrato de métodos para
    /// geração do TOKEN do usuário
    /// </summary>
    public interface IAuthorizationSecurity
    {
        /// <summary>
        /// Método para geração do token do usuário
        /// </summary>
        /// <param name="usuario">Dados do usuário autenticado</param>
        /// <returns>TOKEN JWT para este usuário</returns>
        string CreateToken(Usuario usuario);
    }
}
