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

  width: ${({ theme }) => theme.space.xxlarge};
  height: ${({ theme }) => theme.space.xxlarge};
  margin-right: ${({ theme }) => theme.space.xsmall};

  /* TODO: change to dynamic */
  background-image: url("https://cdn.benchmark.pl/uploads/article/87749/MODERNICON/49e0c496efa2aedbbb84c1a8ebdbb4b125e1dc33.jpg");
  background-size: cover;
  border-radius: 50%;
`

export const VisitArtistIcon = styled(ArrowRightCircleFill)`
  cursor: pointer;
  height: ${({ theme }) => theme.space.xlarge};
  color: ${({ theme }) => theme.colors.primary};
`
