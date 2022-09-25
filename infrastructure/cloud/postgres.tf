resource "azurerm_managed_disk" "postgres" {
  name                 = "${var.prefix}-postgres-disk"
  location             = azurerm_resource_group.tatuaz-test.location
  resource_group_name  = azurerm_resource_group.tatuaz-test.name
  storage_account_type = "Standard_LRS"
  create_option        = "Empty"
  disk_size_gb         = "16"
  tags                 = var.tags
}

resource "kubernetes_persistent_volume" "postgres" {
  metadata {
    name      = "${var.prefix}-postgres-volume"
  }
  spec {
    storage_class_name = var.prefix
    capacity = {
      storage = "16Gi"
    }
    access_modes = ["ReadWriteOnce"]
    persistent_volume_source {
      azure_disk {
        caching_mode  = "None"
        data_disk_uri = azurerm_managed_disk.postgres.id
        disk_name     = azurerm_managed_disk.postgres.name
        kind          = "Managed"
      }
    }
  }
}

resource "kubernetes_persistent_volume_claim" "postgres" {
  metadata {
    name      = "${var.prefix}-postgres-volume-claim"
    namespace = kubernetes_namespace.tatuaz-test.metadata[0].name
  }

  spec {
    storage_class_name = var.prefix
    access_modes = ["ReadWriteOnce"]
    resources {
      requests = {
        storage = "16Gi"
      }
    }
    volume_name = "${kubernetes_persistent_volume.postgres.metadata.0.name}"
  }
}

resource "kubernetes_deployment" "postgres" {
  metadata {
    name      = "${var.prefix}-postgres-server"
    namespace = kubernetes_namespace.tatuaz-test.metadata[0].name
  }

  spec {
    replicas = 1

    selector {
      match_labels = {
        app = "postgres"
      }
    }

    template {
      metadata {
        labels = {
          app = "postgres"
        }
      }

      spec {
        container {
          image = "postgres:14"
          name  = "postgres"
          image_pull_policy = "IfNotPresent"

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

          port {
            container_port = 5432
          }

          env_from {
            secret_ref {
              name = kubernetes_secret.backend.metadata[0].name
            }
          }

          volume_mount {
            mount_path = "/var"
            name = "postgres-mount"
          }
        }

        volume {
          name = "postgres-mount"
          persistent_volume_claim {
            claim_name = kubernetes_persistent_volume_claim.postgres.metadata[0].name
          }
        }
      }
    }
  }
}

resource "kubernetes_service" "postgres" {
  metadata {
    name = "${var.prefix}-postgres-service"
    namespace = kubernetes_namespace.tatuaz-test.metadata[0].name
  }

  spec {
    selector = {
      app = kubernetes_deployment.postgres.metadata[0].name
    }
    port {
      port        = 5432
      target_port = 5432
    }
  }
}
