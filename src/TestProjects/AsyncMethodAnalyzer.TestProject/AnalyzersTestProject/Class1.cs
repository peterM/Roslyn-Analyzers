using System;
using System.Threading.Tasks;

namespace AnalyzersTestProject
{
    public interface IInterface
    {
        Task MyMethodAsync(string s, System.Threading.CancellationToken cancellationToken);
    }

    public class Class1 : IInterface
    {
        public Class1()
        {
        }

        public Task MyMethodAsync(string a, System.Threading.CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
