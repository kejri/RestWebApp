using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestWebAppClient.Helpers
{
    public static class EnumExtender
    {
        public static string GetLocalizedDescription(this Enum @enum)
        {
            if (@enum == null)
                return null;

            string description = @enum.ToString();

            FieldInfo fieldInfo = @enum.GetType().GetField(description);
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Any())
                return attributes[0].Description;

            return description;
        }

        public static T[] GetValues<T>()
        {
            Type enumType = typeof(T);

            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
            }

            List<T> values = new List<T>();

            var fields = from field in enumType.GetFields()
                         where field.IsLiteral
                         select field;

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(enumType);
                values.Add((T)value);
            }

            return values.ToArray();
        }

        public static object[] GetValues(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
            }

            List<object> values = new List<object>();

            var fields = from field in enumType.GetFields()
                         where field.IsLiteral
                         select field;

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(enumType);
                values.Add(value);
            }

            return values.ToArray();
        }

        public static string[] GetLocalizedDescription(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
            }

            var values = new List<string>();

            var fields = from field in enumType.GetFields()
                         where field.IsLiteral
                         select field;

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(enumType);
                string description = value.ToString();
                DescriptionAttribute[] attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Any())
                    description = attributes[0].Description;
                values.Add(description);
            }

            return values.ToArray();
        }

        public static List<EnumPair> GetListEnumPair(Type enumType)
        {
            var result = new List<EnumPair>();
            var values = GetValues(enumType);
            var desriptions = GetLocalizedDescription(enumType);
            for (int i = 0; i < values.Length; i++)
            {
                var item = new EnumPair(desriptions[i], values[i]);
                result.Add(item);
            }
            return result;
        }
    }
}
