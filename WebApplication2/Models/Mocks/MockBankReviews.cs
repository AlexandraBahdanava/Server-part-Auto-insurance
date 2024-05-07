using WebApplication2.Interface;
using WebApplication2.Models.Data;

namespace WebApplication2.Models.Mocks
{
    public class MockBankReviews : IBankReviews
    {
        public IEnumerable<Reviews> AllReviews
        {
            get
            {
                return new List<Reviews>
                {
                    new Reviews { text="все идет по плану",grade="5" }
                };
            }
        }
    }
}
