using ApplicationBusiness;
using ApplicationBusiness.Exceptions;
using ApplicationBusiness.Model;
using Xunit;
using XUnitTestApplicationBusiness.IDalMock.Fake;

namespace XUnitTestApplicationBusiness
{
    public class SocieteBusiness_XUnitTest
    {
        private readonly SocieteBusiness _serv;


        public SocieteBusiness_XUnitTest()
        {
            var uow = new UnitOfWorkFake();
            _serv = new SocieteBusiness(uow);
        }



        [Fact]
        public void AddWithNomNull()
        {
            var soc = new Societe {
                                Nom = null,
                                Siret = null
                            };
            var exception = Assert.Throws<BusinessException>(()=> _serv.Add(soc));
            Assert.Equal(BusinessExceptionCode.NO_VALIDE_NOM, exception.CodeErreur);
        }

        [Fact]
        public void AddWithSiretNull()
        {
            var soc = new Societe
            {
                Nom = "EARL Lefevre HM",
                Siret = null
            };
            var exception = Assert.Throws<BusinessException>(() => _serv.Add(soc));
            Assert.Equal(BusinessExceptionCode.NO_VALIDE_SIRET, exception.CodeErreur);
        }

        [Fact]
        public void AddWithBadLengthSiret()
        {
            var soc = new Societe
            {
                Nom = "EARL Lefevre HM",
                Siret = "12132131"
            };
            var exception = Assert.Throws<BusinessException>(() => _serv.Add(soc));
            Assert.Equal(BusinessExceptionCode.NO_VALIDE_SIRET, exception.CodeErreur);
        }

        [Fact]
        public void AddWithBadCharacterSiret()
        {
            var soc = new Societe
            {
                Nom = "EARL Lefevre HM",
                Siret = "1234567890abcd"
            };
            var exception = Assert.Throws<BusinessException>(() => _serv.Add(soc));
            Assert.Equal(BusinessExceptionCode.NO_VALIDE_SIRET, exception.CodeErreur);
        }

        [Fact]
        public void AddWithBadSiret()
        {
            var soc = new Societe
            {
                Nom = "EARL Lefevre HM",
                Siret = "12345678901234"
            };
            var exception = Assert.Throws<BusinessException>(() => _serv.Add(soc));
            Assert.Equal(BusinessExceptionCode.NO_VALIDE_SIRET, exception.CodeErreur);
        }
    }
}
