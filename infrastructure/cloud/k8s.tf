resource "azurerm_kubernetes_cluster" "tatuaz-test" {
  name                = "${var.prefix}-k8s"
  location            = var.zone
  resource_group_name = azurerm_resource_group.tatuaz-test.name
  dns_prefix          = "${var.prefix}-k8s"
  tags                = var.tags

  default_node_pool {
    name            = "pool1"
    node_count      = 1
    vm_size         = "Standard_B2s"
    os_disk_size_gb = 32
  }

  service_principal {
    client_id     = var.az_principal.app_id
    client_secret = var.az_principal.client_secret
  }
}

data "azurerm_kubernetes_cluster" "tatuaz-test" {
  name                = azurerm_kubernetes_cluster.tatuaz-test.name
  resource_group_name = azurerm_resource_group.tatuaz-test.name
}