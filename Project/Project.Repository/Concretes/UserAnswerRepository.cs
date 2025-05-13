// using Project.Entities;
// using Project.Repository.Contracts;

// namespace Project.Repository.Concretes;

// public class UserAnswerRepository : RepositoryBase<UserAnswer>, IUserAnswerRepository
// {
//     public UserAnswerRepository(RepositoryContext context) : base(context)
//     {
//     }

//     public void CreateUserAnswer(UserAnswer userAnswer)
//     {
//         Create(userAnswer);
//     }

//     public void DeleteUserAnswer(UserAnswer userAnswer)
//     {
//         Delete(userAnswer);
//     }

//     public IQueryable<UserAnswer> GetAllAnswers(bool trackChanges)
//     {
//         return FindAll(trackChanges);
//     }

//     public IQueryable<UserAnswer> GetUserAnswerById(int id, bool trackChanges)
//     {
//         return FindByCondition(u => u.UserAnswerId == id,trackChanges);
//     }

//     public void UpdateUserAnswer(UserAnswer userAnswer)
//     {
//         Update(userAnswer);
//     }
// }