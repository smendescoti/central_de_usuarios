using CentralDeUsuarios.Domain.Core;
using CentralDeUsuarios.Domain.Entities;
using CentralDeUsuarios.Domain.Interfaces.Repositories;
using CentralDeUsuarios.Domain.Interfaces.Security;
using CentralDeUsuarios.Domain.Interfaces.Services;
using CentralDeUsuarios.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Domain.Services
{
    /// <summary>
    /// Implementação dos serviços de domínio de usuários
    /// </summary>
    public class UsuarioDomainService : IUsuarioDomainService
    {
        //atributos
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorizationSecurity _authorizationSecurity;

        /// <summary>
        /// Construtor para injeção de dependência
        /// </summary>
        public UsuarioDomainService(IUnitOfWork unitOfWork, IAuthorizationSecurity authorizationSecurity)
        {
            _unitOfWork = unitOfWork;
            _authorizationSecurity = authorizationSecurity;
        }

        /// <summary>
        /// Método para criar um usuário na aplicação
        /// </summary>
        /// <param name="usuario">Entidade de domínio</param>
        public void CriarUsuario(Usuario usuario)
        {
            //Não é permitido cadastrar usuários com o mesmo email
            DomainException.When(
                    _unitOfWork.UsuarioRepository.GetByEmail(usuario.Email) != null,
                    $"O email {usuario.Email} já está cadastrado, tente outro."
                );

            _unitOfWork.UsuarioRepository.Create(usuario);
        }

        public AuthorizationModel AutenticarUsuario(string email, string senha)
        {
            //consultar o usuário no banco de dados através do email e senha
            var usuario = _unitOfWork.UsuarioRepository.GetByEmailAndSenha(email, senha);

            DomainException.When(
                usuario == null,
                "Acesso negado. Usuário não encontrado."
                );

            return new AuthorizationModel {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataHoraAcesso = DateTime.Now,
                AccessToken = _authorizationSecurity.CreateToken(usuario)
            };
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }        
    }
}
