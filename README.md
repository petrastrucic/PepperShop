# PepperShop
## Prepare environment
1. Install Azure Cosmos Emulator and run it locally. https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-develop-emulator?tabs=windows%2Ccsharp&pivots=api-nosql#install-the-emulator
2. Run Emulator
3. In `appsettings.Development.json` file in the solution, at `AuthKey` set your local emulator key

## Implementation PoCs
### Authentication
#### Client credentials PoC
1. Run IdentityServer (self-hosted launch profile)
   - home url: `https://localhost:5001/`
   - discovery document: `https://localhost:5001/.well-known/openid-configuration`
2. Run CartService API (https launch profile)
3. Run ClientCredentialsClient

#### User credentials PoC
1. Run IdentityServer (self-hosted launch profile)
2. Run CartService API (https launch profile)
3. Run WebClient (WebClient launch profile)
   - you can login with username: Alice and password: alice
   - you can logout
   - you can try login with external test OIDC provider

### Test Cart API using Open API (Swagger)
1. Run IdentityServer (self-hosted launch profile)
2. Run CartService API (https launch profile)
3. Go to `https://localhost:4001/swagger/index.html`.

   - `/cart` endpoint with userId param = `someRandomId` fetches seeded cart item
   - `/cart` endpoint with userId param empty space throws `400 BadRequest` and a validation message
   - `/cart` endpoint with userId param set to any other throws `400 BadRequest` and a validation message
   - you can observe x-correlation-id in the response header 
   - `/identity` endpoint produces `401 Unauthorized` response since Swagger is not configured as one of the trusted clients

### Serilog
Basic logging is added to the project. You can observe local logs on the path `**\PepperShop\Cart service\PepperShop.Cart.API\logs`.

### Health check
Self-service health check is added for the API and it can be checked on the URL `https://localhost:4001/health/self`. Custom check for Cosmos db connection is also added and it can be checked on the URL `https://localhost:4001/health/cosmosdb`. If you exit local Cosmos storage emulator, health checks displays unhealthy status but it takes some time to load the result because of the retry policy.

### CorrelationId middleware
Reads, sets and propagates correlation id in HTTP requests and should also automatically log it using Serilog.
//TODO: fix Serilog

### AutoMapper
Added simple AutoMapper configuration as a starting point for more complex mappings that could be expected later on in the development.

### Unit tests
Initial test using nUnit and Moq libraries.
