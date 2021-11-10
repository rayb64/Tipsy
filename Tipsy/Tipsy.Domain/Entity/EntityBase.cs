// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>CoreEntity.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/5/2021 4:12:13 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Entity
{
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using System;

    public interface IEntityBase
    {
        DateTime Created { get; }
        long Id { get; }
    }

    [Serializable]
    public class EntityBase : IEntityBase
    {
        #region Creation

        protected EntityBase(
            long id)
            : base()
        {
            EntityTraceSource.Instance.Created(this);
            Created = DateTime.Now;
            Id = id;
        }

        #endregion Creation

        #region IEntityBase Members

        public DateTime Created { get; private set; }
        public long Id { get; private set; }

        #endregion IEntityBase Members
    }
}
