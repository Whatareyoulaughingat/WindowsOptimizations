using System;

// Source: https://stackoverflow.com/questions/14488796/does-net-provide-an-easy-way-convert-bytes-to-kb-mb-gb-etc
namespace WindowsOptimizations.Core.Extensions
{
    /// <summary>
    /// Storage size units.
    /// </summary>
    public enum SizeUnits
    {
        Byte, KB, MB, GB,
    }

    /// <summary>
    /// An extension class containg methods for formatting values.
    /// </summary>
    public class StringExtensions
    {
        /// <summary>
        /// Formats the value to a readable storage size such as MB.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static string ToSize(long value, SizeUnits unit)
        {
            return (value / (double)Math.Pow(1024, (long)unit)).ToString("0.00");
        }
    }
}
