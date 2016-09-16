
namespace _8zip
{
    public class ChangeMaxProgressValueEventArgs
    {
         public int Value { get; set; }
         public ChangeMaxProgressValueEventArgs(int newValue)
        {
            Value = newValue;
        }
    }
}
