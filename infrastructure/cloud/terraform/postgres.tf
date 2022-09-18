resource "azurerm_postgresql_server" "tatuaz-test" {
  name                = "${var.prefix}-postgres"
  location            = var.zone
  resource_group_name = azurerm_resource_group.tatuaz-test.name
  tags                = var.tags

  administrator_login          = var.postgres_admin_login
  administrator_login_password = var.postgres_admin_password

  sku_name   = var.postgres_configuration.sku_name
  version    = var.postgres_configuration.version
  storage_mb = var.postgres_configuration.storage_mb

  backup_retention_days        = var.postgres_configuration.backup_retention_days
  geo_redundant_backup_enabled = var.postgres_configuration.geo_redundant_backup_enabled
  auto_grow_enabled            = var.postgres_configuration.auto_grow_enabled

  public_network_access_enabled    = var.postgres_configuration.public_network_access_enabled
  ssl_enforcement_enabled          = var.postgres_configuration.ssl_enforcement_enabled
  ssl_minimal_tls_version_enforced = var.postgres_configuration.ssl_minimal_tls_version_enforced
}