resource "kubernetes_deployment" "gateway" {
  metadata {
    name = var.app.gateway
    labels = {
      app = var.app.gateway
    }
  }
  spec {
    replicas = 2

    selector {
      match_labels = {
        app = var.app.gateway
      }
    }
    template {
      metadata {
        labels = {
          app = var.app.gateway
        }
      }
      spec {
        container {
          image = var.docker-image.gateway
          name  = var.app.gateway
          port {
            name = var.gateway-port.name
            container_port = var.gateway-port.container_port
          }
        }
      }
    }
  }
}
