using HoaM.Application.Features;
using HoaM.Domain.Features;
using MediatR.Pipeline;

namespace HoaM.Extensions.MediatR
{
    internal class PropertyBinder<TRequest>(IParcelRepository repository) : IRequestPreProcessor<TRequest> where TRequest : IParcelBinder
    {
        private readonly IParcelRepository _repository = repository;

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            request.Entity = await _repository.GetByIdAsync(request.Id);
        }
    }
}
