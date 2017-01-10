using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestWebAppClient.Helpers
{
    public class EnumPair
    {
        public EnumPair(string Name, Object Value)
        {
            this.Name = Name;
            this.Value = Value;
        }

        public string Name;
        public Object Value;

        public override string ToString()
        {
            return Name;
        }
    }

    public class EnumItemCollection : ICollection, IEnumerable, IList
    {
        public EnumItemCollection(Type enumType)
        {
            if (enumType.IsEnum)
                this.enumType = enumType;
        }
        Type enumType;

        public Int32 ValueFromName(string name)
        {
            FieldInfo[] fields = this.enumType.GetFields();

            foreach (FieldInfo field in fields)
                if (name == field.Name)
                    return (Int32)field.GetValue(null);

            return -1;
        }

        public class EnumItemEnumerator : IEnumerator
        {

            public EnumItemEnumerator(FieldInfo[] fieldInfo)
            {
                this.fieldInfo = fieldInfo;
                this.index = 0;
            }
            private FieldInfo[] fieldInfo;
            private int index;

            #region IEnumerator Members

            public void Reset()
            {
                this.index = 0;
            }

            public object Current
            {
                get
                {
                    FieldInfo info = fieldInfo[index];
                    DescriptionAttribute[] atributy =
                        (DescriptionAttribute[])info.GetCustomAttributes(
                        typeof(DescriptionAttribute),
                        false);
                    if (atributy != null && atributy.Length > 0)
                    {
                        return new EnumPair(atributy[0].Description, (Int32)info.GetValue(null));
                    }
                    else
                    {
                        return new EnumPair(info.Name, (Int32)info.GetValue(null));
                    }
                }
            }

            public bool MoveNext()
            {
                if (this.index < this.fieldInfo.Length - 1)
                {
                    this.index++;
                    return true;
                }
                else
                    return false;
            }

            #endregion

        }

        #region ICollection Members

        public bool IsSynchronized
        {
            get
            {
                // TODO:  Add EnumItemCollection.IsSynchronized getter implementation
                return false;
            }
        }

        public int Count
        {
            get
            {
                return enumType.GetFields().Length - 1;
            }
        }

        public void CopyTo(Array array, int index)
        {
            // TODO:  Add EnumItemCollection.CopyTo implementation
        }

        public object SyncRoot
        {
            get
            {
                // TODO:  Add EnumItemCollection.SyncRoot getter implementation
                return null;
            }
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return new EnumItemEnumerator(enumType.GetFields());
        }

        #endregion

        #region IList Members

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public object this[int index]
        {
            get
            {
                FieldInfo[] fields = this.enumType.GetFields();
                FieldInfo info = fields[index + 1];
                DescriptionAttribute[] atributy =
                    (DescriptionAttribute[])info.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);
                if (atributy != null && atributy.Length > 0)
                {
                    return new EnumPair(atributy[0].Description, (Int32)info.GetValue(null));
                }
                else
                {
                    return new EnumPair(info.Name, (Int32)info.GetValue(null));
                }
            }
            set
            {
                // TODO:  Add EnumItemCollection.this setter implementation
            }
        }

        public void RemoveAt(int index)
        {
            // TODO:  Add EnumItemCollection.RemoveAt implementation
        }

        public void Insert(int index, object value)
        {
            // TODO:  Add EnumItemCollection.Insert implementation
        }

        public void Remove(object value)
        {
            // TODO:  Add EnumItemCollection.Remove implementation
        }

        public bool Contains(object value)
        {
            // TODO:  Add EnumItemCollection.Contains implementation
            return false;
        }

        public void Clear()
        {
            // TODO:  Add EnumItemCollection.Clear implementation
        }

        public int IndexOf(object value)
        {
            // TODO:  Add EnumItemCollection.IndexOf implementation
            return 0;
        }

        public int Add(object value)
        {
            // TODO:  Add EnumItemCollection.Add implementation
            return 0;
        }

        public bool IsFixedSize
        {
            get
            {
                return true;
            }
        }

        #endregion
    }
}
