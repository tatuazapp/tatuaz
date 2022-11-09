import styled from "styled-components"
import { rem } from "../../../styles/utils"

export const MainContainer = styled.div`
  width: ${rem(230)};
  height: ${rem(230)};
  background: linear-gradient(${({ theme }) => theme.gradients.card});
  border-radius: 15px;
`

export const TopContainer = styled.div`
  display: flex;
  align-items: flex-end;
  justify-content: center;
  height: 60%;
`

export const BottomContainer = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
  height: 40%;
`

export const ValueContainer = styled.div<{ length: number }>`
  width: 80%;
  margin-bottom: ${({ length, theme }) =>
    length < 10
      ? theme.space.medium
      : length > 16
      ? "0px"
      : theme.space.xxsmall};

  font-size: ${({ length }) =>
    length < 10 ? rem(34) : length > 26 ? rem(22) : rem(28)};
  font-weight: 700;
  line-height: ${rem(32)};
  color: ${({ theme }) => theme.colors.primary};
  text-align: center;
`

export const DescriptionContainer = styled.div`
  width: 80%;

  font-size: ${rem(14)};
  font-weight: 700;
  line-height: ${rem(22)};
  color: ${({ theme }) => theme.colors.primary};
  text-align: center;
`
