using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Emiplus.Data.SobreEscrever
{
    internal class KeyedAutoCompleteStringCollection : AutoCompleteStringCollection
    {
        private readonly Dictionary<string, int> keyedValues = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        public void Add(string value, int key)
        {
            if (string.IsNullOrEmpty(value))
                return;

            Add(value);
            if (!keyedValues.ContainsKey(value))
                keyedValues.Add(value, key); // intentionally backwards
        }

        public int Lookup(string value)
        {
            return keyedValues.TryGetValue(value, out var key) ? key : 0;
        }
    }
}