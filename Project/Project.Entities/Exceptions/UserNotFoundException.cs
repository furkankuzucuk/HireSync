
namespace Project.Entities.Exceptions;

public sealed class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(int id) : base($"User with id : {id} could not found")
    {

    }
}