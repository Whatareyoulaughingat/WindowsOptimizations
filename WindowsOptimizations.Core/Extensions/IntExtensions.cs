using System;

// Source: https://stackoverflow.com/questions/14488796/does-net-provide-an-easy-way-convert-bytes-to-kb-mb-gb-etc
namespace WindowsOptimizations.Core.Extensions
{
    public enum SizeUnits
    {
        Byte, KB, MB, GB,
    }

    public class IntExtensions
    {
        public static string ToSize(long value, SizeUnits unit)
        {
            return (value / (double)Math.Pow(1024, (long)unit)).ToString("0.00");
        }
    }
}
