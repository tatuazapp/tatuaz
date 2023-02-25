import { useAuth0 } from "@auth0/auth0-react"
import { useQuery } from "@tanstack/react-query"
import { useRouter } from "next/router"
import { api } from "../../api/apiClient"
import { queryKeys } from "../../api/queryKeys"

export const useOnboardingRedirect = () => {
  const { isAuthenticated, isLoading } = useAuth0()

  const router = useRouter()

  useQuery([queryKeys.whoAmI], api.identity.me, {
    onSuccess: (data) => {
      // user data has been saved to our database
      if (data.success) {
        router.push("/")
      }
    },
  })

  if (!isAuthenticated && !isLoading) return router.push("/")
}
