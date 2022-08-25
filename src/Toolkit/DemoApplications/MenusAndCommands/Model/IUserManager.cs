// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserManager.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Model;

using System.Collections.Generic;

public interface IUserManager
{
   IEnumerable<Role> GetRoles();

   IEnumerable<User> GetUsers(string userName, string password);

   void AddRole(string roleName, string userName, string password);

   void DeleteRole(string roleName, string userName, string password);

   void AddUser(User user, string userName, string password);
}