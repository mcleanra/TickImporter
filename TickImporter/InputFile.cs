using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickImporter
{
    class InputFile
    {
        public string FileName;
        private List<Tick> Ticks;
        private string Symbol;
        private DateTime Date;
        public Status Status;

        //test comment for git
        public InputFile(string filename)
        {
            FileName = filename;
            Symbol = Path.GetFileNameWithoutExtension(filename);
            Date = DateTime.ParseExact(Path.GetFileName(Path.GetDirectoryName(filename)), "yyyyMMdd",
                CultureInfo.InvariantCulture);
            Status = Status.QUEUED;
        }

        public void Read()
        {
            if (String.IsNullOrEmpty(FileName)) throw new Exception("No filename.");

            using (TextReader reader = File.OpenText( FileName ))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.Delimiter = ",";
                var ticksFromCsv = csv.GetRecords<Tick>();
                foreach (var tick in ticksFromCsv)
                {
                    Ticks.Add(tick);
                }
            }
        }

        public void Write()
        {

        }
    }
}
