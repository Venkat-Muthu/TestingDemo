using System;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public class Calculator : ICalculator
    {
        public double Add(double x, double y)
        {
            return x + y;
        }

        public double Subtract(double x, double y)
        {
            return x - y;
        }

        public double Multiply(double x, double y)
        {
            return x * y;
        }

        public double Divide(double x, double y)
        {
            return x / y;
        }

        public async Task<double> AddAsync(double x, double y)
        {
            return await Task.FromResult(Add(x, y));
        }

        public async Task<double> SubtractAsync(double x, double y)
        {
            return await Task.Run(() => Subtract(x, y));
        }

        public async Task<double> MultiplyAsync(double x, double y)
        {
            return await Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(50));
                return Multiply(x, y);
            });
        }

        public async Task<double> DivideAsync(double x, double y)
        {
            return await Task.Factory.StartNew(() => Divide(x, y));
        }
    }
}