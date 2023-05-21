using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralDeUsuarios.Infra.Security.Settings
{
    /// <summary>
    /// Capturar os valores do appsettings.json
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Chave secreta antifalsificação do TOKEN
        /// </summary>
        public string? SecretKey { get; set; }

        /// <summary>
        /// Tempo de expiração do TOKEN em horas
        /// </summary>
        public int ExpirationInHours { get; set; }
    }
}
