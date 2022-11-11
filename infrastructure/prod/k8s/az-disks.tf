resource "azurerm_managed_disk" "az_pg_disk" {
  name                 = "ta-postgres-disk"
  location             = var.az_location
  resource_group_name  = azurerm_resource_group.az_rg.name
  storage_account_type = "Standard_LRS"
  create_option        = "Empty"
  disk_size_gb         = "16"
  tags                 = var.az_tags
}

resource "azurerm_managed_disk" "az_queue_disk" {
  name                 = "ta-queue-disk"
  location             = var.az_location
  resource_group_name  = azurerm_resource_group.az_rg.name
  storage_account_type = "Standard_LRS"
  create_option        = "Empty"
  disk_size_gb         = "1"
  tags                 = var.az_tags
}
