namespace Clif.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new ConsoleApplicationBuilder()
                .Build()
                .Resolve(args);
        }
    }
}
