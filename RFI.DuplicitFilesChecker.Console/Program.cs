using CommandLine;

namespace RFI.DuplicitFilesChecker.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            new Parser(config =>
            {
                config.EnableDashDash = true;
                config.HelpWriter = System.Console.Out;
                config.MaximumDisplayWidth = 200;
            })
                .ParseArguments<CommandLineOptions>(args)
                .WithParsed(o =>
                {
                    System.Console.WriteLine($"Hello World: {o.InputDirectory}, {o.OutputFile}");
                });
        }
    }
}
