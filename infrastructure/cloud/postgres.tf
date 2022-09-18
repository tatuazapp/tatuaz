resource "azurerm_postgresql_server" "tatuaz-test" {
  name                = "${var.prefix}-postgres"
  location            = var.zone
  resource_group_name = azurerm_resource_group.tatuaz-test.name
  tags                = var.tags

  administrator_login          = var.postgres.admin_login
  administrator_login_password = var.postgres.admin_password

  sku_name   = "B_Gen5_1"
  version    = 11
  storage_mb = 8192

  backup_retention_days        = 7
  geo_redundant_backup_enabled = false
  auto_grow_enabled            = false

  public_network_access_enabled = true
  ssl_enforcement_enabled       = true
  ssl_minimal_tls_version_enforced = "TLS1_2"
}