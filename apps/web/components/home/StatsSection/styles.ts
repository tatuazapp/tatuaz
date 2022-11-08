import styled from "styled-components"
import { rem } from "../../../styles/utils"

export const TitleContainer = styled.div`
  width: ${rem(540)};
  height: ${rem(40)};
  margin-top: 50px;
  margin-left: 50px;

  font-size: ${rem(30)};
  color: ${({ theme }) => theme.colors.primary};
`

export const CardsContainer = styled.div`
  display: flex;
  justify-content: space-around;
  margin-top: 50px;
  margin-bottom: 100px;
`
