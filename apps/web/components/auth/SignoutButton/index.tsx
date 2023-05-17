import { useAuth0 } from "@auth0/auth0-react"
import { Button, ButtonProps } from "@chakra-ui/react"
import { ArrowRight } from "@styled-icons/bootstrap/ArrowRight"
import { FunctionComponent } from "react"
import { FormattedMessage } from "react-intl"

type SignOutButtonProps = ButtonProps

export const SignOutButton: FunctionComponent<SignOutButtonProps> = ({
  ...props
}) => {
  const { logout } = useAuth0()

  return (
    <Button
      color="black"
      colorScheme="primary"
      rightIcon={<ArrowRight size={24} />}
      size="lg"
      onClick={() => logout({ returnTo: window.location.origin })}
      {...props}
    >
      <FormattedMessage defaultMessage="Wyloguj" id="UqV7Od" />{" "}
    </Button>
  )
}
