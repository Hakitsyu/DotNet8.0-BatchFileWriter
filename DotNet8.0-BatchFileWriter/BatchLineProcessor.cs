using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNet8._0_BatchFileWriter.Core;

namespace DotNet8._0_BatchFileWriter
{
    public sealed class BatchLineProcessor : BatchProcessor<Line>
    {
        private readonly string _filePath;

        public BatchLineProcessor(string filePath, BatchProcessorOptions options) : base(options)
            => _filePath = filePath;

        public BatchLineProcessor(string filePath) : this(filePath, new()) { }

        protected override void Handle(IList<Line> batch)
        {
            using var writer = File.AppendText(_filePath);
            foreach (var line in batch)
            {
                writer.WriteLine(line);
            }
        }
    }
}
