using HoaM.Application;
using HoaM.Application.Common;
using HoaM.Domain;
using HoaM.Domain.Common;
using MediatR.Pipeline;

namespace HoaM.Extensions.MediatR
{
    internal class AssociationMemberBinder<TRequest>(IAssociationMemberRepository repository) : IRequestPreProcessor<MediatrRequestAdapter<TRequest, IResult>>
        where TRequest : ICommand<IResult>, IMemberBinder
    {
        private readonly IAssociationMemberRepository _repository = repository;

        public async Task Process(MediatrRequestAdapter<TRequest, IResult> request, CancellationToken cancellationToken)
        {
            var command = request.Value as IMemberBinder;

            command.Entity = await _repository.GetByIdAsync(command.Id);
        }
    }
}
