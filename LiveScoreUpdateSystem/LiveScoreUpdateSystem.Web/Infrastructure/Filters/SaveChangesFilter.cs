using LiveScoreUpdateSystem.Data.SaveContext.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiveScoreUpdateSystem.Web.Infrastructure.Filters
{
    public class SaveChangesFilter : IActionFilter
    {
        private readonly ISaveContext saveContext;

        public SaveChangesFilter(ISaveContext unitOfWork)
        {
            this.saveContext = unitOfWork;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.saveContext.SaveChanges();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Just to satisfy the interface. Cannot decouple from it.
        }
    }
}