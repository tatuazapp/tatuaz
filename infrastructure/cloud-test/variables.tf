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

variable "az_principal" {
  type = object({
    app_id          = string
    display_name    = string
    client_secret   = string
    tenant_id       = string
    subscription_id = string
  })
}

variable "postgres" {
  type = object({
    admin_login    = string
    admin_password = string
  })
}
