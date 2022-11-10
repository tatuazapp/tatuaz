resource "kubernetes_deployment" "k8s_history" {
  metadata {
    name = "history-deployment"
    labels = {
      app = "history"
    }
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }

  spec {
    replicas = 1

    selector {
      match_labels = {
        app = "history",
      }
    }

    template {
      metadata {
        labels = {
          app  = "history"
          name = "history-pod"
        }
      }

      spec {
        termination_grace_period_seconds = 30

        container {
          name              = "history"
          image             = "tatuazmainacr.azurecr.io/history:latest"
          image_pull_policy = "Always"
          port {
            container_port = 80
          }

          env_from {
            secret_ref {
              name = kubernetes_secret.k8s_history.metadata[0].name
            }
          }

          liveness_probe {
            http_get {
              path = "/"
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

resource "kubernetes_secret" "k8s_history" {
  metadata {
    name      = "history-secrets"
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }

  data = {
    RabbitMq__Host                  = kubernetes_service.k8s_queue.spec[0].cluster_ip
    RabbitMq__VirtualHost           = "/"
    RabbitMq__Username              = var.k8s_queue.default_user
    RabbitMq__Password              = var.k8s_queue.default_password
    ConnectionStrings__TatuazHistDb = "Server=${kubernetes_service.k8s_postgres_lb.spec[0].cluster_ip};Port=5432;Database=TatuazHistDb;User Id=${var.k8s_postgres.admin_login};Password=${var.k8s_postgres.admin_password};"
  }
}
