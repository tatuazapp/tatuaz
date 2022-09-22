resource "kubernetes_secret" "main" {
  metadata {
    name      = "general-secrets"
    namespace = kubernetes_namespace.tatuaz-test.metadata[0].name
  }

  data = {
    db_username          = azurerm_postgresql_server.main.administrator_login
    db_password          = azurerm_postgresql_server.main.administrator_login_password
    sb_connection_string = azurerm_servicebus_namespace.main.default_primary_connection_string
  }
}
