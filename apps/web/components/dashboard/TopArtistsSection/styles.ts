import { ArrowRightCircleFill } from "@styled-icons/bootstrap"
import { down } from "styled-breakpoints"
import styled from "styled-components"

export const RightSectionContainer = styled.div`
  display: flex;
  flex-direction: column;
  margin-right: ${({ theme }) => theme.space.xlarge};
  margin-left: ${({ theme }) => theme.space.xxxxlarge};
  ${down("xl")} {
    margin-left: ${({ theme }) => theme.space.xlarge};
  }
`

export const TopArtistsSectionWrapper = styled.div`
  width: 285px;
  height: 484px;
  padding: ${({ theme }) => theme.space.large};

  background-color: ${({ theme }) => theme.colors.background2};
  border-radius: ${({ theme }) => theme.radius.medium};

  /* ${down("md")} {
    padding-top: ${({ theme }) => theme.sizes.xxlarge};
  } */

  /* ${down("sm")} {
    font-size: ${({ theme }) => theme.sizes.xlarge};
  } */
`

export const TopArtistsSectionHeader = styled.div`
  display: flex;
  justify-content: space-between;
`

export const TopArtistsSectionTitle = styled.p`
  font-size: ${({ theme }) => theme.sizes.large};
  font-weight: 600;
  color: ${({ theme }) => theme.colors.secondary};
`

export const TopArtistsSectionViewMore = styled.button`
  align-self: flex-end;
  font-size: ${({ theme }) => theme.sizes.xsmall};
  font-weight: 400;
  color: ${({ theme }) => theme.colors.primary};
`
export const TopArtistsSectionArtistList = styled.div``

export const TopArtistsSectionArtistListItem = styled.div`
  display: flex;
  justify-content: space-between;
  padding-top: ${({ theme }) => theme.space.large};
`

export const ArtistPhoto = styled.div`
  display: inline-block;

  width: 25px;
  height: 25px;

  background-color: #bbb;
  border-radius: 50%;
`

export const ArtistData = styled.div`
  color: ${({ theme }) => theme.colors.secondary};
`

export const ArtistName = styled.div`
  font-size: ${({ theme }) => theme.sizes.small};
  font-weight: 500;
`
export const ArtistLocation = styled.div`
  font-size: ${({ theme }) => theme.sizes.xsmall};
  font-weight: 400;
`

export const VisitArtistIcon = styled(ArrowRightCircleFill)`
  height: 34px;
  color: ${({ theme }) => theme.colors.primary};
`
