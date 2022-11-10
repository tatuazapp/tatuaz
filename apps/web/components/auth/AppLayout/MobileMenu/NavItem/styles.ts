import styled from "styled-components"

export const ButtonWrapper = styled.button<{ active?: boolean }>`
  cursor: ${({ active }) => (active ? "default" : " pointer")};

  position: relative;

  display: flex;
  align-items: center;

  /* TODO: Typography */
  font-size: ${({ theme }) => theme.sizes.xxlarge};

  &::after {
    content: "";

    position: absolute;
    bottom: 0;

    width: ${({ active }) => (active ? "100%" : 0)};
    height: 3px;

    background-color: ${({ theme }) => theme.colors.secondary};

    transition: width 0.3s ease-in-out, left 0.3s ease-in-out;
  }

  &:hover {
    color: ${({ theme }) => theme.colors.secondary};

    &::after {
      left: "100%";
      width: 100%;
    }
  }
`
