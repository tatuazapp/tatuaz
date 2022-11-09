import styled from "styled-components"
import { rem } from "../../../styles/utils"

export const ProfileWrapper = styled.div`
  display: flex;
  flex-direction: column;
  gap: ${({ theme }) => theme.space.small};
  align-items: center;
  justify-content: center;

  max-width: ${rem(400)};
  padding: ${({ theme }) => theme.space.small};

  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 8px 32px 0 rgba(31, 38, 135, 0.37);

  transition: all 0.15s ease 0s;

  &:hover {
    transform: translateY(-3px);
    box-shadow: 0 16px 70px 0 rgba(31, 38, 135, 0.37);
  }
`
