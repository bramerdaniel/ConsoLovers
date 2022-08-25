// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Model;

using System.Collections.Generic;
using System.Linq;

public class User
{
   #region Public Properties

   public string Name { get; set; }

   public string Password { get; set; }

   #endregion
}

public class Role
{
   #region Public Properties

   public string Name { get; init; }

   public List<User> Users { get; set; }

   #endregion

   #region Public Methods and Operators

   public override bool Equals(object obj)
   {
      if (ReferenceEquals(null, obj))
         return false;
      if (ReferenceEquals(this, obj))
         return true;
      if (obj.GetType() != this.GetType())
         return false;
      return Equals((Role)obj);
   }

   public override int GetHashCode()
   {
      return (Name != null ? Name.GetHashCode() : 0);
   }

   #endregion

   #region Methods

   protected bool Equals(Role other)
   {
      return Name == other.Name;
   }

   public override string ToString()
   {
      return $"{Name} [{string.Join(", ", Users.Select(x => x.Name))}]";
   }

   #endregion
}