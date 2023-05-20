import { useAuth0 } from "@auth0/auth0-react"
import { useRouter } from "next/router"
import { FormattedMessage } from "react-intl"
import Button from "../../common/buttons/Button"
import { GetATattooButtonWrapper, ButtonContainer } from "./styles"

const GetATattooButton = () => {
  const router = useRouter()
  const { loginWithRedirect, isAuthenticated, isLoading } = useAuth0()

  const isSignedIn = isAuthenticated && !isLoading

  return (
    <GetATattooButtonWrapper>
      <ButtonContainer>
        <Button
          kind="arrowUpRight"
          onClick={
            isSignedIn
              ? () => router.push("/dashboard")
              : () => loginWithRedirect()
          }
        >
          <FormattedMessage defaultMessage="Zrób tatuaż" id="z4IFca" />
        </Button>
      </ButtonContainer>
    </GetATattooButtonWrapper>
  )
}

export default GetATattooButton
