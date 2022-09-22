resource "azurerm_resource_group" "tatuaz-test" {
  name     = var.prefix
  location = var.zone
  tags     = var.tags
}
