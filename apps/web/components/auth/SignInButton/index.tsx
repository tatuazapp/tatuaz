import { useAuth0 } from "@auth0/auth0-react"
import { Button } from "@tatuaz/ui"

export const SignInButton = () => {
  const { loginWithRedirect } = useAuth0()

  return <Button onClick={() => loginWithRedirect()}>Young Login</Button>
}
