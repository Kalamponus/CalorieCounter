using ContractActivity = Contracts.Common.PhysicalActivityLevel;
using DomainActivity = CalorieCounter.Domain.AggregatesModels.UserAggregate.PhysicalActivityLevel;

namespace CalorieCounter.Api.Mapping
{
    public static class PhysicalActivityMapping
    {
        public static DomainActivity MapToDomain(this ContractActivity activity)
        {
            return activity switch
            {
                ContractActivity.None => DomainActivity.None,
                ContractActivity.Light => DomainActivity.Light,
                ContractActivity.Moderate => DomainActivity.Moderate,
                ContractActivity.Intense => DomainActivity.Intense,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }
}
