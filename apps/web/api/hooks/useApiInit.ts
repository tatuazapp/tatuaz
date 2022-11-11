import { useAuth0 } from "@auth0/auth0-react"
import { api } from "../apiClient"

const useApiInit = () => {
  const { getAccessTokenSilently } = useAuth0()

  api.setSecurityData(getAccessTokenSilently)
}

export default useApiInit
