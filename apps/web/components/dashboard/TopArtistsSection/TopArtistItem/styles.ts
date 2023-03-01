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

export const ArtistPhoto = styled.div`
  display: inline-block;

  width: 56px;
  height: 56px;
  margin-right: ${({ theme }) => theme.space.xsmall};

  background-image: url("https://cdn.benchmark.pl/uploads/article/87749/MODERNICON/49e0c496efa2aedbbb84c1a8ebdbb4b125e1dc33.jpg");
  background-size: cover;
  border-radius: 50%;
`

export const ArtistData = styled.div`
  color: ${({ theme }) => theme.colors.secondary};
`

export const ArtistName = styled.p`
  font-size: ${({ theme }) => theme.sizes.small};
  font-weight: 500;
`
export const ArtistLocation = styled.p`
  font-size: ${({ theme }) => theme.sizes.xsmall};
  font-weight: 400;
`

export const VisitArtistIcon = styled(ArrowRightCircleFill)`
  height: 34px;
  color: ${({ theme }) => theme.colors.primary};
  :hover {
    cursor: pointer;
  }
`
