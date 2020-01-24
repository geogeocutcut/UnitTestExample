using System;
using System.Collections.Generic;
using ApplicationBusiness;
using ApplicationBusiness.IBusiness;
using ApplicationBusiness.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApplicationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SocieteController : ControllerBase
    {

        private readonly ILogger<SocieteController> _logger;
        private ISocieteBusiness _serv;

        public SocieteController(ILogger<SocieteController> logger,ISocieteBusiness serv)
        {
            _logger = logger;
            _serv=serv;
        }

        [HttpGet]
        public IEnumerable<Societe> Get()
        {
            return _serv.GetAll();
        }

        [HttpGet("{id}")]
        public Societe Get(Guid id)
        {
            return null;
        }

        [HttpPost]
        public Societe Post([FromBody] Societe societe)
        {
            _serv.Add(societe);
            return societe;
        }

        [HttpPut("{id}")]
        public Societe Put(Guid id, [FromBody] Societe societe)
        {
            
            _serv.Update(societe);
            return societe;
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }

    }
}
