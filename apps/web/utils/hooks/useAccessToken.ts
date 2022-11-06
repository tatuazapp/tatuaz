import { useAuth0 } from "@auth0/auth0-react"

export default function useAccessToken() {
  const { isAuthenticated, getAccessTokenSilently } = useAuth0()

  if (!isAuthenticated) return null

  return getAccessTokenSilently({
    audience: process.env.NEXT_PUBLIC_AUTH0_API_AUDIENCE,
    scope: "read:current_user",
  })
}
