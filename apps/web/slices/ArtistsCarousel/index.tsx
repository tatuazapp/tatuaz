import { Stack } from "@chakra-ui/react"
import { PrismicRichText, SliceComponentProps } from "@prismicio/react"
import { Heading, Paragraph } from "@tatuaz/ui"
import Image from "next/image"
import { FunctionComponent } from "react"
import Slider, { Settings } from "react-slick"
import { useTheme } from "styled-components"
import { breakpointsNumericValues } from "../../styles/theme"
import { ArtistsCarouselSlice } from "../../types.generated"
import {
  CarouselItemWrapper,
  CarouselWrapper,
  Divider,
  ImageWrapper,
} from "./styles"

type ArtistsCarouselProps = SliceComponentProps<ArtistsCarouselSlice>

const sliderSettings = {
  infinite: true,
  speed: 500,
  slidesToShow: 2,
  slidesToScroll: 1,
  dots: true,
  responsive: [
    {
      breakpoint: breakpointsNumericValues.xl,
      settings: {
        slidesToShow: 3,
        slidesToScroll: 3,
        infinite: true,
      },
    },
    {
      breakpoint: breakpointsNumericValues.md,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 2,
        initialSlide: 2,
        arrows: false,
      },
    },
    {
      breakpoint: breakpointsNumericValues.sm,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
      },
    },
  ],
} satisfies Settings

const ArtistsCarousel: FunctionComponent<ArtistsCarouselProps> = ({
  slice,
}) => {
  const theme = useTheme()

  return (
    <CarouselWrapper>
      <Slider {...sliderSettings}>
        {slice.items.map((item, index) => (
          <CarouselItemWrapper key={index}>
            <ImageWrapper>
              <Image
                alt={item.photo.alt}
                height={item.photo.dimensions.height}
                src={item.photo.url}
                width={item.photo.dimensions.width}
              />
            </ImageWrapper>
            <Divider />
            <Stack>
              <Heading color={theme.colors.primary} level={4}>
                <PrismicRichText field={item.title} />
              </Heading>
              <Paragraph level={1}>
                <PrismicRichText field={item.description} />
              </Paragraph>
            </Stack>
          </CarouselItemWrapper>
        ))}
      </Slider>
    </CarouselWrapper>
  )
}

export default ArtistsCarousel
