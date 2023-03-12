import { PrismicRichText, SliceComponentProps } from "@prismicio/react"
import { SuitDiamondFill } from "@styled-icons/bootstrap/SuitDiamondFill"
import { FunctionComponent } from "react"
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
import { theme } from "../../styles/theme"
import { ArtistSectionHeaderSlice } from "../../types.generated"

type ArtistSectionHeaderProps = SliceComponentProps<ArtistSectionHeaderSlice>

const ArtistSectionHeader: FunctionComponent<ArtistSectionHeaderProps> = ({
  slice,
}) => (
  <ArtistsSectionHeaderWrapper>
    <HeaderFirstLineWrapper>
      <FirstLineTextWrapper>
        {slice.primary.ArtistSectionHeaderFirstLine ? (
          <PrismicRichText field={slice.primary.ArtistSectionHeaderFirstLine} />
        ) : (
          <h2>-</h2>
        )}
      </FirstLineTextWrapper>
      <SuitDiamondFill color={theme.colors.primary} size={34} />
    </HeaderFirstLineWrapper>
    <HeaderSecondLineWrapper>
      <SecondLineTextWrapper>
        {slice.primary.ArtistSectionHeaderSecondLine ? (
          <PrismicRichText
            field={slice.primary.ArtistSectionHeaderSecondLine}
          />
        ) : (
          <h2>-</h2>
        )}
      </SecondLineTextWrapper>
      <SecondLineSlider>
        <SliderTrack />
        <SliderThumb />
      </SecondLineSlider>
    </HeaderSecondLineWrapper>
  </ArtistsSectionHeaderWrapper>
)

export default ArtistSectionHeader
