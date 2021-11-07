// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>Calculator.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/7/2021 2:09:32 PM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Logic
{
    using Com.Gmail.Birklid.Ray.Tipsy;
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using Com.Gmail.Birklid.Ray.Tipsy.Entity;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Interface for making the tip distribution calculations.
    /// </summary>
    public interface ICalculator
    {
        /// <summary>
        /// Calculates how the tips should be divided between people.
        /// </summary>
        /// <param name="day">The day for which the calculations are to be made.</param>
        /// <returns>A sequence of check points which hold the calculations.</returns>
        IEnumerable<ICalculatorCheckPoint> Run(IDay day);
    }

    /// <summary>
    /// Business logic for calculating the division of tips between people.
    /// </summary>
    public class Calculator : ICalculator
    {
        #region Private Fields

        private readonly List<ICalculatorCheckPoint> _checkPoints = new List<ICalculatorCheckPoint>();

        #endregion Private Fields

        #region Creation

        /// <summary>
        /// Constructor
        /// </summary>
        public Calculator()
        {
            LogicTraceSource.Instance.Created(this);
        }

        #endregion Creation

        #region ICalculator Members

        /// <summary>
        /// Calculates how the tips should be divided between people.
        /// </summary>
        /// <param name="day">The day for which the calculations are to be made.</param>
        /// <returns>A sequence of check points which hold the calculations.</returns>
        public IEnumerable<ICalculatorCheckPoint> Run(
            IDay day)
        {
            LogicTraceSource.Instance.MethodCall(this);
            CheckArg.IsNotDefault(day, nameof(day));
            _checkPoints.Clear();
            BuildCheckPoints(day);
            for (var i = 1; i < this.CheckPoints.Count; i++)
            {
                var current = CheckPoints[i];
                var previous = CheckPoints[i - 1];
                if (current.ETips != 0 && current.ETips != previous.ETips)
                {
                    var amount = current.ETips - previous.ETips;
                    if (previous.People.Count() > 0)
                    {
                        var share = amount / previous.People.Count();
                        previous.Take = share;
                        previous.Description = string.Format(CultureInfo.InvariantCulture, "({0:0.00} - {1:0.00})/{2} = {3:0.00}", current.ETips, previous.ETips, previous.People.Count(), share);
                    }
                    else
                    {
                        previous.Description = "Error - divide by zero";
                    }
                }
                LogicTraceSource.Instance.Information("Checkpoint: {0}", previous);
            }
            return _checkPoints.ToArray();  // return a copy
        }

        #endregion ICalculator Members

        #region Private Members

        private List<ICalculatorCheckPoint> CheckPoints => _checkPoints;

        /// <summary>
        /// Builds a list of check points, one for each entry in the day.
        /// </summary>
        /// <param name="day">The day for which the check points are to be made.</param>
        private void BuildCheckPoints(
            IDay day)
        {
            foreach (var entry in day.Entries)
            {
                this.CheckPoints.Add(new CalculatorCheckPoint(entry, CheckPoints));
            }
        }

        #endregion Private Members
    }
}
