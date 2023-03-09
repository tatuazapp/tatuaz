import { useAuth0 } from "@auth0/auth0-react"
import { useRouter } from "next/router"
import useMe from "../../api/hooks/useMe"

export const useOnboardingRedirect = () => {
  const { isAuthenticated, isLoading } = useAuth0()

  const router = useRouter()

  const user = useMe()

  if ((!isAuthenticated && !isLoading) || user) return router.push("/")
}
