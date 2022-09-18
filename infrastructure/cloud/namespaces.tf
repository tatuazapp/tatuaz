resource "kubernetes_namespace" "tatuaz-test" {
  metadata {
    name = "tatuaz-test"
    labels = {
      "environment" = "test"
    }
  }
}