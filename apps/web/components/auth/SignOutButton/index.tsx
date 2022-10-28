import { useAuth0 } from "@auth0/auth0-react"
import { Button } from "@tatuaz/ui"

export const SignOutButton = () => {
  const { logout } = useAuth0()

  return (
    <Button
      onClick={() =>
        logout({ returnTo: process.env["NEXT_PUBLIC_AUTH0_REDIRECT_URI"] })
      }
    >
      Young Logout
    </Button>
  )
}
