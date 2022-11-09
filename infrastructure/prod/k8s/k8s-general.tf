resource "kubernetes_namespace" "k8s_ns" {
  metadata {
    name = "tatuaz"
    labels = {
      "cert-manager.io/disable-validation" = true
    }
  }
}
