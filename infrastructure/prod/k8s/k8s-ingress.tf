resource "kubernetes_ingress_v1" "k8s_main_ingress" {
  metadata {
    name      = "ta-main-ingress"
    namespace = kubernetes_namespace.k8s_ns.metadata[0].name
    annotations = {
      "cert-manager.io/cluster-issuer"           = "letsencrypt"
      "nginx.ingress.kubernetes.io/ssl-redirect" = "false"
    }
  }

  spec {
    ingress_class_name = "nginx"

    tls {
      hosts       = ["api.tatuaz.app"]
      secret_name = "tatuaz-test-tls"
    }

    rule {
      host = "api.tatuaz.app"
      http {
        path {
          path = "/"
          backend {
            service {
              name = kubernetes_service.k8s_gateway.metadata[0].name
              port {
                number = 80
              }
            }
          }
        }
      }
    }
  }
}

resource "helm_release" "k8s_nginx_controller" {
  name       = "ta-nginx-controller"
  repository = "https://kubernetes.github.io/ingress-nginx"
  chart      = "ingress-nginx"
  namespace  = kubernetes_namespace.k8s_ns.metadata[0].name

  set {
    name  = "controller.replicaCount"
    value = "1"
  }

  set {
    name  = "controller.service.externalTrafficPolicy"
    value = "Local"
  }
}

resource "helm_release" "k8s_cert_manager" {
  name       = "ta-cert-manager"
  repository = "https://charts.jetstack.io"
  chart      = "cert-manager"
  version    = "1.10.0"
  namespace  = kubernetes_namespace.k8s_ns.metadata[0].name

  set {
    name  = "installCRDs"
    value = "true"
  }

  set {
    name  = "image.repository"
    value = "tatuazmainacr.azurecr.io/jetstack/cert-manager-controller"
  }

  set {
    name  = "image.tag"
    value = "v1.10.0"
  }

  set {
    name  = "webhook.image.repository"
    value = "tatuazmainacr.azurecr.io/jetstack/cert-manager-webhook"
  }

  set {
    name  = "webhook.image.tag"
    value = "v1.10.0"
  }

  set {
    name  = "cainjector.image.repository"
    value = "tatuazmainacr.azurecr.io/jetstack/cert-manager-cainjector"
  }

  set {
    name  = "cainjector.image.tag"
    value = "v1.10.0"
  }
}

resource "kubernetes_manifest" "k8s_letsencrypt" {
  manifest = {
    "apiVersion" = "cert-manager.io/v1"
    "kind"       = "ClusterIssuer"
    "metadata" = {
      "name" = "letsencrypt"
    }
    "spec" = {
      "acme" = {
        "email" = "lukasz.k.sobczak@gmail.com"
        "privateKeySecretRef" = {
          "name" = "letsencrypt"
        }
        "server" = "https://acme-v02.api.letsencrypt.org/directory"
        "solvers" = [
          {
            "http01" = {
              "ingress" = {
                "class" = "nginx"
              }
            }
          }
        ]
      }
    }
  }
}
