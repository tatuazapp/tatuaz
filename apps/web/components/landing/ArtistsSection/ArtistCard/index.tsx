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
    <PhotoWrapper>
      <img
        alt="Artist"
        height={100}
        src="https://picsum.photos/200/300"
        width={100}
      />
    </PhotoWrapper>
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
