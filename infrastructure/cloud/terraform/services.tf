resource "kubernetes_service" "tatuaz-test-gateway" {
  metadata {
    name = "gateway-service"
  }
  spec {
    selector = {
      app = "gateway"
    }
    session_affinity = "ClientIP"
    port {
      port        = 80
      target_port = 80
    }

    type = "LoadBalancer"
  }
}

resource "kubernetes_service" "tatuaz-test-web" {
  metadata {
    name = "web-service"
  }
  spec {
    selector = {
      app = "web"
    }
    session_affinity = "ClientIP"
    port {
      port        = 3333
      target_port = 3333
    }

    type = "LoadBalancer"
  }
}