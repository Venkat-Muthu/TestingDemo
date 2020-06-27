using System.Threading.Tasks;

namespace DemoLibrary
{
    public interface ICalculator
    {
        double Add(double x, double y);
        double Subtract(double x, double y);
        double Multiply(double x, double y);
        double Divide(double x, double y);
        Task<double> AddAsync(double x, double y);
        Task<double> SubtractAsync(double x, double y);
        Task<double> MultiplyAsync(double x, double y);
        Task<double> DivideAsync(double x, double y);
    }
}