using System;

namespace SocieteApi.Model
{
    public class Societe
    {
        public virtual Guid Id { get; set; }
        public virtual string Nom { get; set; }
        public virtual string Siret { get; set; }
    }
}