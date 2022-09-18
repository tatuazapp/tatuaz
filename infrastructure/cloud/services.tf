resource "kubernetes_service" "tatuaz-test-gateway" {
  metadata {
    name      = "gateway-service"
    namespace = kubernetes_namespace.tatuaz-test.metadata[0].name
  }
  spec {
    selector = {
      app = "gateway"
    }
    port {
      port        = 80
      target_port = 80
    }
  }
}


resource "kubernetes_service" "tatuaz-test-web" {
  metadata {
    name      = "web-service"
    namespace = kubernetes_namespace.tatuaz-test.metadata[0].name
  }
  spec {
    selector = {
      app = "web"
    }
    port {
      port        = 3333
      target_port = 3333
    }
  }
}