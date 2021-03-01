namespace Demo.Services
{
    interface ICalculations
    {
        int Get();
        void Subtract(int? number);
        void Sum(int? number);
    }
}