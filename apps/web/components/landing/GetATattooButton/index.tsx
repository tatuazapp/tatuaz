import { Button } from "@chakra-ui/react"
import { ArrowUpRight } from "@styled-icons/bootstrap"
import { FormattedMessage } from "react-intl"
import { GetATattooButtonWrapper, ButtonContainer } from "./styles"

const GetATattooButton = () => (
  <GetATattooButtonWrapper>
    <ButtonContainer>
      <Button
        color="black"
        colorScheme="primary"
        rightIcon={<ArrowUpRight size={24} />}
        size="lg"
        width={{ base: "100%" }}
      >
        <FormattedMessage defaultMessage="Zrób tatuaż" id="z4IFca" />
      </Button>
    </ButtonContainer>
  </GetATattooButtonWrapper>
)

export default GetATattooButton
