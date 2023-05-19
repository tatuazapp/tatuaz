import { useAuth0 } from "@auth0/auth0-react"
import { useQuery } from "@tanstack/react-query"
import { useRouter } from "next/router"
import { api } from "../apiClient"
import { queryKeys } from "../queryKeys"

type PoorMansError = {
  message: string
  response: {
    status: number
  }
}

const useMe = () => {
  const { isAuthenticated, isLoading } = useAuth0()

  const router = useRouter()

  const { data } = useQuery([queryKeys.whoAmI], api.identity.me, {
    onError: async (error: PoorMansError) => {
      if (error.response.status === 401) {
        api.setSecurityData(null)
      }

      // User data has not been saved to our database yet
      if (error.response.status === 403) {
        router.push("/onboarding")
      }
    },

    refetchOnMount: false,
    refetchOnWindowFocus: false,
    retry: false,
    enabled: isAuthenticated,
  })

  if (!isAuthenticated && !isLoading) return null

  if (!data) return undefined

  return { ...data?.value }
}

export default useMe
