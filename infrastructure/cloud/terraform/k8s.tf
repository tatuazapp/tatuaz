resource "azurerm_kubernetes_cluster" "tatuaz-test" {
  name                = "${var.prefix}-k8s"
  location            = var.zone
  resource_group_name = azurerm_resource_group.tatuaz-test.name
  dns_prefix          = "${var.prefix}-k8s"
  tags                = var.tags

  default_node_pool {
    name            = var.cluster_configuration.name
    node_count      = var.cluster_configuration.node_count
    vm_size         = var.cluster_configuration.vm_size
    os_disk_size_gb = var.cluster_configuration.os_disk_size_gb
  }

  service_principal {
    client_id     = var.aks_service_principal_app_id
    client_secret = var.aks_service_principal_client_secret
  }
}

data "azurerm_kubernetes_cluster" "tatuaz-test" {
  name                = azurerm_kubernetes_cluster.tatuaz-test.name
  resource_group_name = azurerm_resource_group.tatuaz-test.name
}