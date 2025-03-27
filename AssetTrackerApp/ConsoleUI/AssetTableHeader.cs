using System.IO;

namespace AssetTrackerApp.ConsoleUI
{
    /// <summary>
    /// Provides common formatting information for asset tables.
    /// </summary>
    public static class AssetTableHeader
    {
        // Define column widths.
        public const int OfficeWidth = 10;
        public const int TypeWidth = 10;
        public const int BrandWidth = 10;
        public const int ModelWidth = 15;
        public const int AmountWidth = 8;
        public const int CurrWidth = 4;
        public const int ConvertedWidth = 12;
        public const int DateWidth = 10;

        /// <summary>
        /// Prints the asset table header to the given TextWriter.
        /// </summary>
        /// <param name="writer">The writer to output the header.</param>
        public static void PrintHeader(TextWriter writer)
        {
            writer.WriteLine(
                $"{"Office",-OfficeWidth} {"Type",-TypeWidth} {"Brand",-BrandWidth} {"Model",-ModelWidth} " +
                $"{"Amount",-AmountWidth} {"Curr",-CurrWidth} {"Converted",-ConvertedWidth} {"Date",-DateWidth}"
            );

            // Calculate total width: sum of all column widths plus one space between each column.
            int totalWidth = OfficeWidth + TypeWidth + BrandWidth + ModelWidth + AmountWidth + CurrWidth + ConvertedWidth + DateWidth + 8;
            writer.WriteLine(new string('-', totalWidth));
        }
    }
}