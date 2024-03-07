using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8._0_BatchFileWriter
{
    public sealed class LineGeneratorBlockingCollectionOutput : ILineGeneratorOutput
    {
        private readonly BlockingCollection<Line> _collection;

        public LineGeneratorBlockingCollectionOutput(BlockingCollection<Line> collection)
            => _collection = collection;

        public Task HandleAsync(Line line)
        {
            WriteGeneratedLine(line);

            _collection.TryAdd(line);
            return Task.CompletedTask;
        }

        static void WriteGeneratedLine(Line line) => Console.WriteLine($"Generated line : {line.Content}");
    }
}
