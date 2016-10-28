namespace _8zip.CustomEvents
{
    public class ShowExceptionMessageArks
    {
        public string Message { set; get; }
        public ShowExceptionMessageArks(string exMessage)
        {
            Message = exMessage;
        }
    }
}
