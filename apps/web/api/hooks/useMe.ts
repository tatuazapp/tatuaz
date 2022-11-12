import { useAuth0 } from "@auth0/auth0-react"
import { useQuery } from "@tanstack/react-query"
import { api } from "../apiClient"
import { queryKeys } from "../queryKeys"

type PoorMansError = {
  status: number
  error: {
    message: string
    stack: string
  }
  ok: boolean
}

const useMe = () => {
  const { user, isAuthenticated, isLoading } = useAuth0()

  const { refetch, data } = useQuery(
    [queryKeys.whoAmI],
    api.users.whoAmICreate,
    {
      onError: async (error: PoorMansError) => {
        if (error.status === 401) {
          api.setSecurityData(undefined)
        }

        // User data has not been saved to our database yet
        if (error.status === 403) {
          await api.users.signUpCreate({
            email: user.email,
            username: user.name,
            phoneNumber: user.phone_number,
          })

          refetch()
        }
      },

      refetchOnMount: false,
      refetchOnWindowFocus: false,
      retry: false,
      enabled: isAuthenticated,
    }
  )

  if (!isAuthenticated && !isLoading) return null

  if (!data) return undefined

  return { ...data?.value }
}

export default useMe
