namespace Go2MusicStore.API.Tests.Managers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Go2MusicStore.API.Interfaces.Managers;
    using Go2MusicStore.Models;
    using Go2MusicStore.Platform.Implementation.Managers;
    using Go2MusicStore.Platform.Interfaces.DataLayer;

    using NSubstitute;

    [TestClass]
    public class AlbumManagerTests : TestClassBase
    {
        private IAlbumManager albumManager;        

        private IUnitOfWork unitOfWorkMock;

        [TestInitialize]
        public void Setup()
        {
            this.unitOfWorkMock = Substitute.For<IUnitOfWork>();
            this.albumManager = new AlbumManager(this.unitOfWorkMock);
        }

        [TestMethod]
        public void AddGenre()
        {
            var genreModel = Substitute.For<Genre>();

            this.albumManager.Add(genreModel);

            this.unitOfWorkMock.Received(1).GetRepository<Genre>().Insert(genreModel);
        }

        [TestMethod]
        public void GetGenres()
        {
            this.albumManager.Get<Genre>();
            this.unitOfWorkMock.Received(1).GetRepository<Genre>().Get();
        }

        [TestMethod]
        public void UpdateGenre()
        {
            var genreModel = Substitute.For<Genre>();
            this.albumManager.Update<Genre>(genreModel);
            this.unitOfWorkMock.Received(1).GetRepository<Genre>().Update(genreModel);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.albumManager.Dispose();
        }
    }
}