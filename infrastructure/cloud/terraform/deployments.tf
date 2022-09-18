resource "kubernetes_deployment" "tatuaz-test-gateway" {
  metadata {
    name = "gateway-deployment"
    labels = {
      deployment = "gateway"
    }
  }

  spec {
    replicas = 2

    selector {
      match_labels = {
        deployment = "gateway"
      }
    }

    template {
      metadata {
        labels = {
          deployment = "gateway"
        }
      }

      spec {
        container {
          image = "tatuazapptestacr.azurecr.io/gateway:latest"
          name  = "gateway-prod"

          resources {
            limits = {
              cpu    = "0.5"
              memory = "512Mi"
            }
            requests = {
              cpu    = "250m"
              memory = "50Mi"
            }
          }

          liveness_probe {
            http_get {
              path = "/weatherforecast"
              port = 80
            }

            initial_delay_seconds = 3
            period_seconds        = 3
          }
        }
      }
    }
  }
}
