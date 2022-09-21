resource "azurerm_kubernetes_cluster" "main" {
  name                  = "${var.prefix}-k8s"
  location              = var.zone
  resource_group_name   = azurerm_resource_group.tatuaz-test.name
  node_resource_group   = "${azurerm_resource_group.tatuaz-test.name}-nodes"
  dns_prefix            = "${var.prefix}-k8s"
  tags                  = var.tags

  default_node_pool {
    name            = "pool1"
    node_count      = 1
    vm_size         = "Standard_B2s"
    os_disk_size_gb = 32
    node_labels = {
      "node.kubernetes.io/exclude-from-external-load-balancers" = true
    }
  }

  service_principal {
    client_id     = var.az_principal.app_id
    client_secret = var.az_principal.client_secret
  }
}
