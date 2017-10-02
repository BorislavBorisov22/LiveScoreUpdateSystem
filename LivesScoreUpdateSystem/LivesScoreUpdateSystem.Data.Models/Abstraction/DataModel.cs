using LivesScoreUpdateSystem.Data.Models.Contracts;
using System;

namespace LivesScoreUpdateSystem.Data.Models.Abstraction
{
    public class DataModel : IDeletable, IAuditable
    {
        public DataModel()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
