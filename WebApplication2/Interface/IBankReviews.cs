using WebApplication2.Models.Data;

namespace WebApplication2.Interface
{
    public interface IBankReviews
    {
        IEnumerable<Reviews>AllReviews { get; }
    }
}
