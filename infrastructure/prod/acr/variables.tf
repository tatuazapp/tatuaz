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
    Environment = "Main"
  }
}
