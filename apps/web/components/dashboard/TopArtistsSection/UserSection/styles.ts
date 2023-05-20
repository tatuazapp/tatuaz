import styled from "styled-components"

export const UserSectionAreaWrapper = styled.div`
  display: flex;
  align-items: center;
  justify-content: end;
`

export const UserName = styled.p`
  margin-left: ${({ theme }) => theme.space.large};
  font-size: ${({ theme }) => theme.sizes.medium};
  font-weight: 600;
  color: ${({ theme }) => theme.colors.secondary};
`
