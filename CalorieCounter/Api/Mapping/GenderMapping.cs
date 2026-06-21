using DomainGender = CalorieCounter.Domain.AggregatesModels.UserAggregate.Gender;
using ContractGender = Contracts.Common.Gender;

namespace CalorieCounter.Api.Mapping
{
    public static class GenderMapping
    {
        public static DomainGender MapToDomain(this ContractGender gender)
        {
            return gender switch
            {
                ContractGender.Male => DomainGender.Male,
                ContractGender.Female => DomainGender.Female,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }
}
 