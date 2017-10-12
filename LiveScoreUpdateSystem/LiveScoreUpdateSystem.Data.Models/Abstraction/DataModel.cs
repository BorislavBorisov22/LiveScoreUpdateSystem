using System;
using LiveScoreUpdateSystem.Data.Models.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiveScoreUpdateSystem.Data.Models.Abstraction
{
    public class DataModel : IDeletable, IAuditable, IDataModel
    {
        public DataModel()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
