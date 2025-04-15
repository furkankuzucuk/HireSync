namespace Project.Repository;

public class UserRepository
{
    private RepositoryContext repositoryContext;

    public UserRepository(RepositoryContext repositoryContext)
    {
        this.repositoryContext = repositoryContext;
    }
}