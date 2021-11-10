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

    // TODO: BUG: As written, there is a rounding error that can happen in here.
    //            I have a test in place that catches this error.
    //            The problem arises when (dollar amount)/(number of people) results in an irrational or repeating decimal number like 1/3
    //
    //            The fix I am leaning toward involves removing 'People' and 'Take' to combine the two into a new class.
    //            Once that is done, the creation of a checkpoint can detect the rounding error and choose victims/winners for losing/gaining
    //            a penny such that the sum of all 'Take' corrects the rounding error.

    /// <summary>
    /// Interface for a calculation check point.
    /// </summary>
    public interface ICalculatorCheckPoint
    {
        /// <summary>
        /// Describes how the money is divided
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// The current total amount of ETips 
        /// </summary>
        decimal ETips { get; }
        /// <summary>
        /// The people among whom the money is to be divided.
        /// </summary>
        IEnumerable<IPerson> People { get; }
        /// <summary>
        /// The share each person receives
        /// </summary>
        decimal Take { get; set; }
        /// <summary>
        /// The time of this checkpoint
        /// </summary>
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
