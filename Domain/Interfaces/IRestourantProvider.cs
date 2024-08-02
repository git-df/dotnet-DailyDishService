using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRestourantProvider
    {
        public string Name { get; }

        Task<object> GetValueAsync(CancellationToken cancellationToken = default);
    }
}
