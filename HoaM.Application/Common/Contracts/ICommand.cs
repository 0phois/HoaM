namespace HoaM.Application.Common
{
    public interface IBaseCommand { }

    public interface ICommand : IBaseCommand { }

    public interface ICommand<TResponse> : IBaseCommand { }

}