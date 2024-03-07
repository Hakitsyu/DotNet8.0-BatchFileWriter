using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8._0_BatchFileWriter
{
    public sealed class LineGeneratorDefaultStrategy : ILineGeneratorStrategy
    {
        public Line Generate() => new Line(new Random().Next().ToString());
    }
}
