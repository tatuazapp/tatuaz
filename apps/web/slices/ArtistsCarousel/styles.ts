import { down } from "styled-breakpoints"
import styled from "styled-components"
import { rem } from "../../styles/utils"

export const ImageWrapper = styled.div`
  width: 100%;
  height: ${rem(400)};

  ${down("md")} {
    max-height: ${rem(250)};
  }

  img {
    width: 100%;
    height: 100%;
    object-fit: cover;
    object-position: center;
  }
`

export const Divider = styled.div`
  width: 100%;
  height: 2px;
  margin: ${({ theme }) => theme.space.medium} 0;
  background-color: #494949;
`

export const CarouselItemWrapper = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: ${({ theme }) => theme.space.medium};
`

export const CarouselWrapper = styled.div`
  margin-top: ${({ theme }) => theme.space.medium};
  margin-bottom: ${({ theme }) => theme.space.xxlarge};

  .slick-dots li button::before {
    color: ${({ theme }) => theme.colors.primary};
  }
`
