// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>Persistence.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/8/2021 7:24:19 AM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.IO
{
    using Com.Gmail.Birklid.Ray.Tipsy.Diagnostics;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Threading.Tasks;

    // I was working in here...  need tests for this, but also need a general 'Serialization' set of tests.

    public interface IPersistence
    {
        T LoadOrCreate<T>(Uri file, Func<T> factory);
        void Save<T>(Uri file, T item);
    }

    public class Persistence : IPersistence
    {
        #region Private Fields

        private static readonly IPersistence _defaultInstance = new Persistence(false);
        private readonly BinaryFormatter _formatter = new BinaryFormatter();
        private readonly bool _zip;
        private static readonly IPersistence _zipInstance = new Persistence(true);

        #endregion Private Fields

        #region Creation

        public Persistence(
            bool zip)
            : base()
        {
            Log.Created(this);
            Log.Verbose($"Persistence created with zip={zip}.");
            _zip = zip;
        }

        #endregion Creation

        public static IPersistence Default => _defaultInstance;
        public static IPersistence Zip => _zipInstance;

        #region IPersistence Members

        public T LoadOrCreate<T>(
            Uri file,
            Func<T> factory)
        {
            Log.MethodCall(this, "LoadOrCreate");
            CheckArg.IsNotDefault(file, nameof(file));
            CheckArg.IsNotDefault(factory, nameof(factory));
            var result = TryLoad<T>(file);
            if (EqualityComparer<T>.Default.Equals(result, default(T)))
            {
                result = factory();
                Log.Information("Created new {0}.", result.GetType());
            }
            return result;
        }

        public void Save<T>(
            Uri file,
            T item)
        {
            Log.MethodCall(this, "Save");
            CheckArg.IsNotDefault(file, nameof(file));
            TryWrite(file, item);
        }

        #endregion IPersistence Members

        #region Private Members

        private static Logger Log => IOTraceSource.Instance;

        private void EnsureDirectory(
            Uri file)
        {
            var path = Path.GetDirectoryName(file.LocalPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private Stream GetReadStream(
            Uri file)
        {
            return this._zip ? OpenReadZip(file) : OpenRead(file);
        }

        private Stream GetWriteStream(
            Uri file)
        {
            return this._zip ? OpenWriteZip(file) : OpenWrite(file);
        }

        private T Load<T>(
            Uri file)
        {
            T result = default(T);
            if(File.Exists(file.LocalPath))
            {
                using (var stream = GetReadStream(file))
                {
                    result = (T)this._formatter.Deserialize(stream);
                }
                Log.Information("Loaded file '{0}'.", file);
            }
            return result;
        }

        private Stream OpenRead(
            Uri file)
        {
            return new FileStream(file.LocalPath, FileMode.Open);
        }

        private Stream OpenReadZip(
            Uri file)
        {
            return new GZipStream(OpenRead(file), CompressionMode.Decompress);
        }

        private Stream OpenWrite(
            Uri file)
        {
            EnsureDirectory(file);
            return new FileStream(file.LocalPath, FileMode.Create);
        }

        private Stream OpenWriteZip(
            Uri file)
        {
            return new GZipStream(OpenWrite(file), CompressionMode.Compress);
        }

        private T TryLoad<T>(
            Uri file)
        {
            var result = default(T);
            try
            {
                result = Load<T>(file);
            }
            catch (SerializationException err)
            {
                Log.Error("Error loading file '{0}'.{1}{2}", file, Environment.NewLine, err);
            }
            catch (InvalidDataException err)
            {
                Log.Error("Error loading file '{0}'.{1}{2}", file, Environment.NewLine, err);
            }
            return result;
        }

        private void TryWrite<T>(
            Uri file,
            T item)
        {
            Write(file, item);
        }

        private void Write<T>(
            Uri file,
            T item)
        {
            using (var stream = GetWriteStream(file))
            {
                this._formatter.Serialize(stream, item);
            }
        }

        #endregion Private Members
    }
}
