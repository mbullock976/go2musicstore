namespace Go2MusicStore.API.Tests.Managers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Go2MusicStore.API.Interfaces.Managers;
    using Go2MusicStore.Platform.Implementation.Managers;

    using NSubstitute;

    [TestClass]
    public class StoreManagerTests : TestClassBase
    {
        private StoreManager storeManager;

        [TestInitialize]
        public void Setup()
        {
            var albumManager = Substitute.For<IAlbumManager>();
            var storeAccountManager = Substitute.For<IStoreAccountManager>();
            var securityManager = Substitute.For<ISecurityManager>();
            this.storeManager = new StoreManager(albumManager, storeAccountManager, securityManager);
        }

        [TestMethod]
        public void ShouldContainAlbumManager()
        {
            var albumManager = this.storeManager.AlbumManager;
            Assert.IsNotNull(albumManager);
        }

        [TestMethod]
        public void ShouldContainStoreAccountManager()
        {
            var storeAccountManager = this.storeManager.StoreAccountManager;
            Assert.IsNotNull(storeAccountManager);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.storeManager.Dispose();
        }
    }
}