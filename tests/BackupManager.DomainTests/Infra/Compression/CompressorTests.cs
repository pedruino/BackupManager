using System;
using System.IO;
using System.IO.Abstractions;
using System.Reflection;
using BackupManager.Domain.Infra.Compression;
using Moq;
using NLog;
using NUnit.Framework;

namespace BackupManager.DomainTests.Infra.Compression
{
    [TestFixture]
    public class CompressorTests
    {
        private static readonly string BaseFolder = GetBaseFolder();
        private FileSystem _mockFileSystemObject;
        private Mock<ILogger> _mockLogger;
        private MockRepository _mockRepository;

        [Test]
        public void Compress_EmptySourceOrEmptyTargetOrInvalidExtensions_ShouldThrowExceptions()
        {
            //Arrange
            var compressor = CreateCompressor();

            //Act
            void DelegateEmptySource() => compressor.Compress(null, "targetFilePath");
            void DelegateEmptyTarget() => compressor.Compress("sourceDirectory", null);
            void DelegateInvalidExtension() => compressor.Compress("sourceDirectory", "compressed.xyz");

            //Assert
            Assert.That(DelegateEmptySource, Throws.ArgumentNullException);
            Assert.That(DelegateEmptyTarget, Throws.ArgumentNullException);
            Assert.That(DelegateInvalidExtension, Throws.ArgumentException, "targetFilePath");
        }

        [Test]
        public void Compress_InvalidSourceValidTarget_ReturnIsFalse()
        {
            //Arrange
            var compressor = CreateCompressor();
            var sourceDirectory = Path.Combine(BaseFolder, "NonExistingFolder\\Database");
            var targetFilePath = Path.Combine(BaseFolder, "TestResults\\compressed.tar.gz");

            //Act
            var result = compressor.Compress(sourceDirectory, targetFilePath);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void Compress_ValidSourceInvalidTarget_ReturnIsFalse()
        {
            //Arrange
            var compressor = CreateCompressor();
            var sourceDirectory = Path.Combine(BaseFolder, "TestArchives\\Database");
            var targetFilePath = Path.Combine(BaseFolder, "NonExistingFolder\\compressed.tar.gz");

            //Act
            var result = compressor.Compress(sourceDirectory, targetFilePath);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void Compress_ValidSourceValidTarget_ReturnIsTrue()
        {
            //Arrange
            var compressor = CreateCompressor();
            var sourceDirectory = Path.Combine(BaseFolder, "TestArchives");
            var targetFilePath = Path.Combine(BaseFolder, "TestResults\\compressed.tar.gz");

            //Act
            var result = compressor.Compress(sourceDirectory, targetFilePath);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Decompress_EmptySourceOrEmptyTargetOrInvalidExtensions_ShouldThrowExceptions()
        {
            //Arrange
            var compressor = CreateCompressor();

            //Act
            void DelegateEmptySource() => compressor.Decompress(null, "targetDirectory");
            void DelegateEmptyTarget() => compressor.Decompress("sourceFilePath", null);
            void DelegateInvalidExtension() => compressor.Decompress("compressed.xyz", "targetDirectory");

            //Assert
            Assert.That(DelegateEmptySource, Throws.ArgumentNullException);
            Assert.That(DelegateEmptyTarget, Throws.ArgumentNullException);
            Assert.That(DelegateInvalidExtension, Throws.ArgumentException, "sourceFilePath");
        }

        [Test]
        public void Decompress_InvalidSourceValidTarget_ReturnIsFalse()
        {
            //Arrange
            var compressor = CreateCompressor();
            var sourceFilePath = Path.Combine(BaseFolder, "TestArchives\\NonExistingFile.tar.gz");
            var targetDirectory = Path.Combine(BaseFolder, "TestResults");

            //Act
            var result = compressor.Decompress(sourceFilePath, targetDirectory);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void Decompress_ValidSourceNonExistingTargetFolder_ReturnIsTrue()
        {
            //Arrange
            var compressor = CreateCompressor();
            var sourceFilePath = Path.Combine(BaseFolder, "TestArchives\\compressed.tar.gz");
            var targetDirectory = Path.Combine(BaseFolder, "TestResults\\NonExistingFolder");

            //Act
            var result = compressor.Decompress(sourceFilePath, targetDirectory);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Decompress_ValidSourceValidTarget_ReturnIsTrue()
        {
            //Arrange
            var compressor = CreateCompressor();
            var sourceFilePath = Path.Combine(BaseFolder, "TestArchives\\compressed.tar.gz");
            var targetDirectory = Path.Combine(BaseFolder, "TestResults");

            //Act
            var result = compressor.Decompress(sourceFilePath, targetDirectory);

            //Assert
            Assert.That(result, Is.True);
        }

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _mockFileSystemObject = new FileSystem();
            _mockLogger = _mockRepository.Create<ILogger>();
            _mockLogger.Setup(logger => logger.Error(It.IsAny<Exception>(), It.IsAny<string>()));
        }

        private static string GetBaseFolder()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            var debugPath = Path.GetDirectoryName(path);
            return Directory.GetParent(Directory.GetParent(debugPath).FullName).FullName;
        }

        private Compressor CreateCompressor()
        {
            return new Compressor(_mockFileSystemObject, _mockLogger.Object);
        }
    }
}