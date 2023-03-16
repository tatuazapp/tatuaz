import { ArrowRightCircle } from "@styled-icons/bootstrap/ArrowRightCircle"
import { down } from "styled-breakpoints"
import styled from "styled-components"

export const SearchAreaWrapper = styled.div`
  display: flex;
  margin-top: ${({ theme }) => theme.space.xlarge};
  ${down("lg")} {
    justify-content: space-between;
  }
`

export const SearchInput = styled.input`
  width: 100%;
  height: ${({ theme }) => theme.sizes.xxlarge};
  padding-top: ${({ theme }) => theme.space.xsmall};
  padding-bottom: ${({ theme }) => theme.space.xsmall};
  padding-left: ${({ theme }) => theme.space.xxxsmall};

  color: ${({ theme }) => theme.colors.secondary};

  background-color: ${({ theme }) => theme.colors.background2};
  border: none;
  border: 2px solid ${({ theme }) => theme.colors.background2};
  border-radius: ${({ theme }) => theme.radius.xxsmall};
  outline: none;

  transition: background-color ${({ theme }) => theme.animationTime.xxfast} ease;

  ::placeholder {
    color: ${({ theme }) => theme.colors.background3};
    opacity: 1;
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
  ${down("md")} {
    margin-right: ${({ theme }) => theme.space.xlarge};
  }
`

export const UserArea = styled.div`
  display: flex;
  align-items: center;
  margin-left: ${({ theme }) => theme.space.xlarge};
  white-space: nowrap;
`

export const UserPhoto = styled.div`
  position: relative;

  width: ${({ theme }) => theme.sizes.xxlarge};
  height: ${({ theme }) => theme.sizes.xxlarge};
  margin-right: ${({ theme }) => theme.space.xsmall};

  /* TODO: change to dynamic */
  background-image: url("https://cdn.benchmark.pl/uploads/article/87749/MODERNICON/49e0c496efa2aedbbb84c1a8ebdbb4b125e1dc33.jpg");
  background-size: cover;
  border-radius: 50%;
`

export const UserIconStatus = styled.div`
  position: absolute;
  right: ${({ theme }) => theme.space.xxxxsmall};
  bottom: ${({ theme }) => theme.space.xxxxsmall};

  width: ${({ theme }) => theme.space.xsmall};
  height: ${({ theme }) => theme.space.xsmall};

  background-color: ${({ theme }) => theme.colors.primary};
  border-radius: 50%;
`

export const MobileSearchIcon = styled(ArrowRightCircle)`
  height: ${({ theme }) => theme.sizes.xxlarge};
  color: ${({ theme }) => theme.colors.primary};
`
