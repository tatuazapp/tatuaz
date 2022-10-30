import { useAuth0 } from "@auth0/auth0-react"
import { Button } from "@chakra-ui/react"
import { FormattedMessage } from "react-intl"

export const SignInButton = () => {
  const { loginWithRedirect } = useAuth0()

  return (
    <Button colorScheme="black" size="lg" onClick={() => loginWithRedirect()}>
      <FormattedMessage defaultMessage="Zaloguj" id="Q6A/N7" />
    </Button>
  )
}
