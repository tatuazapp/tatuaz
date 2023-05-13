import { Button } from "@chakra-ui/react"
import { ArrowUpRight } from "@styled-icons/bootstrap"
import { Heading, Paragraph } from "@tatuaz/ui"
import { FunctionComponent } from "react"
import { FormattedMessage } from "react-intl"
import { theme } from "../../../../styles/theme"
import {
  ArtistCardBackgroundPhoto,
  ArtistCardUserPhoto,
  ArtistCardWrapper,
  UserDataWrapper,
  UserSectionWrapper,
} from "./styles"

type ArtistCardProps = {
  artistName: string
  artistDescription: string
  backgroundPhotoUrl?: string
  foregroundPhotoUrl: string
}

const ArtistCard: FunctionComponent<ArtistCardProps> = ({
  artistName,
  artistDescription,
  foregroundPhotoUrl,
}) => (
  <ArtistCardWrapper>
    <ArtistCardBackgroundPhoto>
      <ArtistCardUserPhoto imageUrl={foregroundPhotoUrl} />
    </ArtistCardBackgroundPhoto>
    <UserSectionWrapper>
      <UserDataWrapper>
        <Heading color={theme.colors.primary} level={5}>
          {artistName}
        </Heading>
        <Paragraph level={4} padding="8px 0px 12px 0px" textAlign="justify">
          {artistDescription}
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
export default ArtistCard
