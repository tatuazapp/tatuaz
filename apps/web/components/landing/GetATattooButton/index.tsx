import { useRouter } from "next/router"
import { FormattedMessage } from "react-intl"
import { GetATattooButtonWrapper, ButtonContainer } from "./styles"
import Button from "../../common/buttons/Button"

const GetATattooButton = () => {
  const router = useRouter()

  return (
    <GetATattooButtonWrapper>
      <ButtonContainer>
        <Button kind="arrowUpRight" onClick={() => router.push("/dashboard")}>
          <FormattedMessage defaultMessage="Zrób tatuaż" id="z4IFca" />
        </Button>
      </ButtonContainer>
    </GetATattooButtonWrapper>
  )
}

export default GetATattooButton
