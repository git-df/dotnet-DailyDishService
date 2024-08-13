using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Options
{
    public class RandomDishOptions
    {
        public string RandomDishUrl { get; set; } = "https://www.themealdb.com/api/json/v1/1/random.php";
    }
}
