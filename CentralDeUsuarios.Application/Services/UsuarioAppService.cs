using AutoMapper;
using CentralDeUsuarios.Application.Commands;
using CentralDeUsuarios.Application.Interfaces;
using CentralDeUsuarios.Domain.Entities;
using CentralDeUsuarios.Domain.Interfaces.Services;
using CentralDeUsuarios.Domain.Models;
using CentralDeUsuarios.Infra.Logs.Interfaces;
using CentralDeUsuarios.Infra.Logs.Models;
using CentralDeUsuarios.Infra.Messages.Models;
using CentralDeUsuarios.Infra.Messages.Producers;
using CentralDeUsuarios.Infra.Messages.ValueObjects;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Application.Services
{
    /// <summary>
    /// Implementação dos serviços de aplicação
    /// </summary>
    public class UsuarioAppService : IUsuarioAppService
    {
        //atributos
        private readonly IMediator _mediatR;
        private readonly ILogUsuariosPersistence _logUsuariosPersistence;

        //construtor para injeção de dependência
        public UsuarioAppService(IMediator mediatR, ILogUsuariosPersistence logUsuariosPersistence)
        {
            _mediatR = mediatR;
            _logUsuariosPersistence = logUsuariosPersistence;
        }

        public async Task CriarUsuario(CriarUsuarioCommand command)
        {
            await _mediatR.Send(command);
        }

        public async Task<AuthorizationModel> AutenticarUsuario(AutenticarUsuarioCommand command)
        {
            return await _mediatR.Send(command);
        }

        public List<LogUsuarioModel> ConsultarLogDeUsuario(string email)
        {
            return _logUsuariosPersistence.GetAll(email);
        }
    }
}
