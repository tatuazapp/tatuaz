import { Carousel } from "react-responsive-carousel"
import ArtistCard from "./ArtistCard"
import { ArtistsSectionWrapper } from "./styles"

const ArtistSection = () => {
  const tmp = "Kk"
  return (
    <ArtistsSectionWrapper>
      <Carousel showArrows={true} width="2000px">
        <ArtistCard />
        <ArtistCard />
      </Carousel>
    </ArtistsSectionWrapper>
  )
}

export default ArtistSection
