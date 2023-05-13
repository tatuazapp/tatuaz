resource "azurerm_kubernetes_cluster" "az_aks" {
  name                = "ta-aks"
  resource_group_name = azurerm_resource_group.az_rg.name
  location            = var.az_location
  tags                = var.az_tags

  dns_prefix = "aks"
  sku_tier   = "Free"

  default_node_pool {
    name                = "np1"
    node_count          = 1
    vm_size             = "Standard_B2s"
    os_disk_size_gb     = 32
    tags                = var.az_tags
    enable_auto_scaling = false
  }
  identity {
    type = "SystemAssigned"
  }
}
