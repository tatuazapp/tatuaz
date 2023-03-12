import { Heading, Paragraph } from "@tatuaz/ui"
import { FormattedMessage } from "react-intl"
import {
  RightSectionContainer,
  TopArtistsSectionHeader,
  TopArtistsSectionViewMore,
  TopArtistsSectionWrapper,
} from "./styles"
import TopArtistsItem from "./TopArtistItem"
import UserSection from "./UserSection"
import { theme } from "../../../styles/theme"

const TopArtistsSection = () => (
  <RightSectionContainer>
    <UserSection />
    <TopArtistsSectionWrapper>
      <TopArtistsSectionHeader>
        <Heading level={4}>
          <FormattedMessage defaultMessage="Topowi artyści" id="J5nXcl" />
        </Heading>
        <TopArtistsSectionViewMore>
          <Paragraph color={theme.colors.primary} level={4}>
            <FormattedMessage defaultMessage="Zobacz więcej" id="sP2Svl" />
          </Paragraph>
        </TopArtistsSectionViewMore>
      </TopArtistsSectionHeader>
      <div>
        <TopArtistsItem />
        <TopArtistsItem />
        <TopArtistsItem />
        <TopArtistsItem />
        <TopArtistsItem />
      </div>
    </TopArtistsSectionWrapper>
  </RightSectionContainer>
)

export default TopArtistsSection
