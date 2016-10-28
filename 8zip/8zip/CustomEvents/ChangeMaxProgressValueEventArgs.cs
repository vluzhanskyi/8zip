
namespace _8zip.CustomEvents
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
