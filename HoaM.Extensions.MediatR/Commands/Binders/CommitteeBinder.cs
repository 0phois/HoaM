using HoaM.Application;
using HoaM.Domain;
using MediatR.Pipeline;

namespace HoaM.Extensions.MediatR
{
    internal class CommitteeBinder<TRequest>(ICommitteeRepository repository) : IRequestPreProcessor<TRequest> where TRequest : ICommitteeBinder
    {
        private readonly ICommitteeRepository _repository = repository;

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            request.Entity = await _repository.GetByIdAsync(request.Id);
        }
    }
}
