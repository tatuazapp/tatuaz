import { down } from "styled-breakpoints"
import styled from "styled-components"
import { rem } from "../../styles/utils"

export const HomepageIntroWrapper = styled.div`
  min-width: ${rem(300)};
  max-width: ${rem(600)};
  margin-right: ${({ theme }) => theme.sizes.xxxxlarge};
  color: ${({ theme }) => theme.colors.secondary};

  ${down("md")} {
    min-width: 0;
    margin-right: 0;
  }
`

export const TitleFirstLineWrapper = styled.div`
  display: flex;
`

export const Slider = styled.div`
  display: flex;
  flex: 1;
  align-items: center;

  min-width: 20%;
  padding-left: ${({ theme }) => theme.sizes.small};

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

export const DescriptionWrapper = styled.div`
  margin-top: ${({ theme }) => theme.sizes.large};
  margin-bottom: ${({ theme }) => theme.sizes.large};
  text-align: justify;
`
