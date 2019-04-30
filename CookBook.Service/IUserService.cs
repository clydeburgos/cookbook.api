using CookBook.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Service
{
    public interface IUserService
    {
        string GenerateJwtForUser(UserViewModel user);
    }
}
