using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocieteApi.DalManager.Repository;
using SocieteApi.Exceptions;
using SocieteApi.Model;

namespace SocieteApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SocieteController : ControllerBase
    {

        private readonly ILogger<SocieteController> _logger;
        private SocieteRepository _repo;

        public SocieteController(ILogger<SocieteController> logger,SocieteRepository repo)
        {
            _logger = logger;
            _repo=repo;
        }

        [HttpGet]
        public IEnumerable<Societe> Get()
        {
            return _repo.GetAll();
        }

        [HttpGet("{id}")]
        public Societe Get(Guid id)
        {
            return null;
        }

        [HttpPost]
        public Societe Post([FromBody] Societe societe)
        {
            societe.Id=Guid.NewGuid();
            
            // validation name
            if(string.IsNullOrEmpty(societe.Nom) || string.IsNullOrWhiteSpace(societe.Nom))
            {
                throw new BusinessException("NO_VALID_DATA","Le nom est obligatoire");
            }

            // validation siret
            // verification not nulle
            if(string.IsNullOrEmpty(societe.Siret) || string.IsNullOrWhiteSpace(societe.Siret))
            {
                throw new BusinessException("NO_VALID_DATA","Le numéro Siret est obligatoire");
            }
            // verification longueur 14
            if(societe.Siret.Count()!=14)
            {
                throw new BusinessException("NO_VALID_DATA","Le numéro Siret est non valide");
            }
            // verification int 
            if(!Int64.TryParse(societe.Siret,out Int64 res))
            {
                throw new BusinessException("NO_VALID_DATA","Le numéro Siret est non valide");
            }
            // Validation de Luhn
            int total = 0;
		    int digit = 0;
            for (int i = 0; i<societe.Siret.Length; i++) 
            {
			        /** Recherche les positions impaires : 1er, 3è, 5è, etc... que l'on multiplie par 2
                       petite différence avec la définition ci-dessus car ici on travail de gauche à droite */

                if ((i % 2) == 0) 
                {
                    digit = int.Parse(societe.Siret[i].ToString()) * 2;
				    /** si le résultat est >9 alors il est composé de deux digits tous les digits devant 
                    s'additionner et ne pouvant être >19 le calcule devient : 1 + (digit -10) ou : digit - 9 */

				    if (digit > 9) digit -= 9;
			    }
			    else 
                {
                    digit = int.Parse(societe.Siret[i].ToString());
                }
			    total += digit;
		    }

            /** Si la somme est un multiple de 10 alors le SIRET est valide */
            if ((total % 10) != 0) 
            {
                throw new BusinessException("NO_VALID_DATA","Le numéro Siret est non valide");
            }
            _repo.Save(societe);
            return societe;

            // 
        }

        [HttpPut("{id}")]
        public Societe Put(Guid id, [FromBody] Societe societe)
        {
            // validation name
            if(string.IsNullOrEmpty(societe.Nom) || string.IsNullOrWhiteSpace(societe.Nom))
            {
                throw new BusinessException("NO_VALID_DATA","Le nom est obligatoire");
            }

            // validation siret
            // verification not nulle
            if(string.IsNullOrEmpty(societe.Siret) || string.IsNullOrWhiteSpace(societe.Siret))
            {
                throw new BusinessException("NO_VALID_DATA","Le numéro Siret est obligatoire");
            }
            // verification longueur 14
            if(societe.Siret.Count()!=14)
            {
                throw new BusinessException("NO_VALID_DATA","Le numéro Siret est non valide");
            }
            // verification int 
            if(!Int64.TryParse(societe.Siret,out Int64 res))
            {
                throw new BusinessException("NO_VALID_DATA","Le numéro Siret est non valide");
            }
            // Validation de Luhn
            int total = 0;
		    int digit = 0;
            for (int i = 0; i<societe.Siret.Length; i++) 
            {
			        /** Recherche les positions impaires : 1er, 3è, 5è, etc... que l'on multiplie par 2
                       petite différence avec la définition ci-dessus car ici on travail de gauche à droite */

                if ((i % 2) == 0) 
                {
                    digit = int.Parse(societe.Siret[i].ToString()) * 2;
				    /** si le résultat est >9 alors il est composé de deux digits tous les digits devant 
                    s'additionner et ne pouvant être >19 le calcule devient : 1 + (digit -10) ou : digit - 9 */

				    if (digit > 9) digit -= 9;
			    }
			    else 
                {
                    digit = int.Parse(societe.Siret[i].ToString());
                }
			    total += digit;
		    }

            /** Si la somme est un multiple de 10 alors le SIRET est valide */
            if ((total % 10) != 0) 
            {
                throw new BusinessException("NO_VALID_DATA","Le numéro Siret est non valide");
            }
            _repo.Update(societe);
            return societe;
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }

    }
}
