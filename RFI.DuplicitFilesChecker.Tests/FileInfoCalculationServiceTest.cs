using System.Linq;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RFI.DuplicitFilesChecker.Services.Entities;
using RFI.DuplicitFilesChecker.Services.Services;
using DirectoryNotFoundException = System.IO.DirectoryNotFoundException;
using FileNotFoundException = System.IO.FileNotFoundException;

namespace RFI.DuplicitFilesChecker.Tests
{
    [TestClass]
    public class FileInfoCalculationServiceTest
    {
        IFileInfoCalculationService _fileInfoCalculationService = new FileInfoCalculationService(MD5.Create());

        [TestMethod]
        public void CalculateFileInfo_NullFileName() =>
            Assert.ThrowsException<FileNotFoundException>(() => _fileInfoCalculationService.CalculateFileInfo(null));

        [TestMethod]
        public void CalculateFileInfo_NonExistingFile() =>
            Assert.ThrowsException<FileNotFoundException>(() => _fileInfoCalculationService.CalculateFileInfo("Non existing file"));

        [TestMethod]
        public void CalculateFileInfo_ValidateFileInfo()
        {
            var fileInfo = _fileInfoCalculationService
                .CalculateFileInfo(@"C:\_GitHub\DuplicitFilesChecker\RFI.DuplicitFilesChecker.Tests\bin\Debug\netcoreapp2.1\TestData\HelloWorld.txt");

            Assert.IsNotNull(fileInfo);
            AssertFileInfo(
                new FileInfo
                {
                    Name = "HelloWorld.txt",
                    Hash = "236bf30c70dc03f69175f030afbe38f3",
                    Path = @"C:\_GitHub\DuplicitFilesChecker\RFI.DuplicitFilesChecker.Tests\bin\Debug\netcoreapp2.1\TestData"
                },
                fileInfo);
        }

        [TestMethod]
        public void CalculateFileInfos_NullDirectoryName() =>
            Assert.ThrowsException<DirectoryNotFoundException>(() => _fileInfoCalculationService.CalculateFileInfos(null));

        [TestMethod]
        public void CalculateFileInfos_NonExistingDirectory() =>
            Assert.ThrowsException<DirectoryNotFoundException>(() => _fileInfoCalculationService.CalculateFileInfos("Non existing directory"));

        [TestMethod]
        public void CalculateFileInfos_ValidateFileInfos()
        {
            var fileInfos = _fileInfoCalculationService.CalculateFileInfos(@".\TestData").OrderBy(f => f.Name).ToList();

            Assert.AreEqual(9, fileInfos.Count());

            AssertFileInfo(
                new FileInfo
                {
                    Name = "0.png",
                    Hash = "8570e2b50bd605de52aed2b239517b8f",
                    Path = @"C:\_GitHub\DuplicitFilesChecker\RFI.DuplicitFilesChecker.Tests\bin\Debug\netcoreapp2.1\TestData"
                },
                fileInfos[0]);

            AssertFileInfo(
                new FileInfo
                {
                    Name = "1.png",
                    Hash = "6f503918a9ea18d16b4bb54db186230d",
                    Path = @"C:\_GitHub\DuplicitFilesChecker\RFI.DuplicitFilesChecker.Tests\bin\Debug\netcoreapp2.1\TestData\1"
                },
                fileInfos[1]);

            AssertFileInfo(
                new FileInfo
                {
                    Name = "1_1_1.png",
                    Hash = "98438f45bdd8783376d89727afe2d05b",
                    Path = @"C:\_GitHub\DuplicitFilesChecker\RFI.DuplicitFilesChecker.Tests\bin\Debug\netcoreapp2.1\TestData\1\1_1"
                },
                fileInfos[2]);

            AssertFileInfo(
                new FileInfo
                {
                    Name = "1_1_2.png",
                    Hash = "1ba8f492f564defd90e38f03edb77535",
                    Path = @"C:\_GitHub\DuplicitFilesChecker\RFI.DuplicitFilesChecker.Tests\bin\Debug\netcoreapp2.1\TestData\1\1_1"
                },
                fileInfos[3]);

            AssertFileInfo(
                new FileInfo
                {
                    Name = "1_2_1.png",
                    Hash = "393b2676ba9a46d60c70928cf6a89130",
                    Path = @"C:\_GitHub\DuplicitFilesChecker\RFI.DuplicitFilesChecker.Tests\bin\Debug\netcoreapp2.1\TestData\1\1_2"
                },
                fileInfos[4]);

            AssertFileInfo(
                new FileInfo
                {
                    Name = "1_2_2.png",
                    Hash = "21fe7695682b0e2ccebf5877db3fef39",
                    Path = @"C:\_GitHub\DuplicitFilesChecker\RFI.DuplicitFilesChecker.Tests\bin\Debug\netcoreapp2.1\TestData\1\1_2"
                },
                fileInfos[5]);

            AssertFileInfo(
                new FileInfo
                {
                    Name = "2.png",
                    Hash = "c02c95ea8c718536590fdc597c56cb10",
                    Path = @"C:\_GitHub\DuplicitFilesChecker\RFI.DuplicitFilesChecker.Tests\bin\Debug\netcoreapp2.1\TestData\2"
                },
                fileInfos[6]);

            AssertFileInfo(
                new FileInfo
                {
                    Name = "2_1_1.png",
                    Hash = "3fa67198a94b18fa25eea25d821d6a64",
                    Path = @"C:\_GitHub\DuplicitFilesChecker\RFI.DuplicitFilesChecker.Tests\bin\Debug\netcoreapp2.1\TestData\2\2_1"
                },
                fileInfos[7]);

            AssertFileInfo(
                new FileInfo
                {
                    Name = "HelloWorld.txt",
                    Hash = "236bf30c70dc03f69175f030afbe38f3",
                    Path = @"C:\_GitHub\DuplicitFilesChecker\RFI.DuplicitFilesChecker.Tests\bin\Debug\netcoreapp2.1\TestData"
                },
                fileInfos[8]);
        }

        private void AssertFileInfo(FileInfo expectedFileInfo, FileInfo actualFileInfo)
        {
            Assert.AreEqual(expectedFileInfo.Name, actualFileInfo.Name);
            Assert.AreEqual(expectedFileInfo.Path, actualFileInfo.Path);
            //Assert.AreEqual(expectedFileInfo.Date, actualFileInfo.Date);
            Assert.AreEqual(expectedFileInfo.Hash, actualFileInfo.Hash);
        }
    }
}
