namespace PepperShop.Cart.API.Settings
{
    /// <summary>
    /// Represents the settings required to connect to a Cosmos DB instance.
    /// </summary>
    public class DatabaseSettings
    {
        /// <summary>
        /// Gets or sets the connection string used to connect to the Cosmos DB instance.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the authentication key used to authenticate with the Cosmos DB instance.
        /// </summary>
        public string AuthKey { get; set; }

        /// <summary>
        /// Gets or sets the name of the database in the Cosmos DB instance.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the name of the collection (or container) within the database in the Cosmos DB instance.
        /// </summary>
        public string ContainerName { get; set; }
    }
}
