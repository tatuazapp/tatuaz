resource "kubernetes_deployment" "web" {
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
          app  = "web"
          name = "web-pod"
        }
      }

      spec {
        termination_grace_period_seconds = 30

        container {
          name              = "web"
          image             = "tatuazmainacr.azurecr.io/web:latest"
          image_pull_policy = "Always"
          port {
            container_port = 443
          }

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
              port = 443
            }

            initial_delay_seconds = 15
            period_seconds        = 15
          }
        }
      }
    }
  }
}

resource "kubernetes_service" "web" {
  metadata {
    name      = "web-service"
    namespace = kubernetes_namespace.tatuaz-test.metadata[0].name
  }
  spec {
    selector = {
      app = "web"
    }
    port {
      port        = 443
      target_port = 443
    }
  }
}
