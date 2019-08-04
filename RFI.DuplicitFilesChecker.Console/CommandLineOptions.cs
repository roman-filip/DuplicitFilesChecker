using CommandLine;

namespace RFI.DuplicitFilesChecker.Console
{
    public class CommandLineOptions
    {
        [Option('d', "directory", Required = false, HelpText = "Input directory for calculation of files info.")]
        public string InputDirectory { get; set; }

        [Option('f', "file", Required = false, HelpText = "Output file where will be stored calculated info about files from given directory.")]
        public string OutputFile { get; set; }
    }
}
