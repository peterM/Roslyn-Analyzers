using System;
using System.Threading.Tasks;

namespace AnalyzersTestProject
{
    public interface IInterface
    {
        Task MyMethod(string s);
    }

    public class Class1 : IInterface
    {
        public Class1()
        {
        }

        public Task MyMethod(string a)
        {
            return Task.CompletedTask;
        }


        public Task<bool> MA()
        {
            return Task.FromResult(true);
        }

        public async void MAX()
        {
            await Task.Delay(20);
        }
    }
}
