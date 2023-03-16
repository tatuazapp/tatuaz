import { ComponentProps, FunctionComponent } from "react"
import styled, { css } from "styled-components"
import { rem } from "../../../utils/utils"

const defaultTextColor = "#fff"
const defaultTextAlign = "left"
const defaultPadding = "0"

export const heading = css`
  font-weight: 600;
`

export const h1 = css`
  font-size: ${rem(48)};
  line-height: ${rem(58)};
`
export const h2 = css`
  font-size: ${rem(36)};
  line-height: ${rem(44)};
`
export const h3 = css`
  font-size: ${rem(32)};
  line-height: ${rem(39)};
`

export const h4 = css`
  font-size: ${rem(24)};
  line-height: ${rem(29)};
`

export const h5 = css`
  font-size: ${rem(20)};
  line-height: ${rem(29)};
`

export const Heading = styled.div<{
  level?: 1 | 2 | 3 | 4 | 5
  color?: string
}>`
  ${heading}

  color: ${({ color }) => color || defaultTextColor};

  ${({ level }) => {
    switch (level) {
      default:
      case 1:
        return h1
      case 2:
        return h2
      case 3:
        return h3
      case 4:
        return h4
      case 5:
        return h5
    }
  }}
`
type HeadingProps = ComponentProps<typeof Heading>

export const Heading1: FunctionComponent<HeadingProps> = ({
  children,
  ...props
}) => (
  <Heading {...props} level={1}>
    {children}
  </Heading>
)
export const Heading2: FunctionComponent<HeadingProps> = ({
  children,
  ...props
}) => (
  <Heading level={2} {...props}>
    {children}
  </Heading>
)

export const p1 = css`
  font-size: ${rem(18)};
  line-height: ${rem(22)};
`
export const p2 = css`
  font-size: ${rem(16)};
  line-height: ${rem(20)};
`
export const p3 = css`
  font-size: ${rem(14)};
  line-height: ${rem(16)};
`
export const p4 = css`
  font-size: ${rem(12)};
  line-height: ${rem(14)};
`

export const Paragraph = styled.div<{
  level?: 1 | 2 | 3 | 4
  strong?: boolean
  color?: string
  textAlign?: "left" | "center" | "right" | "justify"
  padding?: string
}>`
  padding: ${({ padding }) => padding || defaultPadding};
  font-weight: ${({ strong, level }) => (strong || level === 1 ? 600 : 500)};
  color: ${({ color }) => color || defaultTextColor};
  text-align: ${({ textAlign }) => textAlign || defaultTextAlign};

  ${({ level }) => {
    switch (level) {
      default:
      case 1:
        return p1
      case 2:
        return p2
      case 3:
        return p3
      case 4:
        return p4
    }
  }}
`
type ParagraphProps = ComponentProps<typeof Paragraph>

export const Paragraph1: FunctionComponent<ParagraphProps> = ({
  children,
  ...props
}) => (
  <Paragraph {...props} level={1}>
    {children}
  </Paragraph>
)
export const Paragraph1Strong: FunctionComponent<ParagraphProps> = ({
  children,
  ...props
}) => (
  <Paragraph1 {...props} strong>
    {children}
  </Paragraph1>
)
export const Paragraph2: FunctionComponent<ParagraphProps> = ({
  children,
  ...props
}) => (
  <Paragraph {...props} level={2}>
    {children}
  </Paragraph>
)
export const Paragraph2Strong: FunctionComponent<ParagraphProps> = ({
  children,
  ...props
}) => (
  <Paragraph2 {...props} strong>
    {children}
  </Paragraph2>
)
export const Paragraph3: FunctionComponent<ParagraphProps> = ({
  children,
  ...props
}) => (
  <Paragraph {...props} level={3}>
    {children}
  </Paragraph>
)
export const Paragraph3Strong: FunctionComponent<ParagraphProps> = ({
  children,
  ...props
}) => (
  <Paragraph3 {...props} strong>
    {children}
  </Paragraph3>
)

export const Paragraph4: FunctionComponent<ParagraphProps> = ({
  children,
  ...props
}) => (
  <Paragraph {...props} level={4}>
    {children}
  </Paragraph>
)
export const Paragraph4Strong: FunctionComponent<ParagraphProps> = ({
  children,
  ...props
}) => (
  <Paragraph4 {...props} strong>
    {children}
  </Paragraph4>
)

export const semiBold = css`
  font-weight: 600;
`
