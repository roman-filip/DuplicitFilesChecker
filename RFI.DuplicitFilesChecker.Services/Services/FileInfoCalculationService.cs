using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RFI.DuplicitFilesChecker.Services.Services
{
    public class FileInfoCalculationService : IFileInfoCalculationService
    {
        private readonly HashAlgorithm _hashAlgorithm;

        // TODO - dispose hash algorithm

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

            return CreateFileInfo(new FileInfo(filePath));
        }

        public IEnumerable<Entities.FileInfo> CalculateFileInfos(string directory)
        {
            if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException($"Directory '{directory}' for calculation not found.");
            }

            var files = new DirectoryInfo(directory).GetFiles("*", SearchOption.AllDirectories);
            var fileInfos = new List<Entities.FileInfo>(files.Length);
            foreach (var file in files)
            {
                fileInfos.Add(CreateFileInfo(file));
            }

            return fileInfos;
        }

        private Entities.FileInfo CreateFileInfo(FileInfo file) =>
            new Entities.FileInfo
            {
                Name = file.Name,
                Path = file.DirectoryName,
                Date = file.CreationTime,
                Hash = ComputeHash(file)
            };

        private string ComputeHash(FileInfo file)
        {
            FileStream fileStream = file.Open(FileMode.Open);
            fileStream.Position = 0;
            byte[] hashValue = _hashAlgorithm.ComputeHash(fileStream);
            fileStream.Close();

            var sBuilder = new StringBuilder();
            hashValue.ToList().ForEach(h => sBuilder.Append(h.ToString("x2")));
            return sBuilder.ToString();
        }
    }
}
