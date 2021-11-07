// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>DayEntry.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 7:58:41 AM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDayEntry : IEntityBase
    {
        DayAction Action { get; set; }
        decimal Bank { get; set; }
        string BankDescription { get; set; }
        decimal ETips { get; set; }
        IPerson Person { get; set; }
        DateTime Time { get; set; }
    }

    [Serializable]
    public class DayEntry : EntityBase, IDayEntry
    {
        #region Creation

        public DayEntry(
            long id)
            : base(id)
        {
        }

        #endregion Creation

        public DayAction Action { get; set; }
        public decimal Bank { get; set; }
        public string BankDescription { get; set; }
        public decimal ETips { get; set; }
        public IPerson Person { get; set; }
        public DateTime Time { get; set; }

        #region Object Overrides

        public override string ToString()
        {
            return string.Format("{0} {1} {2:T}", this.Person.Name, this.Action, this.Time);
        }

        #endregion Object Overrides
    }
}
