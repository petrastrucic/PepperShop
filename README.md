# Introduction

### Prepare environment for the project
1. Install Azure Cosmos Emulator and run it locally. https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-develop-emulator?tabs=windows%2Ccsharp&pivots=api-nosql#install-the-emulator
2. Run Emulator
3. In `appsettings.Development.json` file in the solution, at `AuthKey` set your local emulator key

### Authentication
#### Client credentials PoC
1. Run IdentityServer (self-hosted launch profile)
   - home url: `https://localhost:5001/`
   - discovery document: `https://localhost:5001/.well-known/openid-configuration`
2. Run CartService API (https launch profile)
3. Run ClientCredentialsClient

#### User credentials PoC
1. Run IdentityServer
2. Run CartService API
3. Run WebClient (WebClient launch profile)
   - you can login with username: Alice and password: alice
   - you can logout
   - you can try login with external test OIDC provider

### Test Cart API using Open API (Swagger)
1. Run all three services
2. Go to `https://localhost:4001/swagger/index.html`.
   
   At this time, Swagger is not configured as one of the trusted clients, so only `/identity` endpoint and PoCs prior to this one are a showcase on authentication and authorisation while `/cart` endpoint is here to showcase implementation of other requirements  
   - `/identity` endpoint, 401 is expected since Swagger is not added as authenticated client yet
   - `/cart` endpoint, you should see content of user's cart
