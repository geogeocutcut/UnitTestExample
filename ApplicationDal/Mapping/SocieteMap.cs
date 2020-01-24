using FluentNHibernate.Mapping;
using ApplicationBusiness.Model;

namespace ApplicationBusiness.DalManager.Mapping
{
    public class SocieteMap: ClassMap<Societe>
    {
        public SocieteMap()
        {
            Table("Societe");
            LazyLoad();
            Id(x => x.Id).Column("id").GeneratedBy.Guid();
            Map(x => x.Nom).Column("nom");
            Map(x => x.Siret).Column("siret");
        }
    }
}