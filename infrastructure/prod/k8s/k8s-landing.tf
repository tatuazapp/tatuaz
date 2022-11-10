resource "kubernetes_deployment" "k8s_landing" {
  metadata {
    name = "landing-deployment"
    labels = {
      app = "landing"
    }
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }

  spec {
    replicas = 1

    selector {
      match_labels = {
        app = "landing",
      }
    }

    template {
      metadata {
        labels = {
          app  = "landing"
          name = "landing-pod"
        }
      }

      spec {
        termination_grace_period_seconds = 30

        container {
          name              = "landing"
          image             = "tatuazmainacr.azurecr.io/landing:latest"
          image_pull_policy = "Always"
          port {
            container_port = 80
          }

          env_from {
            secret_ref {
              name = kubernetes_secret.k8s_landing.metadata[0].name
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

resource "kubernetes_secret" "k8s_landing" {
  metadata {
    name      = "landing-secrets"
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }

  data = {
    RabbitMq__Host                  = kubernetes_service.k8s_queue.spec[0].cluster_ip
    RabbitMq__VirtualHost           = "/"
    RabbitMq__Username              = var.k8s_queue.default_user
    RabbitMq__Password              = var.k8s_queue.default_password
    ConnectionStrings__TatuazMainDb = "Server=${kubernetes_service.k8s_postgres_lb.spec[0].cluster_ip};Port=5432;Database=TatuazMainDb;User Id=${var.k8s_postgres.admin_login};Password=${var.k8s_postgres.admin_password};"
  }
}
