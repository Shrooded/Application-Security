using Microsoft.AspNetCore.Mvc;

namespace ApplicationSecurity.Controllers
{
    public static class AuditConfiguration
    {
        // Enables audit log with a global Action Filter
        public static void AddAudit(MvcOptions mvcOptions)
        {
            //mvcOptions.AddAuditFilter(config => config
            //    .LogAllActions()
            //    .WithEventType("{verb} {controller}.{action}")
            //    .IncludeHeaders()
            //    .IncludeRequestBody()
            //    .IncludeResponseHeaders()
            //);
        }

        // Configures what and how is logged or is not logged
        public static void ConfigureAudit(IServiceCollection serviceCollection)
        {
            // This is explained below
        }
    }
}
