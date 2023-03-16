import ArtistCard from "./ArtistCard"
import { ArtistCardAreaWrapper } from "./styles"

const ArtistsArea = () => (
  <ArtistCardAreaWrapper>
    <ArtistCard
      artistDescription="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
          eiusmod tempor incididunt ut labore et dolore magna aliqua. Consequat
          ac felis donec et odio pellentesque diam volutpat commodo."
      artistName="Kelvin Sam"
    />
    <ArtistCard
      artistDescription="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
       eiusmod tempor incididunt ut labore et dolore magna aliqua. Consequat
       ac felis donec et odio pellentesque diam volutpat commodo."
      artistName="Andrew C"
    />
    <ArtistCard
      artistDescription="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
     eiusmod tempor incididunt ut labore et dolore magna aliqua. Consequat
     ac felis donec et odio pellentesque diam volutpat commodo."
      artistName="Samuel Buro"
    />
    <ArtistCard
      artistDescription="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
        eiusmod tempor incididunt ut labore et dolore magna aliqua. Consequat
        ac felis donec et odio pellentesque diam volutpat commodo."
      artistName="Ejczu Ebezoka"
    />
  </ArtistCardAreaWrapper>
)
export default ArtistsArea
