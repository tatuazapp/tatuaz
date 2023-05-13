resource "kubernetes_persistent_volume" "k8s_queue" {
  metadata {
    name = "queue-volume"
  }
  spec {
    storage_class_name = "queue"
    capacity = {
      storage = "1Gi"
    }
    access_modes = ["ReadWriteOnce"]
    persistent_volume_source {
      azure_disk {
        caching_mode  = "None"
        data_disk_uri = azurerm_managed_disk.az_queue_disk.id
        disk_name     = azurerm_managed_disk.az_queue_disk.name
        kind          = "Managed"
      }
    }
  }
}

resource "kubernetes_persistent_volume_claim" "k8s_queue" {
  metadata {
    name      = "queue-volume-claim"
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }

  spec {
    storage_class_name = "queue"
    access_modes       = ["ReadWriteOnce"]
    resources {
      requests = {
        storage = "1Gi"
      }
    }
    volume_name = kubernetes_persistent_volume.k8s_queue.metadata[0].name
  }
}

resource "kubernetes_deployment" "k8s_queue" {
  metadata {
    name      = "queue-server"
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }

  spec {
    replicas = 1

    selector {
      match_labels = {
        app = "queue-server"
      }
    }

    template {
      metadata {
        labels = {
          app = "queue-server"
        }
      }

      spec {
        container {
          image             = "masstransit/rabbitmq:latest"
          name              = "masstransit"
          image_pull_policy = "IfNotPresent"

          env_from {
            secret_ref {
              name = kubernetes_secret.k8s_queue.metadata[0].name
            }
          }

          volume_mount {
            mount_path = "/var/log/rabbitmq/"
            name       = "queue-mount"
          }

          volume_mount {
            mount_path = "/var/lib/rabbitmq/"
            name       = "queue-mount"
          }

          resources {
            requests = {
              memory = "128Mi"
            }
            limits = {
              memory = "128Mi"
            }
          }
        }

        volume {
          name = "queue-mount"
          persistent_volume_claim {
            claim_name = kubernetes_persistent_volume_claim.k8s_queue.metadata[0].name
          }
        }
      }
    }
  }
}

resource "kubernetes_service" "k8s_queue_lb" {
  metadata {
    name      = "queue-service-lb"
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }

  spec {
    # Tu trzeba robić cyrk https://cloud-provider-azure.sigs.k8s.io/topics/shared-ip/
    load_balancer_ip = "20.199.3.235"
    selector = {
      app = kubernetes_deployment.k8s_queue.metadata[0].name
    }
    port {
      port        = 15672
      target_port = 15672
    }
    type = "LoadBalancer"
  }
}

resource "kubernetes_service" "k8s_queue" {
  metadata {
    name      = "queue-service"
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }

  spec {
    selector = {
      app = kubernetes_deployment.k8s_queue.metadata[0].name
    }
    port {
      port        = 5672
      target_port = 5672
    }
  }
}

resource "kubernetes_secret" "k8s_queue" {
  metadata {
    name      = "queue-secrets"
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }

  data = {
    RABBITMQ_DEFAULT_USER  = var.k8s_queue.default_user
    RABBITMQ_DEFAULT_PASS  = var.k8s_queue.default_password
    RABBITMQ_DEFAULT_VHOST = "/"
  }
}
