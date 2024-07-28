@allowed(['test', 'prod'])
param environment string

var location = 'westeurope'

resource storageAccount 'Microsoft.Storage/storageAccounts@2022-05-01' = {
  name: 'hbfunctionstorage${environment}'
  location: location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'Storage'
}

resource functionAppPlan 'Microsoft.Web/serverfarms@2023-12-01' = {
  name: 'myFunction-app-plan-${environment}'
  location: location
  kind: 'linux'
  properties: {
    reserved: true
  }
  sku: {
    name: 'Y1'
    tier: 'Dynamic'
  }
}

resource functionApp 'Microsoft.Web/sites@2023-12-01' = {
  name: 'hb-demo-functionscicd-${environment}'
  location: location
  kind: 'functionapp'
  properties: {
    serverFarmId: functionAppPlan.id
    siteConfig: {
      linuxFxVersion: 'DOTNET-ISOLATED|8.0'
    }
  }

  resource configuration 'config@2023-12-01' = {
    name: 'appsettings'
    properties: {
      AzureWebJobsStorage: 'DefaultEndpointsProtocol=https;AccountName=${storageAccount.name};EndpointSuffix=${az.environment().suffixes.storage};AccountKey=${storageAccount.listKeys().keys[0].value}'
      WEBSITE_CONTENTAZUREFILECONNECTIONSTRING: 'DefaultEndpointsProtocol=https;AccountName=${storageAccount.name};EndpointSuffix=${az.environment().suffixes.storage};AccountKey=${storageAccount.listKeys().keys[0].value}'
      WEBSITE_CONTENTSHARE: 'function-package'
      FUNCTIONS_EXTENSION_VERSION: '~4'
      FUNCTIONS_WORKER_RUNTIME: 'dotnet-isolated'
      Server: 'DeployedFunctionsApp-${environment}'
      WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED: 1
    }
  }
}

output AppServiceName string = functionApp.name
