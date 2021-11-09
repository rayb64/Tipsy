// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>PersonDataModel.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/8/2021 3:28:17 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.DataModels
{
    using Com.Gmail.Birklid.Ray.Tipsy.Entity;
    using Prism.Mvvm;
    using System;

    public class EntityDataModelBase<T> : BindableBase, IEntityBase where T : IEntityBase
    {
        #region Creation

        protected EntityDataModelBase(
            T entity)
        {
            CheckArg.IsNotDefault(entity, nameof(entity));
            Entity = entity;
        }

        #endregion Creation

        #region IEntityBase Members

        public DateTime Created => Entity.Created;

        public long Id => Entity.Id;

        #endregion IEntityBase Members

        #region Object Overrides

        public override string ToString()
        {
            return Entity.ToString();
        }

        #endregion Object Overrides

        internal T Entity { get; private set; }
    }
}
