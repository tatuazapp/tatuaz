resource "kubernetes_config_map" "main" {
  metadata {
    name      = "general-configmap"
    namespace = kubernetes_namespace.tatuaz-test.metadata[0].name
  }

  data = {
    db_host = kubernetes_service.postgres.spec[0].cluster_ip
  }
}
