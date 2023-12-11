using HoaM.Application;
using HoaM.Application.Common;
using HoaM.Domain;
using MediatR.Pipeline;

namespace HoaM.Extensions.MediatR
{
    internal class CommunityBinder<TRequest>(ICommunityRepository repository) : IRequestPreProcessor<MediatrCommand<TRequest>> where TRequest : ICommand, ICommunityBinder
    {
        private readonly ICommunityRepository _repository = repository;

        public async Task Process(MediatrCommand<TRequest> request, CancellationToken cancellationToken)
        {
            var command = request.Command as ICommunityBinder;

            command.Entity = await _repository.GetByIdAsync(command.Id);
        }
    }
}
