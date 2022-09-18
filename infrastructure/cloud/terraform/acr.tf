resource "azurerm_container_registry" "tatuaz-test" {
  name                = var.acr_configuration.name
  resource_group_name = azurerm_resource_group.tatuaz-test.name
  location            = var.zone
  sku                 = var.acr_configuration.sku
  tags                = var.tags
}

resource "azurerm_role_assignment" "tatuaz-test" {
  principal_id                     = var.aks_service_principal_app_id
  role_definition_name             = var.role_assignment_configuration.role_definition_name
  scope                            = azurerm_container_registry.tatuaz-test.id
  skip_service_principal_aad_check = var.role_assignment_configuration.skip_service_principal_aad_check
}