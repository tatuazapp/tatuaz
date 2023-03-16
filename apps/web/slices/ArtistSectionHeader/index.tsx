import { PrismicRichText, SliceComponentProps } from "@prismicio/react"
import { SuitDiamondFill } from "@styled-icons/bootstrap/SuitDiamondFill"
import { Heading } from "@tatuaz/ui"
import { FunctionComponent } from "react"
import { theme } from "../../styles/theme"
import { ArtistSectionHeaderSlice } from "../../types.generated"
import useIsMobile from "../../utils/hooks/useIsMobile"
import useIsPhone from "../../utils/hooks/useIsPhone"
import useIsSmallMobile from "../../utils/hooks/useIsSmallMobile"
import {
  ArtistsSectionHeaderWrapper,
  HeaderFirstLineWrapper,
  HeaderSecondLineWrapper,
  SecondLineSlider,
  SliderTrack,
  SliderThumb,
} from "./styles"

type ArtistSectionHeaderProps = SliceComponentProps<ArtistSectionHeaderSlice>

const ArtistSectionHeader: FunctionComponent<ArtistSectionHeaderProps> = ({
  slice,
}) => {
  const isMobile = useIsMobile()
  const isPhone = useIsPhone()
  const isSmallMobile = useIsSmallMobile()
  return (
    <ArtistsSectionHeaderWrapper>
      <HeaderFirstLineWrapper>
        <Heading level={isPhone ? 3 : isMobile ? 2 : 1}>
          {slice.primary.ArtistSectionHeaderFirstLine ? (
            <PrismicRichText
              field={slice.primary.ArtistSectionHeaderFirstLine}
            />
          ) : (
            <h2>-</h2>
          )}
        </Heading>

        <SuitDiamondFill color={theme.colors.primary} size={34} />
      </HeaderFirstLineWrapper>
      <HeaderSecondLineWrapper>
        <Heading
          level={isPhone ? 3 : isMobile ? 2 : 1}
          textAlign={isSmallMobile ? "center" : "left"}
        >
          {slice.primary.ArtistSectionHeaderSecondLine ? (
            <PrismicRichText
              field={slice.primary.ArtistSectionHeaderSecondLine}
            />
          ) : (
            <h2>-</h2>
          )}
        </Heading>
        <SecondLineSlider>
          <SliderTrack />
          <SliderThumb />
        </SecondLineSlider>
      </HeaderSecondLineWrapper>
    </ArtistsSectionHeaderWrapper>
  )
}

export default ArtistSectionHeader
