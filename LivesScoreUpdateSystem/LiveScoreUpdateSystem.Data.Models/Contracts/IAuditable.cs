using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreUpdateSystem.Data.Models.Contracts
{
    public interface IAuditable
    {
        DateTime? CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
