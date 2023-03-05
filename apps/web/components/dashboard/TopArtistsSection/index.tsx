import {
  RightSectionContainer,
  TopArtistsSectionHeader,
  TopArtistsSectionTitle,
  TopArtistsSectionViewMore,
  TopArtistsSectionWrapper,
} from "./styles"
import TopArtistsItem from "./TopArtistItem"
import UserSection from "./UserSection"

const TopArtistsSection = () => {
  const tmp = "Kk"
  return (
    <RightSectionContainer>
      <UserSection />
      <TopArtistsSectionWrapper>
        <TopArtistsSectionHeader>
          <TopArtistsSectionTitle>Top Artists</TopArtistsSectionTitle>
          <TopArtistsSectionViewMore>View more</TopArtistsSectionViewMore>
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
}

export default TopArtistsSection
