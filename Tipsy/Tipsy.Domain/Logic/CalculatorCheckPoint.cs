// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>CalculatorCheckPoint.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 2:09:49 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Logic
{
    using Com.Gmail.Birklid.Ray.Tipsy;
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using Com.Gmail.Birklid.Ray.Tipsy.Entity;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Interface for a calculation check point.
    /// </summary>
    public interface ICalculatorCheckPoint
    {
        string Description { get; set; }
        decimal ETips { get; }
        IEnumerable<IPerson> People { get; }
        decimal Take { get; set; }
        DateTime Time { get; }
    }

    public class CalculatorCheckPoint : ICalculatorCheckPoint
    {
        #region Private Fields

        private List<IPerson> _people = new List<IPerson>();

        #endregion Private Fields

        #region Creation

        public CalculatorCheckPoint(
            IDayEntry dayEntry,
            IEnumerable<ICalculatorCheckPoint> checkPoints)
        {
            LogicTraceSource.Instance.Created(this);
            this.ETips = dayEntry.ETips;
            this.Time = dayEntry.Time;
            this.Description = "No calculations to perform";
            var previous = GetPreviousCheckPoint(checkPoints);
            if (previous != null)
            {
                _people.AddRange(previous.People);
            }
            if (dayEntry.Action == DayAction.ClockOff)
            {
                _people.Remove(dayEntry.Person);
            }
            else if (dayEntry.Action == DayAction.ClockOn)
            {
                _people.Add(dayEntry.Person);
            }
        }

        #endregion Creation

        #region ICheckPoint Members

        public string Description { get; set; }
        public decimal ETips { get; set; }
        public IEnumerable<IPerson> People => _people;
        public decimal Take { get; set; }
        public DateTime Time { get; set; }

        #endregion ICheckPoint Members

        #region Object Overrides

        public override string ToString()
        {
            var p = _people.Count == 0 ? "nobody is on" : string.Join(", ", People);
            return string.Format(CultureInfo.InvariantCulture, "{0:T}, ({1}), {2:0.00}, {3:0.00}, {4}", Time, p, ETips, Take, Description);
        }

        #endregion Object Overrides

        private ICalculatorCheckPoint GetPreviousCheckPoint(IEnumerable<ICalculatorCheckPoint> checkPoints) => checkPoints.LastOrDefault();
    }
}
