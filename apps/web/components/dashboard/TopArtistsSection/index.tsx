import { Heading, Paragraph } from "@tatuaz/ui"
import { theme } from "../../../styles/theme"
import {
  RightSectionContainer,
  TopArtistsSectionHeader,
  TopArtistsSectionViewMore,
  TopArtistsSectionWrapper,
} from "./styles"
import TopArtistsItem from "./TopArtistItem"
import UserSection from "./UserSection"

const TopArtistsSection = () => (
  <RightSectionContainer>
    <UserSection />
    <TopArtistsSectionWrapper>
      <TopArtistsSectionHeader>
        <Heading level={4}> Top Artists</Heading>
        <TopArtistsSectionViewMore>
          <Paragraph color={theme.colors.primary} level={4}>
            View more
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
