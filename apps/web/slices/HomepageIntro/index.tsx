import { PrismicRichText, SliceComponentProps } from "@prismicio/react"
import { FunctionComponent } from "react"
import { HomepageIntroSlice } from "../../types.generated"
import { FindArtistButton } from "./FindArtistButton"
import {
  HomepageIntroWrapper,
  TitleSecondLineWrapper,
  TitleWrapper,
  TitleFirstLineTitleWrapper,
  TitleFirstLineWrapper,
  SliderTrack,
  SliderThumb,
  Slider,
  DescriptionWrapper,
} from "./styles"

type HomepageIntroProps = SliceComponentProps<HomepageIntroSlice>

const HomepageIntro: FunctionComponent<HomepageIntroProps> = ({ slice }) => (
  <HomepageIntroWrapper>
    <TitleWrapper>
      <TitleFirstLineWrapper>
        <TitleFirstLineTitleWrapper>
          {slice.primary.TitleFirstLine && (
            <PrismicRichText field={slice.primary.TitleFirstLine} />
          )}
        </TitleFirstLineTitleWrapper>
        <Slider>
          <SliderTrack />
          <SliderThumb />
        </Slider>
      </TitleFirstLineWrapper>
      <TitleSecondLineWrapper>
        {slice.primary.TitleSecondLine && (
          <PrismicRichText field={slice.primary.TitleSecondLine} />
        )}
      </TitleSecondLineWrapper>
    </TitleWrapper>
    <DescriptionWrapper>
      {slice.primary.description && (
        <PrismicRichText field={slice.primary.description} />
      )}
    </DescriptionWrapper>
    <FindArtistButton />
  </HomepageIntroWrapper>
)

export default HomepageIntro
