import styled from "styled-components"

export const ProfileWrapper = styled.div`
  display: flex;
  flex-direction: column;
  gap: 16px;
  align-items: center;
  justify-content: center;

  max-width: 400px;
  padding: 16px;

  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 8px 32px 0 rgba(31, 38, 135, 0.37);

  transition: all 0.15s ease 0s;
  &:hover {
    transform: translateY(-3px);
    box-shadow: 0 16px 70px 0 rgba(31, 38, 135, 0.37);
  }
`
