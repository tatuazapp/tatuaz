resource "azurerm_resource_group" "tatuaz-test" {
  name     = "${var.prefix}-resource-group"
  location = var.zone
  tags     = var.tags
}