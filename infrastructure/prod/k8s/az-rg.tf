resource "azurerm_resource_group" "az_rg" {
  name     = var.az_rg_name
  location = var.az_location
  tags     = var.az_tags
}
