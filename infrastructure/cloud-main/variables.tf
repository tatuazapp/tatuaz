variable "az_principal" {
  type = object({
    app_id          = string
    display_name    = string
    client_secret   = string
    tenant_id       = string
    subscription_id = string
  })
}

variable "tags" {
  type = object({
    Environment = string
    Team        = string
  })
  default = {
    Environment = "Main"
    Team        = "CICD"
  }
}
