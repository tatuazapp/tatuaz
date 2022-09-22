resource "kubernetes_config_map" "main" {
  metadata {
    name      = "general-configmap"
    namespace = kubernetes_namespace.tatuaz-test.metadata[0].name
  }

  data = {
    db_host = azurerm_postgresql_server.main.fqdn
  }
}
