resource "kubernetes_deployment" "k8s_gateway" {
  metadata {
    name   = "gateway-deployment"
    labels = {
      app = "gateway"
    }
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
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

          env_from {
            secret_ref {
              name = kubernetes_secret.k8s_gateway.metadata[0].name
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

resource "kubernetes_service" "k8s_gateway" {
  metadata {
    name      = "gateway-service"
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }
  spec {
    selector = {
      app = "gateway"
    }
    port {
      port        = 80
      target_port = 80
    }
    type = "NodePort"
  }
}

resource "kubernetes_secret" "k8s_gateway" {
  metadata {
    name      = "gateway-secrets"
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }

  data = {
    RabbitMq__Host                  = "queue-service.tatuaz.svc.cluster.local"
    RabbitMq__VirtualHost           = "/"
    RabbitMq__Username              = var.k8s_queue.default_user
    RabbitMq__Password              = var.k8s_queue.default_password
    ConnectionStrings__TatuazMainDb = "Server=postgres-service-lb.tatuaz.svc.cluster.local;Port=5432;Database=TatuazMainDb;User Id=${var.k8s_postgres.admin_login};Password=${var.k8s_postgres.admin_password};Pooling=true;MinPoolSize=0;MaxPoolSize=10;"
    Serilog__CloudLogLevel          = "Error"
    Serilog__BlobFileName           = "gateway-test.log"
    ASPNETCORE_ENVIRONMENT          = "Production"
  }
}
