resource "kubernetes_service" "gateway" {
  metadata {
    name = var.app.gateway
  }
  spec {
    selector = {
      app = kubernetes_deployment.gateway.metadata.0.labels.app
    }
    port {
      port = var.load-balancer-port.port
      target_port = var.load-balancer-port.target_port
    }
    type = "LoadBalancer"
  }
} 
