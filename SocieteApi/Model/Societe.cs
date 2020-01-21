using System;

namespace SocieteApi.Model
{
    public class Societe
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Siret { get; set; }
    }
}