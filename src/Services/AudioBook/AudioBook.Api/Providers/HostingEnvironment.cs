using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBook.API.Providers
{
    public static class HostingEnvironmentExtensions
    {
        public const string TestEnvironment = "Test";

        public static bool IsTest(this IHostingEnvironment hostingEnvironment)
        {
            return hostingEnvironment.IsEnvironment(TestEnvironment);
        }
    }
}
