
namespace Project.Entities.Exceptions;

public sealed class EntityNotFoundException<T> : NotFoundException
{
    public EntityNotFoundException(object key)
        : base($"{typeof(T).Name} with id: {key} could not be found.")
    {
    }
}