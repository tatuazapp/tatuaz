data "azurerm_container_registry" "tatuaz-test" {
  name                = "tatuazmainacr"
  resource_group_name = "tatuaz-main"
}

resource "azurerm_role_assignment" "tatuaz-test" {
  principal_id                     = var.az_principal.app_id
  role_definition_name             = "AcrPull"
  scope                            = data.azurerm_container_registry.tatuaz-test.id
  skip_service_principal_aad_check = true
}