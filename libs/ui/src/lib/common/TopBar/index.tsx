import styled from "styled-components"

export const TopBar = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;

  min-height: 110px;
  padding: ${({ theme }) => theme.space.large};

  color: black;

  background-color: ${({ theme }) => theme.colors.primary};
`
