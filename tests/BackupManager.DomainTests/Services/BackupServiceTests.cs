using BackupManager.Domain.Enumerations;
using BackupManager.Domain.Interfaces;
using BackupManager.Domain.Services;
using Moq;
using NLog;
using NUnit.Framework;
using System.IO.Abstractions.TestingHelpers;
using System.Threading.Tasks;

namespace BackupManager.DomainTests.Services
{
    [TestFixture]
    public class BackupServiceTests
    {
        private Mock<ICompressor> _mockCompressor;
        private MockFileSystem _mockFileSystemObject;
        private Mock<IFirebirdService> _mockFirebirdService;
        private ILogger _mockLoggerObject;
        private MockRepository _mockRepository;
        private Mock<ISettings> _mockSettings;
        private Mock<ISettingsBackupContent> _mockSettingsBackupContent;
        //private Mock<ILogger> _mockLogger;

        //[Test]
        //public async Task CreateBackupFileAsync_CompressorReturnsFalse_FileIsBad()
        //{
        //    // Arrange
        //    //_mockLogger.Setup(logger => logger.Error(It.IsAny<string>()));
        //    _mockSettingsBackupContent.SetupGet(p => p.FullPath).Returns(@"C:\Users\pedruino\source\repos\BackupManager\tests\BackupManager.DomainTests\TestArchives\Database\EMPLOYEE.FDB");
        //    _mockSettingsBackupContent.SetupGet(p => p.ContentType).Returns(ContentType.Database);

        //    _mockCompressor.Setup(c =>
        //            c.Compress(It.IsNotNull<string>(), It.IsNotNull<string>()))
        //                .Returns(false);

        //    var unitUnderTest = CreateService();

        //    // Act
        //    var result = await unitUnderTest.CreateBackupFileAsync();

        //    // Assert
        //    Assert.That(result, Is.Not.Null, "FileInfo is NULL.");
        //    Assert.That(result.Exists, Is.False, "File not exists.");
        //}

