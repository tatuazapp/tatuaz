import { useAuth0 } from "@auth0/auth0-react"
import { MenuItem } from "@chakra-ui/react"
import { FormattedMessage } from "react-intl"

const LogoutMenuItem = () => {
  const { logout } = useAuth0()

  return (
    <MenuItem
      onClick={() =>
        logout({ returnTo: process.env["NEXT_PUBLIC_AUTH0_REDIRECT_URI"] })
      }
    >
      <FormattedMessage defaultMessage="Wyloguj" id="UqV7Od" />
    </MenuItem>
  )
}

export default LogoutMenuItem
