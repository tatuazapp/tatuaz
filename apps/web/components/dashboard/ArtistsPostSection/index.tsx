import ArtistPost from "./ArtistPost"
import ArtistsPostSectionButtonArea from "./ArtistPostSectionButtonsArea"
import { ArtistPostSectionContainer, ArtistPostSectionWrapper } from "./styles"

const ArtistsPostSection = () => {
  const tmp = "Kk"
  return (
    <ArtistPostSectionWrapper>
      <ArtistsPostSectionButtonArea />
      <ArtistPostSectionContainer>
        <ArtistPost />
        <ArtistPost />
        <ArtistPost />
      </ArtistPostSectionContainer>
    </ArtistPostSectionWrapper>
  )
}

export default ArtistsPostSection
