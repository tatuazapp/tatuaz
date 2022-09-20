resource "kubernetes_secret" "backend" {
  metadata {
    name = "main-db"
  }

  data = {
    username = var.postgres.admin_login
    password = var.postgres.admin_password
  }
}