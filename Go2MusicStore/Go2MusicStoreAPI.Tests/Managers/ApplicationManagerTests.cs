namespace Go2MusicStore.API.Tests.Managers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Go2MusicStore.API.Interfaces.Managers;
    using Go2MusicStore.Platform.Implementation.Managers;

    using NSubstitute;

    [TestClass]
    public class ApplicationManagerTests : TestClassBase
    {
        private ApplicationManager applicationManager;

        [TestInitialize]
        public void Setup()
        {
            var storeManager = Substitute.For<IStoreManager>();
            this.applicationManager = new ApplicationManager(storeManager);
        }

        [TestMethod]
        public void ShouldContainStoreManager()
        {
            var storeManager = this.applicationManager.StoreManager;
            Assert.IsNotNull(storeManager);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.applicationManager.Dispose();
        }
    }
}
