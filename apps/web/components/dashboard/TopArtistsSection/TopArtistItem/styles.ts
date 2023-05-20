import { ArrowRightCircleFill } from "@styled-icons/bootstrap"
import styled from "styled-components"

export const TopArtistsSectionArtistListItem = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding-top: ${({ theme }) => theme.space.large};
`

export const ArtistWrapper = styled.div`
  display: flex;
  align-items: center;
  justify-content: start;
`

export const ArtistPhoto = styled.div<{
  photoUrl: string
}>`
  display: inline-block;

  width: ${({ theme }) => theme.space.xxlarge};
  height: ${({ theme }) => theme.space.xxlarge};
  margin-right: ${({ theme }) => theme.space.xsmall};

  background-image: url(${({ photoUrl }) => photoUrl});
  background-size: cover;
  border-radius: 50%;
`

export const VisitArtistIcon = styled(ArrowRightCircleFill)`
  cursor: pointer;
  height: ${({ theme }) => theme.space.xlarge};
  color: ${({ theme }) => theme.colors.primary};
`
