import { down } from "styled-breakpoints"
import styled from "styled-components"

export const CreatePostContainer = styled.div`
  display: flex;

  width: 100%;
  padding: ${({ theme }) => theme.space.large};

  background-color: ${({ theme }) => theme.colors.background2};
  border-radius: ${({ theme }) => theme.radius.medium};

  ${down("lg")} {
    padding-right: ${({ theme }) => theme.space.small};
    padding-left: ${({ theme }) => theme.space.small};
  }
`

export const CreatePostInputButton = styled.input`
  width: 100%;
  margin-left: ${({ theme }) => theme.space.medium};
  padding-top: ${({ theme }) => theme.space.xxsmall};
  padding-bottom: ${({ theme }) => theme.space.xxsmall};
  padding-left: ${({ theme }) => theme.space.small};

  background-color: ${({ theme }) => theme.colors.background3};
  border: none;
  border: 2px solid ${({ theme }) => theme.colors.background2};
  border-radius: ${({ theme }) => theme.radius.medium};
  outline: none;

  transition: background-color ${({ theme }) => theme.animationTime.xxfast} ease;

  ::placeholder {
    color: ${({ theme }) => theme.colors.background1};
    /* opacity: 1; */
    transition: color ${({ theme }) => theme.animationTime.xxfast} ease;
  }

  :hover {
    background-color: ${({ theme }) => theme.colors.background3};
    border: 2px solid ${({ theme }) => theme.colors.secondary};
    ::placeholder {
      color: ${({ theme }) => theme.colors.background4};
      opacity: 1;
    }
  }
  :focus {
    background-color: ${({ theme }) => theme.colors.background3};
    border: 2px solid ${({ theme }) => theme.colors.secondary};
    ::placeholder {
      color: ${({ theme }) => theme.colors.background4};
      opacity: 1;
    }
  }
`
