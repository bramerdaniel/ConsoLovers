// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManager.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Model;

using System;
using System.Collections.Generic;
using System.Linq;

internal class UserManager : IUserManager
{
   #region Constants and Fields

   private List<Role> roles;

   private List<User> users;

   #endregion

   #region Constructors and Destructors

   public UserManager()
   {
      Initialize();
   }

   #endregion

   #region IUserManager Members

   public IEnumerable<Role> GetRoles()
   {
      return roles;
   }

   public IEnumerable<User> GetUsers(string userName, string password)
   {
      Authenticate(userName, password);
      return users;
   }

   public void AddRole(string roleName, string userName, string password)
   {
      if (string.IsNullOrWhiteSpace(roleName))
         throw new ArgumentNullException(nameof(roleName));

      var user = Authenticate(userName, password);
      var admin = GetRole("Admin");
      if (admin == null || !admin.Users.Contains(user))
         throw new InvalidOperationException("Permission denied");

      var role = GetRole(roleName);
      if (role != null)
         throw new InvalidOperationException($"Role {roleName} already exists");

      roles.Add(new Role { Name = roleName, Users = new List<User>() });
   }

   public void DeleteRole(string roleName, string userName, string password)
   {
      if (roleName == null)
         throw new ArgumentNullException(nameof(roleName));

      var user = Authenticate(userName, password);
      var admin = GetRole("Admin");
      if (admin == null || !admin.Users.Contains(user))
         throw new InvalidOperationException("Permission denied");

      var role = GetRole(roleName);
      if (role == null)
         throw new InvalidOperationException($"Role {roleName} could not be found");

      roles.Remove(role);
   }

   public void AddUser(User user, string userName, string password)
   {
      var authenticatedUser = Authenticate(userName, password);
      Authorize(authenticatedUser, "Admin");

      if (users.Any(x => x.Name == user.Name))
         throw new InvalidOperationException($"User {user.Name} already exists");

      users.Add(user);
   }

   public void DeleteUser(User user, string userName, string password)
   {
      var authenticatedUser = Authenticate(userName, password);
      Authorize(authenticatedUser, "Admin");
      users.Remove(user);
   }

   #endregion

   #region Public Methods and Operators

   public Role GetRole(string name)
   {
      return roles.FirstOrDefault(x => x.Name == name);
   }

   #endregion

   #region Methods

   private User Authenticate(string userName, string password)
   {
      var user = users.FirstOrDefault(x => x.Name == userName);
      if (user == null || !string.Equals(user.Password, password))
         throw new InvalidOperationException("Authentication failed");
      return user;
   }

   private void Authorize(User user, string roleName)
   {
      var role = GetRole(roleName);
      if (role != null && role.Users.Contains(user))
         return;

      throw new InvalidOperationException("Permission denied");
   }

   private void Initialize()
   {
      var admin = new User { Name = "Admin", Password = "Admin" };
      var robert = new User { Name = "Robert", Password = "Robert" };
      var sam = new User { Name = "Sam", Password = "Sam" };
      var miranda = new User { Name = "Miranda", Password = "Miranda" };
      var paul = new User { Name = "Paul", Password = "Paul" };

      users = new List<User>
      {
         admin,
         robert,
         sam,
         miranda,
         paul
      };

      roles = new List<Role>
      {
         new() { Name = "Admin", Users = new List<User> { admin, robert } },
         new() { Name = "User", Users = new List<User> { sam } },
         new() { Name = "Operator", Users = new List<User> { miranda } },
         new() { Name = "Guest", Users = new List<User> { paul } },
      };
   }

   #endregion
}