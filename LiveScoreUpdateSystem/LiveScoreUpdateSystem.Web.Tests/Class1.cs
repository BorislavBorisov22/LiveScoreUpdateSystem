using LiveScoreUpdateSystem.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreUpdateSystem.Web.Tests
{
    public class Class1
    {
        public void ST()
        {
            var controller = new ScoresController(null);
            controller.AvailableScores();
        }
    }
}
