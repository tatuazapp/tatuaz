import { Button } from "@chakra-ui/react"
import { ArrowUpRight } from "@styled-icons/bootstrap"
import { Heading, Paragraph } from "@tatuaz/ui"
import { FormattedMessage } from "react-intl"
import { theme } from "../../../../styles/theme"
import {
  ArtistCardBacktroundPhoto,
  ArtistCardUserPhoto,
  ArtistCardWrapper,
  UserDataWrapper,
  UserSectionWrapper,
} from "./styles"

const ArtistsCard = () => {
  const x = 1
  return (
    <ArtistCardWrapper>
      <ArtistCardBacktroundPhoto>
        <ArtistCardUserPhoto />
      </ArtistCardBacktroundPhoto>
      <UserSectionWrapper>
        <UserDataWrapper>
          <Heading color={theme.colors.primary} level={5}>
            Kelvin Sam
          </Heading>
          <Paragraph level={4} padding="8px 0px 12px 0px" textAlign="justify">
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
            eiusmod tempor incididunt ut labore et dolore magna aliqua.
            Consequat ac felis donec et odio pellentesque diam volutpat commodo.
            Venenatis cras sedfelis eget. Quis hendrerit dolor magna eget est
            lorem ipsum.
          </Paragraph>
        </UserDataWrapper>
        <Button
          color={theme.colors.background1}
          colorScheme="primary"
          rightIcon={<ArrowUpRight size={24} />}
          size="md"
        >
          <FormattedMessage defaultMessage="Zrób tatuaż" id="z4IFca" />
        </Button>
      </UserSectionWrapper>
    </ArtistCardWrapper>
  )
}
export default ArtistsCard
