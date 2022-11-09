resource "kubernetes_deployment" "k8s_gateway" {
  metadata {
    name = "gateway-deployment"
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
    # Tu trzeba robić cyrk https://cloud-provider-azure.sigs.k8s.io/topics/shared-ip/
    load_balancer_ip = "20.19.111.17"
    selector = {
      app = "gateway"
    }
    port {
      port        = 80
      target_port = 80
    }
    type = "LoadBalancer"
  }
}

resource "kubernetes_secret" "k8s_gateway" {
  metadata {
    name      = "gateway-secrets"
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }

  data = {
    RabbitMq__Host                  = kubernetes_service.k8s_queue.spec[0].cluster_ip
    RabbitMq__VirtualHost           = "/"
    RabbitMq__Username              = var.k8s_queue.default_user
    RabbitMq__Password              = var.k8s_queue.default_password
    ConnectionStrings__TatuazMainDb = "Server=${kubernetes_service.k8s_postgres.spec[0].cluster_ip};Port=5432;Database=TatuazMainDb;User Id=${var.k8s_postgres.admin_login};Password=${var.k8s_postgres.admin_password};"
  }
}
