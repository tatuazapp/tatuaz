import { PrismicNextImage } from "@prismicio/next"
import { SliceComponentProps } from "@prismicio/react"
import { FunctionComponent } from "react"
import { HomepagePhotosSlice } from "../../types.generated"
import {
  HomepagePhotosWrapper,
  LeftPhotosContainer,
  PhotosContainer,
  RightBottomPhotoContainer,
  RightPhotosContainer,
  RightTopPhotoContainer,
  Slider,
  SliderThumb,
  SliderTrack,
} from "./styles"

type HomepagePhotosProps = SliceComponentProps<HomepagePhotosSlice>

const HomepagePhotos: FunctionComponent<HomepagePhotosProps> = ({ slice }) => (
  <HomepagePhotosWrapper>
    <PhotosContainer>
      <LeftPhotosContainer>
        <PrismicNextImage priority field={slice.primary.FirstPhoto} />
      </LeftPhotosContainer>
      <RightPhotosContainer>
        <RightTopPhotoContainer>
          <PrismicNextImage field={slice.primary.SecondPhoto} />
        </RightTopPhotoContainer>
        <RightBottomPhotoContainer>
          <PrismicNextImage field={slice.primary.ThirdPhoto} />
        </RightBottomPhotoContainer>
      </RightPhotosContainer>
    </PhotosContainer>

    <Slider>
      <SliderTrack />
      <SliderThumb />
    </Slider>
  </HomepagePhotosWrapper>
)

export default HomepagePhotos
