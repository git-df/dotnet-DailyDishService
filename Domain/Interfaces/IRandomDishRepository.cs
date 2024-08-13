using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRandomDishRepository
    {
        Task<RandomDishDto> GetDishAsync(CancellationToken cancellationToken = default);
    }
}
