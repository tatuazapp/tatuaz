import { SliceComponentProps } from "@prismicio/react"
import Image from "next/image"
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
        <Image
          priority
          alt={slice.primary.FirstPhoto.alt}
          height={slice.primary.FirstPhoto.dimensions.height}
          src={slice.primary.FirstPhoto.url}
          width={slice.primary.FirstPhoto.dimensions.width}
        />
      </LeftPhotosContainer>
      <RightPhotosContainer>
        <RightTopPhotoContainer>
          <Image
            alt={slice.primary.SecondPhoto.alt}
            height={slice.primary.SecondPhoto.dimensions.height}
            src={slice.primary.SecondPhoto.url}
            width={slice.primary.SecondPhoto.dimensions.width}
          />
        </RightTopPhotoContainer>
        <RightBottomPhotoContainer>
          <Image
            alt={slice.primary.ThirdPhoto.alt}
            height={slice.primary.ThirdPhoto.dimensions.height}
            src={slice.primary.ThirdPhoto.url}
            width={slice.primary.ThirdPhoto.dimensions.width}
          />
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