        [Test]
        public async Task CreateBackupFileAsync_WhenContentDatabaseAndCompressorReturnsFalse_FileIsBad()
        {
            // Arrange
            _mockSettingsBackupContent.SetupGet(p => p.FullPath).Returns(@"C:\fake\path\database.fdb");
            _mockSettingsBackupContent.SetupGet(p => p.ContentType).Returns(ContentType.Database);
            _mockFirebirdService.Setup(f =>
                f.GenerateFbk(_mockSettingsBackupContent.Object))
                    .Returns(new MockFileInfo(_mockFileSystemObject, @"C:\fake\path\database.fbk"));
            _mockCompressor.Setup(c =>
                    c.Compress(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                        .Returns(false);

            var unitUnderTest = CreateService();

            // Act
            var result = await unitUnderTest.CreateBackupFileAsync();

            // Assert
            Assert.That(result, Is.Not.Null, "FileInfo is NULL.");
            Assert.That(result.Exists, Is.False, "File not exists.");
        }

        [Test]
        public async Task CreateBackupFileAsync_WhenContentDatabaseCompressorReturnsTrue_FileInfoIsOk()
        {
            // Arrange
            _mockSettingsBackupContent.SetupGet(p => p.FullPath).Returns(@"C:\fake\path\database.fdb");
            _mockSettingsBackupContent.SetupGet(p => p.ContentType).Returns(ContentType.Database);

            _mockFirebirdService.Setup(f =>
                    f.GenerateFbk(_mockSettingsBackupContent.Object))
                .Returns(new MockFileInfo(_mockFileSystemObject, @"C:\fake\path\database.fbk"));

            _mockCompressor.Setup(c =>
                    c.Compress(It.IsNotNull<string>(), It.IsNotNull<string>(), It.IsNotNull<string>()))
                        .Callback<string, string, string>((sourceDirectory, targetFilePath, searchPattern) =>
                        {
                            _mockFileSystemObject.AddFile(targetFilePath, MockFileData.NullObject);
                        })
                        .Returns(true);

            var unitUnderTest = CreateService();

            // Act
            var result = await unitUnderTest.CreateBackupFileAsync();

            // Assert
            Assert.That(result, Is.Not.Null, "File is NULL.");
            Assert.That(result.Exists, Is.True, "File NOT exists.");
            Assert.That(result.DirectoryName, Contains.Substring(@"\AppData\Local\Temp"), "File isn't in temp folder.");
            StringAssert.EndsWith(".tar.gz", result.FullName, "File doesn't have a .tar.gz extension.");
        }

        [Test]
        public async Task CreateBackupFileAsync_WhenContentDirectoryAndCompressorReturnsFalse_FileIsBad()
        {
            // Arrange
            _mockSettingsBackupContent.SetupGet(p => p.FullPath).Returns(@"C:\fake\path\database");
            _mockSettingsBackupContent.SetupGet(p => p.ContentType).Returns(ContentType.Directory);

            _mockCompressor.Setup(c =>
                    c.Compress(It.IsNotNull<string>(), It.IsNotNull<string>()))
                .Returns(false);

            var unitUnderTest = CreateService();

            // Act
            var result = await unitUnderTest.CreateBackupFileAsync();

            // Assert
            Assert.That(result, Is.Not.Null, "FileInfo is NULL.");
            Assert.That(result.Exists, Is.False, "File not exists.");
        }

        [Test]
        public async Task CreateBackupFileAsync_WhenContentDirectoryCompressorReturnsTrue_FileInfoIsOk()
        {
            // Arrange
            _mockSettingsBackupContent.SetupGet(p => p.FullPath).Returns(@"C:\fake\path\");
            _mockSettingsBackupContent.SetupGet(p => p.ContentType).Returns(ContentType.Directory);

            _mockCompressor.Setup(c =>
                    c.Compress(It.IsNotNull<string>(), It.IsNotNull<string>()))
                .Callback<string, string>((sourceDirectory, targetFilePath) =>
                {
                    _mockFileSystemObject.AddFile(targetFilePath, MockFileData.NullObject);
                })
                .Returns(true);

            var unitUnderTest = CreateService();

            // Act
            var result = await unitUnderTest.CreateBackupFileAsync();

            // Assert
            Assert.That(result, Is.Not.Null, "File is NULL.");
            Assert.That(result.Exists, Is.True, "File NOT exists.");
            Assert.That(result.DirectoryName, Contains.Substring(@"\AppData\Local\Temp"), "File isn't in temp folder.");
            StringAssert.EndsWith(".tar.gz", result.FullName, "File doesn't have a .tar.gz extension.");
        }

        //[Test]
        //public void DownloadFile_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var unitUnderTest = CreateService();
        //    BackupFile backupFile = TODO;
        //    string localPath = TODO;

        //    // Act
        //    unitUnderTest.DownloadFile(
        //        backupFile,
        //        localPath);

        //    // Assert
        //    Assert.Fail();
        //}

        //[Test]
        //public void GetBackupFiles_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var unitUnderTest = CreateService();
        //    string remotePath = TODO;

        //    // Act
        //    var result = unitUnderTest.GetBackupFiles(
        //        remotePath);

        //    // Assert
        //    Assert.Fail();
        //}

        //[Test]
        //public void GetBackupFiles_StateUnderTest_ExpectedBehavior1()
        //{
        //    // Arrange
        //    var unitUnderTest = CreateService();

        //    // Act
        //    var result = unitUnderTest.GetBackupFiles();

        //    // Assert
        //    Assert.Fail();
        //}

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _mockFileSystemObject = new MockFileSystem();
            _mockFirebirdService = _mockRepository.Create<IFirebirdService>();
            _mockLoggerObject = LogManager.GetCurrentClassLogger();

            _mockSettingsBackupContent = _mockRepository.Create<ISettingsBackupContent>();

            _mockSettings = _mockRepository.Create<ISettings>();
            _mockSettings.SetupGet(s => s.BackupContent).Returns(_mockSettingsBackupContent.Object);

            _mockCompressor = _mockRepository.Create<ICompressor>();
        }

        [TearDown]
        public void TearDown()
        {
            _mockRepository.VerifyAll();
        }

        //[Test]
        //public async Task UploadFile_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var unitUnderTest = CreateService();
        //    FileInfo fileInfo = TODO;
        //    string remotePath = TODO;

        //    // Act
        //    var result = await unitUnderTest.UploadFile(
        //        fileInfo,
        //        remotePath);

        //    // Assert
        //    Assert.Fail();
        //}

        private BackupService CreateService()
        {
            return new BackupService(_mockFileSystemObject, _mockLoggerObject, _mockCompressor.Object, _mockSettings.Object, _mockFirebirdService.Object);
        }
    }
}