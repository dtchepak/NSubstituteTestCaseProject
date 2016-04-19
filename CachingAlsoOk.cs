using System.Collections.Generic;

namespace NSubstituteTestCase
{
    public class CompositeKeyAlsoOk
    {
        public string KeyPart1 { get; }
        public short KeyPart2 { get; }

        public CompositeKeyAlsoOk(string keyPart1, short keyPart2)
        {
            KeyPart1 = keyPart1;
            KeyPart2 = keyPart2;
        }

        // required by NSubstitute
        public override bool Equals(object obj)
        {
            var other = obj as CompositeKeyAlsoOk;
            if (other == null)
            {
                return false;
            }
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

    public interface ICompositeKeyAlsoOkCache
    {
        void Add(CompositeKeyAlsoOk key, string value);

        string Get(CompositeKeyAlsoOk key);
    }

    public class CompositeKeyAlsoOkCache : ICompositeKeyAlsoOkCache
    {
        public CompositeKeyAlsoOkCache()
        {
            _dictionary = new Dictionary<CompositeKeyAlsoOk, string>();
        }

        private readonly Dictionary<CompositeKeyAlsoOk, string> _dictionary;

        public void Add(CompositeKeyAlsoOk key, string value)
        {
            _dictionary.Add(key, value);
        }

        public string Get(CompositeKeyAlsoOk key)
        {
            return _dictionary[key];
        }
    }
}