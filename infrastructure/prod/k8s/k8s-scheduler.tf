resource "kubernetes_deployment" "k8s_scheduler" {
  metadata {
    name = "scheduler-deployment"
    labels = {
      app = "scheduler"
    }
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }

  spec {
    replicas = 1

    selector {
      match_labels = {
        app = "scheduler",
      }
    }

    template {
      metadata {
        labels = {
          app  = "scheduler"
          name = "scheduler-pod"
        }
      }

      spec {
        termination_grace_period_seconds = 30

        container {
          name              = "scheduler"
          image             = "tatuazmainacr.azurecr.io/scheduler:latest"
          image_pull_policy = "Always"

          env_from {
            secret_ref {
              name = kubernetes_secret.k8s_scheduler.metadata[0].name
            }
          }
        }
      }
    }
  }
}

resource "kubernetes_secret" "k8s_scheduler" {
  metadata {
    name      = "scheduler-secrets"
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }

  data = {
    RabbitMq__Host                  = "queue-service.tatuaz.svc.cluster.local"
    RabbitMq__VirtualHost           = "/"
    RabbitMq__Username              = var.k8s_queue.default_user
    RabbitMq__Password              = var.k8s_queue.default_password
    ConnectionStrings__TatuazMainDb = "Server=postgres-service-lb.tatuaz.svc.cluster.local;Port=5432;Database=TatuazMainDb;User Id=${var.k8s_postgres.admin_login};Password=${var.k8s_postgres.admin_password};Pooling=true;MinPoolSize=0;MaxPoolSize=10;"
    Serilog__CloudLogLevel          = "Error"
    Serilog__BlobFileName           = "scheduler-test.log"
    ASPNETCORE_ENVIRONMENT          = "Production"
  }
}
