import { PrismicRichText, SliceComponentProps } from "@prismicio/react"
import { Heading, Paragraph } from "@tatuaz/ui"
import { FunctionComponent } from "react"
import { HomepageIntroSlice } from "../../types.generated"
import useIsPhone from "../../utils/hooks/useIsPhone"
import useIsTablet from "../../utils/hooks/useIsTablet"
import { FindArtistButton } from "./FindArtistButton"
import {
  HomepageIntroWrapper,
  TitleFirstLineWrapper,
  SliderTrack,
  SliderThumb,
  Slider,
  DescriptionWrapper,
} from "./styles"

type HomepageIntroProps = SliceComponentProps<HomepageIntroSlice>

const HomepageIntro: FunctionComponent<HomepageIntroProps> = ({ slice }) => {
  const isTablet = useIsTablet()
  const isPhone = useIsPhone()
  return (
    <HomepageIntroWrapper>
      <TitleFirstLineWrapper>
        <Heading level={isPhone ? 3 : isTablet ? 2 : 1}>
          {slice.primary.TitleFirstLine ? (
            <PrismicRichText field={slice.primary.TitleFirstLine} />
          ) : (
            <h1>-</h1>
          )}
        </Heading>
        <Slider>
          <SliderTrack />
          <SliderThumb />
        </Slider>
      </TitleFirstLineWrapper>
      <Heading level={isPhone ? 3 : isTablet ? 2 : 1}>
        {slice.primary.TitleSecondLine ? (
          <PrismicRichText field={slice.primary.TitleSecondLine} />
        ) : (
          <h1>-</h1>
        )}
      </Heading>
      <DescriptionWrapper>
        <Paragraph level={2}>
          {slice.primary.description ? (
            <PrismicRichText field={slice.primary.description} />
          ) : (
            <h2>-</h2>
          )}
        </Paragraph>
      </DescriptionWrapper>
      <FindArtistButton />
    </HomepageIntroWrapper>
  )
}

export default HomepageIntro
