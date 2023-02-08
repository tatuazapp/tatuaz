import { PrismicRichText, SliceComponentProps } from "@prismicio/react"
import { SuitDiamondFill } from "@styled-icons/bootstrap/SuitDiamondFill"
import { FunctionComponent } from "react"
import { theme } from "../../styles/theme"
import { ArtistSectionHeaderSlice } from "../../types.generated"
import {
  ArtistsSectionHeaderWrapper,
  HeaderFirstLineWrapper,
  HeaderSecondLineWrapper,
  FirstLineTextWrapper,
  SecondLineTextWrapper,
  SecondLineSlider,
  SliderTrack,
  SliderThumb,
} from "./styles"

type ArtistSectionHeaderProps = SliceComponentProps<ArtistSectionHeaderSlice>

const ArtistSectionHeader: FunctionComponent<ArtistSectionHeaderProps> = ({
  slice,
}) => (
  <ArtistsSectionHeaderWrapper>
    <HeaderFirstLineWrapper>
      <FirstLineTextWrapper>
        {/* EXPLORE */}

        {slice.primary.ArtistSectionHeaderFirstLine ? (
          <PrismicRichText
            field={slice.primary.ArtistSectionHeaderSecondLine}
          />
        ) : (
          <h2>Template slice, update me!</h2>
        )}
      </FirstLineTextWrapper>
      <SuitDiamondFill color={theme.colors.primary} size={34} />
    </HeaderFirstLineWrapper>
    <HeaderSecondLineWrapper>
      <SecondLineTextWrapper>OUR ARTIST</SecondLineTextWrapper>
      <SecondLineSlider>
        <SliderTrack />
        <SliderThumb />
      </SecondLineSlider>
    </HeaderSecondLineWrapper>
  </ArtistsSectionHeaderWrapper>
)

export default ArtistSectionHeader
