using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace NSubstituteTestCase
{
    public class CompositeKeyFail : IEquatable<CompositeKeyFail>
    {
        public string KeyPart1 { get; }
        public short KeyPart2 { get; }

        public CompositeKeyFail(string keyPart1, short keyPart2)
        {
            KeyPart1 = keyPart1;
            KeyPart2 = keyPart2;
        }

        // required by NSubstitute
        //public override bool Equals(object obj)
        //{
        //    var other = obj as CompositeKeyFail;
        //    if (other == null)
        //    {
        //        return false;
        //    }
        //    return string.Equals(KeyPart1, other.KeyPart1) && KeyPart2 == other.KeyPart2;
        //}

        public bool Equals(CompositeKeyFail other)
        {
            return string.Equals(KeyPart1, other.KeyPart1) && KeyPart2 == other.KeyPart2;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((KeyPart1 != null ? KeyPart1.GetHashCode() : 0) * 397) ^ KeyPart2.GetHashCode();
            }
        }
    }

    public interface ICompositeKeyFailCache
    {
        void Add(CompositeKeyFail key, string value);

        string Get(CompositeKeyFail key);
    }

    public class CompositeKeyFailCache : ICompositeKeyFailCache
    {
        public CompositeKeyFailCache()
        {
            _dictionary = new Dictionary<CompositeKeyFail, string>();
           // d.Add(new CompositeKeyFail(Constants.KeyPart1, Constants.KeyPart2), Constants.ReturnValue);

           // _dictionary = new ConcurrentDictionary<CompositeKeyFail, string>(d);
        }

        private readonly Dictionary<CompositeKeyFail, string> _dictionary;

        public void Add(CompositeKeyFail key, string value)
        {
            _dictionary.Add(key, value);
        }

        public string Get(CompositeKeyFail key)
        {
            return _dictionary[key];
        }
    }
}