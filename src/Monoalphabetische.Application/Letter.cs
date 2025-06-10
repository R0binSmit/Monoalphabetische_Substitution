namespace Monoalphabetische.Application
{
    public class Letter
    {
        public char Value { get; set; }
        public int Counter { get; set; }

        public Letter(char value, int counter = 0)
        {
            Value = value;
            Counter = counter;
        }
    }
}
