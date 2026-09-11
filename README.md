# Introduction

### Prepare environment for the project
1. Install Azure Cosmos Emulator and run it locally. https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-develop-emulator?tabs=windows%2Ccsharp&pivots=api-nosql#install-the-emulator
2. Run Emulator

### Authentication
#### Client credentials PoC
1. Run IdentityServer (self-hosted launch profile)
- home url: https://localhost:5001/
- discovery document: https://localhost:5001/.well-known/openid-configuration
2. Run CartService API (IIS Express launch profile)
3. Run ClientCredentialsClient

#### User credentials PoC
1. Run IdentityServer
2. Run CartService API
3. Run WebClient (WebClient launch profile)
   - you can login with username: Alice and password: alice
   - you can logout
   - you can try login with external test OIDC provider
