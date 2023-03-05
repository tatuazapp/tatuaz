import { down } from "styled-breakpoints"
import styled from "styled-components"

export const RightSectionContainer = styled.div`
  display: flex;
  flex-direction: column;

  margin-right: ${({ theme }) => theme.space.xlarge};
  margin-left: ${({ theme }) => theme.space.xxxxlarge};

  ${down("xxl")} {
    margin-left: ${({ theme }) => theme.space.xxlarge};
  }
  ${down("xl")} {
    margin-left: ${({ theme }) => theme.space.xlarge};
  }

  ${down("lg")} {
    display: none;
  }
`

export const TopArtistsSectionWrapper = styled.div`
  width: 285px;
  height: 484px;
  padding: ${({ theme }) => theme.space.large};

  background-color: ${({ theme }) => theme.colors.background2};
  border-radius: ${({ theme }) => theme.radius.medium};
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
