import styled from "styled-components"
// import { rem } from "../../styles/utils"

// export const ValueContainer = styled.div<{ length: number }>`
//   width: 80%;
//   margin-bottom: ${({ length, theme }) =>
//     length < 10
//       ? theme.space.medium
//       : length > 16
//       ? "0px"
//       : theme.space.xxsmall};

//   font-size: ${({ length }) =>
//     length < 10 ? rem(34) : length > 26 ? rem(22) : rem(28)};
//   font-weight: 700;
//   line-height: ${rem(32)};
//   color: ${({ theme }) => theme.colors.primary};
//   text-align: center;
// `

export const HomepageIntroWrapper = styled.section`
  max-width: 600px;
  margin: 4em auto;
  color: #ffffff;
  text-align: justify;
`
export const TitleWrapper = styled.span`
  color: #ffffff;
  padding-bottom: ${({ theme }) => theme.space.small};
`
export const TitleFirstLineWrapper = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
`
export const TitleFirstLineTitleWrapper = styled.span`
  width: 60%;
  font-size: ${({ theme }) => theme.sizes.xxlarge};
  color: ${({ theme }) => theme.colors.secondary};
`

export const Slider = styled.div`
  width: 40%;
  display: flex;
  align-items: center;
`

export const SliderTrack = styled.span`
  width: 100%;
  height: ${({ theme }) => theme.sizes.xxxsmall};
  background-color: ${({ theme }) => theme.colors.primary};
`

export const SliderThumb = styled.span`
  display: inline-block;

  width: ${({ theme }) => theme.sizes.small};
  height: ${({ theme }) => theme.sizes.small};

  background-color: ${({ theme }) => theme.colors.primary};
  border-radius: 50%;
`

export const TitleSecondLineWrapper = styled.span`
  text-align: justify;
  font-size: ${({ theme }) => theme.sizes.xxlarge};
  color: ${({ theme }) => theme.colors.secondary};
`

export const DescriptionWrapper = styled.span``
