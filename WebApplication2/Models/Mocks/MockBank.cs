using WebApplication2.Interface;
using WebApplication2.Models.Data;

namespace WebApplication2.Models.Mocks
{
    public class MockBank : IBank
    {
        public IEnumerable<Bank> AllBanks
        {
            get
            {
                return new List<Bank>
                {
                    new Bank { Name="Альфа-банк", Phone="+375-25-123-45-34", schedule= "круглосуточно", contactPerson="Никифоров Алексей Степанович"}
                };
            }
        }
    }
}
