variable "zone" {
  type    = string
  default = "francecentral"
}

variable "tags" {
  type = object({
    Environment = string
    Team        = string
  })
  default = {
    Environment = "Test"
    Team        = "Dev"
  }
}

variable "prefix" {
  type    = string
  default = "tatuaz-test"
}

variable "cluster_configuration" {
  type = object({
    name            = string
    node_count      = number
    vm_size         = string
    os_disk_size_gb = number
  })
  default = {
    name            = "pool1"
    node_count      = 1
    vm_size         = "Standard_B2s"
    os_disk_size_gb = 32
  }
}

variable "postgres_configuration" {
  type = object({
    sku_name                         = string
    version                          = string
    storage_mb                       = number
    backup_retention_days            = number
    geo_redundant_backup_enabled     = bool
    auto_grow_enabled                = bool
    public_network_access_enabled    = bool
    ssl_enforcement_enabled          = bool
    ssl_minimal_tls_version_enforced = string
  })
  default = {
    sku_name                         = "B_Gen5_1"
    version                          = "11"
    storage_mb                       = 8192
    backup_retention_days            = 7
    geo_redundant_backup_enabled     = false
    auto_grow_enabled                = false
    public_network_access_enabled    = true
    ssl_enforcement_enabled          = true
    ssl_minimal_tls_version_enforced = "TLS1_2"
  }
}

variable "acr_configuration" {
  type = object({
    name = string
    sku  = string
  })
  default = {
    name = "tatuazapptestacr"
    sku  = "Basic"
  }
}

variable "role_assignment_configuration" {
  type = object({
    role_definition_name             = string
    skip_service_principal_aad_check = bool
  })
  default = {
    role_definition_name             = "AcrPull"
    skip_service_principal_aad_check = true
  }
}

variable "aks_service_principal_app_id" {
  type = string
}

variable "aks_service_principal_client_secret" {
  type = string
}

variable "postgres_admin_login" {
  type = string
}

variable "postgres_admin_password" {
  type = string
}