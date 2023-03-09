import styled from "styled-components"

export const ArtistPostSectionWrapper = styled.div`
  display: flex;
  flex-direction: column;
`

export const ArtistPostSectionContainer = styled.div`
  overflow: scroll;
  height: calc(100vh - 110px);

  ::-webkit-scrollbar {
    width: 0;
  }

  ::-webkit-scrollbar-track {
    background: ${({ theme }) => theme.colors.background1};
  }
`
