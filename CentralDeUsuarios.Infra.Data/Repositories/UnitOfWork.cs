using CentralDeUsuarios.Domain.Interfaces.Repositories;
using CentralDeUsuarios.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        //atributos
        private readonly SqlServerContext _sqlServerContext;
        private IDbContextTransaction _dbContextTransaction;

        //construtor para injeção de dependência
        public UnitOfWork(SqlServerContext sqlServerContext)
        {
            _sqlServerContext = sqlServerContext;
        }

        public void BeginTransaction()
        {
            _dbContextTransaction = _sqlServerContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _dbContextTransaction.Commit();
        }

        public void Rollback()
        {
            _dbContextTransaction.Rollback();
        }

        public IUsuarioRepository UsuarioRepository => new UsuarioRepository(_sqlServerContext);

        public void Dispose()
        {
            _sqlServerContext.Dispose();
        }
    }
}
