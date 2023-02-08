import {
  ArtistCardWrapper,
  ArtistName,
  DescriptionWrapper,
  Divider,
  NameSection,
  PhotoWrapper,
  VisitArtistIcon,
} from "./styles"

const ArtistCard = () => (
  <ArtistCardWrapper>
    <PhotoWrapper>photo</PhotoWrapper>
    <Divider />
    <NameSection>
      <ArtistName>Artist Name</ArtistName>
      <VisitArtistIcon />
    </NameSection>
    <DescriptionWrapper>
      Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
      tempor incididunt ut labore et dolore magna aliqua.
    </DescriptionWrapper>
  </ArtistCardWrapper>
)

export default ArtistCard
