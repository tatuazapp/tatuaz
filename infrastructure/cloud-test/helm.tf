resource "helm_release" "nginx" {
  name       = "nginx-controller"
  repository = "https://kubernetes.github.io/ingress-nginx"
  chart      = "ingress-nginx"
  namespace  = kubernetes_namespace.tatuaz-test.metadata[0].name

  set {
    name  = "controller.replicaCount"
    value = "1"
  }

  set {
    name  = "controller.service.externalTrafficPolicy"
    value = "Local"
  }
}

resource "helm_release" "cert-manager" {
  name       = "cert-manager"
  repository = "https://charts.jetstack.io"
  chart      = "cert-manager"
  version    = "1.9.1"
  namespace  = kubernetes_namespace.tatuaz-test.metadata[0].name

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
    value = "v1.9.1"
  }

  set {
    name  = "webhook.image.repository"
    value = "tatuazmainacr.azurecr.io/jetstack/cert-manager-webhook"
  }

  set {
    name  = "webhook.image.tag"
    value = "v1.9.1"
  }

  set {
    name  = "cainjector.image.repository"
    value = "tatuazmainacr.azurecr.io/jetstack/cert-manager-cainjector"
  }

  set {
    name  = "cainjector.image.tag"
    value = "v1.9.1"
  }
}
