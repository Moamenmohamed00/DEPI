using ApiSessions.life_time;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSessions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LifeTimeController : ControllerBase
    {
        private readonly TransientService _transient1;
        private readonly TransientService _transient2;
        private readonly SingletonService _single1;
        private readonly SingletonService _single2;
        private readonly ScopedService _scopedService1;
        private readonly ScopedService _scopedService2;
        public LifeTimeController(
            TransientService transient1, TransientService transient2
            , SingletonService singleton1, SingletonService singleton2
            , ScopedService scoped1, ScopedService scoped2)
        {
            _scopedService1 = scoped1;
            _scopedService2 = scoped2;
            _transient1 = transient1;
            _transient2 = transient2;
            _single1 = singleton1;
            _single2 = singleton2;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new{
                transient =new {
                    id1=_transient1.id,
                    id2=_transient2.id,
                    same=_transient1.id==_transient2.id
                },
                singleton = new {id1=_single1.id,
                id2=_single2.id,
                same=_single1.id ==_single2.id
                },
                scoped=new
                {
                    id1 = _scopedService1.id,
                    id2 = _scopedService2.id,
                    same = _scopedService1.id == _scopedService2.id
                }
            });
        }
    }
}
