import styled from "styled-components"

export const MobileMenuContainer = styled.div`
  position: absolute;
  z-index: 9;

  display: flex;
  justify-content: space-between;

  width: 100%;
  min-height: 100vh;
  padding: ${({ theme }) => theme.space.medium};

  background-color: ${({ theme }) => theme.colors.primary};
`
export const ActionContainer = styled.div`
  width: 80%;
`
