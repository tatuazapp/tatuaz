import { down } from "styled-breakpoints"
import styled from "styled-components"

export const ArtistsSectionHeaderWrapper = styled.div`
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

export const HeaderSecondLineWrapper = styled.div`
  display: flex;
  align-items: flex-end;
`

export const SecondLineSlider = styled.div`
  display: flex;
  flex: 1;
  align-items: center;

  min-width: 20%;
  padding-left: ${({ theme }) => theme.sizes.xxxlarge};

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
