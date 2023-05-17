import { useAuth0 } from "@auth0/auth0-react"
import { api } from "../apiClient"

const useApiInit = () => {
  const { getAccessTokenSilently, isAuthenticated, isLoading } = useAuth0()

  if (!isAuthenticated && !isLoading) {
    api.setSecurityData(null)
    return null
  }

  api.setSecurityData(getAccessTokenSilently)
}

export default useApiInit
