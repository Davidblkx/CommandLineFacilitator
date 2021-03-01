namespace Demo.Services
{
    class Calculations : ICalculations
    {
        public int Current { get; set; }

        public void Sum(int? number) { Current += number ?? 1; }
        public void Subtract(int? number) { Current -= number ?? 1; }
        public int Get() { return Current; }
    }
}
