using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8._0_BatchFileWriter.Core
{
    public class BatchProcessorOptions
    {
        private int _batchSize = int.MaxValue;

        public int BatchSize
        {
            get => _batchSize;

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                _batchSize = value;
            }
        }

        public TimeSpan? Interval { get; set; }
    }
}
