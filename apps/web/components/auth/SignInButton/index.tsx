import { useAuth0 } from "@auth0/auth0-react"
import { ArrowRight } from "@styled-icons/bootstrap/ArrowRight"
import { FormattedMessage } from "react-intl"
import Button from "../../common/buttons/Button"

export const SignInButton = () => {
  const { loginWithRedirect } = useAuth0()

  return (
    <Button
      color="black"
      kind="primary"
      rightIcon={<ArrowRight size={24} />}
      size="lg"
      onClick={() => loginWithRedirect()}
    >
      <FormattedMessage defaultMessage="Zaloguj" id="Q6A/N7" />{" "}
    </Button>
  )
}
