namespace SportMap.AL.Abstractions.UseCases
{
    public interface ICommandHandler<in TCommand, TResult>
        where TCommand : ICommand<TResult>
        where TResult : IDTO
    {
        Task<Result<TResult>> Handle(TCommand command, CancellationToken cancellationToken);
    }

    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task<Result> Handle(TCommand command, CancellationToken cancellationToken);
    }
}
