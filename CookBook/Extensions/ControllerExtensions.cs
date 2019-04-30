using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.API.Extensions
{
    public static class ControllerExtensions
    {
        public static string GetUserId(this Controller controller)
        {
            var claimId = controller.User.Claims.FirstOrDefault(c => c.Type == "id");
            return claimId != null ? claimId.Value : string.Empty;
        }
    }
}
