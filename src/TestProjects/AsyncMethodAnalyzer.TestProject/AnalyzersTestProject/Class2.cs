using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnalyzersTestProject
{
    public class Class2
    {
        public async Task M1(CancellationToken cancellationToken)
        {
            await Task.Delay(1);
        }
        public async  void M2()
        {
            await Task.Delay(1);
        }
        public Task<bool> M3()
        {
            return Task.FromResult(true);
        }
    }
}
