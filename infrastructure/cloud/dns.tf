resource "azurerm_public_ip" "tatuaz-test" {
  name                = "${var.prefix}-publicIp"
  resource_group_name = azurerm_resource_group.tatuaz-test.name
  location            = azurerm_resource_group.tatuaz-test.location
  allocation_method   = "Static"

  tags = var.tags
}

resource "azurerm_dns_zone" "tatuaz-test" {
  name                = "tatuaz.app"
  resource_group_name = azurerm_resource_group.tatuaz-test.name
}

resource "azurerm_dns_a_record" "tatuaz-test" {
  name                = "*"
  zone_name           = azurerm_dns_zone.tatuaz-test.name
  resource_group_name = azurerm_resource_group.tatuaz-test.name
  records             = [azurerm_public_ip.tatuaz-test.ip_address]
  ttl                 = 300
}

resource "kubernetes_manifest" "tatuaz-test-letsencrypt" {
  manifest = {
    "apiVersion" = "cert-manager.io/v1"
    "kind"       = "ClusterIssuer"
    "metadata" = {
      "name" = "letsencrypt"
    }
    "spec" = {
      "acme" = {
        "email" = "lukasz.k.sobczak@email.com"
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
          },
        ]
      }
    }
  }
}