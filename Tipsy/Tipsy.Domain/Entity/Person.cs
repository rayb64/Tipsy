// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>Person.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/5/2021 4:15:24 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPerson : IEntityBase
    {
        string Name { get; set; }
    }

    [Serializable]
    public class Person : EntityBase
    {
        #region Creation

        internal Person(
            long id)
            : base(id)
        {
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
