using MediatR;
using Planetary.Application.Interfaces;

namespace Planetary.Application.Commands;

public class DeleteCriteriaByIdCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

public class DeleteCriteriaByIdCommandHandler : IRequestHandler<DeleteCriteriaByIdCommand, bool>
{
    private readonly ICriteriaRepository _repository;

    public DeleteCriteriaByIdCommandHandler(ICriteriaRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteCriteriaByIdCommand request, CancellationToken cancellationToken)
    {
        var criteria = await _repository.GetByIdAsync(request.Id);
        if (criteria == null)
        {
            return false;
        }

        await _repository.DeleteAsync(request.Id);
        return true;
    }
}