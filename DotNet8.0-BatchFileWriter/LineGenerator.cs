using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8._0_BatchFileWriter
{
    public sealed class LineGenerator
    {
        private readonly ILineGeneratorOutput _output;

        private readonly ILineGeneratorStrategy _strategy;

        private readonly LineGeneratorOptions _options;

        public CancellationTokenSource CancellationTokenSource { get; }

        public LineGenerator(ILineGeneratorOutput output, ILineGeneratorStrategy strategy, LineGeneratorOptions options)
            => (_output, _strategy, _options, CancellationTokenSource) = 
            (output, strategy, options, new());

        public LineGenerator(ILineGeneratorOutput output, ILineGeneratorStrategy strategy) : this(output, strategy, new()) { }

        public async Task GenerateAsync()
        {
            while (!CancellationTokenSource.IsCancellationRequested)
            {
                var line = _strategy.Generate();
                await _output.HandleAsync(line);

                await WaitInterval();
            }
        }

        private Task WaitInterval()
            => _options.Interval.HasValue ? Task.Delay(_options.Interval.Value) : Task.CompletedTask;
    }
}
