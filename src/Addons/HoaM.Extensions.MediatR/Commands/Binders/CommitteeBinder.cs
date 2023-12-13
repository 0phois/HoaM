using HoaM.Application;
using HoaM.Application.Common;
using HoaM.Domain;
using HoaM.Domain.Common;
using MediatR.Pipeline;

namespace HoaM.Extensions.MediatR
{
    internal class CommitteeBinder<TRequest>(ICommitteeRepository repository) : IRequestPreProcessor<MediatrRequestAdapter<TRequest, IResult>>
        where TRequest : ICommand<IResult>, ICommitteeBinder
    {
        private readonly ICommitteeRepository _repository = repository;

        public async Task Process(MediatrRequestAdapter<TRequest, IResult> request, CancellationToken cancellationToken)
        {
            var command = request.Value as ICommitteeBinder;

            command.Entity = await _repository.GetByIdAsync(command.Id);
        }
    }
}
