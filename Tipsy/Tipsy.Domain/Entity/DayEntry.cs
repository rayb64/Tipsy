// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>DayEntry.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 7:58:41 AM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Entity
{
    using System;

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
            Time = DateTime.Now;
            BankDescription = string.Empty;
        }

        #endregion Creation

        #region IDayEntry Members

        public DayAction Action { get; set; }
        public decimal Bank { get; set; }
        public string BankDescription { get; set; }
        public decimal ETips { get; set; }
        public IPerson Person { get; set; }
        public DateTime Time { get; set; }

        #endregion IDayEntry Members

        #region Object Overrides

        public override string ToString()
        {
            return string.Format("{0} {1} {2:T}", this.Person.Name, this.Action, this.Time);
        }

        #endregion Object Overrides
    }
}
