using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using NetCore.Core.Entities;
using Newtonsoft.Json;

namespace NetCore.API.Filters
{
    public class AuditAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Stores the Request in an Accessible object
            var request = filterContext.HttpContext.Request;

            //Generate the appropriate key based on the user's Authentication Cookie
            //This is overkill as you should be able to use the Authorization Key from
            //Forms Authentication to handle this. 
            var sessionIdentifier = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(filterContext.HttpContext.Session.Id)).Select(s => s.ToString("x2")));

            //Generate an audit
            var audit = new Audit()
            {
                SessionID = sessionIdentifier,
                AuditID = Guid.NewGuid(),
                IPAddress = filterContext.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
                URLAccessed = request.Path,
                TimeAccessed = DateTime.UtcNow,
                UserName = filterContext.HttpContext.User.Identity.IsAuthenticated ? filterContext.HttpContext.User.Identity.Name : "Anonymous",
                Data = JsonConvert.SerializeObject(request)
            };

            //Stores the Audit in the Database
            //AuditingContext context = new AuditingContext();
            //context.AuditRecords.Add(audit);
            //context.SaveChanges();

            base.OnActionExecuting(filterContext);
        }
    }
}
