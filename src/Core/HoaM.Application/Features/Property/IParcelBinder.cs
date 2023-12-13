using HoaM.Application.Common;
using HoaM.Domain;

namespace HoaM.Application
{
    public interface IParcelBinder : ICommandBinder<Parcel, ParcelId>
    {
    }
}
