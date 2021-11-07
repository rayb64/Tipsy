// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>Entities.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 7:21:20 AM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEntities
    {
        IDayCollection Days { get; }
        IPersonCollection People { get; }
    }

    [Serializable]
    public class Entities : IEntities
    {
        #region Private Fields

        private readonly DayCollection _days;
        private readonly IdFactory _idFactory = new IdFactory();
        private readonly PersonCollection _people;

        #endregion Private Fields

        #region Creation

        public Entities()
            : base()
        {
            _days = new DayCollection(_idFactory);
            _people = new PersonCollection(_idFactory);
        }

        #endregion Creation

        #region IEntities Members

        public IDayCollection Days => _days;
        public IPersonCollection People => _people;

        #endregion IEntities Members
    }
}
