variable "cluster" {
  default = "tatuaz-dev"
}

variable "app" {
  type = object({
    web     = string
    gateway = string
  })
  default = {
    web     = "web"
    gateway = "gateway"
  }
}

variable "docker-image" {
  type = object({
    web     = string
    gateway = string
  })
  default = {
    gateway = "web-docker-image"
    web     = "tatuaz/gateway"
  }
}

variable "gateway-port" {
  type = object({
    name           = string
    container_port = number
  })
  default = {
    name = "port-5000"
    container_port = 5000
  }
}

variable "load-balancer-port" {
  type = object({
    port        = number
    target_port = number
  })
  default = {
    port = 5000
    target_port = 80
  }
}
