import { useAuth0 } from "@auth0/auth0-react"
import { Button } from "../../common/Button"

export const SignOutButton = () => {
  const { logout } = useAuth0()

  return <Button onClick={() => logout()}>Young Logout</Button>
}
