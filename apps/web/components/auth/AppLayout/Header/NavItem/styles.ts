import Link from "next/link"
import styled from "styled-components"
import { rem } from "../../../../../styles/utils"

export const NavItemWrapper = styled(Link)<{ active?: boolean }>`
  cursor: pointer;

  position: relative;

  display: flex;
  align-items: center;

  /* TODO: Typography */
  font-size: ${rem(24)};

  &::after {
    content: "";

    position: absolute;
    bottom: 0;
    left: ${({ active }) => (active ? 0 : "50%")};

    width: ${({ active }) => (active ? "100%" : 0)};
    height: 3px;

    background-color: ${({ theme }) => theme.colors.secondary};

    transition: width 0.3s ease-in-out, left 0.3s ease-in-out;
  }

  &:hover {
    color: ${({ theme }) => theme.colors.secondary};

    &::after {
      left: 0;
      width: 100%;
    }
  }
`
