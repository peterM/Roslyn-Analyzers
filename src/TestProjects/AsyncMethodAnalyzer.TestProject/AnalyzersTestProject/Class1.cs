using System;
using System.Threading;
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

        public Task<bool> MA(System.Threading.CancellationToken cancellationToken, int a)
        {
            return Task.FromResult(true);
        }

        public async Task MAX()
        {
            await Task.Delay(20);
        }

        public void MXX(CancellationToken ca, int a)
        {

        }

        public async void MXXddd(CancellationToken ca, int a)
        {
            Class2 class2 = new Class2();
            await class2.M1(System.Threading.CancellationToken.None);
            class2.M2();
            await class2.M3();
        }

        public void VoidCall()
        {
            MXXddd(CancellationToken.None, 1);
        }
    }
}
