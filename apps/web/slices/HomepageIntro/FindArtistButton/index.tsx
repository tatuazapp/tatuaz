import { Button } from "@chakra-ui/react"
import { ArrowUpRight } from "@styled-icons/bootstrap/ArrowUpRight"
import { FormattedMessage } from "react-intl"

export const FindArtistButton = () => (
  <Button
    color="black"
    colorScheme="primary"
    rightIcon={<ArrowUpRight size={24} />}
    size="lg"
    width={{ base: "100%", md: "auto" }}
  >
    <FormattedMessage defaultMessage="Znajdź artystę" id="okwzd8" />
  </Button>
)
