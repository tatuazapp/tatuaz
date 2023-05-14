resource "kubernetes_persistent_volume" "k8s_postgres" {
  metadata {
    name = "postgres-volume"
  }
  spec {
    storage_class_name = "postgres"
    capacity = {
      storage = "4Gi"
    }
    access_modes = ["ReadWriteOnce"]
    persistent_volume_source {
      azure_disk {
        caching_mode  = "None"
        data_disk_uri = azurerm_managed_disk.az_pg_disk.id
        disk_name     = azurerm_managed_disk.az_pg_disk.name
        kind          = "Managed"
      }
    }
  }
}

resource "kubernetes_persistent_volume_claim" "k8s_postgres" {
  metadata {
    name      = "postgres-volume-claim"
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }

  spec {
    storage_class_name = "postgres"
    access_modes       = ["ReadWriteOnce"]
    resources {
      requests = {
        storage = "4Gi"
      }
    }
    volume_name = kubernetes_persistent_volume.k8s_postgres.metadata[0].name
  }
}

resource "kubernetes_deployment" "k8s_postgres" {
  metadata {
    name      = "postgres-server"
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }

  spec {
    replicas = 1

    selector {
      match_labels = {
        app = "postgres-server"
      }
    }

    template {
      metadata {
        labels = {
          app = "postgres-server"
        }
      }

      spec {
        container {
          image             = "postgis/postgis:15-master"
          name              = "postgres"
          image_pull_policy = "IfNotPresent"

          env_from {
            secret_ref {
              name = kubernetes_secret.k8s_postgres.metadata[0].name
            }
          }

          env {
            name  = "POSTGRES_SHARED_BUFFERS"
            value = "64MB"
          }

          volume_mount {
            mount_path = "/var"
            name       = "postgres-mount"
          }
        }

        volume {
          name = "postgres-mount"
          persistent_volume_claim {
            claim_name = kubernetes_persistent_volume_claim.k8s_postgres.metadata[0].name
          }
        }
      }
    }
  }
}

resource "kubernetes_service" "k8s_postgres_lb" {
  metadata {
    name      = "postgres-service-lb"
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }

  spec {
    # Tu trzeba robić cyrk https://cloud-provider-azure.sigs.k8s.io/topics/shared-ip/
    load_balancer_ip = "20.85.161.230"
    selector = {
      app = kubernetes_deployment.k8s_postgres.metadata[0].name
    }
    port {
      port        = 5432
      target_port = 5432
    }
    type = "LoadBalancer"
  }
}

resource "kubernetes_secret" "k8s_postgres" {
  metadata {
    name      = "postgres-secrets"
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
  }

  data = {
    POSTGRES_USER     = var.k8s_postgres.admin_login
    POSTGRES_PASSWORD = var.k8s_postgres.admin_password
  }
}
