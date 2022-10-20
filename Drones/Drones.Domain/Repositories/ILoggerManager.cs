using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.Domain.Repositories
{
    public interface ILoggerManager
    {
        void LogInformation(string message);
       
        void LogInformation(string message, Exception ex);

        void LogWarning(string message);

        void LogWarning(string message, Exception ex);
       
        void LogError(string message);

        void LogError(string message, Exception ex);
    }
}
