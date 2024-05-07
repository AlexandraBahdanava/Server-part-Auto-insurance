using WebApplication2.Models.Data;

namespace WebApplication2.Interface
{
    public interface IBank
    {
        IEnumerable<Bank> AllBanks { get; }
    }
}
