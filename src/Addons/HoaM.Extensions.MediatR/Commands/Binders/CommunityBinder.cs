using HoaM.Application;
using HoaM.Application.Common;
using HoaM.Domain;
using HoaM.Domain.Common;
using MediatR.Pipeline;

namespace HoaM.Extensions.MediatR
{
    internal class CommunityBinder<TRequest>(ICommunityRepository repository) : IRequestPreProcessor<MediatrRequestAdapter<TRequest, IResult>>
        where TRequest : ICommand<IResult>, ICommunityBinder
    {
        private readonly ICommunityRepository _repository = repository;

        public async Task Process(MediatrRequestAdapter<TRequest, IResult> request, CancellationToken cancellationToken)
        {
            var command = request.Value as ICommunityBinder;

            command.Entity = await _repository.GetByIdAsync(command.Id);
        }
    }
}
