resource "kubernetes_ingress_v1" "tatuaz-test" {
  metadata {
    name      = "main-ingress"
    namespace = kubernetes_namespace.tatuaz-test.metadata[0].name
    annotations = {
      "cert-manager.io/cluster-issuer"        = "letsencrypt"
      "nginx.ingress.kubernetes.io/use-regex" = "true"
    }
  }

  spec {
    ingress_class_name = "nginx"

    tls {
      hosts       = ["tatuaz.app"]
      secret_name = "tatuaz-test-tls"
    }

    rule {
      host = "tatuaz.app"
      http {
        path {
          backend {
            service {
              name = kubernetes_service.web.metadata[0].name
              port {
                number = 443
              }
            }
          }
          path_type = "Prefix"
          path      = "/(.*)"
        }

        path {
          backend {
            service {
              name = kubernetes_service.gateway.metadata[0].name
              port {
                number = 443
              }
            }
          }
          path_type = "Prefix"
          path      = "/api(/|$)(.*)"
        }
      }
    }
  }
}
