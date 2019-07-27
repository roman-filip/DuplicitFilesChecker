using System.IO;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFI.DuplicitFilesChecker.Services.Services;

namespace RFI.DuplicitFilesChecker.Tests
{
    [TestClass]
    public class FileInfoCalculationServiceTest
    {
        IFileInfoCalculationService _fileInfoCalculationService = new FileInfoCalculationService(MD5.Create());

        [TestMethod]
        public void CalculateFileInfo_NullFileName()
        {
            Assert.ThrowsException<FileNotFoundException>(() => _fileInfoCalculationService.CalculateFileInfo(null));
        }

        [TestMethod]
        public void CalculateFileInfo_NonExistingFile()
        {
            Assert.ThrowsException<FileNotFoundException>(() => _fileInfoCalculationService.CalculateFileInfo("Non existing file"));
        }

        [TestMethod]
        public void CalculateFileInfos_NullDirectoryName()
        {
            Assert.ThrowsException<DirectoryNotFoundException>(() => _fileInfoCalculationService.CalculateFileInfos(null));
        }

        [TestMethod]
        public void CalculateFileInfos_NonExistingDirectory()
        {
            Assert.ThrowsException<DirectoryNotFoundException>(() => _fileInfoCalculationService.CalculateFileInfos("Non existing directory"));
        }
    }
}
