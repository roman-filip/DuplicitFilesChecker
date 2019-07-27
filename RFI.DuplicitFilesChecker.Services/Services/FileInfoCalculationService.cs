using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace RFI.DuplicitFilesChecker.Services.Services
{
    public class FileInfoCalculationService : IFileInfoCalculationService
    {
        private readonly HashAlgorithm _hashAlgorithm;

        public FileInfoCalculationService(HashAlgorithm hashAlgorithm)
        {
            _hashAlgorithm = hashAlgorithm;
        }

        public Entities.FileInfo CalculateFileInfo(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File for calculation not found.", filePath);
            }

            throw new NotImplementedException();
        }

        public IEnumerable<Entities.FileInfo> CalculateFileInfos(string directory)
        {
            if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException($"Directory '{directory}' for calculation not found.");
            }

            throw new NotImplementedException();
        }
    }
}
