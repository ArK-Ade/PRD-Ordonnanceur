using CsvHelper;
using PRD_Ordonnanceur.Data;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace PRD_Ordonnanceur.Parser
{
    /// <summary>
    /// 
    /// </summary>
    public static class DataToCsv
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="records"></param>
        public static void ParsingDataOF(List<OF> records)
        {
            using var writer = new StreamWriter("../../../file.csv");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            csv.WriteRecords(records);
        }
    }
}
