using System;
using System.IO;
using Newtonsoft.Json;
using RFI.DuplicitFilesChecker.Services.Services;

namespace RFI.DuplicitFilesChecker.Services.Facades
{
    public class FileWriterFacade
    {
        private readonly IFileInfoCalculationService _fileInfoCalculationService;

        public FileWriterFacade(IFileInfoCalculationService fileInfoCalculationService)
        {
            _fileInfoCalculationService = fileInfoCalculationService;
        }

        public void WriteFilesInfoToFile(string inputDirectory, string outputFile)
        {
            if (File.Exists(outputFile))
            {
                throw new ApplicationException($"File '{outputFile}' already exists.");
            }

            var fileInfos = _fileInfoCalculationService.CalculateFileInfos(inputDirectory);
            using (StreamWriter file = File.CreateText(outputFile))
            {
                JsonSerializer serializer = new JsonSerializer { Formatting = Formatting.Indented };
                serializer.Serialize(file, fileInfos);
            }
        }
    }
}
