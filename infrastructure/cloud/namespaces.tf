resource "kubernetes_namespace" "tatuaz-test" {
  metadata {
    name = "tatuaz-test"
    labels = {
      "cert-manager.io/disable-validation" = true
    }
  }
}