// import { down } from "styled-breakpoints"
import { ArrowRightCircleFill } from "@styled-icons/bootstrap/ArrowRightCircleFill"
import { down } from "styled-breakpoints"
import styled from "styled-components"

export const ArtistCardWrapper = styled.div`
  max-width: 380px;
  /* width: 580px; */

  /* ${down("md")} {
    font-size: ${({ theme }) => theme.sizes.xlarge};
    line-height: ${({ theme }) => theme.sizes.large};
  } */

  ${down("sm")} {
    width: 100%;
  }
`

export const PhotoWrapper = styled.div``

export const Divider = styled.div`
  /* height: ${({ theme }) => theme.sizes.xxxxsmall}; */
  height: 1px;
  margin-top: ${({ theme }) => theme.sizes.medium};
  margin-bottom: ${({ theme }) => theme.sizes.medium};
  background-color: ${({ theme }) => theme.colors.background3};

  /* ${down("md")} {
    font-size: ${({ theme }) => theme.sizes.xlarge};
    line-height: ${({ theme }) => theme.sizes.large};
  } */

  ${down("sm")} {
    margin-top: ${({ theme }) => theme.sizes.large};
  }
`

export const NameSection = styled.div`
  display: flex;
  justify-content: space-between;
  padding-bottom: ${({ theme }) => theme.sizes.medium};

  /* ${down("md")} {
    font-size: ${({ theme }) => theme.sizes.xlarge};
    line-height: ${({ theme }) => theme.sizes.large};
  } */

  ${down("sm")} {
    padding-bottom: ${({ theme }) => theme.sizes.large};
  }
`

export const ArtistName = styled.p`
  font-size: ${({ theme }) => theme.sizes.large};
  font-weight: 600;
  color: ${({ theme }) => theme.colors.primary};
`

export const VisitArtistIcon = styled(ArrowRightCircleFill)`
  /* align-self: center; */
  height: 34px;
  color: ${({ theme }) => theme.colors.primary};
`

export const DescriptionWrapper = styled.div`
  font-size: ${({ theme }) => theme.sizes.small};
  font-weight: 500;
  color: ${({ theme }) => theme.colors.secondary};
`

// export const TitleFirstLineTitleWrapper = styled.div`
//   padding-right: ${({ theme }) => theme.sizes.small};
//   font-size: ${({ theme }) => theme.sizes.xxlarge};
//   line-height: ${({ theme }) => theme.sizes.xxlarge};

//   ${down("md")} {
//     font-size: ${({ theme }) => theme.sizes.xlarge};
//     line-height: ${({ theme }) => theme.sizes.large};
//   }

//   ${down("sm")} {
//     font-size: ${({ theme }) => theme.sizes.xlarge};
//     line-height: ${({ theme }) => theme.sizes.large};
//   }
// `
