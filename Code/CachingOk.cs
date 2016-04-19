using System;
using System.Collections.Generic;

namespace NSubstituteTestCases.Code
{
    public class CompositeKeyOk : IEquatable<CompositeKeyOk>
    {
        public string KeyPart1 { get; }
        public short KeyPart2 { get; }

        public CompositeKeyOk(string keyPart1, short keyPart2)
        {
            KeyPart1 = keyPart1;
            KeyPart2 = keyPart2;
        }

        // required by NSubstitute
        public override bool Equals(object obj)
        {
            var other = obj as CompositeKeyOk;
            if (other == null)
            {
                return false;
            }
            return string.Equals(KeyPart1, other.KeyPart1) && KeyPart1 == other.KeyPart1;
        }

        public bool Equals(CompositeKeyOk other)
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

    public interface ICompositeKeyOkCache
    {
        void Add(CompositeKeyOk key, string value);

        string Get(CompositeKeyOk key);
    }

    public class CompositeKeyOkCache : ICompositeKeyOkCache
    {
        public CompositeKeyOkCache()
        {
            _dictionary = new Dictionary<CompositeKeyOk, string>();
           // d.Add(new CompositeKeyOk(Constants.KeyPart1, Constants.KeyPart2), Constants.ReturnValue);

           // _dictionary = new ConcurrentDictionary<CompositeKeyOk, string>(d);
        }

        private readonly Dictionary<CompositeKeyOk, string> _dictionary;

        public void Add(CompositeKeyOk key, string value)
        {
            _dictionary.Add(key, value);
        }

        public string Get(CompositeKeyOk key)
        {
            return _dictionary[key];
        }
    }
}