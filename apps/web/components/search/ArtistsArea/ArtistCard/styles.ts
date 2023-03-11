import { down } from "styled-breakpoints"
import styled from "styled-components"
import { rem } from "../../../../styles/utils"

export const ArtistCardWrapper = styled.div`
  max-width: ${rem(282)};
  height: ${rem(432)};
  margin-right: ${({ theme }) => theme.space.xxxsmall};
  margin-bottom: ${({ theme }) => theme.space.small};
  margin-left: ${({ theme }) => theme.space.xxxsmall};

  background-color: ${({ theme }) => theme.colors.background2};
  border-radius: ${({ theme }) => theme.radius.medium};

  ${down("sm")} {
    max-width: 100%;
    height: 100%;
  }
`

export const ArtistCardBacktroundPhoto = styled.div`
  position: relative;

  height: ${rem(165)};
  margin-bottom: ${rem(51)};

  background-image: url("https://t3.ftcdn.net/jpg/01/01/05/24/360_F_101052491_D8WlkJsZclF5kO8LsA7AstXI9Ir4iuFl.jpg");
  background-size: cover;
  border-top-left-radius: ${({ theme }) => theme.radius.medium};
  border-top-right-radius: ${({ theme }) => theme.radius.medium};
`

export const ArtistCardUserPhoto = styled.div`
  position: absolute;
  right: 0;
  bottom: ${rem(-47)};
  left: 0;

  width: ${rem(94)};
  height: ${rem(94)};
  margin: auto;

  /* TODO: change to dynamic */
  background-image: url("https://cdn.benchmark.pl/uploads/article/87749/MODERNICON/49e0c496efa2aedbbb84c1a8ebdbb4b125e1dc33.jpg");
  background-size: cover;
  border: ${rem(4)} solid ${({ theme }) => theme.colors.background2};
  border-radius: 50%;
`

export const UserDataWrapper = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
`

export const UserSectionWrapper = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-between;

  height: ${rem(216)};
  padding-right: ${({ theme }) => theme.space.medium};
  padding-bottom: ${({ theme }) => theme.space.xsmall};
  padding-left: ${({ theme }) => theme.space.medium};
`
