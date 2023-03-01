import styled from "styled-components"

export const ArtistPostSectionWrapper = styled.div`
  display: flex;
  flex-direction: column;
`

export const ArtistPostSectionContainer = styled.div`
  overflow: scroll;
  height: calc(100vh - 110px);

  /* width */
  ::-webkit-scrollbar {
    width: 0;
  }

  /* Track */
  ::-webkit-scrollbar-track {
    background: ${({ theme }) => theme.colors.background1};
  }
`
