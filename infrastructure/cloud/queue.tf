resource "azurerm_servicebus_namespace" "main" {
  name                = "${var.prefix}-servicebus-namespace"
  location            = azurerm_resource_group.tatuaz-test.location
  resource_group_name = azurerm_resource_group.tatuaz-test.name
  sku                 = "Basic"
  tags                = var.tags
}
