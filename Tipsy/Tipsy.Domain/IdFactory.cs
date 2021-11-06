// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>IdFactory.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/6/2021 7:27:21 AM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IIdFactory
    {
        long Next(Type type);
        int NextInt(Type type);
    }

    [Serializable]
    public class IdFactory : IIdFactory
    {
        #region Private Fields

        private readonly Dictionary<string, long> _lookup = new Dictionary<string, long>();

        #endregion Private Fields

        #region Creation

        public IdFactory()
            : base()
        {
        }

        #endregion Creation

        #region IIdFactory Members

        public long Next(
            Type type)
        {
            long result = 1;
            var key = type.FullName;
            lock (_lookup)
            {
                if (_lookup.ContainsKey(key))
                {
                    result = ++_lookup[key];
                }
                else
                {
                    _lookup[key] = result;
                }
            }
            return result;
        }

        public int NextInt(
            Type type)
        {
            return (int)Next(type);
        }

        #endregion IIdFactory Members

        #region Object Overrides

        public override bool Equals(
            object obj)
        {
            return obj is IdFactory factory &&
                _lookup.SequenceEqual(factory._lookup);
        }

        public override int GetHashCode()
        {
            return 1222360384 + EqualityComparer<Dictionary<string, long>>.Default.GetHashCode(_lookup);
        }

        public override string ToString()
        {
            return _lookup.Count + " types registered.";
        }

        #endregion Object Overrides
    }

    public static class IIdFactoryExtensions
    {
        public static long Next<T>(
            this IIdFactory target,
            T obj)
        {
            CheckArg.IsNotDefault(obj, nameof(obj));
            return target.Next(obj.GetType());
        }
        
        public static int NextInt<T>(
            this IIdFactory target,
            T obj)
        {
            CheckArg.IsNotDefault(obj, nameof(obj));
            return target.NextInt(obj.GetType());
        }
    }
}
