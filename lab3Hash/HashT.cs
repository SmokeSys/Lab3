using System.Collections;
using System.Collections.Generic;

namespace lab3Hash
{
    class HashCell<TKey,TValue>
    {
        public TKey key;
        public TValue value;
        public HashCell(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
    }
    public class HashTabl<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        HashCell<TKey, TValue>[] table;
        private int _count;
        public int Count { get { return _count; } }
        public int Capacity { get { return table.Length; } }

        #region Constructors
        public HashTabl()
        {
            _count = 0;
            table = new HashCell<TKey, TValue>[100];
        }
        public HashTabl(int capacity)
        {
            _count = 0;
            table = new HashCell<TKey, TValue>[capacity];
        }
#endregion

        public void Insert(TKey key, TValue value)  
        {
            HashCell<TKey, TValue> temp = new HashCell<TKey, TValue>(key, value);
            int tempi = temp.key.GetHashCode() % Capacity;
            int q = 0;
            int t = tempi;
            while (t < Capacity)
            {
                if (table[t] != null && table[t].value.Equals(value)) //if contains value == current value
                    return;
                if (table[t] == null)
                {
                    _count++;
                    table[t] = temp;
                    return;
                }
                q++;
                t = tempi + q * q;
                if (t >= Capacity)
                    ChangeCapacity();
            }
        }

        void ChangeCapacity()
        {
            HashTabl<TKey, TValue> temp = new HashTabl<TKey, TValue>(Capacity * 4);
            foreach (var t in table)
            {
                if (t == null) continue;
                temp.Insert(t.key, t.value);
            }
            table = temp.table;
            _count = temp._count;
        }


        public TValue Search(TKey key)
        {
            int temp = key.GetHashCode() % Capacity;
            int tempi = temp;
            int q = 0;            
            while (tempi < Capacity)
            {
                if (table[tempi] == null) return default;
                if (table[tempi].key.Equals(key))
                {
                    return table[tempi].value;
                }
                q++;
                tempi = temp + q * q;
            }
            return default;
        }

        public void Delete(TKey key, TValue value)
        {
            int tempkey = key.GetHashCode() % Capacity;
            int tempi = tempkey;
            int q = 0;
            while (tempi < Capacity)
            {
                if (table[tempi] == null) return;
                if (table[tempi].value.Equals(value))
                {
                    _count--;
                    int q1 = q + 1;
                    int tempi1 = tempi + q1 * q1;
                    while(tempi1 < Capacity)
                    {
                        if (table[tempi1] == null) return;
                        if (table[tempi1].key.Equals(key))
                        {
                            table[tempi] = table[tempi1];
                            table[tempi1] = null;
                            tempi = tempi1;
                        }
                        q1++;
                        tempi1 = tempkey + q1 * q1;
                    }
                    return;
                }
                q++;
                tempi = tempkey + q * q;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < Capacity; i++)
                yield return new KeyValuePair<TKey, TValue>(table[i].key, table[i].value);
        }                  

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
