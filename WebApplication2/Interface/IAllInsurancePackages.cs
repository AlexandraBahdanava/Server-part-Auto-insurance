using WebApplication2.Models.Data;

namespace WebApplication2.Interface
{
    public interface IAllInsurancePackages
    {
        IEnumerable <InsurancePackages> AllInsurancePackage { get; }
       InsurancePackages GetInsurancePackage (int insurancePackageId);

    }
}
