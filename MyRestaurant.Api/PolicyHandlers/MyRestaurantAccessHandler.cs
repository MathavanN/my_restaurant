using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyRestaurant.Api.PolicyHandlers
{
    public class MyRestaurantAccessHandler : AuthorizationHandler<MyRestaurantAccessRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MyRestaurantAccessRequirement requirement)
        {
            if (context.User.IsInRole(requirement.Role))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
