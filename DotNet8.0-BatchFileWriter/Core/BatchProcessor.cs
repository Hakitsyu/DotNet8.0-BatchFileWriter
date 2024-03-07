using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8._0_BatchFileWriter.Core
{
    public abstract class BatchProcessor<T> : IDisposable
    {
        private readonly BatchProcessorOptions _options;

        public BlockingCollection<T> Collection { get; }

        public CancellationTokenSource CancellationTokenSource { get; }

        protected BatchProcessor(BatchProcessorOptions options)
            => (_options, Collection, CancellationTokenSource) = 
            (options, new(), new());

        protected BatchProcessor() : this(new BatchProcessorOptions()) { }

        protected abstract void Handle(IList<T> batch);

        public async Task ProcessAsync()
        {
            while (!CancellationTokenSource.IsCancellationRequested)
            {
                var batchSize = _options.BatchSize;
                var batch = new List<T>();

                while (batchSize > 0 && Collection.TryTake(out var item))
                {
                    batch.Add(item);
                    batchSize--;
                }

                Handle(batch);
                await WaitInterval();
            }
        }

        private Task WaitInterval()
            => _options.Interval.HasValue ? Task.Delay(_options.Interval.Value) : Task.CompletedTask;

        public void Dispose() => Collection.Dispose();
    }
}
