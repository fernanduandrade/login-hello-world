namespace AwesomeApp.API.Infra.Data;

public interface IUnitOfWork
{
    Task Commit(); 
}