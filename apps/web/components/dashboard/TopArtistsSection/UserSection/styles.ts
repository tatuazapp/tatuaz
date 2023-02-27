import styled from "styled-components"

export const UserSectionAreaWrapper = styled.div`
  display: flex;
  align-items: center;
  justify-content: end;

  width: 100%;
  height: 120px;
`

export const UserName = styled.p`
  margin-left: ${({ theme }) => theme.space.large};
  font-size: ${({ theme }) => theme.sizes.medium};
  font-weight: 600;
  color: ${({ theme }) => theme.colors.secondary};
`

export const UserPhoto = styled.div`
  display: inline-block;

  width: 56px;
  height: 56px;

  background-image: url("https://cdn.benchmark.pl/uploads/article/87749/MODERNICON/49e0c496efa2aedbbb84c1a8ebdbb4b125e1dc33.jpg");
  background-size: cover;
  border-radius: 50%;
`
