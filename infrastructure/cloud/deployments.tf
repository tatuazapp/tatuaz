resource "kubernetes_deployment" "tatuaz-test-gateway" {
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
          app = "gateway"
        }
      }

      spec {
        termination_grace_period_seconds = 30

        container {
          name              = "gateway-prod"
          image             = "tatuazmainacr.azurecr.io/gateway:latest"
          image_pull_policy = "Always"

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

            initial_delay_seconds = 15
            period_seconds        = 15
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
    namespace = kubernetes_namespace.tatuaz-test.metadata[0].name
  }

  spec {
    replicas = 2

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
        termination_grace_period_seconds = 30

        container {
          name              = "web-prod"
          image             = "tatuazmainacr.azurecr.io/web:latest"
          image_pull_policy = "Always"

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

            initial_delay_seconds = 15
            period_seconds        = 15
          }
        }
      }
    }
  }
}
