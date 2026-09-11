using Azure.Core;
using Azure.Identity;
using Microsoft.Azure.Cosmos;

namespace PepperShop.Cart.API.Utilities
{
    public class DatabaseInitialisers
    {
        /// <summary>
        /// Initializes the database and containers for the application.
        /// </summary>
        public static async Task InitialiseDbStuffAsync()
        {
            // Local emulator values
            // TODO: ADD ENDPOINT AS configuration["Cosmos:Endpoint"]
            var endpoint = "https://localhost:8081";
            var emulatorKey = Environment.GetEnvironmentVariable("COSMOS_EMULATOR_KEY") ?? "";

            // Use emulator in Development; use DefaultAzureCredential in non-local environments
            CosmosClient client;
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                client = new CosmosClient(endpoint, emulatorKey);
            }
            else
            {
                TokenCredential credential = new DefaultAzureCredential();
                client = new CosmosClient(
                    accountEndpoint: endpoint,
                    tokenCredential: credential);
            }

            // TODO?: retry policy based on DatabaseResponse class
            // New instance of Database class referencing the server-side database
            Database database = await client.CreateDatabaseIfNotExistsAsync(
                id: "peppershopDatabase"
            );

            // TODO?: retry policy based on ContainerResponse class
            // New instance of Container class referencing the server-side container
            Container container = await database.CreateContainerIfNotExistsAsync(
                id: "carts",
                partitionKeyPath: "/id",
                throughput: 400
            );

            await SeedDataAsync(container);
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
