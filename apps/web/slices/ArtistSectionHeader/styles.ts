import { down } from "styled-breakpoints"
import styled from "styled-components"

export const ArtistsSectionHeaderWrapper = styled.div`
  min-width: 300px;
  padding-top: ${({ theme }) => theme.sizes.xxxlarge};
  color: ${({ theme }) => theme.colors.secondary};

  ${down("md")} {
    padding-top: ${({ theme }) => theme.sizes.xxlarge};
  }

  ${down("sm")} {
    display: flex;
    flex-direction: column;
    align-items: center;
    padding-top: ${({ theme }) => theme.sizes.medium};
  }
`

export const HeaderFirstLineWrapper = styled.div`
  display: flex;
  align-items: center;
  justify-content: start;
`
export const FirstLineTextWrapper = styled.div`
  padding-right: ${({ theme }) => theme.sizes.large};
  font-size: ${({ theme }) => theme.sizes.xxlarge};
  font-weight: 700;
  line-height: ${({ theme }) => theme.sizes.xxlarge};

  ${down("md")} {
    font-size: ${({ theme }) => theme.sizes.xlarge};
    font-weight: 600;
    line-height: ${({ theme }) => theme.sizes.xxlarge};
  }

  ${down("sm")} {
    padding-right: 0;
    font-size: ${({ theme }) => theme.sizes.large};
    line-height: ${({ theme }) => theme.sizes.large};
  }
`

export const HeaderSecondLineWrapper = styled.div`
  display: flex;
  align-items: flex-end;
`

export const SecondLineTextWrapper = styled.div`
  padding-right: ${({ theme }) => theme.sizes.xxxlarge};
  font-size: ${({ theme }) => theme.sizes.xxlarge};
  font-weight: 700;
  line-height: ${({ theme }) => theme.sizes.xxxlarge};

  ${down("md")} {
    font-size: ${({ theme }) => theme.sizes.xlarge};
    font-weight: 600;
    line-height: ${({ theme }) => theme.sizes.xlarge};
  }

  ${down("sm")} {
    padding-right: 0;
    font-size: ${({ theme }) => theme.sizes.large};
    line-height: ${({ theme }) => theme.sizes.large};
  }
`

export const SecondLineSlider = styled.div`
  display: flex;
  flex: 1;
  align-items: center;
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
