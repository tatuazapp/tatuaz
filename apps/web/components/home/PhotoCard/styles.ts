import styled from "styled-components"
import { rem } from "../../../styles/utils"

export const MainContainer = styled.div`
  width: ${rem(230)};
  height: ${rem(300)};
  background-color: ${({ theme }) => theme.colors.photoCard};
  border-radius: ${({ theme }) => theme.radius.small};
`

export const TopContainer = styled.div`
  height: 70%;
`

export const BottomContainer = styled.div`
  display: flex;
  justify-content: center;
  height: 30%;
`

export const PhotoContainer = styled.div<{ url: string }>`
  width: 100%;
  height: 100%;

  background-image: url(${({ url }) => url});
  background-repeat: no-repeat;
  background-position: center;
  background-size: cover;
  border: solid ${({ theme }) => theme.colors.photoCard} 15px;
  border-radius: ${({ theme }) => theme.radius.large};
`

export const DescriptionContainer = styled.div`
  width: 80%;

  font-size: ${rem(14)};
  font-weight: 700;
  line-height: ${rem(22)};
  color: ${({ theme }) => theme.colors.primary};
`
