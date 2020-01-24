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
        public void Test01_AddWithNomNull()
        {
            var soc = new Societe {
                                Nom = null,
                                Siret = null
                            };
            var exception = Assert.Throws<BusinessException>(()=> _serv.Add(soc));
            Assert.Equal(BusinessExceptionCode.NO_VALIDE_NOM, exception.CodeErreur);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("12122131")]
        [InlineData("1234567890abcd")]
        [InlineData("12345678901234")]
        public void Test02_AddWithBadSiret(string siret)
        {
            var soc = new Societe
            {
                Nom = "EARL Lefevre HM",
                Siret = siret
            };
            var exception = Assert.Throws<BusinessException>(() => _serv.Add(soc));
            Assert.Equal(BusinessExceptionCode.NO_VALIDE_SIRET, exception.CodeErreur);
        }
    }
}
