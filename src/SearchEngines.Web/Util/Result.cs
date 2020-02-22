namespace SearchEngines.Web.Util
{
    public class Result<T> where T : class
    {
        public bool IsOk { get; set; }
        public string ErrorMessage { get; set; }
        public T Value { get; set; }
    }
}