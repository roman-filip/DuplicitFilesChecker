using System.Security.Cryptography;
using CommandLine;
using RFI.DuplicitFilesChecker.Services.Facades;
using RFI.DuplicitFilesChecker.Services.Services;

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
                    new FileWriterFacade(new FileInfoCalculationService(MD5.Create()))
                        .WriteFilesInfoToFile(o.InputDirectory, o.OutputFile);
                });
        }
    }
}
