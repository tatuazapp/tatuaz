terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "3.25.0"
    }
  }
}

provider "azurerm" {
  features {}
  subscription_id = var.az_creds.subscription_id
  client_id       = var.az_creds.client_id
  client_secret   = var.az_creds.client_secret
  tenant_id       = var.az_creds.tenant_id
}

resource "azurerm_resource_group" "tatuaz-main" {
  name     = "tatuaz-main"
  location = "francecentral"
  tags     = var.az_tags
}

resource "azurerm_container_registry" "tatuaz-main" {
  name                = "tatuazmainacr"
  resource_group_name = azurerm_resource_group.tatuaz-main.name
  location            = azurerm_resource_group.tatuaz-main.location
  sku                 = "Basic"
  admin_enabled       = false
  tags                = var.az_tags
}
