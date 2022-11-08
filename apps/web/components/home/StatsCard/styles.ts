import styled from "styled-components"
import { rem } from "../../../styles/utils"

export const TitleContainer = styled.div`
  width: 30rem;
  height: 3rem;
  margin-top: 3rem;
  margin-left: 3rem;

  font-family: "Comfortaa";
  font-size: 30px;
  font-weight: 500;
  font-style: normal;
  line-height: 33px;
  color: ${({ theme }) => theme.colors.primary};
`
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
  margin-top: ${rem(5)};
`

export const ValueContainer = styled.div`
  width: 80%;
  margin-bottom: ${(props) =>
    props.defaultValue < 10
      ? "20px"
      : props.defaultValue > 16
      ? "0px"
      : "10px"};

  font-family: "Comfortaa";
  font-size: ${(props) =>
    props.defaultValue < 10
      ? "34px"
      : props.defaultValue > 26
      ? "22px"
      : "28px"};
  font-weight: 700;
  font-style: normal;
  line-height: 32px;
  color: ${({ theme }) => theme.colors.primary};
  text-align: center;
`

export const DescriptionContainer = styled.div`
  width: 80%;

  font-family: "Comfortaa";
  font-size: 14px;
  font-weight: 700;
  font-style: normal;
  line-height: 22px;
  color: ${({ theme }) => theme.colors.primary};
  text-align: center;
`
