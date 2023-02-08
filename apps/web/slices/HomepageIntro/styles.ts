import { down } from "styled-breakpoints"
import styled from "styled-components"
// import { rem } from "../../styles/utils"

export const HomepageIntroWrapper = styled.div`
  min-width: 300px;
  max-width: 600px;
  color: ${({ theme }) => theme.colors.secondary};
`
export const TitleWrapper = styled.div`
  width: 100%;
`
export const TitleFirstLineWrapper = styled.div`
  display: flex;

  ${down("md")} {
    margin-bottom: ${({ theme }) => theme.sizes.medium};
  }
`
export const TitleFirstLineTitleWrapper = styled.div`
  padding-right: ${({ theme }) => theme.sizes.small};
  font-size: ${({ theme }) => theme.sizes.xxlarge};
  line-height: ${({ theme }) => theme.sizes.xxlarge};

  ${down("md")} {
    font-size: ${({ theme }) => theme.sizes.xlarge};
    line-height: ${({ theme }) => theme.sizes.large};
  }

  ${down("sm")} {
    font-size: ${({ theme }) => theme.sizes.xlarge};
    line-height: ${({ theme }) => theme.sizes.large};
  }
`

export const Slider = styled.div`
  display: flex;
  flex: 1;
  align-items: center;
  /* vertical-align: middle; */
  min-width: 20%;

  ${down("sm")} {
    display: none;
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

export const TitleSecondLineWrapper = styled.div`
  font-size: ${({ theme }) => theme.sizes.xxlarge};
  line-height: ${({ theme }) => theme.sizes.xxxlarge};

  ${down("md")} {
    font-size: ${({ theme }) => theme.sizes.xlarge};
    line-height: ${({ theme }) => theme.sizes.large};
  }

  ${down("sm")} {
    font-size: ${({ theme }) => theme.sizes.xlarge};
    line-height: ${({ theme }) => theme.sizes.large};
  }
`

export const DescriptionWrapper = styled.div`
  margin-top: ${({ theme }) => theme.sizes.large};
  margin-bottom: ${({ theme }) => theme.sizes.large};
  text-align: justify;
`
