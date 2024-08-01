using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyDishController : ControllerBase
    {
        private readonly ICacheRepository _cacheRepository;

        public DailyDishController(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        [HttpGet]
        public ActionResult Get(string key) => Ok(_cacheRepository.Get<string>(key));

        [HttpPut]
        public ActionResult Put(string key, string value)
        {
            _cacheRepository.Set(key, value);

            return Created(key, value);
        }
    }
}
