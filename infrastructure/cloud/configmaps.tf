resource "kubernetes_config_map" "network" {
  metadata {
    name      = "general-configmap"
    namespace = kubernetes_namespace.tatuaz-test.metadata[0].name
  }

  data = {
    db_host = "idk:5432"
  }
}