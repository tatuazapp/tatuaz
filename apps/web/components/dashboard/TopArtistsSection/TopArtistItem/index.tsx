import {
  ArtistData,
  ArtistLocation,
  ArtistName,
  ArtistPhoto,
  ArtistWrapper,
  TopArtistsSectionArtistListItem,
  VisitArtistIcon,
} from "./styles"

const TopArtistsItem = () => (
  <TopArtistsSectionArtistListItem>
    <ArtistWrapper>
      <ArtistPhoto />
      <ArtistData>
        <ArtistName>Steve Burn</ArtistName>
        <ArtistLocation>California</ArtistLocation>
      </ArtistData>
    </ArtistWrapper>

    <VisitArtistIcon />
  </TopArtistsSectionArtistListItem>
)

export default TopArtistsItem
