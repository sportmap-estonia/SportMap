namespace SportMap.AL.Abstractions.UseCases
{
    public interface ICommand;
    public interface ICommand<TResult> : ICommand where TResult : IDTO;
}
