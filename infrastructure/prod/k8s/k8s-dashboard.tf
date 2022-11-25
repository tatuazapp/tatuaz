resource "kubernetes_deployment" "k8s_dashboard" {
  metadata {
    name = "dashboard-deployment"
    labels = {
      app = "dashboard"
    }
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }

  spec {
    replicas = 1

    selector {
      match_labels = {
        app = "dashboard",
      }
    }

    template {
      metadata {
        labels = {
          app  = "dashboard"
          name = "dashboard-pod"
        }
      }

      spec {
        termination_grace_period_seconds = 30

        container {
          name              = "dashboard"
          image             = "tatuazmainacr.azurecr.io/dashboard:latest"
          image_pull_policy = "Always"
          port {
            container_port = 80
          }

          env_from {
            secret_ref {
              name = kubernetes_secret.k8s_dashboard.metadata[0].name
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

resource "kubernetes_secret" "k8s_dashboard" {
  metadata {
    name      = "dashboard-secrets"
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }

  data = {
    RabbitMq__Host                  = "queue-service.tatuaz.svc.cluster.local"
    RabbitMq__VirtualHost           = "/"
    RabbitMq__Username              = var.k8s_queue.default_user
    RabbitMq__Password              = var.k8s_queue.default_password
    ConnectionStrings__TatuazMainDb = "Server=postgres-service-lb.tatuaz.svc.cluster.local;Port=5432;Database=TatuazMainDb;User Id=${var.k8s_postgres.admin_login};Password=${var.k8s_postgres.admin_password};"
  }
}
