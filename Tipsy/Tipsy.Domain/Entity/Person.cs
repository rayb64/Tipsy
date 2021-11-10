// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>Person.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/5/2021 4:15:24 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Entity
{
    using System;

    public interface IPerson : IEntityBase
    {
        string Name { get; set; }
    }

    [Serializable]
    public class Person : EntityBase, IPerson
    {
        #region Creation

        public Person(
            long id)
            : base(id)
        {
            Name = string.Empty;
        }

        #endregion Creation

        #region IEntityBase Members

        public string Name { get; set; }

        #endregion IEntityBase Members

        #region Object Overrides

        public override string ToString()
        {
            return this.Name;
        }

        #endregion Object Overrides
    }
}
