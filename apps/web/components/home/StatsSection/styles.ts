import styled from "styled-components"
import { rem } from "../../../styles/utils"

export const TitleContainer = styled.div`
  width: ${rem(540)};
  height: ${rem(40)};
  margin-top: ${({ theme }) => theme.space.xxlarge};
  margin-left: ${({ theme }) => theme.space.xxlarge};

  font-size: ${rem(30)};
  color: ${({ theme }) => theme.colors.primary};
`

export const CardsContainer = styled.div`
  display: flex;
  justify-content: space-around;
  margin-top: ${({ theme }) => theme.space.xxlarge};
  margin-bottom: ${({ theme }) => theme.space.xxlarge};
`
