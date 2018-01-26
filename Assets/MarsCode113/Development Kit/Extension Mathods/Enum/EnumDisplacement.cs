using System;
using System.ComponentModel;

namespace EnumExtention.Displacement
{
    public static class EnumDisplacement
    {

        /// <summary>
        /// Get previous element.
        /// </summary>
        public static TEnum Prev<TEnum>(this Enum input, bool cycleable = false) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return Move<TEnum>(input, -1, cycleable);
        }


        /// <summary>
        /// Get next element.
        /// </summary>
        public static TEnum Next<TEnum>(this Enum input, bool cycleable = false) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return Move<TEnum>(input, 1, cycleable);
        }


        /// <summary>
        /// Get element with specified offset.
        /// </summary>
        private static TEnum Move<TEnum>(this Enum input, int offset, bool cycleable = false)
        {
            if(!typeof(TEnum).IsEnum)
                throw new ArgumentException("TEnum must be an enumerated type");

            // Get enum elements and length.
            var values = Enum.GetNames(input.GetType());
            var length = values.Length;
            if(length == 0)
                return default(TEnum);

            // Calculate enum position.
            var inputName = Enum.GetName(input.GetType(), input);
            var pos = Array.IndexOf(values, inputName) + offset;
            if(pos < 0)
                pos = cycleable ? length - 1 : 0;
            else if(pos >= length)
                pos = cycleable ? 0 : length - 1;

            return (TEnum)Enum.Parse(input.GetType(), values[pos]);
        }


        /// <summary>
        /// Get index.
        /// </summary>
        public static int Index(this Enum input)
        {
            var values = Enum.GetNames(input.GetType());
            var inputName = Enum.GetName(input.GetType(), input);
            return Array.IndexOf(values, inputName);
        }


        /// <summary>
        /// Get description. 
        /// </summary>
        public static string Description(this Enum input)
        {
            DescriptionAttribute[] da = (DescriptionAttribute[])(input.GetType().GetField(input.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return da.Length > 0 ? da[0].Description : input.ToString();
        }

    }
}
