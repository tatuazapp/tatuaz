import { FunctionComponent } from "react"
import {
  BottomContainer,
  DescriptionContainer,
  MainContainer,
  PhotoContainer,
  TopContainer,
} from "./styles"

type PhotoCardProps = {
  imageURL: string
  description: string
}

const PhotoCard: FunctionComponent<PhotoCardProps> = ({
  imageURL,
  description,
}) => (
  <MainContainer>
    <TopContainer>
      <PhotoContainer url={imageURL} />
    </TopContainer>
    <BottomContainer>
      <DescriptionContainer> {description}</DescriptionContainer>
    </BottomContainer>
  </MainContainer>
)

export default PhotoCard
