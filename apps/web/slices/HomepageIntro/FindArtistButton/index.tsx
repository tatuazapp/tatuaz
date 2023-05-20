import { useAuth0 } from "@auth0/auth0-react"
import { useRouter } from "next/router"
import { FormattedMessage } from "react-intl"
import Button from "../../../components/common/buttons/Button"

export const FindArtistButton = () => {
  const router = useRouter()
  const { loginWithRedirect, isAuthenticated, isLoading } = useAuth0()

  const isSignedIn = isAuthenticated && !isLoading

  return (
    <Button
      kind="arrowUpRight"
      onClick={
        isSignedIn ? () => router.push("/dashboard") : () => loginWithRedirect()
      }
    >
      <FormattedMessage defaultMessage="Znajdź artystę" id="okwzd8" />
    </Button>
  )
}
