resource "kubernetes_secret" "backend" {
  metadata {
    name      = "main-db"
    namespace = kubernetes_namespace.tatuaz-test.metadata[0].name
  }

  data = {
    POSTGRES_USER                 = var.postgres.admin_login
    POSTGRES_PASSWORD             = var.postgres.admin_password
    SERVICE_BUS_CONENCTION_STRING = azurerm_servicebus_namespace.main.default_primary_connection_string
  }
}
