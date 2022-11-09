variable "az_creds" {
  type = object({
    subscription_id = string
    client_id       = string
    client_secret   = string
    tenant_id       = string
  })
}

variable "az_tags" {
  type = object({
    Environment = string
  })
  default = {
    Environment = "Test"
  }
}

variable "az_rg_name" {
  type    = string
  default = "tatuaz-test"
}

variable "az_location" {
  type    = string
  default = "France Central"
}

variable "k8s_postgres" {
  type = object({
    admin_login    = string
    admin_password = string
  })
}

variable "k8s_queue" {
  type = object({
    default_user     = string
    default_password = string
  })
}
