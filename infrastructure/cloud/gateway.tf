resource "kubernetes_deployment" "gateway" {
  metadata {
    name = "gateway-deployment"
    labels = {
      app = "gateway"
    }
    namespace = kubernetes_namespace.tatuaz-test.metadata[0].name
  }

  spec {
    replicas = 2

    selector {
      match_labels = {
        app = "gateway",
      }
    }

    template {
      metadata {
        labels = {
          app  = "gateway"
          name = "gateway-pod"
        }
      }

      spec {
        termination_grace_period_seconds = 30

        container {
          name              = "gateway"
          image             = "tatuazmainacr.azurecr.io/gateway:latest"
          image_pull_policy = "Always"
          port {
            container_port = 80
          }

          resources {
            limits = {
              cpu    = "500m"
              memory = "512Mi"
            }
            requests = {
              cpu    = "250m"
              memory = "50Mi"
            }
          }

          liveness_probe {
            http_get {
              path = "/api"
              port = 80
            }

            initial_delay_seconds = 15
            period_seconds        = 15
          }
        }
      }
    }
  }
}

resource "kubernetes_service" "gateway" {
  metadata {
    name      = "gateway-service"
    namespace = kubernetes_namespace.tatuaz-test.metadata[0].name
  }
  spec {
    selector = {
      app = "gateway"
    }
    port {
      port        = 443
      target_port = 80
    }
  }
}
