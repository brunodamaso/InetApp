using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace INetApp.Services.Interfaces
{
    public interface IRepositoryService
    {
        Task<T> Get<T>(object pk, bool withChildren = true) where T : new();
    
    }
}
