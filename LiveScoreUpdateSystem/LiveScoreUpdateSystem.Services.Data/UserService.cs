using LiveScoreUpdateSystem.Data.Models;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Services.Data.Abstraction;

namespace LiveScoreUpdateSystem.Services.Data
{
    public class UserService : DataService<User>
    {
        public UserService(IEfRepository<User> dataSet) 
            : base(dataSet)
        {
        }
    }
}
