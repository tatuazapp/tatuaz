data "azurerm_container_registry" "main" {
  name                = "tatuazmainacr"
  resource_group_name = "tatuaz-main"
}