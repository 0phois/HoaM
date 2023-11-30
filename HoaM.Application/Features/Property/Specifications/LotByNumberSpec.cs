using HoaM.Domain;
using HoaM.Domain.Features;
using TanvirArjel.EFCore.GenericRepository;

namespace HoaM.Application.Features
{
    internal sealed class LotByNumberSpec : Specification<Lot>
    {
        public LotByNumberSpec(LotNumber number)
        {
            Conditions.Add(lot => lot.Number == number);
        }
    }
}
