﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emiplus.Data.SobreEscrever
{
    class KeyedAutoCompleteStringCollection : AutoCompleteStringCollection
    {

        private readonly Dictionary<string, int> keyedValues =
            new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        public void Add(string value, int key)
        {
            base.Add(value);
            keyedValues.Add(value, key); // intentionally backwards
        }

        public int Lookup(string value)
        {
            int key;
            if (keyedValues.TryGetValue(value, out key))
            {
                return key;
            }
            else
            {
                return 0;
            }
        }

    }
}
