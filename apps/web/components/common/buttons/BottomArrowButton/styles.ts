import styled from "styled-components"
import { rem } from "../../../../styles/utils"

export const BottomArrowButton = styled.button<{
  width?: number
  maxWidth?: number
  height?: number
  disabled?: boolean
  variant?: "primary" | "secondary"
  direction?: "left" | "right"
}>`
  cursor: pointer;

  position: relative;

  overflow: visible;

  width: ${({ width }) => (width ? `${rem(width)}` : "100%")};
  min-width: ${rem(40)};
  max-width: ${({ maxWidth }) => (maxWidth ? `${rem(maxWidth)}` : rem(330))};
  height: ${({ height }) => (height ? `${rem(height)}` : "auto")};
  min-height: ${rem(40)};
  margin: 0;
  padding: 0;

  font-size: ${rem(20)};
  font-weight: 600;
  line-height: ${rem(24)};
  color: ${({ theme }) => theme.colors.primary};
  text-align: left;

  background: none;
  border: none;
  border-bottom: 3px solid ${({ theme }) => theme.colors.primary};

  ${({ variant, theme }) =>
    variant === "secondary" &&
    `
    color: ${theme.colors.background4};
    border-bottom: 3px solid ${theme.colors.background4};
  `}

  ${({ disabled, theme }) =>
    disabled &&
    `
    cursor: not-allowed;
    color: ${theme.colors.background4};
    border-bottom: 3px solid ${theme.colors.background4};
    
    filter: opacity(0.5);
    `}

  ${({ direction }) =>
    direction === "left" &&
    `
    text-align: right;
  `}


  &:after {
    content: "";

    position: absolute;
    right: -9px;
    bottom: -9px;
    transform: translateX(-50%) rotate(-45deg);

    width: ${rem(15)};
    height: ${rem(15)};

    border-right: 3px solid ${({ theme }) => theme.colors.primary};
    border-bottom: 3px solid ${({ theme }) => theme.colors.primary};

    ${({ variant, theme }) =>
      variant === "secondary" &&
      `
      border-right: 3px solid ${theme.colors.background4};
      border-bottom: 3px solid ${theme.colors.background4};
    `}

    ${({ direction }) =>
      direction === "left" &&
      `
      right: auto;
      left: -9px;
      transform: translateX(50%) rotate(135deg);


    `}
  }
`
