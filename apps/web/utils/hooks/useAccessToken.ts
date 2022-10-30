import { useAuth0 } from "@auth0/auth0-react"
import { tryCatch } from "@tatuaz/utils"

export default function useAccessToken() {
  const { getAccessTokenSilently } = useAuth0()

  return tryCatch(
    getAccessTokenSilently({
      audience: process.env.NEXT_PUBLIC_AUTH0_API_AUDIENCE,
      scope: "read:current_user",
    })
  )
}
