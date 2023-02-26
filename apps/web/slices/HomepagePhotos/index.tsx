import * as prismicH from "@prismicio/helpers"
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

const HomepagePhotos: FunctionComponent<HomepagePhotosProps> = ({ slice }) => {
  const leftPhoto = prismicH.asImageSrc(slice.primary.FirstPhoto)
  const rightTopPhoto = prismicH.asImageSrc(slice.primary.SecondPhoto)
  const rightBottomPhoto = prismicH.asImageSrc(slice.primary.ThirdPhoto)

  return (
    <HomepagePhotosWrapper>
      <PhotosContainer>
        <LeftPhotosContainer>
          <Image alt="leftPhoto" height={500} src={leftPhoto} width={55550} />
        </LeftPhotosContainer>
        <RightPhotosContainer>
          <RightTopPhotoContainer>
            <Image
              alt="rightTopPhoto"
              height={55550}
              src={rightTopPhoto}
              width={5000}
            />
          </RightTopPhotoContainer>
          <RightBottomPhotoContainer>
            <Image
              alt="rightBottomPhoto"
              height={100}
              src={rightBottomPhoto}
              width={200}
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
}

export default HomepagePhotos
