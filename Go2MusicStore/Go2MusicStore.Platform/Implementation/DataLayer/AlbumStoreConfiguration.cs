namespace Go2MusicStore.Platform.Implementation.DataLayer
{
    using System.Data.Entity;
    using System.Data.Entity.SqlServer;

    public class AlbumStoreConfiguration : DbConfiguration
    {
        /* Connection Resiliency:
         * All you have to do to enable connection resiliency is create a class in your assembly 
         * that derives from the DbConfiguration class, and in that class set the 
         * SQL Database execution strategy, which in EF is another term for retry policy. 
         */
        public AlbumStoreConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }

        // Then Change all of the catch blocks that catch DataException exceptions 
        //   so that they catch RetryLimitExceededException exceptions instead.
    }
}