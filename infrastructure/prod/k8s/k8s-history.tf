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

          env_from {
            secret_ref {
              name = kubernetes_secret.k8s_history.metadata[0].name
            }
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
    RabbitMq__Host                  = "queue-service.tatuaz.svc.cluster.local"
    RabbitMq__VirtualHost           = "/"
    RabbitMq__Username              = var.k8s_queue.default_user
    RabbitMq__Password              = var.k8s_queue.default_password
    ConnectionStrings__TatuazHistDb = "Server=postgres-service-lb.tatuaz.svc.cluster.local;Port=5432;Database=TatuazHistDb;User Id=${var.k8s_postgres.admin_login};Password=${var.k8s_postgres.admin_password};"
  }
}
