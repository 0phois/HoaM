using HoaM.Application.Features;
using HoaM.Domain.Features;
using MediatR.Pipeline;

namespace HoaM.Extensions.MediatR
{
    internal class AssociationMemberBinder<TRequest>(IAssociationMemberRepository repository) : IRequestPreProcessor<TRequest> where TRequest : IMemberBinder
    {
        private readonly IAssociationMemberRepository _repository = repository;

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            request.Entity = await _repository.GetByIdAsync(request.Id);
        }
    }
}
