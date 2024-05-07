using WebApplication2.Interface;
using WebApplication2.Models.Data;

namespace WebApplication2.Models.Mocks
{
    public class MockAllInsurancePackages : IAllInsurancePackages
    {
        private readonly IBank _bank=new MockBank();
        public IEnumerable<InsurancePackages> AllInsurancePackage { 
            get
            {
                return new List<InsurancePackages>
                {
                    new InsurancePackages { price=125, duration= new DateTime(2015, 7, 20), carType="Грузовик", description="Какой-то текст", Bank= _bank.AllBanks.First()}
                };
            }

        }

        public InsurancePackages GetInsurancePackage(int insurancePackageId)
        {
            throw new NotImplementedException();
        }
    }
}
