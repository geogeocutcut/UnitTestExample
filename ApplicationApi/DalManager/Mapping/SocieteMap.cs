using FluentNHibernate.Mapping;
using ApplicationApi.Model;


namespace ApplicationApi.DalManager.Mapping
{
    public class SocieteMap: ClassMap<Societe>
    {
        public SocieteMap()
        {
            Table("Societe");
            LazyLoad();
            Id(x => x.Id).Column("id");
            Map(x => x.Nom).Column("nom");
            Map(x => x.Siret).Column("siret");
        }
    }
}