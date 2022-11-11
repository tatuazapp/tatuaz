provider "azurerm" {
  features {}
  subscription_id = var.az_creds.subscription_id
  client_id       = var.az_creds.client_id
  client_secret   = var.az_creds.client_secret
  tenant_id       = var.az_creds.tenant_id
}

provider "kubernetes" {
  host                   = azurerm_kubernetes_cluster.az_aks.kube_config.0.host
  client_certificate     = base64decode(azurerm_kubernetes_cluster.az_aks.kube_config.0.client_certificate)
  client_key             = base64decode(azurerm_kubernetes_cluster.az_aks.kube_config.0.client_key)
  cluster_ca_certificate = base64decode(azurerm_kubernetes_cluster.az_aks.kube_config.0.cluster_ca_certificate)
  experiments {
    manifest_resource = true
  }
}

provider "helm" {
  kubernetes {
    host                   = azurerm_kubernetes_cluster.az_aks.kube_config.0.host
    client_certificate     = base64decode(azurerm_kubernetes_cluster.az_aks.kube_config.0.client_certificate)
    client_key             = base64decode(azurerm_kubernetes_cluster.az_aks.kube_config.0.client_key)
    cluster_ca_certificate = base64decode(azurerm_kubernetes_cluster.az_aks.kube_config.0.cluster_ca_certificate)
  }
}
