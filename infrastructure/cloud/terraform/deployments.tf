resource "kubernetes_deployment" "tatuaz-test-gateway" {
  metadata {
    name = "gateway-deployment"
    labels = {
      app = "gateway"
    }
  }

  spec {
    replicas = 1

    selector {
      match_labels = {
        app = "gateway",
      }
    }

    template {
      metadata {
        labels = {
          app = "gateway"
        }
      }

      spec {
        container {
          image = "tatuazapptestacr.azurecr.io/tatuaz/gateway:latest"
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

resource "kubernetes_deployment" "tatuaz-test-web" {
  metadata {
    name = "web-deployment"
    labels = {
      app = "web"
    }
  }

  spec {
    replicas = 1

    selector {
      match_labels = {
        app = "web"
      }
    }

    template {
      metadata {
        labels = {
          app = "web"
        }
      }

      spec {
        container {
          image = "tatuazapptestacr.azurecr.io/tatuaz/web:latest"
          name  = "web-prod"

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
              path = "/"
              port = 3333
            }

            initial_delay_seconds = 3
            period_seconds        = 3
          }
        }
      }
    }
  }
}
