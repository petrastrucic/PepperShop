using Azure.Core;
using Azure.Identity;
using Microsoft.Azure.Cosmos;
using PepperShop.Cart.API.Settings;

namespace PepperShop.Cart.API.Utilities
{
    public class DatabaseInitiator
    {
        /// <summary>
        /// Initializes the database and containers for the application.
        /// </summary>
        public static async Task ConfigureDatabaseAsync(DatabaseSettings dbSettings)
        {
            // Use emulator in Development; use DefaultAzureCredential in non-local environments
            CosmosClient client;
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                client = new CosmosClient(dbSettings.ConnectionString, dbSettings.AuthKey);
            }
            else
            {
                TokenCredential credential = new DefaultAzureCredential();
                client = new CosmosClient(
                    accountEndpoint: dbSettings.ConnectionString,
                    tokenCredential: credential);
            }

            // TODO?: retry policy based on DatabaseResponse class
            // New instance of Database class referencing the server-side database
            Database database = await client.CreateDatabaseIfNotExistsAsync(
                id: dbSettings.DatabaseName
            );

            // TODO?: retry policy based on ContainerResponse class
            // New instance of Container class referencing the server-side container
            Container container = await database.CreateContainerIfNotExistsAsync(
                id: dbSettings.ContainerName,
                partitionKeyPath: "/id",
                throughput: 400
            );

            //await SeedDataAsync(container);
        }

        private static async Task SeedDataAsync(Container container)
        {
            // Implement your data seeding logic here
            // For example, you can create initial items in the database
            // or perform any other necessary setup tasks.

            Data.Entities.Cart item = new Data.Entities.Cart(
                "someRandomId",
                null);

            Data.Entities.Cart createdItem = await container.CreateItemAsync(
                item: item,
                partitionKey: new PartitionKey("someRandomId")
            );
        }
    }
}
