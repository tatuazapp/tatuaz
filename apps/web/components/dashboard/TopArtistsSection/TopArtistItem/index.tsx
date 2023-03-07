import { Paragraph } from "@tatuaz/ui"
import {
  ArtistPhoto,
  ArtistWrapper,
  TopArtistsSectionArtistListItem,
  VisitArtistIcon,
} from "./styles"

const TopArtistsItem = () => (
  <TopArtistsSectionArtistListItem>
    <ArtistWrapper>
      <ArtistPhoto />
      <div>
        <Paragraph level={2}>Steve Burn</Paragraph>
        <Paragraph level={4}>California</Paragraph>
      </div>
    </ArtistWrapper>
    <VisitArtistIcon />
  </TopArtistsSectionArtistListItem>
)

export default TopArtistsItem
