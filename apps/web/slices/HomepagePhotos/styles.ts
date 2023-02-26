import { down } from "styled-breakpoints"
import styled from "styled-components"

export const HomepagePhotosWrapper = styled.div`
  width: 100%;
  max-width: 600px;
  margin-left: ${({ theme }) => theme.sizes.xxxxlarge};

  color: ${({ theme }) => theme.colors.secondary};
  color: ${({ theme }) => theme.colors.secondary};

  ${down("md")} {
    margin-right: ${({ theme }) => theme.sizes.small};
    margin-bottom: ${({ theme }) => theme.sizes.xlarge};
    margin-left: ${({ theme }) => theme.sizes.small};
  }
`

export const PhotosContainer = styled.div`
  display: flex;
`

export const LeftPhotosContainer = styled.div`
  width: 60%;
  margin-right: ${({ theme }) => theme.sizes.large};

  ${down("sm")} {
    margin-right: ${({ theme }) => theme.sizes.small};
  }
`

export const RightPhotosContainer = styled.div`
  display: flex;
  flex-direction: column;
  width: 40%;
`

export const RightTopPhotoContainer = styled.div`
  height: 60%;
  margin-bottom: ${({ theme }) => theme.sizes.small};
  margin-bottom: ${({ theme }) => theme.sizes.large};

  ${down("sm")} {
    margin-bottom: ${({ theme }) => theme.sizes.small};
  }
`

export const RightBottomPhotoContainer = styled.div`
  height: 40%;
`

export const Slider = styled.div`
  display: flex;
  flex: 1;
  align-items: center;

  min-width: 20%;
  margin-top: ${({ theme }) => theme.sizes.small};
  margin-left: ${({ theme }) => theme.sizes.large};

  ${down("md")} {
    display: none;
  }

  ${down("sm")} {
    display: flex;
    margin-left: ${({ theme }) => theme.sizes.small};
  }
`

export const SliderTrack = styled.div`
  width: 100%;
  height: ${({ theme }) => theme.sizes.xxxxsmall};
  background-color: ${({ theme }) => theme.colors.primary};
`

export const SliderThumb = styled.div`
  display: inline-block;

  width: ${({ theme }) => theme.sizes.xsmall};
  height: ${({ theme }) => theme.sizes.xsmall};

  background-color: ${({ theme }) => theme.colors.primary};
  border-radius: 50%;
`
